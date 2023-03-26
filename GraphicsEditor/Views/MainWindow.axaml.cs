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

namespace GraphicsEditor.Views
{
    public partial class MainWindow : Window
    {
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;
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
            List<string> imagePath = (List<string>)dragEventArgs.Data.Get(DataFormats.FileNames);
            Image img = new Image();
            img.Source = new Bitmap(imagePath.ElementAt(0));
            canvas.Children.Add(img);
        }
        public void ShapePointerEnter(object sender,  PointerEventArgs pointerEventArgs)
        {
            var draggableControl = pointerEventArgs.Device.Captured as Shape;
            if (draggableControl == null)
            {
                return;
            }
            originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            isDragging = true;
            clickPosition = pointerEventArgs.GetPosition(this);
        }
        public void ShapeMovedEvent(object sender, PointerEventArgs pointerEventArgs)
        {
            var draggableControl = pointerEventArgs.Device.Captured as Shape;
            if (isDragging && draggableControl != null)
            {
                Point currentPosition = pointerEventArgs.GetPosition(this);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);
                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
            }
        }
        public void ShapeReleasedEvent(object sender, PointerEventArgs pointerEventArgs)
        {
            var draggableControl = pointerEventArgs.Device.Captured as Shape;
            isDragging = false;
        }
    }
}