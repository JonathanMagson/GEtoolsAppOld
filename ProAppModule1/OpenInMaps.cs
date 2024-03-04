using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEtools
{
    internal class OpenInMaps : MapTool
    {
        public OpenInMaps()
        {
            OverlayControlID = "MapToolWithOverlayControl_EmbeddableControl";
            OverlayControlCanResize = true;
            OverlayControlPositionRatio = new System.Windows.Point(0, 0.95); // bottom left
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                e.Handled = true;
        }

        protected override Task HandleMouseDownAsync(MapViewMouseButtonEventArgs e)
        {
            return QueuedTask.Run(() =>
            {
                var mapPoint = ActiveMapView.ClientToMap(e.ClientPoint);
                var coords = GeometryEngine.Instance.Project(mapPoint, SpatialReferences.WGS84) as MapPoint;
                if (coords == null) return;

                // Open Google Maps in the browser
                OpenGoogleMapsInBrowser(coords.Y, coords.X);

                //activate the explore tool
                FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
            });
        }

        private void OpenGoogleMapsInBrowser(double latitude, double longitude)
        {
            try
            {
                // Generate the Google Maps URL with the provided coordinates
                string googleMapsUrl = $"https://www.google.com/maps?q={latitude},{longitude}";

                // Open the URL in the default web browser
                Process.Start(new ProcessStartInfo(googleMapsUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // Handle error
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }
    }
    
    
}
