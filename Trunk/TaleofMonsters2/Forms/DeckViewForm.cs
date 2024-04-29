using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using ControlPlus;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;
using TaleofMonsters.Forms.Items.Regions.Decorators;
using TaleofMonsters.Forms.MagicBook;
using TaleofMonsters.Forms.Pops;
using TaleofMonsters.MainItem;

namespace TaleofMonsters.Forms
{
    internal sealed partial class DeckViewForm : BasePanel
    {
        private VirtualRegion virtualRegion;

        private const int cardWidth = 60;
        private const int cardHeight = 75;
        private const int yoff = 35;
        private bool show;
        private int xCount, yCount;
        private int cardCount;
        private int page;
        private int tar = -1;
        private int floor;
        private DeckCard[] dcards;
        private Bitmap tempImage;
        private bool isDirty = true;
        private Dictionary<int, string> cardAttr;
        private CardDetail cardDetail;

        private PopMenuDeck popMenuDeck;
        private PoperContainer popContainer;

        public DeckViewForm()
        {
            InitializeComponent();
            virtualRegion = new VirtualRegion(this);
            for (int i = 0; i < 3; i++)
            {
                ButtonRegion region = new ButtonRegion(i + 1, 12+85*i, 40, 74, 24, i + 1, "CommonButton1.JPG", "CommonButton1On.JPG");
                region.AddDecorator(new RegionTextDecorator(region, 8, 7, 10, Color.Black));
                virtualRegion.AddRegion(region);
            }
            virtualRegion.SetRegionDecorator(1, 0, "我的卡组");
            virtualRegion.SetRegionDecorator(2, 0, "全部卡片");
            virtualRegion.SetRegionDecorator(3, 0, " 新卡片");

            virtualRegion.RegionClicked += new VirtualRegion.VRegionClickEventHandler(virtualRegion_RegionClicked);

            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonNext.ImageNormal = PicLoader.Read("System", "NextButton.JPG");
            this.bitmapButtonNext.ImageMouseOver = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonNext.ImagePressed = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonPre.ImageNormal = PicLoader.Read("System", "PreButton.JPG");
            this.bitmapButtonPre.ImageMouseOver = PicLoader.Read("System", "PreButtonOn.JPG");
            this.bitmapButtonPre.ImagePressed = PicLoader.Read("System", "PreButtonOn.JPG");
            this.bitmapButtonNextD.ImageNormal = PicLoader.Read("System", "NextButton.JPG");
            this.bitmapButtonNextD.ImageMouseOver = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonNextD.ImagePressed = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonPreD.ImageNormal = PicLoader.Read("System", "PreButton.JPG");
            this.bitmapButtonPreD.ImageMouseOver = PicLoader.Read("System", "PreButtonOn.JPG");
            this.bitmapButtonPreD.ImagePressed = PicLoader.Read("System", "PreButtonOn.JPG");
            this.bitmapButtonAdd.ImageNormal = PicLoader.Read("System", "PlusButton.JPG");
            this.bitmapButtonAdd.ImageMouseOver = PicLoader.Read("System", "PlusButtonOn.JPG");
            this.bitmapButtonAdd.ImagePressed = PicLoader.Read("System", "PlusButtonOn.JPG");
            this.bitmapButtonDel.ImageNormal = PicLoader.Read("System", "DelButton.JPG");
            this.bitmapButtonDel.ImageMouseOver = PicLoader.Read("System", "DelButtonOn.JPG");
            this.bitmapButtonDel.ImagePressed = PicLoader.Read("System", "DelButtonOn.JPG");
          
            cardDetail = new CardDetail(this, 605, 35, 480);

            xCount = 10;
            yCount = 5;
            cardCount = xCount * yCount;
            tempImage = new Bitmap(600, 375);

            popMenuDeck = new PopMenuDeck();
            popContainer = new PoperContainer(popMenuDeck);
            popMenuDeck.PoperContainer = popContainer;
            popMenuDeck.Form = this;
        }

