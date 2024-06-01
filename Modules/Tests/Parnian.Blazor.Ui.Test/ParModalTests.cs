using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Parnian.Blazor.Ui.Components;
using Parnian.Blazor.Ui.Components.Modals;
using Parnian.Blazor.Ui.Components.Overlays;
using Moq;
using Xunit;

public class ParModalTests : TestContext
{
    public ParModalTests()
    {
        var mockOverlayService = new Mock<IParOverlayService>();
        Services.AddSingleton<IParOverlayService>(new ParOverlayService());
    }
  
    [Fact]
    public void ParModal_RendersCorrectly()
    {
        // Arrange
        var cut = RenderComponent<ParModal>(parameters => parameters
            .Add(p => p.Show, false)
        );
        
        // Act
        // Render the component

        // Assert
        cut.MarkupMatches(""); // Adjust expected HTML for initial render when Show is false
    }

    [Fact]
    public void ParModal_ShowsContent_WhenOpened()
    {
        // Arrange
        var overlayProviderComponent = RenderComponent<ParOverlayProvider>();
        var modalComponent = RenderComponent<ParModal>(parameters => parameters
            .Add(p => p.Show, true)
        );

        // Act
        modalComponent.Render();

        // Assert
        Assert.NotNull(overlayProviderComponent.Find(".par-modal-content"));
        overlayProviderComponent.Find(".par-modal-content").MarkupMatches("<div class=\"par-modal-content show\"></div>"); // Adjust expected HTML
    }

    [Fact]
    public void ParModal_HidesContent_WhenClosed()
    {
        // Arrange
        var overlayProviderComponent = RenderComponent<ParOverlayProvider>();
        var modalComponent = RenderComponent<ParModal>(parameters => parameters
            .Add(p => p.Show, false)
        );

        // Act
        modalComponent.Render();

        // Assert
        Assert.Throws<ElementNotFoundException>(() => overlayProviderComponent.Find(".par-modal-content.show"));
    }

    [Fact]
    public Task ParModal_Closes_WhenBackdropClicked()
    {
        // Arrange
        var overlayProviderComponent = RenderComponent<ParOverlayProvider>();
        bool showChangedInvoked = false;
        var modalComponent = RenderComponent<ParModal>(parameters => parameters
            .Add(p => p.Show, true)
            .Add(p => p.BackdropMode, ParBackdropMode.Default)
            .Add(p => p.ShowChanged, EventCallback.Factory.Create<bool>(this, (value) => showChangedInvoked = !value))
        );

        // Act
        overlayProviderComponent.Find(".par-modal-backdrop").Click();

        // Assert
        Assert.True(showChangedInvoked);
        return Task.CompletedTask;
    }

    [Fact]
    public Task ParModal_DoesNotClose_WhenBackdropModeStatic()
    {
        // Arrange
        var overlayProviderComponent = RenderComponent<ParOverlayProvider>();
        bool showChangedInvoked = false;
        var modalComponent = RenderComponent<ParModal>(parameters => parameters
            .Add(p => p.Show, true)
            .Add(p => p.BackdropMode, ParBackdropMode.Static)
            .Add(p => p.ShowChanged, EventCallback.Factory.Create<bool>(this, (value) => showChangedInvoked = !value))
        );

        // Act
        overlayProviderComponent.Find(".par-modal-backdrop").Click();

        // Assert
        Assert.False(showChangedInvoked);
        return Task.CompletedTask;
    }
}
