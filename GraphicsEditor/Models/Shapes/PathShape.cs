using GraphicsEditor.ViewModels;
using System.Linq;

namespace GraphicsEditor.Models.Shapes
{
    public class PathShape : ShapeEntity 
    {
        private string startPoint;
        public string StartPoint { get => startPoint; set => SetAndRaise(ref startPoint, value); }
        public string FillColor { get; set; }
        public string CommandPath { get; set; }
        public PathShape() { }
        public PathShape(MainWindowViewModel cr) : base(cr.ShapeName, cr.ShapeStrokeColor, cr.ShapeStrokeThickness, cr.ShapeAngle, cr.ShapeAngleCenter, cr.ShapeScaleTransform, cr.ShapeSkewTransform)
        {
            StartPoint = cr.ShapeStartPoint;
            FillColor = cr.ShapeFillColor.Color.ToString();
            CommandPath = cr.ShapeCommandPath;
        }

        public override PathShape AddToList(MainWindowViewModel cr)
        {
            if (cr.ShapeCommandPath == null)
                return null;
            return new PathShape(cr);
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeCommandPath = this.CommandPath;
            main.ShapeFillColor = main.ColoredBrush.First(p => p.Color == Avalonia.Media.Color.Parse(this.FillColor));
        }

        public override void Change(double x, double y)
        {
            this.StartPoint = $"{x},{y}";
        }
    }
}
