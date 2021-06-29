# WindowImageStreamer [![nuget](https://img.shields.io/badge/Nuget-v1.0-informational)](https://www.nuget.org/packages/WindowImageStreamer/)

A simple window bitmap streaming library for windows machines. Simply provide a handle to a window and start requesting bitmaps!

## Example Usage #1

`WindowImageRetriever` lets you request a bitmap from a target window.

```cs
// WindowImageRetriever Example -- Provide your own window handle to target
var retriever = new WindowImageRetriever((IntPtr)0x003B0682, TargetArea.EntireWindow);
if (retriever.TryGetWindowImage(out Bitmap bmp))
{
    try
    {
        bmp.Save("entire.bmp");
    }
    catch (Exception ex) { }
    finally {
        bmp.Dispose();
    }
}
else
{
    // do something else...
}
```

Note: If `TryGetWindowImage` fails the bitmap given will be null.

### Example Usage #2

`WindowImageStreamer` lets you request a bitmap from a target window at a specified rate. The `WindowImageStreamer` class derives functionality from `WindowImageRetriever` so it also provides `WindowImageRetriever`'s functionalities.

```cs
// WindowImageStreamer Example -- Provide your own window handle to target
var imgStreamer = new WindowImageStreamer((IntPtr)0x1036065C, TargetArea.OnlyClientArea, 500);
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
    finally {
        bmp?.Dispose();
    }
};
imgStreamer.ImageRetrievalError += delegate
{
    MessageBox.Show("WindowImageStreamer was unable to retrieve a bitmap from the target window.");
};
imgStreamer.Start();
```

## Cleaning Up
You are responsible for disposing of the `Bitmap` when finished.

## Important Note
The underlying method used to get the bitmap of the target window is the <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-printwindow">PrintWindow</a> function. It is important to note that this function relies on the target window's process to correctly render the window and return it and therefore may not always work correctly.
