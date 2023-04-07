using Avalonia.Controls;
using Avalonia.Media;
using GraphicsEditor.Models;
using GraphicsEditor.Models.LoadAndSave;
using GraphicsEditor.Models.Shapes;
using GraphicsEditor.Views;
using GraphicsEditor.Views.ShapesPages;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace GraphicsEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindow mainWindow;
        private UserControl shapeContent;
        public ShapesCollection list;
        public ShapeCreator creator;
        public IEnumerable<ISaverLoaderFactory> SaverLoaderFactoryCollection { get; set; }

        private ShapeEntity selectedEntity;
        private int selectedShapeIndex = 0;
        private ObservableCollection<ISolidColorBrush> coloredBrush;
        private ISolidColorBrush shapeStrokeColor, shapeFillColor;
        private string shapeName, shapeCommandPath, shapeStartPoint, shapeEndPoint, shapePoints, shapeAngleCenter, shapeScaleTransform, shapeSkewTransform;
        private double shapeHeight, shapeWidth, shapeStrokeThickness, shapeAngle;

        public ObservableCollection<UserControl> shapesPagesCollection = new() 
        { 
            new StraightLineShapePage(),
            new PolyLineShapePage(),
            new PolygonShapePage(),
            new RectangleShapePage(),
            new EllipsShapePage(),
            new PathShapePage()
        };

        public MainWindowViewModel(Window window) 
        {
            UserControl shape = shapesPagesCollection.ElementAt(SelectedShapeIndex);
            ShapeContent = shape;
            list = new ShapesCollection();
            mainWindow = (MainWindow)window;

            coloredBrush = new ObservableCollection<ISolidColorBrush>(
                typeof(Brushes)
                .GetProperties()
                .Select(propertyInfo => (ISolidColorBrush)propertyInfo.GetValue(propertyInfo))
                );

            ShapeStartPoint = "";
            ShapeStrokeColor = ShapeFillColor = ColoredBrush[0];
            ShapeStrokeThickness = 1;

            AddButton = ReactiveCommand.Create(() => { AddShape(); });
            ClearButton = ReactiveCommand.Create(() => { Clear(); });
            DeleteButton = ReactiveCommand.Create<ShapeEntity>(DeleteShape);
            ImportButton = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.OpenFileDialogMenu(param);
            });
            ExportButton = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.SaveFileDialogMenu(param);
            });
            Avalonia.Controls.Shapes.Rectangle rect = new Avalonia.Controls.Shapes.Rectangle();
        }

        public void LoadShapes(string path)
        {
            list.shapeList.Clear();

            var shapeLoader = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateLoader();

            if (shapeLoader != null)
            {
                var newList = new ObservableCollection<ShapeEntity>(shapeLoader.Load(path));
                foreach (var shape in newList)
                {
                    ShapeCreator.Load(shape, list);
                }
            }
        }
        public void SaveShapes(string path, string parametr, Canvas canvas)
        {
            if (parametr != "png")
            {
                var shapeSaver = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateSaver();

                if (shapeSaver != null)
                {
                    shapeSaver.Save(ShapeList, path);
                }
            }
            else
            {
                var shapeSaver = new PNGSaver();
                shapeSaver.Save(canvas, path);
            }
        }

        public void AddShape()
        {
            ShapeCreator newCreator = new ShapeCreator(this);
            newCreator.Create(SelectedShapeIndex, list);
        }
        public void DeleteShape(ShapeEntity item)
        {
            list.DeleteItem(item);
        }
        public void Clear()
        {
            ShapeName = "";
            ShapeStrokeColor = ShapeFillColor = ColoredBrush[0];
            ShapeStrokeThickness = 1;
            ShapeStartPoint = "";
            ShapeEndPoint = "";
            ShapePoints = "";
            ShapeWidth = 0;
            ShapeHeight = 0;
            ShapeCommandPath = "";
            ShapeAngle = 0;
            ShapeAngleCenter = "";
            ShapeScaleTransform = "";
            ShapeSkewTransform = "";
        }
        public void CurrentShapeContent(ShapeEntity item)
        {
            ShapeCreator creator = new ShapeCreator();
            var index = creator.ListIndexOfCurrentShape(item);
            ShapeContent = shapesPagesCollection.ElementAt(index);
            SelectedShapeIndex = index;
            item.SetPropertiesOfCurrentShape(this);
        }

        public string OpenFileName { get; set; }
        public ShapeEntity SelectedEntity 
        { 
            get =>selectedEntity;
            set 
            {
                this.RaiseAndSetIfChanged(ref selectedEntity, value);
                if (selectedEntity != null)
                {
                    CurrentShapeContent(selectedEntity);
                }
            }
        }
        public ObservableCollection<ShapeEntity> ShapeList
        { 
            get => list.shapeList;
            set=> this.RaiseAndSetIfChanged(ref list.shapeList, value);
        }
        public int SelectedShapeIndex 
        { 
            get => selectedShapeIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedShapeIndex, value);
                Clear();
                ShapeContent = shapesPagesCollection.ElementAt(SelectedShapeIndex);
            } 
        }
        public UserControl ShapeContent
        {
            get => shapeContent;
            set
            {
                this.RaiseAndSetIfChanged(ref shapeContent, value);
            }
        }

        public ObservableCollection<ISolidColorBrush> ColoredBrush
        {
            get => coloredBrush;
            set => this.RaiseAndSetIfChanged(ref coloredBrush, value);
        }
        public ISolidColorBrush ShapeStrokeColor
        {
            get => shapeStrokeColor;
            set => this.RaiseAndSetIfChanged(ref shapeStrokeColor, value);
        }
        public ISolidColorBrush ShapeFillColor
        {
            get => shapeFillColor;
            set => this.RaiseAndSetIfChanged(ref shapeFillColor, value);
        }
        public string ShapeName 
        {
            get => shapeName; 
            set=>this.RaiseAndSetIfChanged(ref shapeName, value); 
        }
        public string ShapeStartPoint 
        {
            get => shapeStartPoint;
            set => this.RaiseAndSetIfChanged(ref shapeStartPoint, value);
        }
        public string ShapeEndPoint 
        {
            get => shapeEndPoint;
            set => this.RaiseAndSetIfChanged(ref shapeEndPoint, value);
        }
        public string ShapePoints 
        { 
            get => shapePoints;
            set => this.RaiseAndSetIfChanged(ref shapePoints, value);
        }
        public double ShapeWidth
        {
            get => shapeWidth;
            set => this.RaiseAndSetIfChanged(ref shapeWidth, value);
        }
        public double ShapeHeight
        { 
            get => shapeHeight;
            set => this.RaiseAndSetIfChanged(ref shapeHeight, value); 
        }
        public double ShapeStrokeThickness
        { 
            get => shapeStrokeThickness; 
            set => this.RaiseAndSetIfChanged(ref shapeStrokeThickness, value); 
        }
        public string ShapeCommandPath
        { 
            get => shapeCommandPath; 
            set => this.RaiseAndSetIfChanged(ref shapeCommandPath, value); 
        }
        public double ShapeAngle
        { 
            get => shapeAngle; 
            set => this.RaiseAndSetIfChanged(ref shapeAngle, value); 
        }
        public string ShapeAngleCenter
        { 
            get => shapeAngleCenter;
            set => this.RaiseAndSetIfChanged(ref shapeAngleCenter, value);
        }
        public string ShapeScaleTransform
        { 
            get => shapeScaleTransform;
            set => this.RaiseAndSetIfChanged(ref shapeScaleTransform, value);
        }
        public string ShapeSkewTransform
        { get => shapeSkewTransform;
            set => this.RaiseAndSetIfChanged(ref shapeSkewTransform, value);
        }

        public ReactiveCommand<string, Unit> ImportButton { get; }
        public ReactiveCommand<string, Unit> ExportButton { get; }

        public ReactiveCommand<Unit, Unit> AddButton { get; }
        public ReactiveCommand<Unit, Unit> ClearButton { get; }
        public ReactiveCommand<ShapeEntity, Unit> DeleteButton { get; }
    }
}