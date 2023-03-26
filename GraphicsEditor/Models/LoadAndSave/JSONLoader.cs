using Avalonia.Controls.Shapes;
using GraphicsEditor.Models.Shapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.LoadAndSave
{
    public class JSONLoader : IShapeEntityLoader
    {
        public IEnumerable<ShapeEntity> Load(string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling= Newtonsoft.Json.TypeNameHandling.Auto;
            using (StreamReader file = File.OpenText(path))
            {
                ObservableCollection<ShapeEntity>? shapes = (ObservableCollection<ShapeEntity>)serializer.Deserialize(file, typeof(ObservableCollection<ShapeEntity>));
                return shapes;
            }
        }
    }
}
