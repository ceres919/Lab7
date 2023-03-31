using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using GraphicsEditor.Models.LoadAndSave;
using GraphicsEditor.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace GraphicsEditor.Views
{
    public partial class MainWindow : Window
    {
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;
        private TranslateTransform changedPoints;
        private TransformGroup trGpoup;
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
            AddHandler(DragDrop.DropEvent, CanvasDragEnter);
            AddHandler(DragDrop.DropEvent, CanvasDrop);
            AddHandler(Canvas.PointerPressedEvent, ShapePointerEnter);
            AddHandler(Canvas.PointerMovedEvent, ShapeMovedEvent);
            AddHandler(Canvas.PointerReleasedEvent, ShapeReleasedEvent);
        }

        public async void OpenFileDialogMenu(string parametr)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            switch (parametr)
            {
                //case "png":
                //    openFileDialog.Filters.Add(
                //        new FileDialogFilter
                //        {
                //            Name = "PNG files",
                //            Extensions = new string[] { "png" }.ToList()
                //        });
                //    break;
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
                    dataContext.SaveShapes(result, parametr);
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
            //Image img = new Image();
            //img.Source = new Bitmap(imagePath.ElementAt(0));
            //canvas.Children.Add(img);
        }
        public void ShapePointerEnter(object sender,  PointerEventArgs pointerEventArgs)
        {
            
            var draggableControl = pointerEventArgs.Pointer.Captured as Shape;
            if (draggableControl == null )
            {
                return;
            }
            if (pointerEventArgs.GetCurrentPoint(this).Properties.IsLeftButtonPressed && pointerEventArgs.Source.InteractiveParent is Canvas)
            {
                originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                changedPoints = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                trGpoup = draggableControl.RenderTransform as TransformGroup ?? new TransformGroup();
                isDragging = true;
                clickPosition = pointerEventArgs.GetPosition(canvas);
                if (DataContext is MainWindowViewModel dataContext)
                {
                    var item = dataContext.list.AfterMoveChange(draggableControl.Name, 0, 0);
                    dataContext.CurrentShapeContent(item);
                }
            }
            
        }
        public void ShapeMovedEvent(object sender, PointerEventArgs pointerEventArgs)
        {
            var draggableControl = pointerEventArgs.Pointer.Captured as Shape;
            if (isDragging && draggableControl != null)
            {
                draggableControl.RenderTransform = null;
                Point currentPosition = pointerEventArgs.GetPosition(canvas);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                changedPoints.X = transform.X;
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);
                changedPoints.Y = transform.Y;

                if (trGpoup.Children.Count > 0)
                {
                    TransformGroup newGroup = new TransformGroup();
                    newGroup.Children.Add(new TranslateTransform(transform.X, transform.Y));
                    foreach (var item in trGpoup.Children)
                    {
                        newGroup.Children.Add(item);
                    }
                    draggableControl.RenderTransform = newGroup;
                }
                else
                {
                    draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
                }
            }
        }
        public void ShapeReleasedEvent(object sender, PointerEventArgs pointerEventArgs)
        {
            isDragging = false;
            var draggableControl = pointerEventArgs.Pointer.Captured as Shape;
            
            if (draggableControl != null)
            {
                if (DataContext is MainWindowViewModel dataContext)
                {
                    draggableControl.RenderTransform = null;
                    if (trGpoup.Children.Count > 0)
                    {
                        draggableControl.RenderTransform = trGpoup;
                    }
                    var item = dataContext.list.AfterMoveChange(draggableControl.Name, changedPoints.X, changedPoints.Y);
                    dataContext.CurrentShapeContent(item); 
                }
            }
        }
    }
}