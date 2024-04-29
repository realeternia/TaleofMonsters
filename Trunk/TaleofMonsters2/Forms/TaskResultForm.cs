using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Tasks;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Forms
{
    internal sealed partial class TaskResultForm : BasePanel
    {
        private bool show;
        private int taskId;
        private List<int> items;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        public TaskResultForm()
        {
            InitializeComponent();
            this.bitmapButtonClose2.ImageNormal = PicLoader.Read("System", "CancelButton.JPG");
            this.bitmapButtonClose2.ImageMouseOver = PicLoader.Read("System", "CancelButtonOn.JPG");
            this.bitmapButtonClose2.ImagePressed = PicLoader.Read("System", "CancelButtonOn.JPG");
            virtualRegion = new VirtualRegion(this);
            virtualRegion.AddRegion(new SubVirtualRegion(1, 95, 200, 60, 60, 1));
            virtualRegion.AddRegion(new SubVirtualRegion(2, 175, 200, 60, 60,2));
            virtualRegion.AddRegion(new SubVirtualRegion(3, 255, 200, 60, 60, 3));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        public void SetInfo(int tid)
        {
            taskId = tid;
            SoundManager.Play("System", "QuestCompleted.wav");

            TaskConfig taskConfig = ConfigData.GetTaskConfig(taskId);
            items = new List<int>();
            if (taskConfig.Card != 0)
            {
                items.Add(taskConfig.Card);
                items.Add(3);                
            }
            for (int i = 0; i < taskConfig.Item.Count; i++)
            {
                items.Add(taskConfig.Item[i].Id);
                items.Add(taskConfig.Item[i].Value);
            }

            show = true;
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            Image img;
            if(info>items.Count/2)
            {
                return;
            }

            int id = items[info*2-2];
            int type = items[info*2-1];
            if(type==1)
            {
                img = HItemBook.GetPreview(id);
            }
            else if (type == 2)
            {
                img = EquipBook.GetPreview(id);
            }
            else
            {
                img = CardAssistant.GetCard(id).GetPreview(CardPreviewType.Normal, new int[] { });
            }
            tooltip.Show(img, this, x, y);
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(this);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            TaskBook.Award(taskId);
            UserProfile.InfoTask.EndTask(taskId);
            Close();
        }

        private void TaskResultForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString("任务完成", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            Image back = PicLoader.Read("System", "TaskResultBack.JPG");
            e.Graphics.DrawImage(back, 15, 35, 504, 284);
            back.Dispose();

            virtualRegion.Draw(e.Graphics);

            if (show)
            {
                font = new Font("微软雅黑", 15, FontStyle.Bold);
                Font font2 = new Font("宋体", 12, FontStyle.Regular);
                TaskConfig taskConfig = ConfigData.GetTaskConfig(taskId);
                e.Graphics.DrawString(taskConfig.Name, font, Brushes.White, 101, 51);
                GameResource ges = GameResource.Parse(taskConfig.Resource);
                ges.Add(GameResourceType.Gold, TaskBook.GetMoneyReal(taskId));
                int[] res = ges.ToArray();
                e.Graphics.DrawString(string.Format("{0}", ges.Gold), font2, Brushes.White, 155, 103);
                e.Graphics.DrawString(string.Format("{0}", TaskBook.GetExpReal(taskId)), font2, Brushes.White, 155, 123);
                for (int i = 0; i < 6; i++)
                {
                    e.Graphics.DrawString(res[i + 1].ToString(), font2, Brushes.Pink, 135 + 68 * i, 159);
                }
                font.Dispose();
                font2.Dispose();

                if (items.Count>0)
                {
                    int cur = 0;
                    for (int i = 0; i < items.Count; i+=2)
                    {
                        int id = items[i];
                        int type = items[i + 1];
                        if (type == 1)
                        {
                            e.Graphics.DrawImage(HItemBook.GetHItemImage(id), 95 + cur*80, 200, 60, 60);
                        }
                        else if (type == 2)
                        {
                            e.Graphics.DrawImage(EquipBook.GetEquipImage(id), 95 + cur * 80, 200, 60, 60);
                        }
                        else
                        {
                            e.Graphics.DrawImage(CardAssistant.GetCard(id).GetCardImage(60, 60), 95, 200, 60, 60);
                        }
                        cur++;
                    }
                }
            }
        }

    }
}