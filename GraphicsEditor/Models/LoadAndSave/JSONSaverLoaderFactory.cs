using System.IO;

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
