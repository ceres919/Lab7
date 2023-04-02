﻿using Avalonia;
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
    public class PolyLineShape : ShapeEntity
    {
        public string Points { get; set; }
        public PolyLineShape() { }
        public PolyLineShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness, cr.shapeAngle, cr.shapeAngleCenter, cr.shapeScaleTransform, cr.shapeSkewTransform)
        {
            Points = cr.shapePoints;
        }

        public override PolyLineShape AddToList(ShapeCreator cr)
        {
            if (cr.shapePoints == null) 
                return null;
            return new PolyLineShape(cr);
        }
        public override Shape AddThisShape()
        {
            Points? points = GroupOfPointsParse(this.Points);
            if (points == null) return null;
            TransformGroup transformation = new TransformGroup();
            if (this.Angle != 0 || this.AngleCenter != "" || this.ScaleTransform != "" || this.SkewTransform != "")
                transformation = ShapeTransformationSetter(this.Angle, this.AngleCenter, this.ScaleTransform, this.SkewTransform);

            return new Polyline
            {
                Name = this.Name,
                Points = points,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
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
        }

        public override Shape Change(Shape changedShape, double x, double y)
        {
            Polyline newShape = changedShape as Polyline;
            Points points = GroupOfPointsParse(this.Points);
            Points newPoints = new Points();
            string str ="";

            foreach(var point in points)
            {
                Point s = new Point(point.X + x, point.Y + y);
                newPoints.Add(s);
                str += $"{s.X},{s.Y} ";
            }
            str = str.TrimEnd(' ');
            this.Points = str;
            newShape.Points = newPoints;
            return newShape;
        }
    }
}
