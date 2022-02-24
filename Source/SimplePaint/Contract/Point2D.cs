using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Contract
{
    public class Point2D : IShape
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D()
        {

            X = 0;
            Y = 0;
            Thickness = 1;
            Color = Brushes.Red;
            StrokeDashArray = DoubleCollection.Parse("1 0");

        }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
            Thickness = 1;
            Color = Brushes.Red;
            Color = Brushes.Transparent;
            StrokeDashArray = DoubleCollection.Parse("1 0");
        }

        public Point2D(double x, double y, int thickness, Brush color, Brush fill, DoubleCollection strokeDashArray)
        {
            X = x;
            Y = y;
            Thickness = thickness;
            Color = color;
            Fill = fill;
            StrokeDashArray = strokeDashArray;
        }

        public string Name
        {
            get => "Point2D";
        }

        public int Thickness { get; set; }
        public Brush Color { get; set; }
        public Brush Fill { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        public IShape Next()
        {
            return new Point2D();
        }

        public void HandleStart(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void HandleEnd(double x, double y)
        {
            X = x;
            Y = y;
        }

        public UIElement Draw()
        {
            Line line1Point = new Line()
            {
                X1 = X,
                Y1 = Y,
                X2 = X,
                Y2 = Y,
                StrokeThickness = Thickness,
                Stroke = Color,
                Fill = Fill,
                StrokeDashArray = StrokeDashArray

            };

            return line1Point;
        }

        public UIElement DrawSelection()
        {
            Line Line1Point = new Line()
            {
                X1 = X,
                Y1 = Y,
                X2 = X,
                Y2 = Y,
                StrokeThickness = Thickness,
                Stroke = Color,
                Fill = Fill,
                StrokeDashArray = StrokeDashArray

            };

            return Line1Point;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
