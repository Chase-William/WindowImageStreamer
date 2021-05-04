# WindowImageStreamer

A simple window streaming library for windows machines.

## Example Usage #1

`WindowImageRetriever` lets you request a bitmap from a target window.

```cs
// WindowImageRetriever Example -- Provide your own window handle to target
WindowImageRetriever retriever = new((IntPtr)0x003B0682, TargetArea.EntireWindow);

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
```

Note: If `TryGetWindowImage` fails it will always return null for the bitmap.

### Example Usage #2

`WindowImageStreamer` lets you request a bitmap from a target window at a specified rate. The `WindowImageStreamer` class derives functionality from `WindowImageRetriever` so it also provides `WindowImageRetriever`'s functionalities.

```cs
// WindowImageStreamer Example -- Provide your own window handle to target
WindowImageStreamer imgStreamer = new((IntPtr)0x1036065C, TargetArea.OnlyClientArea, 500);
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
