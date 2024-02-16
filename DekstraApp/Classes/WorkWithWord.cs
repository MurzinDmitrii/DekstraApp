using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Image = Xceed.Document.NET.Image;
using Microsoft.Win32;

namespace DekstraApp.Classes
{
    internal class WorkWithWord
    {
        public static void SavePhoto(Canvas canvas)
        {
            Rect rect = new Rect(canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap(200,
            200, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            System.IO.File.WriteAllBytes("logo.png", ms.ToArray());
        }
        public static void PrintPhoto(string a)
        {
            var myImageFullPath = "logo.png";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            using (DocX document = DocX.Create(openFileDialog.FileName))
            {
                // Add an image into the document.    
                Image image = document.AddImage(myImageFullPath);

                // Create a picture (A custom view of an Image).
                Picture picture = image.CreatePicture();

                Xceed.Document.NET.Paragraph p1 = document.InsertParagraph();
                // Append content to the Paragraph
                p1.AppendLine("Изображение графа: ");
                p1.AppendLine();
                p1.AppendPicture(picture);
                p1.AppendLine();
                p1.Append(a);

                // Save this document.
                document.Save();
            }
        }
    }
}
