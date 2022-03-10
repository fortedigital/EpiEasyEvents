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
                .SelectMany(
                    type => type.GetInterfaces()
                        .Where(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IContentEventHandler<,>))
                        .Select(it => (it, type)));

            foreach (var (interfaceToRegister, typeToRegister) in interfaceWithTypeTuples)
            {
                services.AddTransient(interfaceToRegister, typeToRegister);
            }
        }
    }
}