        internal override void Init()
        {
            base.Init();
            show = true;

            changeDeck(1);
            cardAttr = new Dictionary<int, string>();
            UpdateButtonState();
            UpdateDeckButtonState();            

            SoundManager.PlayBGM("TOM003.MP3");
            IsChangeBgm = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void changeDeck(int type)
        {
            floor = type;
            if(floor == 1)
            {
                MemDeckData dk = UserProfile.InfoCard.SelectedDeck;
                dcards = new DeckCard[50];
                for (int i = 0; i < 50; i++)
                {
                    int cid = dk.GetCardAt(i);
                    dcards[i] = UserProfile.InfoCard.GetDeckCardById(cid);
                }
            }
            else if (floor == 2)
            {
                dcards = new DeckCard[UserProfile.InfoCard.cards.Count];
                UserProfile.InfoCard.cards.Values.CopyTo(dcards, 0);
                Array.Sort(dcards, new CompareDeckCardByType());
            }
            else if (floor == 3)
            {
                dcards = new DeckCard[UserProfile.InfoCard.newcards.Count];
                for (int i = 0; i < dcards.Length; i++)
                {
                    dcards[i] = UserProfile.InfoCard.GetDeckCardById(UserProfile.InfoCard.newcards[i]);
                }
            }
            page = 0;
            tar = -1;
            cardDetail.SetInfo(dcards[0]);
            UpdateButtonState();
            UpdateDeckButtonState();
            for (int i = 0; i < 3; i++)
            {
                virtualRegion.SetRegionState(i + 1, RegionState.Free);
            }
            virtualRegion.SetRegionState(type, RegionState.Blacken);
            isDirty = true;
            Invalidate();
        }

        private void DeckViewForm_MouseMove(object sender, MouseEventArgs e)
        {
            cardDetail.CheckMouseMove(e.X, e.Y);

            int truex = e.X - 5;
            int truey = e.Y - 35 - yoff;
            if (truex > 0 && truex < xCount * cardWidth && truey > 0 && truey < yCount * cardHeight)
            {
                int temp = truex / cardWidth + truey / cardHeight * xCount + cardCount * page;
                if (temp != tar)
                {
                    if (temp < dcards.Length)
                    {
                        tar = temp;
                    }
                    else
                    {
                        tar = -1;
                    }
                    Invalidate(new Rectangle(5, 35+yoff, 600, 375));
                }
            }
            else
            {
                if (tar != -1)
                {
                    tar = -1;
                    Invalidate(new Rectangle(5, 35 + yoff, 600, 375));
                }
            }
        }

        private void DeckViewForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (tar == -1)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                cardDetail.SetInfo(dcards[tar]);
                Invalidate(new Rectangle(605, 35, 200, 475));
            }
            else if (e.Button == MouseButtons.Right)
            {
                int cardid = dcards[tar].Id;
                popMenuDeck.Clear();

                if (!UserProfile.InfoCard.SelectedDeck.HasCard(cardid))
                {
                    popMenuDeck.AddItem("activate", "添加到卡组");
                    popMenuDeck.AddItem("delete", "丢弃卡片", "Red");
                }
                else
                {
                    popMenuDeck.AddItem("remove", "从卡组移除");
                }
                #region 经验豆处理
                int expBeanSubType = 0;
                switch (dcards[tar].GetCardType())
                {
                    case CardTypes.Monster: expBeanSubType = HItemTypes.ExpBeanMonster;break;
                    case CardTypes.Weapon: expBeanSubType = HItemTypes.ExpBeanWeapon; break;
                    case CardTypes.Spell: expBeanSubType = HItemTypes.ExpBeanSpell; break;
                }

                List<IntPair> itemDatas = UserProfile.Profile.InfoBag.GetItemCountBySubtype(expBeanSubType);
                foreach (IntPair itemData in itemDatas)
                {
                    HItemConfig itemConfig = ConfigData.GetHItemConfig(itemData.type);
                    string text = string.Format("使用{0}(拥有{1})", itemConfig.Name, itemData.value);
                   string tag = "expbean" + itemData.type;
                   popMenuDeck.AddItem(tag, text, HSTypes.I2RareColor(itemConfig.Rare));
                }
                #endregion

                popMenuDeck.AddItem("cancel", "取消");
                popMenuDeck.TargetCard = dcards[tar];
                popMenuDeck.Floor = floor;
                popMenuDeck.AutoResize();
                popContainer.Show(this, e.Location.X, e.Location.Y);                
            }
        }

