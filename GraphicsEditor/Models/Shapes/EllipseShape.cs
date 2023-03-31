﻿using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class EllipseShape : ShapeEntity
    {
        public string StartPoint { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string FillColor { get; set; }
        public EllipseShape() { }
        public EllipseShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness, cr.shapeAngle, cr.shapeAngleCenter, cr.shapeScaleTransform, cr.shapeSkewTransform) 
        {
            StartPoint = cr.shapeStartPoint;
            Width = cr.shapeWidth;
            Height = cr.shapeHeight;
            FillColor = cr.shapeFillColor;
        }

        public override EllipseShape AddToList(ShapeCreator cr)
        {
            if (cr.shapeStartPoint == null || cr.shapeWidth == 0 || cr.shapeHeight == 0)
                return null;
            return new EllipseShape(cr);
        }
        public override Shape AddThisShape()
        {
            var startPoint = PointsParse(this.StartPoint);
            if (startPoint == null)
                return null;
            TransformGroup transformation = new TransformGroup();
            if (this.Angle != 0 || this.AngleCenter != "" || this.ScaleTransform != "" || this.SkewTransform != "")
                transformation = ShapeTransformationSetter(this.Angle, this.AngleCenter, this.ScaleTransform, this.SkewTransform);

            return new Ellipse
            {
                Name = this.Name,
                Margin = new(startPoint[0], startPoint[1], 0, 0),
                Height = this.Height,
                Width = this.Width,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(this.FillColor)),
                RenderTransform = transformation
            };
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeStartPoint = this.StartPoint;
            main.ShapeHeight = this.Height;
            main.ShapeWidth = this.Width;
            main.ShapeFillColor = this.FillColor;
        }

        public override Shape Change(Shape changedShape, double x, double y)
        {
            var startPoint = PointsParse(this.StartPoint);
            this.StartPoint = $"{startPoint[0] + x},{startPoint[1] + y}";
            Ellipse newShape = changedShape as Ellipse;
            newShape.Margin = new(startPoint[0] + x, startPoint[1] + y, 0, 0);
            return newShape;
        }
    }
}
