﻿using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using ControlPlus;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Tasks;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Forms
{
    internal sealed partial class TaskForm : BasePanel
    {
        private int selectTid = -1;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;
        private int[] tids;

        public TaskForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");

            InitTasks();
        }

        public void InitTasks()
        {
            virtualRegion = new VirtualRegion(this);
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
            int id = 1;
            tids = TaskBook.GetTaskByLevels();
            foreach (int tid in tids)
            {
                TaskConfig taskConfig = ConfigData.GetTaskConfig(tid);
                virtualRegion.AddRegion(new PictureRegion(id, 24 + taskConfig.Position.X * 32, 82 + taskConfig.Position.Y * 32, 28, 28, id, VirtualRegionCellType.Task, tid));
                id++;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bitmapButtonRefresh_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx2.Show("是否花5钻石立刻完成任务?") == DialogResult.OK)
            {
                if (UserProfile.InfoBag.PayDiamond(5))
                {
                    UserProfile.InfoTask.EndTask(selectTid);
                }
            }
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            int id = info;
            if (id > 0)
            {
                if (UserProfile.InfoTask.GetTaskStateById(key) > 0)
                {
                    Image image = TaskBook.GetPreview(key);
                    tooltip.Show(image, this, x, y);
                }
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(this);
        }

        private void TaskForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 任务 ", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, 22 + i*32, 80, 22 + i*32, 80 + 320);
            }
            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, 22, 80 + i * 32, 22 + 320, 80 + i * 32);
            }

            foreach (int tid in tids)
            {
                TaskConfig taskConfig = ConfigData.GetTaskConfig(tid);
                RLXY src = taskConfig.Position;
                int fid = taskConfig.Former;
                if (fid!=0)
                {
                    RLXY dest = ConfigData.GetTaskConfig(fid).Position;
                    Pen pen = new Pen(UserProfile.InfoTask.GetTaskStateById(tid) > 0 ? Color.Lime : Color.Gray, 2);
                    int yoff = 3;
                    if (src.Y!=dest.Y)
                    {
                        yoff = -3;
                        e.Graphics.DrawLine(pen, 22 + dest.X * 32 + 16, 80 + 32 * src.Y + 16 + yoff, 22 + dest.X * 32 + 16, 80 + 32 * dest.Y + 16);
                    }
                    if (src.X != dest.X)
                    {
                        e.Graphics.DrawLine(pen, 22 + dest.X * 32 + 16, 80 + 32 * src.Y + 16 + yoff, 22 + src.X * 32 + 16, 80 + 32 * src.Y + 16 + yoff);
                    }
                    pen.Dispose();
                }
            }

            virtualRegion.Draw(e.Graphics);
        }

    }
}