        public void MenuRefresh(bool needUpdate)
        {
            isDirty = true;
            Invalidate(new Rectangle(5, 35, 800, 475));
            if (needUpdate)
                cardDetail.SetInfo(dcards[tar]);
        }

        public void ClearCard()
        {
            dcards[tar] = new DeckCard(-1,0,0,0);
        }

        private void UpdateButtonState()
        {
            bitmapButtonPre.Enabled = page > 0;
            bitmapButtonNext.Enabled = (page + 1) * cardCount < dcards.Length;
            bitmapButtonPre.Visible = bitmapButtonPre.Enabled || bitmapButtonNext.Enabled;
            bitmapButtonNext.Visible = bitmapButtonPre.Enabled || bitmapButtonNext.Enabled;
        }

        private void buttonPre_Click(object sender, EventArgs e)
        {
            page--;
            if (page < 0)
            {
                page++;
                return;
            }
            tar = -1;
            cardDetail.SetInfo(dcards[page*cardCount]);
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(5, 35, 800, 475));
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            page++;
            if (page > (dcards.Length - 1) / cardCount)
            {
                page--;
                return;
            }
            tar = -1;
            cardDetail.SetInfo(dcards[page * cardCount]);
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(5, 35, 800, 475));
        }

        private void UpdateDeckButtonState()
        {
            bitmapButtonPreD.Enabled = UserProfile.InfoCard.deckId > 0;
            bitmapButtonNextD.Enabled = UserProfile.InfoCard.deckId + 1 < SysConstants.PlayDeckCount;
            bitmapButtonAdd.Visible = floor == 1;
            bitmapButtonPreD.Visible = floor == 1;
            bitmapButtonNextD.Visible = floor == 1;
            bitmapButtonDel.Visible = floor == 1;
        }

        private void buttonNextD_Click(object sender, EventArgs e)
        {
            int tmp = UserProfile.InfoCard.deckId + 1;
            if (tmp >= SysConstants.PlayDeckCount)
            {
                return;
            }
            UserProfile.InfoCard.deckId = tmp;
            changeDeck(1);
            UpdateDeckButtonState();
        }

        private void buttonPreD_Click(object sender, EventArgs e)
        {
            int tmp = UserProfile.InfoCard.deckId - 1;
            if (tmp < 0)
            {
                return;
            }
            UserProfile.InfoCard.deckId = tmp;
            changeDeck(1);
            UpdateDeckButtonState();
        }

        private void buttonAddD_Click(object sender, EventArgs e)
        {
            string name = UserProfile.InfoCard.SelectedDeck.Name;
            if (PopDeckChangeName.Show(ref name))
            {
                UserProfile.InfoCard.SelectedDeck.Name = name.Length > 5 ? name.Substring(0, 5) : name;
            }
            isDirty = true;
        }

        private void buttonDelD_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx2.Show("确定要清除卡组内所有卡片？") == DialogResult.OK)
            {
                for (int i = 0; i < 50; i++)
                {
                    UserProfile.InfoCard.SelectedDeck.SetCardAt(i, -1);
                }
            }
            isDirty = true;
        }

        void virtualRegion_RegionClicked(int info, MouseButtons button)
        {
            if (button == MouseButtons.Left)
            {
                changeDeck(info);
            }
        }

        private void refreshDict()
        {
            cardAttr.Clear();
            for (int i = 0; i < 50; i++)
            {
                addCardAttr(UserProfile.InfoCard.SelectedDeck.GetCardAt(i), "D");
            }
            foreach (int cid in UserProfile.InfoCard.newcards)
            {
                addCardAttr(cid, "N");
            }
        }

        private void addCardAttr(int cardid, string mark)
        {
            if (cardAttr.ContainsKey(cardid))
            {
                cardAttr[cardid] += mark;
            }
            else
            {
                cardAttr.Add(cardid, mark);
            }
        }

        private string getCardAttr(int cardid)
        {
            if (!cardAttr.ContainsKey(cardid))
                return "";

            return cardAttr[cardid];
        }

        public void DrawOnDeckView(Graphics g, DeckCard card, int x, int y, bool isSelected, string attr)
        {
            CardAssistant.DrawBase(g, card.BaseId, x, y, cardWidth, cardHeight, isSelected);

            if (card.Id == -1)
            {
                return;
            }

          //  g.FillPie(Brushes.Gray, x + 5, y + cardHeight - 30, 20, 20, 0, 360);
          //  g.FillPie(Brushes.GreenYellow, x + 5, y + cardHeight - 30, 20, 20, 0, card.Exp * 360 / ExpTree.GetNextRequiredCard(card.Level));

            if (attr.Contains("D"))
            {
                Image mark = PicLoader.Read("System", "MarkSelect.PNG");
                g.DrawImage(mark, x, y, cardWidth, cardHeight);
                mark.Dispose();
            }
            if (attr.Contains("N"))
            {
                Image mark = PicLoader.Read("System", "MarkNew.PNG");
                g.DrawImage(mark, x, y, 30, 30);
                mark.Dispose();
            }
            int off = 0;
            if (isSelected)
            {
                off = 3;
            }
            Font fontsong = new Font("宋体", 9, FontStyle.Regular);
            string text = string.Format("{0}({1})", card.Name, card.Level);
            g.DrawString(text, fontsong, Brushes.Black, x + 5 + off, y + 5 + off);
            g.DrawString(text, fontsong, Brushes.Gainsboro, x + 4 + off, y + 4 + off);
            fontsong.Dispose();
        }

        private void DeckViewForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString("我的卡片", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            virtualRegion.Draw(e.Graphics);

            cardDetail.Draw(e.Graphics);

            if (show)
            {
                int pages = dcards.Length / cardCount + 1;
                int cardLimit = (page != pages - 1) ? cardCount : (dcards.Length % cardCount);
                int former = cardCount * page + 1;

                if (isDirty)
                {
                    refreshDict();
                    tempImage.Dispose();
                    tempImage = new Bitmap(600, 375);
                    Graphics g = Graphics.FromImage(tempImage);
                    for (int i = former - 1; i < former + cardLimit - 1; i++)
                    {
                        int ri = i % (xCount * yCount);
                        int x = (ri % xCount) * cardWidth;
                        int y = (ri / xCount) * cardHeight;
                        DrawOnDeckView(g, dcards[i], x, y, false, getCardAttr(dcards[i].Id));
                    }
                    g.Dispose();

                    isDirty = false;
                }
                e.Graphics.DrawImage(tempImage, 5, 35 + yoff);
                if (tar != -1)
                {
                    int ri = tar % (xCount * yCount);
                    int x = (ri % xCount) * cardWidth + 5;
                    int y = (ri / xCount) * cardHeight + 35 + yoff;
                    DrawOnDeckView(e.Graphics, dcards[tar], x, y, true, getCardAttr(dcards[tar].Id));
                }

                if (floor == 1)
                {
                    MemDeckData deck = UserProfile.InfoCard.SelectedDeck;

                    Pen pen = new Pen(Brushes.Red, 3);
                    e.Graphics.DrawRectangle(pen, 5+1,35+yoff+1,cardWidth*5-2,cardHeight*5-2);
                    pen.Dispose();
                    pen = new Pen(Brushes.Blue, 3);
                    e.Graphics.DrawRectangle(pen, 5 + cardWidth * 5+1, 35 + yoff+1, cardWidth * 5-2, cardHeight * 5-2);
                    pen.Dispose();
                    font = new Font("宋体", 10, FontStyle.Regular);
                    e.Graphics.DrawString(string.Format("怪物卡 ({0}/25)", deck.Mcount), font, Brushes.Red, 10, 451);
                    e.Graphics.DrawString(string.Format("武器/魔法卡 ({0}/25)", deck.Wcount), font, Brushes.Blue, 310, 451);

                    e.Graphics.DrawString(string.Format("{0} ({1}枚)", deck.Name, deck.Count), font, Brushes.White, 10, 473);
                    font.Dispose(); 
                }
            }
        }
    }
}