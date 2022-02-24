using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class Point2DToStringConverter : IShapeToStringConverter
    {
        public string Name
        {
            get => "Point2D";
        }    

        public string Convert(IShape shape)
        {

            Point2D point2D = (Point2D)shape;

            string name = point2D.Name;
            double x = point2D.X;
            double y = point2D.Y;

            int thickness = point2D.Thickness;
            string color = point2D.Color.ToString();
            string strokeDashArray = point2D.StrokeDashArray.ToString();

            string result = $"{name} [({x}, {y})], [{thickness}], [{color}], [{strokeDashArray}]";
            return result;
        }
        public IShape ConvertBack(string buffer)
        {
            Point2D point2D = new Point2D();
            return point2D;
        }    
    }
}
