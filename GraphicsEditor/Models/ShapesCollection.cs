using GraphicsEditor.Models.Shapes;
using System.Collections.ObjectModel;

namespace GraphicsEditor.Models
{
    public class ShapesCollection
    {
        public ObservableCollection<ShapeEntity> shapeList = new ObservableCollection<ShapeEntity>();

        public ShapesCollection() 
        {
            shapeList = new ObservableCollection<ShapeEntity>();
        }

        public void AddItem(ShapeEntity item)
         {
            foreach (ShapeEntity itemEntity in shapeList)
            {
                if (itemEntity.Name == item.Name)
                {
                    var index = shapeList.IndexOf(itemEntity);
                    shapeList.Remove(itemEntity);
 
                    break;
                }
            }
            shapeList.Add(item);
        }
        public void DeleteItem(ShapeEntity item)
        {
            var index = shapeList.IndexOf(item);
            shapeList.Remove(item);
        }
    }
}
