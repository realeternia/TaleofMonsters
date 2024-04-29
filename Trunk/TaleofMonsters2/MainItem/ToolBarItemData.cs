using System;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.User;
using ConfigDatas;
using TaleofMonsters.DataType.User.Mem;

namespace TaleofMonsters.MainItem
{
    internal class ToolBarItemData
    {
        public int Id { get { return MainIconConfig.Id; }}

        public MainIconConfig MainIconConfig;

        public bool Enable;
        public bool LastInCD;
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public ToolBarItemData(int id)
        {
            MainIconConfig = ConfigData.GetMainIconConfig(id);
            X = MainIconConfig.X;
            Y = MainIconConfig.Y;
            Width = MainIconConfig.Width;
            Height = MainIconConfig.Height;
            Enable = true;
        }

        public bool InCD
        {
            get
            {
                if (MainIconConfig.Type != (int)ToolBarItemTypes.Time)
                {
                    return false;
                }
                return UserProfile.InfoRecord.GetRecordById(MainIconConfig.Record) > NarlonLib.Core.TimeTool.DateTimeToUnixTime(DateTime.Now);
            }
        }

        public void SetSize(int px,int py, int wid, int heg)
        {
            X = px;
            Y = py;
            Width = wid;
            Height = heg;
        }

        public void Update(Control parent)
        {
            bool inCdState = InCD;
            if (inCdState || LastInCD)
            {
                parent.Invalidate(new Rectangle(X + 6, Y + 50, 60, 20));
                if (LastInCD != inCdState)
                {
                    parent.Invalidate(new Rectangle(X, Y, Width, 50));
                    LastInCD = inCdState;
                }
            }            
        }

        public int GetRecordLimit(MemPlayerRecordTypes rd)
        {
            switch (rd)
            {
                case MemPlayerRecordTypes.HeroExpPoint: return ExpTree.GetNextRequiredCard(UserProfile.InfoBasic.level);
            }
            return 0;
        }

        public void Draw(Graphics g, int menuTar)
        {
            bool isTarget = menuTar == Id;
            Image button = PicLoader.Read("Button", string.Format("{0}{1}.PNG", MainIconConfig.Icon, isTarget && !InCD ? "On" : ""));
            int buttony = Y;

            string info = "";
            if (InCD)
            {
                int timediff = UserProfile.InfoRecord.GetRecordById(MainIconConfig.Record) - NarlonLib.Core.TimeTool.DateTimeToUnixTime(DateTime.Now);
                info = string.Format("{0:00}:{1:00}", timediff / 60, timediff % 60);

                Rectangle destBack = new Rectangle(X, buttony, Width, Height);
                g.DrawImage(button, destBack, 0, 0, Width, Height, GraphicsUnit.Pixel, HSImageAttributes.ToGray);
            }
            else
            {
                if (isTarget && Id < 20)
                {
                    buttony = Y - 7;
                }
                if (MainIconConfig.Type == (int)ToolBarItemTypes.Limit)
                {
                    info = string.Format("{0:00.0}%", (double)UserProfile.InfoRecord.GetRecordById(MainIconConfig.Record) * 100 / GetRecordLimit((MemPlayerRecordTypes)MainIconConfig.Record));
                }
                g.DrawImage(button, X, buttony, Width, Height);
            }
            button.Dispose();

            if (info!="")
            {
                Font font = new Font("ËÎÌå", 9, FontStyle.Regular);
                g.DrawString(info, font, Brushes.Black, X + 8, Y + 51);
                g.DrawString(info, font, Brushes.Lime, X + 6, Y + 50);
                font.Dispose();
            }
           
            Font fontMenu = new Font("ËÎÌå", 11, FontStyle.Bold);
            g.DrawString(MainIconConfig.Name, fontMenu, Brushes.Black, X + 7, Y + 36);
            g.DrawString(MainIconConfig.Name, fontMenu, Brushes.White, X + 5, Y + 35);
            fontMenu.Dispose();

            if (isTarget)
            {
                Font font = new Font("Î¢ÈíÑÅºÚ", 12, FontStyle.Bold);
                g.DrawString(MainIconConfig.Des, font, Brushes.White, 11, 663);
                font.Dispose();
            }
        }

        public bool InRegion(int mouseX, int mouseY)
        {
            return mouseX > X && mouseX < X + Width && mouseY > Y && mouseY < Y + Height;
        }
    }
}