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
