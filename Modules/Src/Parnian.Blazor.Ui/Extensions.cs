using Microsoft.Extensions.DependencyInjection;
using Parnian.Blazor.Ui.Components.Overlays;

namespace Parnian.Blazor.Ui;

public static class Extensions
{
  public static IServiceCollection AddParnianUi(this IServiceCollection services)
  {
    services.AddSingleton<IParOverlayService,ParOverlayService>();
    return services;
  }
  
  
}
