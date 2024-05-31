namespace Parnian.Blazor.Ui.Components.Overlays;

public class ParOverlayService
{
  private readonly List<ParOverlay> _overlays = [];

  public List<ParOverlay> GetOverlays()
  {
    return _overlays;
  }

  public void AddOverlay(ParOverlay overlay)
  {
    try
    {
      _overlays.Add(overlay);
      Added.Invoke(this, overlay);
    }
    catch (Exception ex)
    {
      // ignored
      Console.WriteLine(ex.Message);
    }
  }

  public event EventHandler<ParOverlay> Added = default!;
  public event EventHandler<ParOverlay> Removed = default!;

  public void RemoveOverlay(ParOverlay overlay)
  {
    try
    {
      _overlays.Remove(overlay);
      Removed.Invoke(this, overlay);
    }
    catch (Exception ex)
    {
      // ignored
      Console.WriteLine(ex.Message);
    }
  }
}
