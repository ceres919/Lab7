using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using GraphicsEditor.Models.Shapes;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models
{
    public class ShapeCreator
    {
        public readonly ObservableCollection<ShapeEntity> shapes = new()
        {
            new LineShape(),
            new PolyLineShape(),
            new PolygonShape(),
            new RectangleShape(),
            new EllipseShape(),
            new PathShape()
        };
        public string shapeName;
        public string? shapeStartPoint;
        public string? shapeEndPoint;
        public string? shapePoints;
        public double shapeWidth;
        public double shapeHeight;
        public string shapeStrokeColor;
        public string? shapeFillColor;
        public double shapeStrokeThickness;
        public string? shapeCommandPath;
        public double shapeAngle;
        public string? shapeAngleCenter;
        public string? shapeScaleTransform;
        public string? shapeSkewTransform;

        public ShapeCreator() { }
        public ShapeCreator(MainWindowViewModel main) 
        {
            shapeName = main.ShapeName;
            shapeStartPoint = main.ShapeStartPoint;
            shapeEndPoint = main.ShapeEndPoint;
            shapePoints = main.ShapePoints;
            shapeWidth = main.ShapeWidth;
            shapeHeight = main.ShapeHeight;
            shapeStrokeColor = main.ShapeStrokeColor;
            shapeFillColor = main.ShapeFillColor;
            shapeStrokeThickness = main.ShapeStrokeThickness;
            shapeCommandPath = main.ShapeCommandPath;
            shapeAngle = main.ShapeAngle;
            shapeAngleCenter = main.ShapeAngleCenter;
            shapeScaleTransform = main.ShapeScaleTransform;
            shapeSkewTransform = main.ShapeSkewTransform;
        }
        public void Create(int index, ShapesCollection list, Canvas canvas) 
        {
            if (shapeName == null) return;
            ShapeEntity newItem = shapes.ElementAt(index).AddToList(this);
            if (newItem == null) return;
            Shape newShape = newItem.AddThisShape();
            //Shape? newShape = shapes.ElementAt(index).AddThisShape(this);
            //if (newShape == null) return;
            //ShapeEntity newItem = shapes.ElementAt(index).AddToList(this);
            list.AddItem(newItem, newShape, canvas);
        }
        public void Load(ShapeEntity listItem, ShapesCollection list, Canvas canvas)
        {
            Shape newShape = listItem.AddThisShape();
            list.AddItem(listItem, newShape, canvas);
        }
        public int ListIndexOfCurrentShape(ShapeEntity item)
        {
            var curType = item.GetType();
            var typedItem = shapes.First(p=> p.GetType() == curType);
            var index = shapes.IndexOf(typedItem);
            return index;
        }
    }
}
