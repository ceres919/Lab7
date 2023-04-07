using System.Linq;
using System.Xml.Serialization;
using GraphicsEditor.ViewModels;
using Avalonia.Media;
using System.Globalization;
using DynamicData.Binding;

namespace GraphicsEditor.Models.Shapes
{
    [XmlInclude(typeof(LineShape))]
    [XmlInclude(typeof(PolyLineShape))]
    [XmlInclude(typeof(PolygonShape))]
    [XmlInclude(typeof(RectangleShape))]
    [XmlInclude(typeof(EllipseShape))]
    [XmlInclude(typeof(PathShape))]
    public abstract class ShapeEntity : AbstractNotifyPropertyChanged
    {
        public string Name { get; set; }
        public string StrokeColor { get; set; }
        public double StrokeThickness { get; set; }
        public double Angle { get; set; }
        public string AngleCenter { get; set; } //= null;
        public string ScaleTransform { get; set; } //= null;
        public string SkewTransform { get; set; } //= null;

        public ShapeEntity() { }
        public ShapeEntity(string name, ISolidColorBrush strokeColor, double strokeThickness, double angle, string angleCenter, string scaleTransform, string skewTransform)
        {
            Name = name;
            StrokeColor = strokeColor.Color.ToString();
            StrokeThickness = strokeThickness;
            Angle = angle;
            AngleCenter = angleCenter;
            ScaleTransform = scaleTransform;
            SkewTransform = skewTransform;
        }

        public abstract ShapeEntity AddToList(MainWindowViewModel cr);
        public abstract void Change(double x, double y);

        public virtual void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            main.ShapeName = this.Name;
            main.ShapeStrokeColor = main.ColoredBrush.First(p => p.Color == Avalonia.Media.Color.Parse(this.StrokeColor));
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
    }
}
