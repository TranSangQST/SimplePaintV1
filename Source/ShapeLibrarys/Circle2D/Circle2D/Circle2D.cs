using Contract;
using System;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Circle2D
{
    public class Ellipse2D : IShape
    {
        public Point2D LeftTop { get; set; }
        public Point2D RightBottom { get; set; }

        public Ellipse2D()
        {
            LeftTop = new Point2D();
            RightBottom = new Point2D();
            Thickness = 1;
            Color = Brushes.Red;
            Fill = Brushes.Transparent;
            StrokeDashArray = DoubleCollection.Parse("1 0");
        }

        public Ellipse2D(Point2D leftTop, Point2D rightBottom, int thickness, Brush color, Brush fill, DoubleCollection strokeDashArray)
        {
            LeftTop = new Point2D(leftTop.X, leftTop.Y);
            RightBottom = new Point2D(rightBottom.X, rightBottom.Y);
            Thickness = thickness;
            Color = color;
            Fill = fill;
            StrokeDashArray = strokeDashArray;
        }

        public string Name => "Ellipse2D";

        public int Thickness { get; set; }
        public Brush Color { get; set; }
        public Brush Fill { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }

        public UIElement Draw()
        {

            double width = RightBottom.X - LeftTop.X;
            double height = RightBottom.Y - LeftTop.Y;

            var ellipse = new Ellipse()
            {
                Width = width >= 0 ? width : -width,
                Height = height >= 0 ? height : -height,


                StrokeThickness = Thickness,
                Stroke = Color,
                Fill = Fill,
                StrokeDashArray = StrokeDashArray

            };

            // RightBottom.X >= LeftTop.X;
            if (width >= 0)
            {
                Canvas.SetLeft(ellipse, LeftTop.X);
            }
            // RightBottom.X < LeftTop.X;
            //Mặc dù RightBottom.X < LeftTop.X (tức là RightBottom ở bên trái của _lefttop
            //nên RightBottom không phải Right Bottom trên hình vẽ thực
            //Nhưng để dễ hiểu vẫn để nó là Right Bottom
            else
            {
                Canvas.SetLeft(ellipse, RightBottom.X);
            }

            // RightBottom.Y >= LeftTop.Y;
            if (height >= 0)
            {
                Canvas.SetTop(ellipse, LeftTop.Y);
            }
            // RightBottom.Y < LeftTop.Y;
            else
            {
                Canvas.SetTop(ellipse, RightBottom.Y);
            }


            return ellipse;
        }

        public UIElement DrawSelection()
        {


            //Point2D leftTop = (Point2D)LeftTop.Next();
            //leftTop.Thickness = 20;
            //leftTop.Color = new SolidColorBrush(Colors.Gold);
            //var lt = leftTop.Draw();


            //Point2D rightBottom = (Point2D)RightBottom.Next();
            //rightBottom.Thickness = 5;
            //var rb = rightBottom.Draw();

            double width = RightBottom.X - LeftTop.X;
            double height = RightBottom.Y - LeftTop.Y;

            var rect = new Rectangle()
            {
                Width = width >= 0 ? width : -width,
                Height = height >= 0 ? height : -height,


                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Blue),
                Fill = new SolidColorBrush(Colors.Transparent),
                StrokeDashArray = DoubleCollection.Parse("4 4"),


            };



            // RightBottom.X >= LeftTop.X;
            if (width >= 0)
            {
                Canvas.SetLeft(rect, LeftTop.X);
            }
            // RightBottom.X < LeftTop.X;
            //Mặc dù RightBottom.X < LeftTop.X (tức là RightBottom ở bên trái của _lefttop
            //nên RightBottom không phải Right Bottom trên hình vẽ thực
            //Nhưng để dễ hiểu vẫn để nó là Right Bottom
            else
            {
                Canvas.SetLeft(rect, RightBottom.X);
            }

            // RightBottom.Y >= LeftTop.Y;
            if (height >= 0)
            {
                Canvas.SetTop(rect, LeftTop.Y);
            }
            // RightBottom.Y < LeftTop.Y;
            else
            {
                Canvas.SetTop(rect, RightBottom.Y);
            }

            return rect;
        }

        public void HandleStart(double x, double y)
        {
            LeftTop = new Point2D() { X = x, Y = y };
        }

        public void HandleEnd(double x, double y)
        {
            RightBottom = new Point2D() { X = x, Y = y };
        }

        public IShape Next()
        {
            return new Ellipse2D();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
