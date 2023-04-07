using Avalonia.Media;
using GraphicsEditor.ViewModels;

namespace GraphicsEditor.Models.Shapes
{
    public class EllipseShape : ShapeEntity
    {
        private string startPoint;
        public string StartPoint { get => startPoint; set => SetAndRaise(ref startPoint, value); }
        public double Width { get; set; }
        public double Height { get; set; }
        public string FillColor { get; set; }
        public EllipseShape() { }
        public EllipseShape(MainWindowViewModel cr) : base(cr.ShapeName, cr.ShapeStrokeColor, cr.ShapeStrokeThickness, cr.ShapeAngle, cr.ShapeAngleCenter, cr.ShapeScaleTransform, cr.ShapeSkewTransform) 
        {
            StartPoint = cr.ShapeStartPoint;
            Width = cr.ShapeWidth;
            Height = cr.ShapeHeight;
            FillColor = cr.ShapeFillColor.Color.ToString();
        }

        public override EllipseShape AddToList(MainWindowViewModel cr)
        {
            if (cr.ShapeStartPoint == null || cr.ShapeWidth == 0 || cr.ShapeHeight == 0)
                return null;
            return new EllipseShape(cr);
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeHeight = this.Height;
            main.ShapeWidth = this.Width;
            main.ShapeFillColor = SolidColorBrush.Parse(this.FillColor);
        }

        public override void Change(double x, double y)
        {
            this.StartPoint = $"{x},{y}";
        }
    }
}
