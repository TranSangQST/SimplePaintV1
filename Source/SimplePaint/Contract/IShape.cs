using System;
using System.Windows;
using System.Windows.Media;

namespace Contract
{
    public interface IShape: ICloneable
    {
        string Name { get; }
        int Thickness { get; set; }
        Brush Color { get; set; }
        Brush Fill { get; set; }
        DoubleCollection StrokeDashArray { get; set; }

        void HandleStart(double x, double y);
        void HandleEnd(double x, double y);
        UIElement Draw();

        UIElement DrawSelection();

        IShape Next();

        Object Clone();

        
    }
}
