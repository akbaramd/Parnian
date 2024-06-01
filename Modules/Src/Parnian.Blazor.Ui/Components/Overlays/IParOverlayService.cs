namespace Parnian.Blazor.Ui.Components.Overlays;

public interface IParOverlayService
{
  
  public event EventHandler<ParOverlay> Added ;
  public event EventHandler<ParOverlay> Removed ;
  public event EventHandler<ParOverlay> Changed ;
  
  void AddOverlay(ParOverlay overlay);


   void NotifyChanged(ParOverlay overlay);


   void RemoveOverlay(ParOverlay overlay);


   IEnumerable<ParOverlay> GetOverlays();

}
