using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WIS;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            { // WindowImageRetriever Example -- Provide your own window handle to target
                var retriever = new WindowImageRetriever((IntPtr)0x02E90990, TargetArea.EntireWindow);

                {
                    if (retriever.TryGetWindowImage(out Bitmap bmp))
                    {
                        try
                        {
                            bmp.Save("entire.bmp");
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        // do something else...
                    }
                    bmp?.Dispose();
                }
            }

            { // WindowImageStreamer Example -- Provide your own window handle to target
                var imgStreamer = new WindowImageStreamer((IntPtr)0x02E90990, TargetArea.OnlyClientArea, 500);
                imgStreamer.ImageReceived += (sender, args) =>
                {
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            using var memStream = new MemoryStream();
                            args.Image.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bmpImg = new BitmapImage();
                            bmpImg.BeginInit();
                            bmpImg.StreamSource = memStream;
                            bmpImg.CacheOption = BitmapCacheOption.OnLoad;
                            bmpImg.EndInit();
                            imgView.Source = bmpImg;
                        }); // Update View
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        args.Image?.Dispose();
                    }
                };
                imgStreamer.ImageRetrievalError += delegate
                {
                    MessageBox.Show("WindowImageStreamer was unable to retrieve a bitmap from the target window.");
                };
                imgStreamer.Start();
            }
        }
    }
}
