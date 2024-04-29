using System;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace TaleofMonsters.Controler.Loader
{
    class PicLoader
    {
        static public Image Read(String dir, String path)
        {
            Bitmap bmp = null;
            try
            {
                Assembly myAssembly = Assembly.LoadFrom("PicResource.dll");
                Stream myStream = myAssembly.GetManifestResourceStream(String.Format("PicResource.{0}.{1}", dir, path));
                if (myStream != null) bmp = new Bitmap(myStream);
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }
    }
}
