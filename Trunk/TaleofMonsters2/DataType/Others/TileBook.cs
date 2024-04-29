using System;
using System.Drawing;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;

namespace TaleofMonsters.DataType.Others
{
    public static class TileBook
    {
        public static TileMatchResult MatchTile(int id, int mtype, int weakTile)
        {
            int type = ConfigDatas.ConfigData.GetTileConfig(id).Type;

            if (type == 0)
            {
                return TileMatchResult.NotMatch;
            }

            if (mtype == type)
            {
                return TileMatchResult.Enhance;
            }

            int tp = 1 << (type - 1);
            if ((weakTile & tp) == 0)
            {
                return TileMatchResult.Weaken;
            }

            return TileMatchResult.NotMatch;
        }

        static public Image GetTileImage(int id, int width, int height)
        {
            string fname = string.Format("Tiles/{0}{1}x{2}", id, width, height);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("Tiles", string.Format("{0}.JPG", id));
                if (image.Width != width || image.Height != height)
                {
                    image = image.GetThumbnailImage(width, height, null, new IntPtr(0));
                }
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }
    }

    public enum TileMatchResult
    {
        NotMatch, Enhance, Weaken
    }
}
