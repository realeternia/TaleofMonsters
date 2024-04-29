using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using NarlonLib.Control;
using NarlonLib.Core;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;
using ControlPlus;

namespace TaleofMonsters.Forms
{
    internal sealed partial class MergeWeaponForm : BasePanel
    {
        private int methodid;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;
        private int[] itemCounts;
        private MemMergeData[] mergeInfos;
        private MemMergeData currentInfo;
        private NLSelectPanel selectPanel;
        private string timeText;

        public MergeWeaponForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            virtualRegion = new VirtualRegion(this);
            virtualRegion.AddRegion(new PictureAnimRegion(1, 170, 71, 50, 50, 1, VirtualRegionCellType.Equip, 0));
            virtualRegion.AddRegion(new PictureAnimRegion(2, 170, 164, 50, 50, 2, VirtualRegionCellType.Equip, 0));
            virtualRegion.AddRegion(new PictureAnimRegion(3, 170, 259, 40, 40, 3, VirtualRegionCellType.Item, 0));
            virtualRegion.AddRegion(new PictureAnimRegion(4, 302, 259, 40, 40, 4, VirtualRegionCellType.Item, 0));
            virtualRegion.AddRegion(new PictureAnimRegion(5, 170, 309, 40, 40, 5, VirtualRegionCellType.Item, 0));
            virtualRegion.AddRegion(new PictureAnimRegion(6, 302, 309, 40, 40, 6, VirtualRegionCellType.Item, 0));

            virtualRegion.RegionClicked += new VirtualRegion.VRegionClickEventHandler(virtualRegion_RegionClicked);
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);

