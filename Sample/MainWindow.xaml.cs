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
            // Provide hwnd value here from external


            { // WindowImageRetriever Example -- Provide your own window handle to target
                WindowImageRetriever retriever = new WindowImageRetriever((IntPtr)0x003B0682, TargetArea.EntireWindow);

                {
                    if (retriever.TryGetWindowImage(out Bitmap bmp))
                    {
                        Console.WriteLine();
                        bmp.Save("entire.bmp");
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                    if (bmp != null)
                        bmp.Dispose();
                }
            }

            { // WindowImageStreamer Example -- Provide your own window handle to target
                WindowImageStreamer imgStreamer = new WindowImageStreamer((IntPtr)0x1036065C, TargetArea.OnlyClientArea, 100);                
                imgStreamer.ImageReceived += (sender, args) =>
                {
                    try
                    {

                        Dispatcher.Invoke(() =>
                        {
                            using MemoryStream memStream = new();
                            args.Image.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
                            BitmapImage bmpImg = new();
                            bmpImg.BeginInit();
                            bmpImg.StreamSource = memStream;
                            bmpImg.CacheOption = BitmapCacheOption.OnLoad;
                            bmpImg.EndInit();
                            imgView.Source = bmpImg;
                        }); // Update View
                    }                    
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        args.Image.Dispose();
                    }                    
                };
                imgStreamer.Start();
            }
        }
    }
}
