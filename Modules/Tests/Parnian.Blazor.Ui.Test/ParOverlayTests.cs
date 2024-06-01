using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Parnian.Blazor.Ui.Components;
using Parnian.Blazor.Ui.Components.Overlays;

namespace Parnian.Blazor.Ui.Test;

public class ParOverlayTests : TestContext
{
  private readonly ParOverlayService overlayService;


  public ParOverlayTests()
    {
       overlayService = new ParOverlayService();
      Services.AddSingleton<IParOverlayService>(overlayService);
    }

    [Fact]
    public void MyOverlay_AddsOverlay_OnInitialized()
    {
        // Arrange
        var cut = RenderComponent<ParOverlay>();

        // Act
        cut.Render();

        // Assert
        Assert.True(overlayService.GetOverlays().Any());
    }

    [Fact]
    public void MyOverlay_RemovesOverlay_OnDispose()
    {
        // Arrange
        var cut = RenderComponent<ParOverlay>();

        // Act
        cut.Instance.Dispose();

        // Assert
        Assert.False(overlayService.GetOverlays().Any());
    }

    [Fact]
    public void MyOverlay_NotifiesOverlayService_OnAfterRender()
    {
        // Arrange
        var cut = RenderComponent<ParOverlay>();

        // Act
        cut.Render();

        // Assert
        Assert.True(overlayService.GetOverlays().Any());
    }

}