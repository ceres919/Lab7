using System;

namespace GraphicsEditor.Models.LoadAndSave
{
    public interface ISaverLoaderFactory
    {
        IShapeEntityLoader CreateLoader();
        IShapeEntitySaver CreateSaver();
        bool IsMatch(string path);
    }
}
