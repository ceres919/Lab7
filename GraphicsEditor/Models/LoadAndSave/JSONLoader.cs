using GraphicsEditor.Models.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

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
