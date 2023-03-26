using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace GraphicsEditor.Models.LoadAndSave
{
    internal class XMLSaverLoaderFactory : ISaverLoaderFactory
    {
        public IShapeEntityLoader CreateLoader()
        {
            return new XMLLoader();
        }

        public IShapeEntitySaver CreateSaver()
        {
            return new XMLSaver();
        }

        public bool IsMatch(string path)
        {
            return ".xml".Equals(Path.GetExtension(path));
        }
    }
}
