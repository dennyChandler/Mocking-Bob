using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms; 

namespace MemeGenerator
{
    public class MemeGenerator
    {
        public MemeGenerator(){}

        public string CreateMeme(string imagePath, string topText, string bottomText)
        {            
            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 500f);

            Guid fileGuid = Guid.NewGuid();
            string newFilePath = $"{Path.GetTempPath()}\\{Path.GetFileNameWithoutExtension(imagePath)}-{fileGuid.ToString()}.jpg";
            Bitmap bitmap = (Bitmap)Image.FromFile(imagePath);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Serif", 72, FontStyle.Bold))
                {
                    graphics.DrawString(topText, arialFont, Brushes.White, firstLocation);
                    graphics.DrawString(bottomText, arialFont, Brushes.White, secondLocation);
                }


                GraphicsPath p = new GraphicsPath();
                p.AddString(
                     topText,            
                    FontFamily.GenericSansSerif,  
                    (int)FontStyle.Bold,      
                    graphics.DpiY * 72 / 72,       
                    new Point(10, 10),             
                    new StringFormat());        

                p.AddString(
                    bottomText,           
                   FontFamily.GenericSansSerif,  
                   (int)FontStyle.Bold,     
                   graphics.DpiY * 72 / 72,       
                   new Point(10, 500),              
                   new StringFormat());          

                var pen = new Pen(Color.FromArgb(0,0,0),5);
                graphics.DrawPath(pen, p);
            }

            bitmap.Save(newFilePath);

            return newFilePath;
        }

       
    }
}
