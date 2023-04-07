using GraphicsEditor.Models.Shapes;
using System.Collections.ObjectModel;
using System.IO;
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
