using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Models.LoadAndSave
{
    public interface ISaverLoaderFactory
    {
        IShapeEntityLoader CreateLoader();
        IShapeEntitySaver CreateSaver();
        bool IsMatch(string path);
    }
}
