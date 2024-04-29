using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ConfigDatas;
using ControlPlus;
using NarlonLib.Core;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.World;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.NPCs;
using TaleofMonsters.DataType.Scenes;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms;
using TaleofMonsters.MainItem;
using TaleofMonsters.DataType.Scenes.SceneObjects;
using TaleofMonsters.DataType.Items;
using System.Threading;

namespace TaleofMonsters
{
    internal partial class MainForm : Form
    {
        private static MainForm instance;
        private HSCursor myCursor;
        private Image mainBottom;
        private Image miniMap;
        private Image mainTop;
        private Image miniBack;
        private Image backPicture;
        private int npcTar = -1;
        private List<SceneObject> sceneItems;
        private string sceneName;
        private string mouseStr;
        private long lastMouseMoveTime;
        private int timeTick;
        private int page;
        private int timeMinutes;
        private Thread workThread;

        public MainForm()
        {
            InitializeComponent();
            myCursor = new HSCursor(this);
            instance = this;
            ConfigData.LoadData();
        }

        public static MainForm Instance
        {
            get { return instance; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string version = FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
            Text = string.Format("幻兽传说卡片版 v{0}", version);
            tabPageLogin.BackgroundImage = PicLoader.Read("System", "LogBack.JPG");
            mainTop = PicLoader.Read("System", "MainTop.JPG");
            mainBottom = PicLoader.Read("System", "MainBottom.JPG");
            miniBack = PicLoader.Read("System", "MiniBack.PNG");

            WorldInfoManager.LoadCardUniqueId();
            string lname = UserProfile.GetHabitData();
            if (lname != "" && WorldInfoManager.GetPlayerPid(lname) != 0)
            {
                textBoxName.Text = lname;
                ChangePage(1);
            }
            else
            {
                ChangePage(0);
            }
            myCursor.ChangeCursor("default");

            workThread = new Thread(TimeGo);
            workThread.IsBackground = true;
            workThread.Start();
        }

        public void ChangePage(int pg)
        {
            if (pg == 0)
            {
                textBoxName.Text = "";
                textBoxPasswd.Text = "";
                SoundManager.PlayBGM("TOM000.MP3");
            }
            else if (pg == 1)
            {
                UserProfile.ProfileName = textBoxName.Text;
                int pid = WorldInfoManager.GetPlayerPid(UserProfile.ProfileName);
                if (pid == 0)
                {
                    UserProfile.Profile = new Profile(0);
                    CreatePlayerForm cpf = new CreatePlayerForm();
                    cpf.ShowDialog();
                    if (cpf.Result == DialogResult.Cancel)
                        return;
                }
                else
                {
                    UserProfile.LoadFromDB(pid);
                }
                MainTipManager.Refresh();
                SoundManager.PlayBGM("TOM001.MP3");
                ChangeMap(UserProfile.InfoBasic.mapId);
                UserProfile.Profile.OnLogin();
                timeMinutes = (int) DateTime.Now.TimeOfDay.TotalMinutes;
                if (UserProfile.Profile.InfoBasic.job==0)//无职业
                {
                    DealPanel(new SelectJobForm());
                }
            }
            page = pg;
            viewStack1.SelectedIndex = page;
        }

        public void ChangeMap(int mapid)
        {
            if (backPicture != null)
                backPicture.Dispose();
            SceneConfig sceneConfig = ConfigData.GetSceneConfig(mapid);
            backPicture = PicLoader.Read("Scene", string.Format("{0}.JPG", sceneConfig.Url));
            sceneName = sceneConfig.Name;

            GenerateMiniMap(mapid, sceneConfig.WindowX, sceneConfig.WindowY);

            UserProfile.InfoBasic.mapId = mapid;

            SystemMenuManager.ResetIconState(); //reset main icon state

            RefreshNpcState();
            tabPageGame.Invalidate();
        }

        private void GenerateMiniMap(int mapid, int wx, int wy)
        {
            Image allMap = PicLoader.Read("Map", "worldmap.JPG"); //生成世界地图
            Graphics g = Graphics.FromImage(allMap);
            Font font = new Font("微软雅黑", 18, FontStyle.Bold);
            foreach (SceneMapIconConfig mapIconConfig in ConfigData.SceneMapIconDict.Values)
            {
                if (mapIconConfig.IconX < wx || mapIconConfig.IconX > wx + 300 || mapIconConfig.IconY < wy || mapIconConfig.IconY > wy + 300)
                    continue;
                string tname = ConfigData.GetSceneConfig(mapIconConfig.Id).Name;
                g.DrawString(tname, font, Brushes.Black, mapIconConfig.IconX + 2, mapIconConfig.IconY+ 21);
                g.DrawString(tname, font, mapIconConfig.Id == mapid ? Brushes.Lime : Brushes.White, mapIconConfig.IconX, mapIconConfig.IconY + 20);
            }
            font.Dispose();
            g.Dispose();

            if (miniMap != null)
            {
                miniMap.Dispose();
            }
            miniMap = new Bitmap(150, 150); //绘制小地图
            g = Graphics.FromImage(miniMap);
            Rectangle destRect = new Rectangle(0, 0, 150, 150);
            g.DrawImage(allMap, destRect, wx, wy, 300, 300, GraphicsUnit.Pixel);
            g.Dispose();
            allMap.Dispose();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(backPicture, 0, 50, 1000, 600);
            e.Graphics.DrawImage(mainTop, 0, 0, 1000, 50);
            e.Graphics.DrawImage(mainBottom, 0, 650, 1000, 35);

            Font font = new Font("微软雅黑", 12, FontStyle.Bold);
            Font font2 = new Font("宋体", 9, FontStyle.Bold);
            if (UserProfile.Profile != null && UserProfile.InfoBasic.mapId != 0)
            {
                Image head = PicLoader.Read("Player", string.Format("{0}.PNG", UserProfile.InfoBasic.face));
                e.Graphics.DrawImage(head, 5, 5, 40, 40);
                head.Dispose();
                e.Graphics.DrawString(UserProfile.InfoBasic.level.ToString(), font2, Brushes.Black, 3, 3);
                e.Graphics.DrawString(UserProfile.InfoBasic.level.ToString(), font2, Brushes.White, 2, 2);
                e.Graphics.DrawString(UserProfile.ProfileName, font, Brushes.White, 50, 10);

                LinearGradientBrush b1 = new LinearGradientBrush(new Rectangle(50, 30, 100, 5), Color.LawnGreen, Color.LightSalmon, LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(b1, 50, 30, Math.Min(UserProfile.InfoBasic.ap, 100), 5);
                e.Graphics.DrawRectangle(Pens.Navy, 50, 30, 100, 5);
                b1.Dispose();

                int len = (int)e.Graphics.MeasureString(sceneName, font).Width;
                e.Graphics.DrawString(sceneName, font, Brushes.White, new PointF(928 - len / 2, 8));
                if (timeMinutes >= 960 && timeMinutes < 1080)
                {
                    Brush yellow = new SolidBrush(Color.FromArgb(50, 255, 200, 0));
                    e.Graphics.FillRectangle(yellow, 0, 50, 1000, 600);
                    yellow.Dispose();
                }
                else if (timeMinutes >= 1080)
                {
                    Brush blue = new SolidBrush(Color.FromArgb(80, 0, 0, 150));
                    e.Graphics.FillRectangle(blue, 0, 50, 1000, 600);
                    blue.Dispose();
                }

                for (int i = 0; i < UserProfile.InfoBag.dayItems.Count; i++)
                {
                    e.Graphics.DrawImage(HItemBook.GetHItemImage(UserProfile.InfoBag.dayItems[i]), 12 + 18 * i, 35, 16, 16);
                }

                e.Graphics.DrawString(UserProfile.InfoBag.resource.Lumber.ToString(), font, Brushes.White, new PointF(235, 13));
                e.Graphics.DrawString(UserProfile.InfoBag.resource.Stone.ToString(), font, Brushes.White, new PointF(317, 13));
                e.Graphics.DrawString(UserProfile.InfoBag.resource.Mercury.ToString(), font, Brushes.White, new PointF(399, 13));
                e.Graphics.DrawString(UserProfile.InfoBag.resource.Carbuncle.ToString(), font, Brushes.White, new PointF(481, 13));
                e.Graphics.DrawString(UserProfile.InfoBag.resource.Sulfur.ToString(), font, Brushes.White, new PointF(563, 13));
                e.Graphics.DrawString(UserProfile.InfoBag.resource.Gem.ToString(), font, Brushes.White, new PointF(645, 13));
                e.Graphics.DrawString(UserProfile.InfoBag.resource.Gold.ToString(), font, Brushes.White, new PointF(728, 13));
                e.Graphics.FillRectangle(Brushes.DimGray, 180, 41, 630, 8); 
                b1 = new LinearGradientBrush(new Rectangle(180, 41, 630, 8), Color.White, Color.Gray, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(b1, 180, 41, Math.Min(UserProfile.InfoBasic.exp * 630 / ExpTree.GetNextRequired(UserProfile.InfoBasic.level), 630), 8);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), 180, 41, 630, 8);
                b1.Dispose();
            }
#if DEBUG
            e.Graphics.DrawString(mouseStr, font, Brushes.White, 10, 630);
#endif
            font.Dispose();
            font2.Dispose();

            e.Graphics.DrawImage(miniMap, 828, 43, 150, 150);
            e.Graphics.DrawImage(miniBack, 798, 38, 185, 160);

            SystemMenuManager.DrawAll(e.Graphics);

            //画NPC
            foreach (SceneObject obj in sceneItems)
            {
                obj.Draw(e.Graphics, npcTar);
            }

            MainTipManager.DrawAll(e.Graphics);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBoxEx.Show("游戏账号不能为空！");
                return;
            }

            UserProfile.SaveHabitData(textBoxName.Text);
            ChangePage(1);
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            string version = FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
            Font font = new Font("微软雅黑", 12, FontStyle.Regular);
            e.Graphics.DrawString("Version " + version, font, Brushes.White, 865, 710);
            font.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (viewStack1.SelectedIndex == 1)
            {
                UserProfile.Profile.OnLogout();
                UserProfile.SaveToDB();
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastMouseMoveTime + 50 > TimeTool.GetNowMiliSecond())
            {
                return;
            }
            lastMouseMoveTime = TimeTool.GetNowMiliSecond();

            if (SystemMenuManager.UpdateToolbar(e.X, e.Y))
            {
                tabPageGame.Invalidate();
            }

            int nTemp = -1;
            foreach (SceneObject obj in sceneItems)
            {
                if (e.X > obj.X && e.X < obj.X + 80 && e.Y > obj.Y + 50 && e.Y < obj.Y + 110)
                    nTemp = obj.Id;
            }
            if (npcTar != nTemp)
            {
                npcTar = nTemp;
                tabPageGame.Invalidate();
            }

#if DEBUG
            mouseStr = string.Format("{0},{1}", e.X - 35, e.Y - 85);
            tabPageGame.Invalidate(new Rectangle(10, 630, 100, 30));
#endif
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            SystemMenuManager.CheckItemClick((SystemMenuIds)SystemMenuManager.MenuTar);

            foreach (SceneObject obj in sceneItems)
            {
                if (obj.Id == npcTar)
                {
                    obj.CheckClick();
                }
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (viewStack1.SelectedIndex == 1)
            {
                SystemMenuManager.CheckHotKey(e.KeyCode);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void AddTip(string newtip, string color)
        {
            MainTipManager.AddTip(newtip, color);
            tabPageGame.Invalidate();
        }

        public void RefreshPanel()
        {
            tabPageGame.Invalidate();
        }

        public void DealPanel(BasePanel panel)
        {
            foreach (Control control in tabPageGame.Controls)
            {
                if (control.GetType() == panel.GetType())
                {
                    RemovePanel(control as BasePanel);
                    return;
                }
            }

            if (panel.NeedBlackForm)
            {
                tabPageGame.Controls.Add(BlackWallForm.Instance);
                SystemMenuManager.isHotkeyEnabled = false;
            }
            panel.Init();            
            tabPageGame.Controls.Add(panel);
            panel.BringToFront();
            SystemMenuManager.formCount++;
        }

        public void RemovePanel(BasePanel panel)
        {
            if (panel.IsChangeBgm)
            {
                SoundManager.PlayLastBGM();
            }
            if (panel.NeedBlackForm)
            {
                tabPageGame.Controls.Remove(BlackWallForm.Instance);
                SystemMenuManager.isHotkeyEnabled = true;
            }
            tabPageGame.Controls.Remove(panel);
            SystemMenuManager.formCount--;
        }

        public void RefreshNpcState()
        {
            sceneItems = new List<SceneObject>();
            sceneItems.AddRange(SceneBook.GetWarps(UserProfile.InfoBasic.mapId));
            sceneItems.AddRange(NPCBook.GetNPCOnMap(UserProfile.InfoBasic.mapId));

            tabPageGame.Invalidate();
        }

        private void TimeGo()
        {
            while (true)
            {
                if (page == 1)
                {
                    timeTick++;
                    if (timeTick > 1000)
                    {
                        timeTick -= 1000;
                    }
                    foreach (Control control in tabPageGame.Controls)
                    {
                        if (control is BasePanel)
                        {
                            (control as BasePanel).OnFrame(timeTick);
                        }
                    }

                    if (SystemMenuManager.isHotkeyEnabled && (timeTick%5) == 0)
                    {
                        SystemMenuManager.UpdateAll(tabPageGame);

                        if (MainTipManager.OnFrame())
                        {
                            tabPageGame.Invalidate();
                        }

                        if (UserProfile.Profile != null && sceneItems != null)
                        {
                            int time = (int)DateTime.Now.TimeOfDay.TotalMinutes;
                            if (timeMinutes == 0 || time != timeMinutes)
                            {
                                if (timeMinutes == 0 || (time % 60) == 0)
                                {
                                    timeMinutes = time;
                                    if (time == 0)
                                    {
                                        UserProfile.Profile.OnNewDay();
                                    }
                                    tabPageGame.Invalidate();
                                }
                            }
                            else
                            {
                                tabPageGame.Invalidate(new Rectangle(10, 8, 130, 18));
                            }
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }
    }
}