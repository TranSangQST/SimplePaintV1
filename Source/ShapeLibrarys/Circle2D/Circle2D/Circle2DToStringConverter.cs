using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Circle2D
{
    public class Ellipse2DToStringConverter : IShapeToStringConverter
    {
        public string Name
        {
            get => "Ellipse2D";
        }

        public string Convert(IShape shape)
        {
            //Ex: Ellipse2D [(2, 2), (1, 1)], [1], [#FFFF0000], [1, 0]

            Ellipse2D ellipse2D = (Ellipse2D)shape;

            string name = ellipse2D.Name;
            double leftTopX = ellipse2D.LeftTop.X;
            double rightBottomX = ellipse2D.RightBottom.X;
            double leftTopY = ellipse2D.LeftTop.Y;
            double rightBottomY = ellipse2D.RightBottom.Y;

            int thickness = ellipse2D.Thickness;
            string color = ellipse2D.Color.ToString();
            string fill = ellipse2D.Fill.ToString();
            string strokeDashArray = ellipse2D.StrokeDashArray.ToString();

            string result = $"{name} [({leftTopX}, {leftTopY}), ({rightBottomX}, {rightBottomY})], [{thickness}], [{color}], [{fill}], [{strokeDashArray}]";
            return result;
        }


        public IShape ConvertBack(string buffer)
        {

            string pattern = @"(?<=\[)(.*?)(?=\])";

            MatchCollection mc = Regex.Matches(buffer, pattern);

            string pointBuffer = mc[0].ToString();
            string thicknessBuffer = mc[1].ToString();
            string colorBuffer = mc[2].ToString();
            string fillBuffer = mc[3].ToString();
            string strokeDashArrayBuffer = mc[4].ToString();


            int thickness = int.Parse(thicknessBuffer);
            Brush color = (SolidColorBrush)new BrushConverter().ConvertFromString(colorBuffer);
            Brush fill = (SolidColorBrush)new BrushConverter().ConvertFromString(fillBuffer);
            DoubleCollection strokeDashArray = DoubleCollection.Parse(strokeDashArrayBuffer);



            string pointPattern = @"(?<=\()(.*?)(?=\))";

            MatchCollection pointMC = Regex.Matches(pointBuffer, pointPattern);

            string leftTopBuffer = pointMC[0].ToString();
            string rightBottomBuffer = pointMC[1].ToString();

            string[] leftTopTokens = leftTopBuffer.Split(new string[] { "," }, StringSplitOptions.None);
            double leftTopX = double.Parse(leftTopTokens[0]);
            double leftTopY = double.Parse(leftTopTokens[1]);

            string[] rightBottomTokens = rightBottomBuffer.Split(new string[] { "," }, StringSplitOptions.None);
            double rightBottomX = double.Parse(rightBottomTokens[0]);
            double rightBottomY = double.Parse(rightBottomTokens[1]);

            Point2D leftTop = new Point2D(leftTopX, leftTopY);
            Point2D rightBottom = new Point2D(rightBottomX, rightBottomY);

            IShape result = new Ellipse2D(leftTop, rightBottom, thickness, color, fill, strokeDashArray);
            return result;
        }

    }
}
