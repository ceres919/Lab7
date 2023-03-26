using GraphicsEditor.Models.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicsEditor.Models.LoadAndSave
{
    public class XMLSaver : IShapeEntitySaver
    {
        public void Save(ObservableCollection<ShapeEntity> data, string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<ShapeEntity>));

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                formatter.Serialize(stream, data);
            }
        }
    }
}
