using AuthHub.Infrastructure.Dependency.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthHub.Infrastructure.Dependency
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionBehaviour<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(DatabaseTypeBehaviour<,>));
                //config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(SwaggerAuthMiddleware<,>));

            });

            return services;
        }

    }
}
