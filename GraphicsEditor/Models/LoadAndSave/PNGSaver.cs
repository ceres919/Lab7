using Avalonia.Media.Imaging;
using Avalonia;
using GraphicsEditor.Models.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace GraphicsEditor.Models.LoadAndSave
{
    public class PNGSaver
    {
        public void Save(Canvas canv, string path)
        {
            var pixelSize = new PixelSize((int)canv.Bounds.Width, (int)canv.Bounds.Height);
            var point = new Point(canv.Bounds.Position.X, canv.Bounds.Position.Y);
            var size = new Size(canv.Bounds.Width, canv.Bounds.Height);
            using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Vector(96, 96)))
            {
                canv.Measure(size);
                canv.Arrange(new Rect(size));
                bitmap.Render(canv);
                canv.Arrange(new Rect(point, size));
                bitmap.Save(path);
            }
        }
    }
}
