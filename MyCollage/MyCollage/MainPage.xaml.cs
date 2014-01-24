using MyCollage.Helpers;
using MyCollage.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyCollage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page,INotifyPropertyChanged
    {
        private string _imagen0;

        public string Imagen0
        {
            get { return _imagen0; }
            set { _imagen0 = value;
                if(PropertyChanged!=null)
                    PropertyChanged(this,new PropertyChangedEventArgs("Imagen0"));
            }
        }
        

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.DataContext = this;
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Clear any previously returned files between iterations of this scenario

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;


            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                StringBuilder output = new StringBuilder("Picked files:\n");
                int indice = 0;

                if (files.Count == 3)
                {
                    

                    var rd = new ResourceDictionary();
                    rd.Source = new Uri("ms-appx:///Common/StandardStyles.xaml", UriKind.RelativeOrAbsolute);

                    // código del método

                    this.PhotoTemplate.Style = rd["data3_1"] as Style;

                    // resto del código

                }
                output.Append("cantidad "+files.Count+"\n");
                // Application now has read/write access to the picked file(s)
                foreach (StorageFile file in files)
                {
                    indice++;
                    //path es la direccion q me ayudara mucho a jalar las fotos en una lista y jugar con las posiciones
                    output.Append("nombre: " + file.Name + "path: " + file.Path + "propiedades: " + file.Properties + "\n");
                    if (file != null)
                    {
                        // Ensure the stream is disposed once the image is loaded
                        using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                        {
                            // Set the image source to the selected bitmap
                            BitmapImage bitmapImage = new BitmapImage();
                            //bitmapImage.DecodePixelHeight = decodePixelHeight;
                            //bitmapImage.DecodePixelWidth = decodePixelWidth;

                            await bitmapImage.SetSourceAsync(fileStream);
                            //Scenario2Image.Source = bitmapImage;
                            Imagen0 = bitmapImage.ToString();
                            //switch (indice)
                            //{
                            //    case 1:
                            //        img0.Source = bitmapImage;
                            //        break;
                            //    case 2:
                            //        img1.Source = bitmapImage;
                            //        break;
                            //    default:
                            //        img2.Source = bitmapImage;
                            //        break;
                            //}

                        }
                    }
                }
                var msg = new MessageDialog(output.ToString(), " x!");
                await msg.ShowAsync();
                //OutputTextBlock.Text = output.ToString();
            }

        }

        PopupHelper popHelper = new PopupHelper();
        TemplateControl templates = new TemplateControl();
        private void btnSquare_Click(object sender, RoutedEventArgs e)
        {
            var boton = (Button)sender;
            var trans = boton.TransformToVisual(Window.Current.Content);
            var location = trans.TransformPoint(default(Point));
            location.Y -= (templates.Height + 5);
            location.X -= (templates.Width / 2);

            popHelper.Show(templates, location);
        }

        private void btnFree_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
