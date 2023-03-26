using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GraphicsEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.Shapes
{
    public class PathShape : ShapeEntity 
    {
        public string FillColor { get; set; }
        public string CommandPath { get; set; }
        public PathShape() { }
        public PathShape(ShapeCreator cr) : base(cr.shapeName, cr.shapeStrokeColor, cr.shapeStrokeThickness, cr.shapeAngle, cr.shapeAngleCenter, cr.shapeScaleTransform, cr.shapeSkewTransform)
        {
            FillColor = cr.shapeFillColor;
            CommandPath = cr.shapeCommandPath;
        }

        public override PathShape AddToList(ShapeCreator cr)
        {
            if (cr.shapeCommandPath == null)
                return null;
            return new PathShape(cr);
        }
        public override Shape AddThisShape()
        {
            Geometry gem;
            try
            {
                gem = Geometry.Parse(this.CommandPath);
            }
            catch
            {
                return null;
            }
            //if (gem == null) return null;
            TransformGroup transformation = new TransformGroup();
            if (this.Angle != 0 || this.AngleCenter != "" || this.ScaleTransform != "" || this.SkewTransform != "")
                transformation = ShapeTransformationSetter(this.Angle, this.AngleCenter, this.ScaleTransform, this.SkewTransform);

            return new Path
            {
                Name = this.Name,
                Data = gem,
                Stroke = new SolidColorBrush(Color.Parse(this.StrokeColor)),
                StrokeThickness = this.StrokeThickness,
                Fill = new SolidColorBrush(Color.Parse(this.FillColor)),
                RenderTransform = transformation
            };
        }
        public override void SetPropertiesOfCurrentShape(MainWindowViewModel main)
        {
            base.SetPropertiesOfCurrentShape(main);
            main.ShapeCommandPath = this.CommandPath;
            main.ShapeFillColor = this.FillColor;
        }
    }
}
