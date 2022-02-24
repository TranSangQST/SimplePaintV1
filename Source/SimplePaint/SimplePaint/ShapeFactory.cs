using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaint
{

    public class ShapeFactory
    {
        Dictionary<string, IShape> _prototypes = null; // Prototype

        public ShapeFactory()
        {

            _prototypes = new Dictionary<string, IShape>();
            // Nạp danh sách các tập tin dll
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath) + "//" + "shapeDllFiles";
            //string folder = Path.GetDirectoryName(exePath);


            var fis = new DirectoryInfo(folder).GetFiles("*.dll");

            foreach (var f in fis) // Lần lượt duyệt qua các file dll
            {

                string fname = f.Name;
                //if (fname.Equals("fluent.dll"))
                //{
                //    continue;
                //} 
                    

                var assembly = Assembly.LoadFile(f.FullName);
                var types = assembly.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsClass && typeof(IShape).IsAssignableFrom(t))
                    {
                        IShape shape = (IShape)Activator.CreateInstance(t);
                        _prototypes.Add(shape.Name, shape);
                    }
                }
                int debug1 = 1;
            }
            int debug2 = 2;
        }

        public IShape Create(string choice)
        {
            IShape shape = _prototypes[choice].Next();

            return shape;
        }

        public List<IShape> GetAllShape()
        {
            return _prototypes.Values.ToList();
        }


    }
}
