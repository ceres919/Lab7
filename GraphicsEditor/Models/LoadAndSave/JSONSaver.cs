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
    public class JSONSaver : IShapeEntitySaver
    {
        public void Save(ObservableCollection<ShapeEntity> shapes, string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, shapes);
            }
        }
    }
}
