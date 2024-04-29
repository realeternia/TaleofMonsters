using System;
using System.Drawing;
using System.Windows.Forms;
using ControlPlus;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms.Items.Core;

namespace TaleofMonsters.Forms
{
    internal partial class ExpBottleForm : BasePanel
    {
        private Image backImage;
        private int addon;

        public ExpBottleForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonC1.ImageNormal = PicLoader.Read("Button", "ExpButton.JPG");
            this.bitmapButtonC1.ImageMouseOver = PicLoader.Read("Button", "ExpButtonOn.JPG");
            this.bitmapButtonC1.ImagePressed = PicLoader.Read("Button", "ExpButtonOn.JPG");
            this.bitmapButtonC3.ImageNormal = PicLoader.Read("Button", "AddExpButton.JPG");
            this.bitmapButtonC3.ImageMouseOver = PicLoader.Read("Button", "AddExpButtonOn.JPG");
            this.bitmapButtonC3.ImagePressed = PicLoader.Read("Button", "AddExpButtonOn.JPG");
            backImage = PicLoader.Read("MiniGame", "t3.JPG");
        }

        internal override void Init()
        {
            base.Init();

            radioButton1.Checked = true;
        }

        private void bitmapButtonC1_Click(object sender, EventArgs e)
        {
            if (UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.HeroExpPoint) < addon)
            {
                return;
            }

            int itemId = 0;
            if (addon == 10)
            {
                itemId = 32101;
            }
            else if (addon == 50)
            {
                itemId = 32102;
            }
            else if (addon == 150)
            {
                itemId = 32103;
            }

            UserProfile.InfoRecord.AddRecordById((int)MemPlayerRecordTypes.HeroExpPoint, -addon);
            UserProfile.InfoBag.AddItem(itemId, 1);
            bitmapButtonC1.Enabled = UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.HeroExpPoint) >= addon;

            panelBack.Invalidate();
        }

        private void bitmapButtonC3_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx2.Show("是否花10钻石随机增加经验瓶50-100点经验?") == DialogResult.OK)
            {
                if (UserProfile.InfoBag.PayDiamond(10))
                {
                    UserProfile.InfoRecord.AddRecordById((int)MemPlayerRecordTypes.HeroExpPoint, NarlonLib.Math.MathTool.GetRandom(50, 100));
                    panelBack.Invalidate();
                }
            }
        }

        private void panelIcons_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(backImage, 0, 0, panelBack.Width, panelBack.Height);

            Font font = new Font("宋体", 11, FontStyle.Bold);
            DrawShadeText(e.Graphics, string.Format("经验值 {0}/{1}", UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.HeroExpPoint), ExpTree.GetNextRequiredCard(UserProfile.InfoBasic.level)), font, Brushes.White, 20, 30);
            font.Dispose();
        }

        private void ExpBottleForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString("经验瓶", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();
        }

        private void bitmapButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DrawShadeText(Graphics g, string text, Font font, Brush Brush, int x, int y)
        {
            g.DrawString(text, font, Brushes.Black, x + 1, y + 1);
            g.DrawString(text, font, Brush, x, y);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            addon = int.Parse((sender as Control).Tag.ToString());
            bitmapButtonC1.Enabled = UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.HeroExpPoint) >= addon;
        }

    }
}
