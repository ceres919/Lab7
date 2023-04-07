using GraphicsEditor.ViewModels;

namespace GraphicsEditor.Models.Shapes
{
    
    public class LineShape : ShapeEntity
    {

        private string startPoint;
        private string endPoint;

        public string StartPoint { get => startPoint; set => SetAndRaise(ref startPoint, value); }

        public string EndPoint { get => endPoint; set => SetAndRaise(ref endPoint, value); }
        public LineShape() { }
        public LineShape(MainWindowViewModel cr) : base(cr.ShapeName, cr.ShapeStrokeColor, cr.ShapeStrokeThickness, cr.ShapeAngle, cr.ShapeAngleCenter, cr.ShapeScaleTransform, cr.ShapeSkewTransform)
        {
            StartPoint = cr.ShapeStartPoint;
            EndPoint = cr.ShapeEndPoint;
        }
         
        public override LineShape AddToList(MainWindowViewModel cr)
        {
            if (cr.ShapeStartPoint == null || cr.ShapeEndPoint == null)
                return null;
            return new LineShape(cr);
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeEndPoint = this.EndPoint;
        }

        public override void Change(double x, double y)
        {
            var startPoint = PointsParse(this.StartPoint);
            var endPoint = PointsParse(this.EndPoint);
            this.StartPoint = $"{startPoint[0] + x},{startPoint[1] + y}";
            this.EndPoint = $"{endPoint[0] + x},{endPoint[1] + y}";
        }
    }
}
