using Contract;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Line2D
{
    public class Line2D: IShape
    {
        public Point2D Start { get; set; }
        public Point2D End { get; set; }

        public Line2D()
        {
            Start = new Point2D();
            End = new Point2D();
            Thickness = 1;
            Color = Brushes.Red;
            Fill = Brushes.Transparent;
            StrokeDashArray = DoubleCollection.Parse("1 0");
        }    

        public Line2D(Point2D start, Point2D end, int thickness, Brush color, Brush fill, DoubleCollection strokeDashArray)
        {
            Start = new Point2D(start.X, start.Y);
            End = new Point2D(end.X, end.Y);

            Thickness = thickness;
            Color = color;
            Fill = fill;
            StrokeDashArray = strokeDashArray;
        }

        public string Name => "Line2D";

        public int Thickness { get; set; }
        public Brush Color { get; set; }
        public Brush Fill { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }

        public void HandleStart(double x, double y)
        {
            Start = new Point2D() { X = x, Y = y };
        }

        public void HandleEnd(double x, double y)
        {
            End = new Point2D() { X = x, Y = y };
        }

        public UIElement Draw()
        {
            Line line = new Line()
            {
                X1 = Start.X,
                Y1 = Start.Y,
                X2 = End.X,
                Y2 = End.Y,

                StrokeThickness = Thickness,
                Stroke = Color,
                Fill = Fill,
                StrokeDashArray = StrokeDashArray

            };

            return line;
        }

        public UIElement DrawSelection()
        {


            double width = End.X - Start.X;
            double height = End.Y - Start.Y;

            var rect = new Rectangle()
            {
                Width = width >= 0 ? width : -width,
                Height = height >= 0 ? height : -height,


                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Fill = new SolidColorBrush(Colors.Transparent),
                StrokeDashArray = DoubleCollection.Parse("4 4"),


            };



            // End.X >= Start.X;
            if (width >= 0)
            {
                Canvas.SetLeft(rect, Start.X);
            }
            // End.X < Start.X;
            //Mặc dù End.X < Start.X (tức là End ở bên trái của _lefttop
            //nên End không phải Right Bottom trên hình vẽ thực
            //Nhưng để dễ hiểu vẫn để nó là Right Bottom
            else
            {
                Canvas.SetLeft(rect, End.X);
            }

            // End.Y >= Start.Y;
            if (height >= 0)
            {
                Canvas.SetTop(rect, Start.Y);
            }
            // End.Y < Start.Y;
            else
            {
                Canvas.SetTop(rect, End.Y);
            }

            return rect;
        }

        public IShape Next()
        {
            return new Line2D();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
