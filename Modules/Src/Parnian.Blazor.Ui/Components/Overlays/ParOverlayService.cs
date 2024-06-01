namespace Parnian.Blazor.Ui.Components.Overlays;

public class ParOverlayService : IParOverlayService
{
  private readonly List<ParOverlay> _overlays = new();

  public event EventHandler<ParOverlay> Added = default!;
  public event EventHandler<ParOverlay> Removed = default!;
  public event EventHandler<ParOverlay> Changed = default!;

  public void AddOverlay(ParOverlay overlay)
  {
    if (!_overlays.Contains(overlay))
    {
      _overlays.Add(overlay);
      Added?.Invoke(this, overlay);
    }
  }

  public void NotifyChanged(ParOverlay overlay)
  {
    Changed?.Invoke(this, overlay);
  }

  public void RemoveOverlay(ParOverlay overlay)
  {
    if (_overlays.Contains(overlay))
    {
      _overlays.Remove(overlay);
      Removed?.Invoke(this, overlay);
    }
  }

  public IEnumerable<ParOverlay> GetOverlays()
  {
    return _overlays;
  }
}
