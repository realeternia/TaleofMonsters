using System.Drawing;

namespace TaleofMonsters.Controler.Battle.Data.MemFlow
{
    internal class FlowWord
    {
        protected string word;
        protected Font font;
        protected int speedY;
        protected int speedX;
        protected int size;
        protected Color color;
        protected Point startPoint;
        public Point position;
        protected int duration;

        public bool IsFinished
        {
            get
            {
                duration--;
                position = new Point(position.X + speedX, position.Y - speedY);
                return duration <= 0;
            }
        }

        public FlowWord(string word, Point point, string color)
            : this(word, point, 0, color, 0, 0, 0, 3, 15)
        {
        }

        public FlowWord(string word, Point point, int size, string color, int offX, int offY)
            : this(word, new Point(point.X + offX, point.Y + offY), 0, color, offX, offY, 0, 3, 15)
        {
        }

        public FlowWord(string word, Point point, int size, string color, int offX, int offY, int speedX, int speedY, int duration)
        {
            this.word = word;
            this.size = size + 14;
            this.font = new Font("΢���ź�", this.size, FontStyle.Bold);
            this.color = Color.FromName(color);
            this.speedX = speedX;
            this.speedY = speedY;
            startPoint = new Point(point.X + offX, point.Y + offY);
            position = startPoint;
            this.duration = duration;
            if (startPoint.X> 800)
            {
                startPoint.X = 800;
            }
        }

        internal virtual void Draw(Graphics g)
        {
            using(Brush brush = new SolidBrush(color))
            {
                g.DrawString(word, font, brush, position.X, position.Y);
            }
        }
    }
}
