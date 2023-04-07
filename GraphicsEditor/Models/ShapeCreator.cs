using GraphicsEditor.Models.Shapes;
using GraphicsEditor.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphicsEditor.Models
{
    public class ShapeCreator
    {
        private readonly ObservableCollection<ShapeEntity> shapesClasses = new()
        {
            new LineShape(),
            new PolyLineShape(),
            new PolygonShape(),
            new RectangleShape(),
            new EllipseShape(),
            new PathShape()
        };
        private MainWindowViewModel viewModel;

        public ShapeCreator() { }
        public ShapeCreator(MainWindowViewModel main) 
        {
            viewModel = main;
        }

        public void Create(int index, ShapesCollection list) 
        {
            if (viewModel.ShapeName == null) return;
            ShapeEntity newItem = shapesClasses.ElementAt(index).AddToList(viewModel);
            if (newItem == null) return;
            list.AddItem(newItem);
        }
        public static void Load(ShapeEntity listItem, ShapesCollection list)
        {
            list.AddItem(listItem);
        }
        public int ListIndexOfCurrentShape(ShapeEntity item)
        {
            var curType = item.GetType();
            var typedItem = shapesClasses.First(p=> p.GetType() == curType);
            var index = shapesClasses.IndexOf(typedItem);
            return index;
        }
    }
}
