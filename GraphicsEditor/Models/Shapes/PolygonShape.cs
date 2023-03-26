using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class PolygonShape : ShapeEntity
    {
        public string Points { get; set; }
        public string FillColor { get; set; }
        public PolygonShape() { }
        public PolygonShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness, cr.shapeAngle, cr.shapeAngleCenter, cr.shapeScaleTransform, cr.shapeSkewTransform)
        {
            Points = cr.shapePoints;
            FillColor = cr.shapeFillColor;
        }

        public override PolygonShape AddToList(ShapeCreator cr)
        {
            if (cr.shapePoints == null) 
                return null;
            return new PolygonShape(cr);
        }
        public override Shape AddThisShape()
        {
            Points? points = GroupOfPointsParse(this.Points);
            if (points == null) 
                return null;
            TransformGroup transformation = new TransformGroup();
            if (this.Angle != 0 || this.AngleCenter != "" || this.ScaleTransform != "" || this.SkewTransform != "")
                transformation = ShapeTransformationSetter(this.Angle, this.AngleCenter, this.ScaleTransform, this.SkewTransform);

            return new Polygon
            {
                Name = this.Name,
                Points = points,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(this.FillColor)),
                RenderTransform = transformation
            };
        }
        public Points GroupOfPointsParse(string str)
        {
            Points points = new Points();
            try
            {
                var str_points = str.Split(" ");
                foreach (var point in str_points)
                {
                    var str_point = point.Split(",");
                    var p1 = double.Parse(str_point[0], CultureInfo.InvariantCulture.NumberFormat);
                    var p2 = double.Parse(str_point[1], CultureInfo.InvariantCulture.NumberFormat);
                    points.Add(new Point(p1, p2));
                }
            }
            catch
            {
                return null;
            }
            return points;
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapePoints = this.Points;
            main.ShapeFillColor = this.FillColor;
        }
    }
}
