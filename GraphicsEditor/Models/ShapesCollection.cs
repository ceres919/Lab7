using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using GraphicsEditor.Models.Shapes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class ShapesCollection
    {
        public ObservableCollection<ShapeEntity> shapeList = new ObservableCollection<ShapeEntity>();
        public ObservableCollection<Shape> shapesCollection = new ObservableCollection<Shape>();

        public ShapesCollection() 
        {
            shapeList = new ObservableCollection<ShapeEntity>();
            shapesCollection = new ObservableCollection<Shape>();
        }

        public void AddItem(ShapeEntity item, Shape shape, Canvas canvas)
         {
            foreach (ShapeEntity itemEntity in shapeList)
            {
                if (itemEntity.Name == item.Name)
                {
                    var index = shapeList.IndexOf(itemEntity);
                    shapeList.Remove(itemEntity);
                    var sh = shapesCollection.ElementAt(index);
                    shapesCollection.Remove(sh);
                    canvas.Children.Remove(sh);
                    break;
                }
            }
            shapeList.Add(item);
            shapesCollection.Add(shape);
            canvas.Children.Add(shape);
        }
        public void DeleteItem(ShapeEntity item, Canvas canvas)
        {
            var index = shapeList.IndexOf(item);
            shapeList.Remove(item);
            var shape = shapesCollection.ElementAt(index);
            canvas.Children.Remove(shape);
            shapesCollection.Remove(shape);

        }
        
    }
}
