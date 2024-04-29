using System.Drawing;
using TaleofMonsters.Controler.Loader;

namespace TaleofMonsters.DataType.Scenes.SceneObjects
{
    class SceneObject
    {
        protected int id;
        protected int x;
        protected int y;
        protected string name;
        protected string figue;

        public int Id
        {
            get { return id; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public string Name
        {
            get { return name; }
        }

        public virtual string Figue
        {
            get { return figue; }
        }

        public virtual void Draw(Graphics g, int target)
        {
            Font font = new Font("Î¢ÈíÑÅºÚ", 12, FontStyle.Bold);
            Image head = PicLoader.Read("NPC", string.Format("{0}.PNG", Figue));
            int ty = y + 50;
            if (target == id)
            {
                g.DrawImage(head, x - 5, ty - 5, 70, 70);
                g.DrawString(name, font, Brushes.Black, x + 5, ty + 49);
                g.DrawString(name, font, Brushes.Yellow, x + 2, ty + 47);
            }
            else
            {
                g.DrawImage(head, x, ty, 60, 60);
                g.DrawString(name, font, Brushes.Black, x + 3, ty + 47);
                g.DrawString(name, font, Brushes.White, x, ty + 45);
            }
            head.Dispose();
            font.Dispose();
        }

        public virtual void CheckClick()
        {
        }
    }
}
