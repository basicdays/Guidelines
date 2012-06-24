using System.Drawing;
using System.IO;
using System.Web.Mvc;
//using ImageResizer;

namespace Guidelines.WebUI.ActionResults
{
	//public class ImageResult : FileStreamResult
	//{
	//    public ImageResult(string filePath, int maxWidth = 500, int maxHeight = 500)
	//        : base(ProcessImage(filePath, maxWidth, maxHeight), "Image/jpeg")
	//    { }

	//    private static MemoryStream ProcessImage(string filePath, int maxWidth, int maxHeight)
	//    {
	//        Bitmap temporaryImage = ImageBuilder.Current.Build(filePath, new ResizeSettings
	//        {
	//            MaxWidth = maxWidth,
	//            MaxHeight = maxHeight,
	//            Format = "jpg"
	//        });

	//        var stream = new MemoryStream();
	//        temporaryImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
	//        stream.Seek(0, SeekOrigin.Begin);

	//        temporaryImage.Dispose();

	//        return stream;
	//    }
	//}
}