using GraphicsEditor.Models.Shapes;
using System.Collections.ObjectModel;

namespace GraphicsEditor.Models.LoadAndSave
{
    public interface IShapeEntitySaver
    {
        void Save(ObservableCollection<ShapeEntity> people, string path);
    }
}
