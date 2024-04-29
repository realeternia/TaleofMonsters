using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Cards;

namespace TaleofMonsters.Controler.Battle.Components
{
    internal sealed partial class LifeClock : UserControl
    {
        internal delegate void LifeClockFigueEventHandler();
        internal event LifeClockFigueEventHandler HeadClicked;

        private Player player;
        private Image back;
        private Image head;
        private string nameStr;
        private bool isLeft;

        #region 属性
        internal Player Player
        {
            get { return player; }
            set
            {
                player = value;
                if (player != null)
                {
                    player.AngerChanged += new Player.PlayerPointEventHandler(player_LifeChanged);
                    player.ManaChanged += new Player.PlayerPointEventHandler(player_ManaChanged);
                    player.ExtraChanged +=new Player.PlayerPointEventHandler(player_ExtraChanged);
                }
                Invalidate();
            }
        }

        internal bool IsLeft
        {
            get { return isLeft; }
            set { isLeft = value; }
        }
        #endregion

        internal LifeClock()
        {
            InitializeComponent();
            DoubleBuffered = true;
            back = PicLoader.Read("System", "LifeBack.JPG");
            head = PicLoader.Read("People", "0.PNG");
        }

        internal void SetPlayer(string name, int headid)
        {
            nameStr = name;
			if(head != null)
				head.Dispose();
            head = PicLoader.Read("Player", string.Format("{0}.PNG", headid));
            Invalidate();
        }

        internal void SetPlayer(int id)
        {
            if (id <= 0)
				return;

			PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(id);
            nameStr = peopleConfig.Name;
			if(head != null)
				head.Dispose();
			head = PicLoader.Read("People", string.Format("{0}.PNG", peopleConfig.Figue));
			Invalidate();
        }

        private void player_LifeChanged()
        {
            Invalidate(new Rectangle(isLeft ? 68 : 4, 18, Width - 72, 10));
        }

        private void player_ManaChanged()
        {
            Invalidate(new Rectangle(isLeft ? 68 : 4, 30, Width - 72, 10));
        }

        private void player_ExtraChanged()
        {
            Invalidate(new Rectangle(isLeft ? 68 : 4, 2, Width - 72, 13));
        }

