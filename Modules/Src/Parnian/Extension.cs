using Parnian.Endpoints;

namespace Parnian;

public static class Extension
{
  public static IServiceCollection AddParnian(this IServiceCollection services)
  {
    services.Scan(x =>
    {
      x.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies()).AddClasses(filter => filter.AssignableTo<IEndpoint>())
        .AsSelfWithInterfaces().WithTransientLifetime();
    });
    return services;
  }
  
  public static IEndpointRouteBuilder MapParnian(this IEndpointRouteBuilder app)
  {
    var ser = app.ServiceProvider.GetServices<IEndpoint>();
    foreach (var endpoint in ser)
    {
      endpoint.Handle(app);
    }
    return app;
  }
}
