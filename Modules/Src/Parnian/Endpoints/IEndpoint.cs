namespace Parnian.Endpoints;

public interface IEndpoint
{
  public void Handle(IEndpointRouteBuilder builder);
}
