using Parnian.Endpoints;

namespace Parnaan.Template.Blazor.Endpoints;

public class TestEndpoint : IEndpoint
{

  public string Prefix = "/api";
  
  public void Handle(IEndpointRouteBuilder builder)
  {
    builder.MapGet($"{Prefix}/test", () => "ss");
  }
}
