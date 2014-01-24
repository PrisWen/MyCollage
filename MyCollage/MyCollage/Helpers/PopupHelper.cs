using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace MyCollage.Helpers
{
    class PopupHelper
    {
        Popup _popup;

        public PopupHelper()
        {
            _popup= new Popup();
            _popup.IsLightDismissEnabled = true;
        }
        public void Show(UserControl control, Point location)
        {
            _popup.Child = control;
            _popup.HorizontalOffset = location.X;
            _popup.VerticalOffset = location.Y;
            _popup.Width = control.Width;
            _popup.Height = control.Height;
            _popup.IsOpen = true;
        }
    }
}
