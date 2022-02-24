using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IShapeToStringConverter
    {
        string Name { get; }
        public string Convert(IShape shape);
        public IShape ConvertBack(string buffer);
    }

}
