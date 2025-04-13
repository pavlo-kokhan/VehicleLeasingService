using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using VehicleLeasing.API.Results;

namespace VehicleLeasing.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddFluentValidationAutoValidation(
                configuration =>
                {
                    configuration.DisableDataAnnotationsValidation = true;
                })
            .AddValidatorsFromAssemblies(
                new[]
                {
                    Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(Result)),
                },
                ServiceLifetime.Singleton);
}