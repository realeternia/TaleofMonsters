﻿using System;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.User;
using NarlonLib.Control;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.Core;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Forms.Pops
{
    internal partial class PopBuyProduct : Form
    {
        private int itemid;
        private int itemtype;
        private int itemprice;
        private int count;
        private string fontcolor;
        private VirtualRegion virtualRegion;
        private ImageToolTip toolTip =MainItem.SystemToolTip.Instance;

        public PopBuyProduct()
        {
            InitializeComponent();
            BackgroundImage = PicLoader.Read("System", "DeckChoose.PNG");
            FormBorderStyle = FormBorderStyle.None;
            virtualRegion = new VirtualRegion(this);
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        private void PopBuyProduct_Load(object sender, EventArgs e)
        {
            count = 1;
            textBoxTotal.Text = (count * itemprice).ToString();
            if (itemtype == 1)
            {
                fontcolor = HSTypes.I2RareColor(ConfigDatas.ConfigData.GetHItemConfig(itemid).Rare);
            }
            else
            {
                fontcolor = HSTypes.I2QualityColor(ConfigDatas.ConfigData.GetEquipConfig(itemid).Quality);
                buttonAdd.Enabled = false;
                buttonMinus.Enabled = false;
                textBoxCount.ReadOnly = true;
            }
        }

        void virtualRegion_RegionLeft()
        {
            toolTip.Hide(this);
        }

        void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            Image img;
            if (itemtype == 1)
            {
                img = HItemBook.GetPreview(itemid);
            }
            else
            {
                img = EquipBook.GetPreview(itemid);
            }
            toolTip.Show(img, this, 108, 44);
        }

        private void MessageBoxEx_Paint(object sender, PaintEventArgs e)
        {
            virtualRegion.Draw(e.Graphics);

            string itemname;
            if (itemtype == 1)
            {
                itemname = ConfigDatas.ConfigData.GetHItemConfig(itemid).Name;
            }
            else
            {
                itemname = ConfigDatas.ConfigData.GetEquipConfig(itemid).Name;
            }
            Font font = new Font("微软雅黑", 10, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.FromName(fontcolor));
            e.Graphics.DrawString(itemname, font, brush, 134, 52);
            brush.Dispose();
            e.Graphics.DrawString("数量", font,Brushes.White ,69, 106);
            e.Graphics.DrawString("总价", font, Brushes.White, 69, 138);
            font.Dispose();
            e.Graphics.DrawImage(HSIcons.GetIconsByEName("res8"), 212, 140, 16, 16);
        }

        public static void Show(int id, int type, int price)
        {
            PopBuyProduct mb = new PopBuyProduct();
            mb.itemid = id;
            mb.itemtype = type;
            mb.itemprice = price;
            mb.virtualRegion.AddRegion(new PictureRegion(1, 68, 44, 40, 40, 1, type == 1 ? VirtualRegionCellType.Item : VirtualRegionCellType.Equip, id));
            mb.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserProfile.InfoBag.PayDiamond(itemprice * count))
            {
                if (itemtype == 1)
                {
                    UserProfile.InfoBag.AddItem(itemid, count);
                }
                else
                {
                    UserProfile.InfoBag.AddEquip(itemid);
                }

                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar >= 32 && !char.IsDigit(e.KeyChar))
             {
                 e.Handled = true;
             }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCount.Text == "")
                textBoxCount.Text = @"1";
            count = int.Parse(textBoxCount.Text);
            if (count>=999)
            {
                count = 999;
                textBoxCount.Text = @"999";
            }
            else if (count <= 0)
            {
                count = 1;
                textBoxCount.Text = @"1";
            }
            textBoxTotal.Text = (count * itemprice).ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (count < 999)
            {
                count++;
                textBoxCount.Text = count.ToString();
                textBoxTotal.Text = (count*itemprice).ToString();
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (count>1)
            {
                count--;
                textBoxCount.Text = count.ToString();
                textBoxTotal.Text = (count * itemprice).ToString();
            }
        }

    }
}