namespace Parnian.Blazor.Ui.Components.Overlays;
public class ParOverlayService
{
    private readonly List<ParOverlay> _overlays = new();

    public event EventHandler<ParOverlay> Added;
    public event EventHandler<ParOverlay> Removed;
    public event EventHandler<ParOverlay> Changed;

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
        Added?.Invoke(this, overlay);
    }

    public void RemoveOverlay(ParOverlay overlay)
    {
        if (_overlays.Contains(overlay))
        {
            _overlays.Remove(overlay);
            Removed?.Invoke(this, overlay);
        }
    }

    public IEnumerable<ParOverlay> GetOverlays() => _overlays;
}
