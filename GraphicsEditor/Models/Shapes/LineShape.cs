using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace GraphicsEditor.Models.Shapes
{
    
    public class LineShape : ShapeEntity
    {
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }
        public LineShape() { }
        public LineShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness, cr.shapeAngle, cr.shapeAngleCenter, cr.shapeScaleTransform, cr.shapeSkewTransform)
        {
            StartPoint = cr.shapeStartPoint;
            EndPoint = cr.shapeEndPoint;
        }

        public override LineShape AddToList(ShapeCreator cr)
        {
            if (cr.shapeStartPoint == null || cr.shapeEndPoint == null)
                return null;
            return new LineShape(cr);
        }
        public override Shape AddThisShape()
        {
            var startPoint = PointsParse(this.StartPoint);
            var endPoint = PointsParse(this.EndPoint);
            if (startPoint == null || endPoint == null) 
                return null;
            TransformGroup transformation = new TransformGroup();
            if (this.Angle != 0 || this.AngleCenter != "" || this.ScaleTransform != "" || this.SkewTransform != "")
                transformation = ShapeTransformationSetter(this.Angle, this.AngleCenter, this.ScaleTransform, this.SkewTransform);

            return new Line
            {
                Name = this.Name,
                StartPoint = new Point(startPoint[0], startPoint[1]),
                EndPoint = new Point(endPoint[0], endPoint[1]),
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                RenderTransform = transformation
            };
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeEndPoint = this.EndPoint;
        }
    }
}
