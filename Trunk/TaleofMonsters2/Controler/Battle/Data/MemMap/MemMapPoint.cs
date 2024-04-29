using System.Drawing;

namespace TaleofMonsters.Controler.Battle.Data.MemMap
{
    internal class MemMapPoint
    {
        private int xid;
        private int x;
        private int y;
        private int tile;
        private int sideIndex;
        private int owner;

        public MemMapPoint(int xid, int x, int y, int total, int tile)
        {
            this.xid = xid;
            this.x = x;
            this.y = y;
            if (xid <= 4)
            {
                sideIndex = xid;
            }
            else if (xid > total - 5)
            {
                sideIndex = total - xid - 1;
            }
            else
            {
                sideIndex = 99;
            }
            this.tile = tile;
            owner = 0;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public bool IsLeft
        {
            get { return xid <= 4; }
        }

        public int Tile
        {
            get { return tile; }
            set { tile = value; }
        }

        public int SideIndex
        {
            get { return sideIndex; }
        }

        public int Owner
        {
            get { return owner; }
        }

        public void UpdateOwner(int id)
        {
            owner = id;
        }

        public Point ToPoint()
        {
            return new Point(x, y);
        }

        public override string ToString()
        {
            return string.Format("x={0},y={1},tile={2}", x, y, tile);
        }
    }
}
