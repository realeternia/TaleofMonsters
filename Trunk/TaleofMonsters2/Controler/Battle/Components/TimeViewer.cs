using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;

namespace TaleofMonsters.Controler.Battle.Components
{
    internal partial class TimeViewer : UserControl
    {
        private int time;
        private int round;
        private int weather;
        private int daytime;
        private int special;
        private bool isShow;

        internal TimeViewer()
        {
            InitializeComponent();
        }

        internal void Init()
        {
            time = 32;
            daytime = 1;
            isShow = true;
        }

        internal bool TimeGo() //每一个回合返回true
        {
            if (time++ > 96) time = 0;
            TimeState.Instance.IsNight = (time < 24 || time >= 72);
            daytime = TimeState.Instance.IsNight ? 2 : 1;
            if (time == 24)
            {
                SoundManager.Play("Time", "DaybreakRooster.wav");
            }
            else if (time == 72)
            {
                SoundManager.Play("Time", "DuskWolf.wav");
            }
            if (time % 12 == 0)
            {
                if (MathTool.GetRandom(2) == 0)
                    weather = MathTool.GetRandom(9);
                if (MathTool.GetRandom(4) == 0)
                    special = MathTool.GetRandom(2);
            }
            if (round++ > 30)
            {
                round = 0;
            }
            Invalidate();

            return round == 0;
        }

        private void TimeViewer_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush b1 = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.LightGreen, Color.Green, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(b1, 0, 0, round * Width / 30, 30);
            b1.Dispose();

            Font font = new Font("Arial", 20, FontStyle.Regular);
            e.Graphics.DrawString(string.Format("{0:00}:{1:00}", time / 4, (time % 4) * 15), font, Brushes.White, 22, 0);
            font.Dispose();

            if(isShow)
            {
                string url = string.Format("d{0}.JPG", daytime);
                Image img = PicLoader.Read("Weather", url);
                e.Graphics.DrawImage(img, 6, 35, 30, 30);
                img.Dispose();
                url = string.Format("w{0}.JPG", weather);
                img = PicLoader.Read("Weather", url);
                e.Graphics.DrawImage(img, 41, 35, 30, 30);
                img.Dispose();
                url = string.Format("s{0}.JPG", special);
                img = PicLoader.Read("Weather", url);
                e.Graphics.DrawImage(img, 76, 35, 30, 30);
                img.Dispose();
            }
        }
    }
}
