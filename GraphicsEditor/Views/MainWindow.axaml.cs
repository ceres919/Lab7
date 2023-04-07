using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.VisualTree;
using GraphicsEditor.Models.LoadAndSave;
using GraphicsEditor.Models.Shapes;
using GraphicsEditor.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsEditor.Views
{
    public partial class MainWindow : Window
    {
        protected bool isDragging;
        private Canvas canv;
        private Point clickPosition;
        private Point oldClickPosition;
        private Point pointerPositionIntoShape;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this)
            {
                SaverLoaderFactoryCollection = new ISaverLoaderFactory[]
                    {
                        new XMLSaverLoaderFactory(),
                        new JSONSaverLoaderFactory(),
                    },
            };
            AddHandler(DragDrop.DragEnterEvent, CanvasDragEnter);
            AddHandler(DragDrop.DropEvent, CanvasDrop);
            AddHandler(PointerPressedEvent, ShapePointerPressedEvent);
            AddHandler(PointerMovedEvent, ShapeMovedEvent);
            AddHandler(PointerReleasedEvent, ShapeReleasedEvent);
            
        }

        public async void OpenFileDialogMenu(string parametr)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            switch (parametr)
            {
                case "xml":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "XML files",
                            Extensions = new string[] { "xml" }.ToList()
                        });
                    break;
                case "json":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
                    break;
            }
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    dataContext.LoadShapes(result[0]);
                }
            }
        }

        public async void SaveFileDialogMenu(string parametr)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            switch (parametr)
            {
                case "png":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "PNG files",
                            Extensions = new string[] { "png" }.ToList()
                        });
                    break;
                case "xml":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "XML files",
                            Extensions = new string[] { "xml" }.ToList()
                        });
                    break;
                case "json":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
                    break;
            }
            string? result = await saveFileDialog.ShowAsync(this);

            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    canv = this.GetVisualDescendants().OfType<Canvas>().FirstOrDefault();
                    dataContext.SaveShapes(result, parametr, canv);
                }
            }
        }

        public void CanvasDragEnter(object sender, DragEventArgs dragEventArgs)
        {
            dragEventArgs.DragEffects = DragDropEffects.Copy;
        }
        public void CanvasDrop(object sender, DragEventArgs dragEventArgs)
        {
            List<string> path = (List<string>)dragEventArgs.Data.Get(DataFormats.FileNames);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (path != null)
                {
                    dataContext.LoadShapes(path.ElementAt(0));
                }
            }
        }

        public void ShapePointerPressedEvent(object sender,  PointerEventArgs pointerEventArgs)
        {
            if (pointerEventArgs.Source is Shape draggableShape)
            {
                if (pointerEventArgs.GetCurrentPoint(this).Properties.IsLeftButtonPressed && pointerEventArgs.Source.InteractiveParent is ContentPresenter && draggableShape.Name != null)
                {
                    isDragging = true;
                    pointerPositionIntoShape = pointerEventArgs.GetPosition(draggableShape);
                    canv = this.GetVisualDescendants().OfType<Canvas>().FirstOrDefault();
                    clickPosition = pointerEventArgs.GetPosition(canv);
                    oldClickPosition = clickPosition;

                    if (DataContext is MainWindowViewModel dataContext)
                    {
                        var item = dataContext.ShapeList.First(p => p.Name == draggableShape.Name);
                        dataContext.CurrentShapeContent(item);
                    }
                }
            }
            
        }
        public void ShapeMovedEvent(object sender, PointerEventArgs pointerEventArgs)
        {
            if (isDragging && pointerEventArgs.Source is Shape draggableShape && pointerEventArgs.Source.InteractiveParent is ContentPresenter)
            {
                Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());
                if (DataContext is MainWindowViewModel dataContext)
                {
                    var item = dataContext.ShapeList.First(p => p.Name == draggableShape.Name);
                    var type = item.GetType();
                    if(type == typeof(RectangleShape) || type == typeof(EllipseShape) || type == typeof(PathShape))
                    {
                        var x = currentPointerPosition.X - (int)pointerPositionIntoShape.X;
                        var y = currentPointerPosition.Y - (int)pointerPositionIntoShape.Y;
                        item.Change(x, y);
                    }
                    else
                    {
                        var x = currentPointerPosition.X - oldClickPosition.X;
                        var y = currentPointerPosition.Y - oldClickPosition.Y;
                        oldClickPosition = currentPointerPosition;
                        item.Change(x, y);
                    }
                    item.SetPropertiesOfCurrentShape(dataContext);
                }
            }
        }
        public void ShapeReleasedEvent(object sender, PointerEventArgs pointerEventArgs)
        {
            isDragging = false;
            
        }
    }
}