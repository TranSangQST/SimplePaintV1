using Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Line2D
{
    public class Line2DToStringConverter : IShapeToStringConverter
    {
        public string Name
        {
            get => "Line2D";
        }

        public string Convert(IShape shape)
        {
            //Ex: Line2D [(2, 2), (1, 1)], [1], [#FFFF0000], [1, 0]

            Line2D line2D = (Line2D)shape;

            string name = line2D.Name;
            double startX = line2D.Start.X;
            double endX = line2D.End.X;
            double startY = line2D.Start.Y;
            double endY = line2D.End.Y;

            int thickness = line2D.Thickness;
            string color = line2D.Color.ToString();
            string fill = line2D.Fill.ToString();
            string strokeDashArray = line2D.StrokeDashArray.ToString();

            string result = $"{name} [({startX}, {startY}), ({endX}, {endY})], [{thickness}], [{color}], [{fill}], [{strokeDashArray}]";
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

            string startBuffer = pointMC[0].ToString();
            string endBuffer = pointMC[1].ToString();

            string[] startTokens = startBuffer.Split(new string[] { "," }, StringSplitOptions.None);
            double startX = double.Parse(startTokens[0]);
            double startY = double.Parse(startTokens[1]);

            string[] endTokens = endBuffer.Split(new string[] { "," }, StringSplitOptions.None);
            double endX = double.Parse(endTokens[0]);
            double endY = double.Parse(endTokens[1]);

            Point2D start = new Point2D(startX, startY);
            Point2D end = new Point2D(endX, endY);

            IShape result = new Line2D(start, end, thickness, color, fill, strokeDashArray);
            return result;
        }
    }
}
