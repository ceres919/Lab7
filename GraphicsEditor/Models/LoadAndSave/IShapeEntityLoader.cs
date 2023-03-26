using GraphicsEditor.Models.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.LoadAndSave
{
    public interface IShapeEntityLoader
    {
        IEnumerable<ShapeEntity> Load(string path);
    }
}
