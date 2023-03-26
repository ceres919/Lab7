using Avalonia.Controls.Shapes;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GraphicsEditor.ViewModels;
using Avalonia.Media;
using System.Globalization;

namespace GraphicsEditor.Models.Shapes
{
    [XmlInclude(typeof(LineShape))]
    [XmlInclude(typeof(PolyLineShape))]
    [XmlInclude(typeof(PolygonShape))]
    [XmlInclude(typeof(RectangleShape))]
    [XmlInclude(typeof(EllipseShape))]
    [XmlInclude(typeof(PathShape))]
    public abstract class ShapeEntity
    {
        public string Name { get; set; }
        public string StrokeColor { get; set; }
        public double StrokeThickness { get; set; }
        public double Angle { get; set; }
        public string? AngleCenter { get; set; }
        public string? ScaleTransform { get; set; }
        public string? SkewTransform { get; set; }

        public ShapeEntity() { }
        public ShapeEntity(string name, string strokeColor, double strokeThickness, double angle, string angleCenter, string scaleTransform, string skewTransform)
        {
            Name = name;
            StrokeColor = strokeColor;
            StrokeThickness = strokeThickness;
            Angle = angle;
            AngleCenter = angleCenter;
            ScaleTransform = scaleTransform;
            SkewTransform = skewTransform;
        }

        public abstract Shape AddThisShape();
        public abstract ShapeEntity AddToList(ShapeCreator cr);
        public virtual void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            main.ShapeName = this.Name;
            main.ShapeStrokeColor = this.StrokeColor;
            main.ShapeStrokeThickness = this.StrokeThickness;
            main.ShapeAngle = this.Angle;
            main.ShapeAngleCenter = this.AngleCenter;
            main.ShapeScaleTransform = this.ScaleTransform;
            main.ShapeSkewTransform = this.SkewTransform;
        }
        public double[] PointsParse(string str)
        {
            double[] point = { 0, 0 };
            try
            {
                var str_point = str.Split(",");
                point[0] = double.Parse(str_point[0], CultureInfo.InvariantCulture.NumberFormat);
                point[1] = double.Parse(str_point[1], CultureInfo.InvariantCulture.NumberFormat);

            }
            catch
            {
                return null;
            }
            return point;
        }
        
        public TransformGroup ShapeTransformationSetter(double angle, string cen_angle, string str_scale, string str_skew)
        { 
            TransformGroup group = new TransformGroup();
            if(angle != 0)
            {
                var center = PointsParse(cen_angle);
                RotateTransform rotate_transform = new RotateTransform() { Angle = angle, CenterX = center[0], CenterY = center[1] };
                group.Children.Add(rotate_transform);
            }
            if(str_scale != "")
            {
                var scale = PointsParse(str_scale);
                ScaleTransform scale_transform = new ScaleTransform(scale[0], scale[1]);
                group.Children.Add(scale_transform);
            }
            if(str_skew != "")
            {
                var skew = PointsParse(str_skew);
                SkewTransform skew_transform = new SkewTransform(skew[0], skew[1]);
                group.Children.Add(skew_transform);
            }
            return group;
        }
    }
}
