using Contract;
using System;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Square2D
{
    public class Square2D : IShape
    {
        public Point2D LeftTop { get; set; }
        public Point2D RightBottom { get; set; }


        public Square2D()
        {
            LeftTop = new Point2D();
            RightBottom = new Point2D();
            Thickness = 1;
            Color = Brushes.Red;
            Fill = Brushes.Transparent;
            StrokeDashArray = DoubleCollection.Parse("1 0");
        }

        public Square2D(Point2D leftTop, Point2D rightBottom, int thickness, Brush color, Brush fill, DoubleCollection strokeDashArray)
        {
            LeftTop = new Point2D(leftTop.X, leftTop.Y);
            RightBottom = new Point2D(rightBottom.X, rightBottom.Y);
            Thickness = thickness;
            Color = color;
            Fill = fill;
            StrokeDashArray = strokeDashArray;
        }

        public string Name => "Square2D";

        public int Thickness { get; set; }
        public Brush Color { get; set; }
        public Brush Fill { get; set; }
        public DoubleCollection StrokeDashArray { get; set; }

        public UIElement Draw()
        {

            double width = RightBottom.X - LeftTop.X;
            double height = RightBottom.Y - LeftTop.Y;


            double dx = width >= 0 ? width : -width;
            double dy = height >= 0 ? height : -height;

            if (dx >= dy)
            {
                double d = dx - dy;
                if (height >= 0)
                {
                    HandleEnd(RightBottom.X, RightBottom.Y + d);
                }
                else
                {
                    HandleEnd(RightBottom.X, RightBottom.Y - d);
                }

            }
            else
            {
                double d = dy - dx;
                if (width >= 0)
                {
                    HandleEnd(RightBottom.X + d, RightBottom.Y);
                }
                else
                {
                    HandleEnd(RightBottom.X - d, RightBottom.Y);
                }
            }

            width = RightBottom.X - LeftTop.X;
            height = RightBottom.Y - LeftTop.Y;


            var rect = new Rectangle()
            {
                Width = width >= 0 ? width : -width,
                Height = height >= 0 ? height : -height,


                StrokeThickness = Thickness,
                Stroke = Color,
                Fill = Fill,
                StrokeDashArray = StrokeDashArray,
                //Stretch = Stretch.Uniform,
                AllowDrop = false
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
                Stroke = new SolidColorBrush(Colors.White),
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
            return new Square2D();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
