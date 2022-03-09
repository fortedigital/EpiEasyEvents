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
            var typesToRegister = assemblies.SelectMany(a => a.GetTypes())
                .Where(type => type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IContentEventHandler<,>)));

            foreach (var typeToRegister in typesToRegister)
            {
                // Register against all the interfaces implemented
                // by this concrete class
                var interfacesToRegister = typeToRegister.GetInterfaces();
                foreach (var interfaceToRegister in interfacesToRegister)
                {
                    services.AddTransient(interfaceToRegister, typeToRegister);
                }
            }
        }
    }
}