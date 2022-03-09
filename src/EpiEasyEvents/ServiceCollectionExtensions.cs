using System.Linq;
using System.Reflection;
using Forte.EpiEasyEvents;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEpiEasyEvents(this IServiceCollection services, Assembly assembly)
        {
            services.AddSingleton<EasyEventsRegistry>();
            RegisterEventHandlers(services, assembly);

            return services;
        }

        public static void UseEpiEasyEvents(this IApplicationBuilder app)
        {
            var easyEventsRegistry = app.ApplicationServices.GetRequiredService<EasyEventsRegistry>();
            easyEventsRegistry.RegisterEvents();
        }

        private static void RegisterEventHandlers(IServiceCollection services, Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
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