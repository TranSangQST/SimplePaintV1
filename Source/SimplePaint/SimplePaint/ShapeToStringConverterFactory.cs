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
    public class ShapeToStringConverterFactory
    {
        Dictionary<string, IShapeToStringConverter> _prototypes = null; // Prototype

        public ShapeToStringConverterFactory()
        {

            _prototypes = new Dictionary<string, IShapeToStringConverter>();
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
                    if (t.IsClass && typeof(IShapeToStringConverter).IsAssignableFrom(t))
                    {
                        IShapeToStringConverter shapeToStringConverter = (IShapeToStringConverter)Activator.CreateInstance(t);
                        _prototypes.Add(shapeToStringConverter.Name, shapeToStringConverter);
                    }
                }
                int debug1 = 1;
            }
            int debug2 = 2;
        }

        public IShapeToStringConverter Create(string choice)
        {
            IShapeToStringConverter shapeToStringConverter = _prototypes[choice];

            return shapeToStringConverter;
        }

        public List<IShapeToStringConverter> GetAllConverter()
        {
            return _prototypes.Values.ToList();
        }


    }

}
