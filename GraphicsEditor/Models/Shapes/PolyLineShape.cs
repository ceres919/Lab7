using Avalonia;
using GraphicsEditor.ViewModels;
using System.Globalization;

namespace GraphicsEditor.Models.Shapes
{
    public class PolyLineShape : ShapeEntity
    {
        private string lotPoints;
        public string LotPoints { get => lotPoints; set => SetAndRaise(ref lotPoints, value); }
        public PolyLineShape() { }
        public PolyLineShape(MainWindowViewModel cr) : base(cr.ShapeName, cr.ShapeStrokeColor, cr.ShapeStrokeThickness, cr.ShapeAngle, cr.ShapeAngleCenter, cr.ShapeScaleTransform, cr.ShapeSkewTransform)
        {
            LotPoints = cr.ShapePoints;
        }

        public override PolyLineShape AddToList(MainWindowViewModel cr)
        {
            if (cr.ShapePoints == null) 
                return null;
            return new PolyLineShape(cr);
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
            main.ShapePoints = this.LotPoints;
        }

        public override void Change(double x, double y)
        {
            Points points = GroupOfPointsParse(this.LotPoints);
            Points newPoints = new Points();
            string str = "";

            foreach (var point in points)
            {
                Point s = new Point(point.X + x, point.Y + y);
                newPoints.Add(s);
                str += $"{s.X},{s.Y} ";
            }
            str = str.TrimEnd(' ');
            this.LotPoints = str;
        }
    }
}
