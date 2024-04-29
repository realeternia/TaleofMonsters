using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using NarlonLib.Control;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.Peoples;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Forms;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Controler.Battle
{
    internal sealed partial class BattleResultForm : BasePanel
    {
        private bool show;
        private bool isWin;
        private int leftId;
        private int rightId;
        private int[] resource;
        private int exp;
        private int dropCardId;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        internal BattleResultForm()
        {
            InitializeComponent();
            NeedBlackForm = true;
            this.bitmapButtonClose2.ImageNormal = PicLoader.Read("System", "CancelButton.JPG");
            this.bitmapButtonClose2.ImageMouseOver = PicLoader.Read("System", "CancelButtonOn.JPG");
            this.bitmapButtonClose2.ImagePressed = PicLoader.Read("System", "CancelButtonOn.JPG");
            virtualRegion = new VirtualRegion(this);
            virtualRegion.AddRegion(new PictureAnimRegion(1, 102, 270, 60, 60, 1, VirtualRegionCellType.Card, 0));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        internal override void Init()
        {
            base.Init();
            SoundManager.Play("System", "QuestCompleted.wav");

            isWin = BattleInfo.Instance.PlayerWin;
            rightId = PlayerManager.RightPlayer.PeopleId;
            leftId = PlayerManager.LeftPlayer.PeopleId;
            virtualRegion.SetRegionInfo(1, 0);
            if (leftId == 0)
            {
                BattleInfo battleInfo = BattleInfo.Instance;
                PeopleDrop drop = new PeopleDrop(rightId);
                dropCardId = drop.GetDropCard(BattleInfo.Instance.CardRatePlus.ToArray());
                resource = drop.GetDropResource();
                PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(rightId);
                exp = ExpTree.GetNextRequired(peopleConfig.Level) / 2 / (15 + Math.Abs(UserProfile.InfoBasic.level - peopleConfig.Level) * 3) + 1;
                if (UserProfile.InfoBag.GetDayItem(HItemSpecial.DoubleExpItem))
                {
                    exp *= 2;
                }

                resource[0] = resource[0]*(100 + battleInfo.GoldRatePlus)/100;
                exp = exp*(100 + battleInfo.ExpRatePlus)/100;

                if (isWin)
                {
                    if (dropCardId > 0)
                    {
                        virtualRegion.SetRegionInfo(1, dropCardId);
                    }
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                        resource[i] /= 5;
                    exp /= 4;
                }
                if (UserProfile.InfoBag.GetDayItem(HItemSpecial.DoubleGoldItem))
                {
                    resource[0] *= 2;
                }
            }
            else
            {
                resource = (new GameResource()).ToArray();
            }
            show = true;
            Reward();
        }

        private void Reward()
        {
            if (leftId > 0)
            {
                return;
            }

            if (isWin)
            {
                if (dropCardId > 0)
                {
                    UserProfile.InfoCard.AddCard(dropCardId);
                }
                UserProfile.InfoRival.AddRivalState(rightId, true);
                UserProfile.InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalWin, 1);
                UserProfile.InfoRecord.AddRecordById((int)MemPlayerRecordTypes.ContinueWin, 1);
                UserProfile.InfoTask.UpdateTaskAddonWin(rightId, PlayerManager.RightPlayer.Level, 1);
            }
            else
            {
                UserProfile.InfoRival.AddRivalState(rightId, false);
                UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.ContinueWin, 0);
            }

            UserProfile.InfoBasic.AddExp(exp);
            UserProfile.InfoBag.AddResource(resource);
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            if (dropCardId > 0)
            {
                Image image = CardAssistant.GetCard(dropCardId).GetPreview(CardPreviewType.Normal, new int[] { });
                tooltip.Show(image, this, x, y);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            if (dropCardId > 0)
            {
                tooltip.Hide(this);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            PlayerManager.Clear();
            Close();
        }

        private void BattleResultForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString("对战结果", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            Image back = PicLoader.Read("System", "BattleResultBack.JPG");
            e.Graphics.DrawImage(back, 15, 35, 504, 354);
            back.Dispose();

            virtualRegion.Draw(e.Graphics);

            if (show)
            {
                font = new Font("微软雅黑", 15, FontStyle.Bold);
                Font font2 = new Font("宋体", 12, FontStyle.Regular);

                if (leftId == 0)
                {
                    Image head = PicLoader.Read("Player", string.Format("{0}.PNG", UserProfile.InfoBasic.face));
                    e.Graphics.DrawImage(head, 95, 40, 50, 50);
                    head.Dispose();
                    e.Graphics.DrawString(UserProfile.ProfileName, font, Brushes.White, 155, 45);
                }
                else
                {
                    PeopleConfig leftPeople = ConfigData.GetPeopleConfig(leftId);
                    Image head = PicLoader.Read("People", string.Format("{0}.PNG", leftPeople.Figue));
                    e.Graphics.DrawImage(head, 95, 40, 50, 50);
                    head.Dispose();
                    e.Graphics.DrawString(leftPeople.Name, font, Brushes.White, 155, 45);
                }

                PeopleConfig rightPeople = ConfigData.GetPeopleConfig(rightId);
                Image head2 = PicLoader.Read("People", string.Format("{0}.PNG", rightPeople.Figue));
                e.Graphics.DrawImage(head2, 305, 40, 50, 50);
                head2.Dispose();
                head2 = PicLoader.Read("System", "Win.PNG");
                e.Graphics.DrawImage(head2, isWin ? 135 : 345, 40, 109, 70);
                head2.Dispose();
                e.Graphics.DrawString(rightPeople.Name, font, Brushes.White, 370, 45);

                BattleInfo battleInfo = BattleInfo.Instance;
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.LeftMonsterAdd), font2, Brushes.White, 159, 103);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.LeftSpellAdd), font2, Brushes.White, 159, 123);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.LeftHeroAdd), font2, Brushes.White, 259, 103);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.LeftWeaponAdd), font2, Brushes.White, 259, 123);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.LeftKill), font2, Brushes.White, 159, 143);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.LeftHeroKill), font2, Brushes.White, 259, 143);

                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.RightMonsterAdd), font2, Brushes.White, 373, 103);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.RightSpellAdd), font2, Brushes.White, 373, 123);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.RightHeroAdd), font2, Brushes.White, 473, 103);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.RightWeaponAdd), font2, Brushes.White, 473, 123);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.RightKill), font2, Brushes.White, 373, 143);
                e.Graphics.DrawString(string.Format("{0,2:D}", battleInfo.RightHeroKill), font2, Brushes.White, 473, 143);

                TimeSpan span = battleInfo.EndTime - battleInfo.StartTime;
                e.Graphics.DrawString(string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds), font2, Brushes.White, 158, 195);
                e.Graphics.DrawString(string.Format("{0}", battleInfo.Round), font2, Brushes.White, 158, 175);

                e.Graphics.DrawString(resource[0].ToString(), font2, Brushes.Yellow, 126, 232);
                for (int i = 1; i < 7; i++)
                {
                    e.Graphics.DrawString(resource[i].ToString(), font2, Brushes.Pink, 141 + 57 * i, 232);
                }
                font.Dispose();
                font2.Dispose();
            }
        }

    }
}