            selectPanel = new NLSelectPanel(8, 34, 154, 400, this);
            selectPanel.ItemHeight = 50;
            selectPanel.SelectIndexChanged += selectPanel_SelectedIndexChanged;
            selectPanel.DrawCell += new NLSelectPanel.SelectPanelCellDrawHandler(selectPanel_DrawCell);
        }

        internal override void Init()
        {
            base.Init();
            refreshInfo();
            OnFrame(0);
        }

        private void refreshInfo()
        {
            itemCounts = new int[8];
            mergeInfos = UserProfile.InfoWorld.GetAllMergeData();
            Array.Sort(mergeInfos, new CompareByMethod());
            selectPanel.ClearContent();
            foreach (MemMergeData merge in mergeInfos)
            {
                EquipConfig equipConfig = ConfigData.GetEquipConfig(merge.target);
                selectPanel.AddContent(equipConfig.Id);
            }
            selectPanel.SelectIndex = 0;
            Invalidate(selectPanel.Rectangle);
            UpdateMethod();
        }

        private void selectPanel_SelectedIndexChanged()
        {
            methodid = 0;

            UpdateMethod();
        }

        private void selectPanel_DrawCell(Graphics g, int info, int xOff, int yOff)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(info);
            g.DrawImage(EquipBook.GetEquipImage(info), 5 + xOff, 5 + yOff, 40, 40);
            Font font = new Font("微软雅黑", 11.25F, FontStyle.Bold);
            SolidBrush sb = new SolidBrush(Color.FromName(HSTypes.I2QualityColor(equipConfig.Quality)));
            g.DrawString(equipConfig.Name, font, sb, 50 + xOff, 5 + yOff);
            sb.Dispose();
            font.Dispose();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            methodid = (methodid + 1) % currentInfo.Count;

            UpdateMethod();
        }

        public void DoMerge(int index)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(currentInfo.target);

            UserProfile.InfoBag.resource.Gold -= equipConfig.Value / 2;
            if (equipConfig.BaseId != 0)
            {
                UserProfile.InfoBag.DeleteEquip(equipConfig.BaseId);
            }

            foreach (IntPair pairValue in currentInfo[index])
            {
                UserProfile.InfoBag.DeleteItem(pairValue.type, pairValue.value);
            }

            UserProfile.InfoBag.AddEquip(equipConfig.Id);
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            if (selectPanel.SelectIndex < 0)
                return;

            EquipConfig equipConfig = ConfigData.GetEquipConfig(currentInfo.target);
            if (equipConfig.BaseId != 0 && UserProfile.InfoBag.GetEquipCount(equipConfig.BaseId) == 0)
            {
                AddFlowCenter("缺少装备原件", "Red");
                return;
            }
            foreach (IntPair pairValue in currentInfo[methodid])
            {
                if (UserProfile.InfoBag.GetItemCount(pairValue.type) < pairValue.value)
                {
                    AddFlowCenter("合成材料不足", "Red");
                    return;
                }
            }
            if (UserProfile.InfoBag.resource.Gold < equipConfig.Value / 2)
            {
                AddFlowCenter("金钱不足", "Red");
                return;
            }

            DoMerge(methodid);
            UpdateMethod();
        }

        private void UpdateMethod()
        {
            if (selectPanel.SelectIndex < 0)
                return;

            int targetid = selectPanel.SelectInfo;
            if (targetid == 0)
                return;

            foreach (MemMergeData memMergeData in mergeInfos)
            {
                if (memMergeData.target == targetid)
                {
                    currentInfo = memMergeData;
                }
            }

            EquipConfig equipConfig = ConfigData.GetEquipConfig(targetid);
            virtualRegion.SetRegionInfo(1, equipConfig.Id);
            itemCounts[0] = UserProfile.InfoBag.GetEquipCount(equipConfig.Id);

            if (equipConfig.BaseId != 0)
            {
                virtualRegion.SetRegionInfo(2, equipConfig.BaseId);
                itemCounts[1] = UserProfile.InfoBag.GetEquipCount(equipConfig.BaseId);
            }
            int index = 2;
            foreach (IntPair pair in currentInfo[methodid])
            {
                virtualRegion.SetRegionInfo(index+1, pair.type);
                itemCounts[index] = UserProfile.InfoBag.GetItemCount(pair.type);
                index++;
            }
            for (int i = index; i < 6; i++)
            {
                virtualRegion.SetRegionInfo(i+1, 0);
            }

            Invalidate();
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            if (key == 0)
            {
                return;
            }

            int id = info;
            Image image;
            if (id < 3)
            {
                image = EquipBook.GetPreview(key);
            }
            else
            {
                image = HItemBook.GetPreview(key);
            }

            tooltip.Show(image, this, x, y);
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(this);
        }

        private void virtualRegion_RegionClicked(int info, MouseButtons button)
        {
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        delegate void RefreshInfoCallback();
        internal override void OnFrame(int tick)
        {
            base.OnFrame(tick);
            if ((tick % 6) == 0)
            {
                TimeSpan span = TimeTool.UnixTimeToDateTime(UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastMergeTime) + SysConstants.MergeWeaponDura) - DateTime.Now;
                if (span.TotalSeconds > 0)
                {
                    timeText = string.Format("更新剩余 {0}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
                    Invalidate(new Rectangle(165, 412, 150, 20));
                }
                else
                {
                    BeginInvoke(new RefreshInfoCallback(refreshInfo));
                }
            }
        }

        private void MergeWeaponForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 合成 ", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            Image back = PicLoader.Read("System", "MergeTable.JPG");
            e.Graphics.DrawImage(back, 166, 35, 240, 353);
            back.Dispose();

            if (currentInfo == null)
            {
                return;
            }

            font = new Font("宋体", 9);
            e.Graphics.DrawString(timeText, font, Brushes.YellowGreen, 165, 412);
            font.Dispose();

            int targetid = selectPanel.SelectInfo;
            EquipConfig equipConfig = ConfigData.GetEquipConfig(targetid);
            EquipConfig baseEquipConfig = ConfigData.GetEquipConfig(equipConfig.BaseId);
            font =new Font("微软雅黑", 14,  FontStyle.Bold);
            e.Graphics.DrawString((equipConfig.Value/2).ToString().PadLeft(5, ' '), font, Brushes.Orange, 323, 390);
            e.Graphics.DrawImage(HSIcons.GetIconsByEName("res1"), 383, 390, 24, 24);
            font.Dispose();

            font =new Font("微软雅黑", 10, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.FromName(HSTypes.I2QualityColor(equipConfig.Quality)));
            e.Graphics.DrawString(equipConfig.Name, font, brush, 233, 74);
            brush.Dispose();
            e.Graphics.DrawString(string.Format("拥有{0}件", itemCounts[0]), font, itemCounts[0] == 0 ? Brushes.Gray : Brushes.Lime, 259, 99);
            brush = new SolidBrush(Color.FromName(HSTypes.I2QualityColor(baseEquipConfig.Quality)));
            e.Graphics.DrawString(baseEquipConfig.Name, font, brush, 233, 164);
            brush.Dispose();
            e.Graphics.DrawString(string.Format("拥有{0}件", itemCounts[1]), font, itemCounts[1] == 0 ? Brushes.Red : Brushes.Lime, 259, 189);
            int index = 2;
            foreach (IntPair pair in currentInfo[methodid])
            {
                brush = new SolidBrush(Color.FromName(HSTypes.I2RareColor(ConfigData.GetHItemConfig(pair.type).Rare)));
                e.Graphics.DrawString(ConfigData.GetHItemConfig(pair.type).Name, font, brush, 216 + (index % 2) * 132, 259 + (index / 2 - 1) * 50);
                brush.Dispose();
                e.Graphics.DrawString(string.Format("{0}/{1}", itemCounts[index], pair.value), font, itemCounts[index] >= pair.value ? Brushes.Lime : (itemCounts[index] == 0 ? Brushes.Gray : Brushes.Red), 242 + (index % 2) * 132, 280 + (index / 2-1) * 50);

                index++;
            }
            font.Dispose();

            virtualRegion.Draw(e.Graphics);
        }
    }
}