        private void LifeClock_Paint(object sender, PaintEventArgs e)
        {
            int anger = 0;
            int maxAnger = 100;
            int mana = 0;
            int maxMana = 100;

            if (back != null)
            {
                e.Graphics.DrawImage(back, 0, 0, Width, Height);
            }
            if (head != null)
            {
                if (isLeft)
                    e.Graphics.DrawImage(head, 4, 3, 63, 64);
                else
                    e.Graphics.DrawImage(head, Width - 67, 3, 63, 64);
            }
            if (player != null)
            {
                anger = player.Anger;
                mana = player.Mana;
                maxAnger = player.MaxAnger;
                maxMana = player.MaxMana;
            }

            Brush b1 = new LinearGradientBrush(new Rectangle(0, 18, 500, 10), Color.FromArgb(255, 120, 120), Color.FromArgb(255, 0, 0), LinearGradientMode.Vertical);
            Brush b2 = new LinearGradientBrush(new Rectangle(0, 30, 500, 10), Color.FromArgb(120, 120, 255), Color.FromArgb(0, 0, 255), LinearGradientMode.Vertical);
            Pen pen = new Pen(Brushes.Black, 2);
            if (isLeft)
            {
                e.Graphics.DrawRectangle(Pens.Black, 68, 18, 300, 10);
                e.Graphics.FillRectangle(b1, 68, 18, anger*300/maxAnger, 10);
                e.Graphics.DrawRectangle(Pens.Black, 68, 30, 300, 10);
                e.Graphics.FillRectangle(b2, 68, 30, mana * 300 / maxMana, 10);
               
                for (int i = 1; i < maxAnger; i++)
                {
                    e.Graphics.DrawLine(pen, 68 + i * 30, 18,68+ i * 30, 40);
                }
            }
            else
            {
                e.Graphics.DrawRectangle(Pens.Black, 10, 18, 300, 10);
                e.Graphics.FillRectangle(b1, Width - 70 - anger * 300 / maxAnger, 18, anger * 300 / maxAnger, 10);
                e.Graphics.DrawRectangle(Pens.Black, 10, 30, 300, 10);
                e.Graphics.FillRectangle(b2, Width - 70 - mana * 300 / maxMana, 30, mana * 300 / maxMana, 10);

                for (int i = 1; i < maxAnger; i++)
                {
                    e.Graphics.DrawLine(pen, 10 + i * 30, 18, 10 + i * 30, 40);
                }
            }
            pen.Dispose();
            b1.Dispose();
            b2.Dispose();
            Font font = new Font("幼圆", 12, FontStyle.Bold);
            int lenth = (int) e.Graphics.MeasureString(nameStr, font).Width;
            e.Graphics.DrawString(nameStr, font, Brushes.White, isLeft ? 72 : Width - 72 - lenth, 44);
            font.Dispose();
            if (player != null)
            {
                e.Graphics.FillRectangle(Brushes.DarkSlateGray, IsLeft ? 308 - 6 * 40 : 34 + 6 * 40, 3, 36, 12);
                e.Graphics.DrawImage(Core.HSIcons.GetIconsByEName("sym1"), IsLeft ? 310 - 6 * 40 : 36 + 6 * 40, 2, 12, 12);

                e.Graphics.FillRectangle(Brushes.DarkSlateGray, IsLeft ? 308 - 5 * 40 : 34 + 5 * 40, 3, 36, 12);
                e.Graphics.DrawImage(Core.HSIcons.GetIconsByEName("sym2"), IsLeft ? 310 - 5 * 40 : 36 + 5 * 40, 2, 12, 12);

                e.Graphics.FillRectangle(Brushes.DarkSlateGray, IsLeft ? 308 - 4 * 40 : 34 + 4 * 40, 3, 36, 12);
                e.Graphics.DrawImage(Core.HSIcons.GetIconsByEName("sym3"), IsLeft ? 310 - 4 * 40 : 36 + 4 * 40, 2, 12, 12);

                font = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString(string.Format("{0:00}", player.GetExtraInfo(PlayerExtraInfo.Time)), font, Brushes.White, IsLeft ? 308 - 6 * 40 + 17 : 34 + 6 * 40 + 17, 2);
                e.Graphics.DrawString(string.Format("{0:00}", player.GetExtraInfo(PlayerExtraInfo.Soul)), font, Brushes.White, IsLeft ? 308 - 5 * 40 + 17 : 34 + 5 * 40 + 17, 2);
                e.Graphics.DrawString(string.Format("{0:00}", player.GetExtraInfo(PlayerExtraInfo.Nature)), font, Brushes.White, IsLeft ? 308 - 4 * 40 + 17 : 34 + 4 * 40 + 17, 2);
                font.Dispose();

                int id = 0;

                if (player.HasBuff(BuffEffectTypes.WizardView)) //buff中显示卡片
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        ActiveCard card = player.GetDeckCardAt(i);                        
                        if (card.Id>0)
                        {
                            Image cimg = CardAssistant.GetCard(card.CardId).GetCardImage(16, 16);
                            e.Graphics.DrawImage(cimg, new Rectangle(IsLeft ? 355 - 18*id : 9 + 18*id, 46, 16, 16), new Rectangle(0, 0, cimg.Width, cimg.Height), GraphicsUnit.Pixel);
                            id++;
                        }
                    }
                }
            }
        }

        private void LifeClock_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isLeft)
                {
                    if (e.X > 4 && e.X < 67 && e.Y > 3 && e.Y < 67)
                    {
                        if (HeadClicked != null)
                            HeadClicked();
                    }
                }
            }
        }
    }
}
