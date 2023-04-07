using GraphicsEditor.Models.Shapes;
using System.Collections.Generic;

namespace GraphicsEditor.Models.LoadAndSave
{
    public interface IShapeEntityLoader
    {
        IEnumerable<ShapeEntity> Load(string path);
    }
}
