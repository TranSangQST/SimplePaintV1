using Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace SimplePaint
{
    /// <summary>
    /// Intershape logic for MainWindow.xaml
    /// </summary>

public partial class MainWindow : Fluent.RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        enum ExportTypeEnum : int
        {
            PNG = 0,
            JPEG = 1,
        }

        bool _isDrawing = false;
        bool _isSelection = false;
        int _selectionIndex = -1;

        string _currentSaveFile = "";

        List<IShape> _shapes;
        IShape _preview;
        string _selectedShapeName = "";

        Brush _currentOutlineColor = new SolidColorBrush(Colors.Red);
        Brush _currentFillColor = new SolidColorBrush(Colors.Transparent);
        int _currentStrokeThickness = 1;
        DoubleCollection _currentStrokeDashArray = DoubleCollection.Parse("1 0");


        class StrokeSize
        {
            public StrokeSize()
            {
                Thickness = 1;
            }

            public StrokeSize(int thickness)
            {
                Thickness = thickness;
            }

            public int Thickness { get; set; }
        }

        class StrokeDash
        {
            public StrokeDash()
            {
                StrokeDashArray = DoubleCollection.Parse("1 0");
            }    
                
            public StrokeDash(DoubleCollection strokeDashArray)
            {
                StrokeDashArray = strokeDashArray;
            }

            public DoubleCollection StrokeDashArray { get; set; }
        }    

        List<StrokeSize> _strokeThicknesses = new List<StrokeSize>();
        List<StrokeDash> _strokeDashArray = new List<StrokeDash>();
            




        ShapeFactory _shapeFactory;
        ShapeToStringConverterFactory _shapeToStringConverterFactory;

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = System.IO.Path.GetDirectoryName(exePath);


            string lineDLLPath = folder + "\\" + "Line2D.dll";
            string shapeDLLFilesPath = folder + "\\" + "shapeDllFiles";
            string newLineDLLPath = shapeDLLFilesPath + "\\" + "Line2D.dll";

            if(!Directory.Exists(shapeDLLFilesPath))
            {
                Directory.CreateDirectory(shapeDLLFilesPath);
            }
            if (File.Exists(lineDLLPath))
            {

                File.Copy(lineDLLPath, newLineDLLPath, true);
            }



            _shapeFactory = new ShapeFactory();
            _shapes = new List<IShape>();
            allShapePrototypes = _shapeFactory.GetAllShape();

            _shapeToStringConverterFactory = new ShapeToStringConverterFactory();



            // Tạo ra các nút bấm hàng mẫu
            foreach (var shape in allShapePrototypes)
            {
                var button = new Fluent.Button()
                {
                    Header = shape.Name,
                };


                button.Click += prototypeButton_Click;
                prototypesRbGbox.Items.Add(button);

               
            }

           
            



            //Color redColor = Color.FromRgb(255, 0, 0);
            outlineColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(_currentOutlineColor.ToString());
            fillColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(_currentFillColor.ToString());

            


            _strokeThicknesses.Add(new StrokeSize(1));
            for (int i = 1; i <= 5; i++)
            {
                StrokeSize strokeSize = new StrokeSize(i * 2);
                _strokeThicknesses.Add(strokeSize);
            }
            strokeThicknessGallery.ItemsSource = _strokeThicknesses;
            strokeThicknessGallery.SelectedIndex = 0;


            _strokeDashArray.Add(new StrokeDash(DoubleCollection.Parse("1 0")));
            _strokeDashArray.Add(new StrokeDash(DoubleCollection.Parse("1 1")));
            _strokeDashArray.Add(new StrokeDash(DoubleCollection.Parse("1 2")));
            _strokeDashArray.Add(new StrokeDash(DoubleCollection.Parse("2 1")));
            _strokeDashArray.Add(new StrokeDash(DoubleCollection.Parse("2 2")));

            strokeDashGallery.ItemsSource = _strokeDashArray;
            strokeDashGallery.SelectedIndex = 0;



            _selectedShapeName = allShapePrototypes[0].Name;

            _preview = _shapeFactory.Create(_selectedShapeName);
            _preview.Color = _currentOutlineColor; 
            _preview.Fill = _currentFillColor; 
            _preview.Thickness = _currentStrokeThickness;
            _preview.StrokeDashArray = _currentStrokeDashArray;
            Title = $"{_preview.Name}";

            //UIElement selection = DrawSelection();
            //canvas.Children.Add(selection);


        }
        List<IShape> allShapePrototypes = new List<IShape>();



        private void canvas_MouseDown(object sender,
            MouseButtonEventArgs e)
        {

            if(_isSelection)
            {

                int count = canvas.Children.Count;
                canvas.Children.RemoveAt(count-1);

                //Sinh ra đối tượng mẫu kế
                _preview = _shapeFactory.Create(_selectedShapeName);
                _preview.Color = _currentOutlineColor;
                _preview.Fill = _currentFillColor;
                _preview.Thickness = _currentStrokeThickness;
                _preview.StrokeDashArray = _currentStrokeDashArray;

                _selectionIndex = -1;
                _isSelection = false;
                return;
            }

            _isDrawing = true;

            Point pos = e.GetPosition(canvas);

            _preview.HandleStart(pos.X, pos.Y);
        }


        private void canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (_isSelection)
            {
                return;
            }

            if (_isDrawing)
            {
                Point pos = e.GetPosition(canvas);
                _preview.HandleEnd(pos.X, pos.Y);

                // Xoá hết các hình vẽ cũ
                canvas.Children.Clear();

                // Vẽ lại các hình trước đó
                foreach (var shape in _shapes)
                {
                    UIElement element = shape.Draw();
                    canvas.Children.Add(element);
                }

                // Vẽ hình preview đè lên
                canvas.Children.Add(_preview.Draw());

            }
        }


        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {


            if (_isSelection)
            {
                if (e.OriginalSource is Line)
                {
                    int x = 1;
                }
                if (e.OriginalSource is Rectangle)
                {
                    int y = 1;
                }
                if (e.OriginalSource is Ellipse)
                {
                    int z = 1;
                }
                return;
            }

            //Bắt buộc phải MouseDown rồi (tức _isDrawing = true) thì khi MouseUp mới vẽ
            if (_isDrawing)
            {
                _isDrawing = false;


                // Thêm đối tượng cuối cùng vào mảng quản lí
                Point pos = e.GetPosition(canvas);
                _preview.HandleEnd(pos.X, pos.Y);
                _shapes.Add(_preview);

                // Sinh ra đối tượng mẫu kế
                //_preview = _shapeFactory.Create(_selectedShapeName);
                //_preview.Color = _currentOutlineColor;
                //_preview.Fill = _currentFillColor;
                //_preview.Thickness = _currentStrokeThickness;
                //_preview.StrokeDashArray = _currentStrokeDashArray;


                // Ve lai Xoa toan bo
                canvas.Children.Clear();

                // Ve lai tat ca cac hinh
                foreach (var shape in _shapes)
                {
                    var element = shape.Draw();
                    canvas.Children.Add(element);
                }




                _selectionIndex = _shapes.Count - 1;
                _isSelection = true;

                UIElement selection = _preview.DrawSelection();
                canvas.Children.Add(selection);

                //List<UIElement> selectionX = new List<UIElement>();
                //selectionX.Add(selection);
                //selectionX.Add(selection);

            }
        }


        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }


        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            //File Save chưa tồn tại
            if (_currentSaveFile.Equals(""))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog.Filter = "Binary File (*.bin)|*.bin";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {


                    _currentSaveFile = saveFileDialog.FileName; 

                    string fileDetail = "";
                    foreach (IShape shape in _shapes)
                    {
                        IShapeToStringConverter shapeToStringConverter = _shapeToStringConverterFactory.Create(shape.Name);

                        string shapeInfor = shapeToStringConverter.Convert(shape);
                        fileDetail += shapeInfor + "\n";

                        int debug1 = 1;

                    }

                    FileStream fs = new FileStream(_currentSaveFile, FileMode.OpenOrCreate);
                    BinaryWriter bw = new BinaryWriter(fs);

                    bw.Write(fileDetail);

                    fs.Close();
                    bw.Close();

                    int debug = 1;
                    //System.IO.File.WriteAllText(_currentSaveFile, fileDetail);
                }
            }
            //File Preset đã tồn tại
            else
            {
                string fileDetail = "";
                foreach (IShape shape in _shapes)
                {
                    IShapeToStringConverter shapeToStringConverter = _shapeToStringConverterFactory.Create(shape.Name);

                    string shapeInfor = shapeToStringConverter.Convert(shape);
                    fileDetail += shapeInfor + "\n";

                }

                FileStream fs = new FileStream(_currentSaveFile, FileMode.OpenOrCreate);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(fileDetail);

                fs.Close();
                bw.Close();

                int debug = 1;

                //System.IO.File.WriteAllText(_currentSaveFile, fileDetail);
            }
        }


        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.Filter = "Binary File (*.bin)|*.bin";

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {


               _currentSaveFile = saveFileDialog.FileName;

               string fileDetail = "";
               foreach (IShape shape in _shapes)
               {
                   IShapeToStringConverter shapeToStringConverter = _shapeToStringConverterFactory.Create(shape.Name);

                   string shapeInfor = shapeToStringConverter.Convert(shape);
                   fileDetail += shapeInfor + "\n";

               }

                FileStream fs = new FileStream(_currentSaveFile, FileMode.OpenOrCreate);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(fileDetail);

                int debug = 1;

                fs.Close();
                bw.Close();
                //System.IO.File.WriteAllText(_currentSaveFile, fileDetail);
            }
        }


        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;

                _shapes.Clear();

                FileStream fs = new FileStream(filename, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                string fileDetail = br.ReadString();

                fs.Close();
                br.Close();

                int debug = 1;

                using (var reader = new StringReader(fileDetail))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();

                        if(line == null)
                        {
                            break;
                        }    

                        // Trích xuất từ đầu tiên để biết nên tạo ra luật gì
                        int firstSpaceIndex = line.IndexOf(" ");
                        string firstWord = "";
                        if (firstSpaceIndex > 0)
                        {
                            firstWord = line.Substring(0, firstSpaceIndex);
                        }
                        else
                        {
                            firstWord = line;
                        }

                        IShapeToStringConverter shapeToStringConverter = _shapeToStringConverterFactory.Create(firstWord);
                        IShape shape = shapeToStringConverter.ConvertBack(line);
                        _shapes.Add(shape);
                    }
                }


                //// ve lai xoa toan bo
                canvas.Children.Clear();

                // Ve lai tat ca cac hinh
                foreach (var shape in _shapes)
                {
                    var element = shape.Draw();
                    canvas.Children.Add(element);
                }
            }
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog SaveFileDialog = new SaveFileDialog();

            SaveFileDialog.FileName = "save";        
            SaveFileDialog.DefaultExt = ".png";     
            SaveFileDialog.Filter = "PNG Files|*.png|JPEG Files|*.jpeg";
            //SaveFileDialog.FilterIndex = 0;

            if (SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string fileName = SaveFileDialog.FileName;

                int index = fileName.LastIndexOf('.');

                string extension = "";
                if (index >= 0)
                {

                    int extensionLen = (fileName.Length - 1) - (index + 1) + 1;
                    extension = fileName.Substring(index + 1, extensionLen);
                }

                if (!extension.Equals(""))
                {
                    if (extension.Equals("png"))
                    {
                        SaveControlImage(canvas, SaveFileDialog.FileName, (int)ExportTypeEnum.PNG);
                    }
                    else if (extension.Equals("jpeg"))
                    {
                        SaveControlImage(canvas, SaveFileDialog.FileName, (int)ExportTypeEnum.JPEG);
                    }


                }
            }
        }


        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            //openFileDialog.Multiselect = false;
            //openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";


            //if (openFileDialog.ShowDialog() == true)
            //{
            //    string filename = openFileDialog.FileName;

            //    //Image2D shape = (Image2D) _shapeFactory.Create("Image2D");
               
                

            //}

        }
       

        //Tham khảo từ:
        //http://csharphelper.com/blog/2015/10/save-wpf-control-images-in-c/
        //http://csharphelper.com/blog/2019/05/make-an-image-containing-shadowed-text-in-wpf-and-c/?fbclid=IwAR0fK_D4NfgXwVusfsDR4cwNHIkZSERPi0sjODDUrZZ4IUobrqwY0gJIdOs
        // Save a control's image.
        private void SaveControlImage(FrameworkElement control, string filename, int exportType = 0)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(control);

            DrawingVisual dv = new DrawingVisual();

            // Fill a rectangle the same size as the control
            // with a brush containing images of the control.
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(control);
                ctx.DrawRectangle(brush, null, new Rect(rect.Size));
            }

            int width = (int)control.ActualWidth;
            int height = (int)control.ActualHeight;
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                width, height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);

            // Make a PNG encoder.

            if(exportType == (int)ExportTypeEnum.PNG)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(rtb));

                // Save the file.
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    encoder.Save(fs);
                }
                return;
            }
            else if (exportType == (int)ExportTypeEnum.JPEG)
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();


                encoder.Frames.Add(BitmapFrame.Create(rtb));

                // Save the file.
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    encoder.Save(fs);
                }
                return;
            }


        }

        private void prototypeButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedShapeName = (sender as Fluent.Button).Header as string;

            _preview = _shapeFactory.Create(_selectedShapeName);
            _preview.Color = _currentOutlineColor;
            _preview.Fill = _currentFillColor;
            _preview.Thickness = _currentStrokeThickness;
            _preview.StrokeDashArray = _currentStrokeDashArray;


            Title = $"{_preview.Name}";
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {

            //if (_isSelection)
            //{
            //    _isSelection = false;
            //}
            //else
            //{
            //    _isSelection = true;
            //}
        }


        private void outlineColorPicker_SelectedColorChanged(object sender, RoutedEventArgs e)
        {

            Color currentOutlineColorPicker = (Color) outlineColorPicker.SelectedColor;
            _currentOutlineColor = new SolidColorBrush(currentOutlineColorPicker);


            if(_isSelection)
            {
                if(_selectionIndex >= 0)
                {
                    IShape selectedShape = _shapes[_selectionIndex];
                    selectedShape.Color = _currentOutlineColor;


                    // Ve lai Xoa toan bo
                    canvas.Children.Clear();

                    // Ve lai tat ca cac hinh
                    foreach (var shape in _shapes)
                    {
                        var element = shape.Draw();
                        canvas.Children.Add(element);
                    }

                    UIElement selection = selectedShape.DrawSelection();
                    canvas.Children.Add(selection);



                }    
                return;
            }    


            if(_preview != null)
            {
                _preview.Color = _currentOutlineColor;
            }    
            

            //outlineColor = (Color)colorGallery.SelectedColor;
            //if (controlSelector != null && controlSelector.itemChoose != null)
            //{
            //    controlSelector.itemChoose.ColorStroke = outlineColor;
            //}
        }

        private void fillColorPicker_SelectedColorChanged(object sender, RoutedEventArgs e)
        {
            Color currentFillColorPicker = (Color)fillColorPicker.SelectedColor;
            _currentFillColor = new SolidColorBrush(currentFillColorPicker);


            if (_isSelection)
            {
                if (_selectionIndex >= 0)
                {
                    IShape selectedShape = _shapes[_selectionIndex];
                    selectedShape.Fill = _currentFillColor;


                    // Ve lai Xoa toan bo
                    canvas.Children.Clear();

                    // Ve lai tat ca cac hinh
                    foreach (var shape in _shapes)
                    {
                        var element = shape.Draw();
                        canvas.Children.Add(element);
                    }

                    UIElement selection = selectedShape.DrawSelection();
                    canvas.Children.Add(selection);



                }
                return;
            }


            if (_preview != null)
            {
                _preview.Fill = _currentFillColor;
            }


            //fillColor = (Color)colorGallery.SelectedColor;
            //if (controlSelector != null && controlSelector.itemChoose != null)
            //{
            //    controlSelector.itemChoose.ColorStroke = fillColor;
            //}
        }

        private void strokeThicknessGallery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = strokeThicknessGallery.SelectedIndex;
            _currentStrokeThickness = _strokeThicknesses[index].Thickness;

            if (_preview != null)
            {
                _preview.Thickness = _currentStrokeThickness;
            }
        }


        private void strokeDashGallery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = strokeDashGallery.SelectedIndex;
            _currentStrokeDashArray = _strokeDashArray[index].StrokeDashArray;

            if (_preview != null)
            {
                _preview.StrokeDashArray = _currentStrokeDashArray;
            }
        }


    }

}

