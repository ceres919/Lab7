using GraphicsEditor.Models.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicsEditor.Models.LoadAndSave
{
    public class XMLLoader : IShapeEntityLoader
    {
        public IEnumerable<ShapeEntity> Load(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<ShapeEntity>));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ObservableCollection<ShapeEntity>? newList = formatter.Deserialize(fs) as ObservableCollection<ShapeEntity>;
                if (newList == null)
                {
                    newList = new ObservableCollection<ShapeEntity>();
                }
                return newList;
            }
        }
    }
}
