using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System.Linq;

namespace GEtools
{
    internal class ToggleNVDI : Button
    {
        protected override void OnClick() 
        {
            QueuedTask.Run(() =>
            {

                string layerName = "NVDI";
                var toggleLayer = MapView.Active.Map.GetLayersAsFlattenedList().FirstOrDefault(layer => layer.Name == layerName) as Layer;
                if (!toggleLayer.IsVisible)
                  toggleLayer.SetVisibility(true);
                else
                  toggleLayer.SetVisibility(false);

            });

        }
    }
}

