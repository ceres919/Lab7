using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.LoadAndSave
{
    internal class JSONSaverLoaderFactory : ISaverLoaderFactory
    {
        public IShapeEntityLoader CreateLoader()
        {
            return new JSONLoader();
        }

        public IShapeEntitySaver CreateSaver()
        {
            return new JSONSaver();
        }

        public bool IsMatch(string path)
        {
            return ".json".Equals(Path.GetExtension(path));
        }
    }
}
