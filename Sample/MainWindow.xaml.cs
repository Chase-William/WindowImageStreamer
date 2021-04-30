using System;
using System.Collections.Generic;
using System.Drawing;
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

using WindowImageStreamer;

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
            WindowImageRetriever retriever = new WindowImageRetriever((IntPtr)0x003B0086, TargetArea.OnlyClientArea);

            if (retriever.TryGetWindowImage(out Bitmap bmp))
            {
                Console.WriteLine();
                bmp.Save("Example.bmp");
            } 
            else
            {
                Console.WriteLine();
            }

            if (bmp != null)
                bmp.Dispose();
        }
    }
}
