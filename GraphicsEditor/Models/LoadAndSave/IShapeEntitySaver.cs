using Avalonia.Controls;
using GraphicsEditor.Models.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.LoadAndSave
{
    public interface IShapeEntitySaver
    {
        void Save(ObservableCollection<ShapeEntity> people, string path);
    }
}
