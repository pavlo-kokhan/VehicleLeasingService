using System.Reflection;
using MediatR;
using VehicleLeasing.API.Pipelines;
using VehicleLeasing.API.Pipelines.Generic;
using VehicleLeasing.API.Results;

namespace VehicleLeasing.API.Extensions;

public static class PipelineBehavioursExtensions
{
    public static IServiceCollection AddPipelines(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddValidationPipelines(Assembly.GetExecutingAssembly());
    
    private static IServiceCollection AddValidationPipelines(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var requestTypes = assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IRequest<Result>)))
            .ToList();

        foreach (var requestType in requestTypes)
        {
            var genericResultRequestType = requestType
                .GetInterfaces()
                .FirstOrDefault(i => i.GetGenericArguments().Any(a => a.GenericTypeArguments.Length == 1));

            if (genericResultRequestType is not null)
            {
                var resultArgument = genericResultRequestType.GetGenericArguments().First();
                var interfaceValidationPipeline = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, resultArgument);
                var validationPipelineType = typeof(ValidationPipeline<,>).MakeGenericType(requestType, resultArgument.GetGenericArguments().First());

                serviceCollection.Add(new ServiceDescriptor(interfaceValidationPipeline, validationPipelineType, ServiceLifetime.Singleton));
            }
            else
            {
                var resultRequestType = requestType
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetGenericArguments().Any(a => a == typeof(Result)));

                if (resultRequestType is null)
                {
                    continue;
                }

                var resultArgument = resultRequestType.GetGenericArguments().First();
                var interfaceValidationPipeline = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, resultArgument);
                var validationPipelineType = typeof(ValidationPipeline<>).MakeGenericType(requestType);

                serviceCollection.Add(new ServiceDescriptor(interfaceValidationPipeline, validationPipelineType, ServiceLifetime.Scoped));
            }
        }

        return serviceCollection;
    }
}