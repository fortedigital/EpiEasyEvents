using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Forte.EpiEasyEvents;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEpiEasyEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<EventsRegistry>();
            RegisterEventHandlers(services, assemblies);

            return services;
        }

        public static IServiceCollection AddEpiEasyEvents(this IServiceCollection services, Assembly assembly)
        {
            return services.AddEpiEasyEvents(new[] {assembly});
        }

        public static void UseEpiEasyEvents(this IApplicationBuilder app)
        {
            var easyEventsRegistry = app.ApplicationServices.GetRequiredService<EventsRegistry>();
            easyEventsRegistry.RegisterEvents();
        }

        private static void RegisterEventHandlers(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var interfaceWithTypeTuples = assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsAbstract == false)
                .SelectMany(
                    type => type.GetInterfaces()
                        .Where(
                            interfaceType => IsAssignableToGenericType(interfaceType, typeof(IContentChangedHandler<,>)) &&
                                             IsNotMarkerInterface(interfaceType.GetGenericTypeDefinition()))
                        .Select(it => (it, type)));

            foreach (var (interfaceToRegister, typeToRegister) in interfaceWithTypeTuples)
            {
                services.AddTransient(interfaceToRegister, typeToRegister);
            }
        }

        private static bool IsNotMarkerInterface(Type interfaceGenericTypeDefinition)
        {
            return interfaceGenericTypeDefinition != typeof(IContentEventHandler<,>) &&
                   interfaceGenericTypeDefinition != typeof(IContentChangedHandler<,>) &&
                   interfaceGenericTypeDefinition != typeof(IContentSecurityEventHandler<,>);
        }

        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            var baseType = givenType.BaseType;
            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }
    }
}
