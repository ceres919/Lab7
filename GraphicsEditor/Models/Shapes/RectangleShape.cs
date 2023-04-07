using GraphicsEditor.ViewModels;
using System.Linq;

namespace GraphicsEditor.Models.Shapes
{
    public class RectangleShape : ShapeEntity
    {
        private string startPoint;
        public string StartPoint { get => startPoint; set => SetAndRaise(ref startPoint, value); }
        public double Width { get; set; }
        public double Height { get; set; }
        public string FillColor { get; set; }
        public RectangleShape() { }
        public RectangleShape(MainWindowViewModel cr) : base(cr.ShapeName, cr.ShapeStrokeColor, cr.ShapeStrokeThickness, cr.ShapeAngle, cr.ShapeAngleCenter, cr.ShapeScaleTransform, cr.ShapeSkewTransform) 
        {
            StartPoint = cr.ShapeStartPoint;
            Width = cr.ShapeWidth;
            Height = cr.ShapeHeight;
            FillColor = cr.ShapeFillColor.Color.ToString();
        }

        public override RectangleShape AddToList(MainWindowViewModel cr)
        {
            if (cr.ShapeStartPoint == null || cr.ShapeWidth == 0 || cr.ShapeHeight == 0)
                return null;
            return new RectangleShape(cr);
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeHeight = this.Height;
            main.ShapeWidth = this.Width;
            main.ShapeFillColor = main.ColoredBrush.First(p => p.Color == Avalonia.Media.Color.Parse(this.FillColor));
        }

        public override void Change(double x, double y)
        {
            this.StartPoint = $"{x},{y}";
        }
    }
}
