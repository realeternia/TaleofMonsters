using System;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.Core;
using TaleofMonsters.Forms.Items.Core;
using ConfigDatas;

namespace TaleofMonsters.Forms.MagicBook
{
    internal sealed partial class CardViewForm : BasePanel
    {
        private const int cardWidth = 45;
        private const int cardHeight = 56;
        private const int xCount = 12;
        private const int yCount = 8;
        private const int cardCount = xCount * yCount;
        private int totalCount;
        private int page;
        private bool show;
        private int tar = -1;
        private int[] cards;
        private Bitmap tempImage;
        private bool isDirty = true;
        private CardDetail cardDetail;

        public CardViewForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonNext.ImageNormal = PicLoader.Read("System", "NextButton.JPG");
            this.bitmapButtonNext.ImageMouseOver = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonNext.ImagePressed = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonPre.ImageNormal = PicLoader.Read("System", "PreButton.JPG");
            this.bitmapButtonPre.ImageMouseOver = PicLoader.Read("System", "PreButtonOn.JPG");
            this.bitmapButtonPre.ImagePressed = PicLoader.Read("System", "PreButtonOn.JPG");
            tempImage = new Bitmap(cardWidth * xCount, cardHeight*yCount);

            cardDetail = new CardDetail(this, cardWidth*xCount + 65, 35, 525);
        }

        internal override void Init()
        {
            base.Init();
            show = true;
            totalCount = ConfigData.MonsterDict.Count + ConfigData.WeaponDict.Count + ConfigData.SpellDict.Count;
            cards = new int[totalCount];
            comboBoxType.SelectedIndex = 0;
            comboBoxLevel.SelectedIndex = 0;
            ChangeCards(-1, 0);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int typecode = HSTypes.Attr2I(comboBoxType.SelectedItem.ToString());
            int level = comboBoxLevel.SelectedIndex;
            ChangeCards(typecode, level);
        }

        private void ChangeCards(int cardCode, int level)
        {
            page = 0;
            totalCount = 0;
            tar = -1;
            cardDetail.SetInfo(-1);
            #region 数据装载
            for (int i = 0; i < ConfigData.MonsterDict.Count; i++)
            {
                MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(i + 10001);
                if (cardCode != -1 && monsterConfig.Type != cardCode)
                    continue;
                if (level != 0 && monsterConfig.Star != level)
                    continue;
                cards[totalCount] = monsterConfig.Id;
                totalCount++;
            }
            for (int i = 0; i < ConfigData.WeaponDict.Count; i++)
            {
                WeaponConfig weaponConfig = ConfigData.GetWeaponConfig(i + 20001);
                if (cardCode != -1 && weaponConfig.Type != cardCode)
                    continue;
                if (level != 0 && weaponConfig.Star != level)
                    continue;
                cards[totalCount] = weaponConfig.Id;
                totalCount++;
            }
            for (int i = 0; i < ConfigData.SpellDict.Count; i++)
            {
                SpellConfig spellConfig = ConfigData.GetSpellConfig(i + 30001);
                if (cardCode != -1 && spellConfig.Type != cardCode)
                    continue;
                if (level != 0 && spellConfig.Star != level)
                    continue;
                cards[totalCount] = spellConfig.Id;
                totalCount++;
            }

            #endregion
            UpdateButtonState();

            cardDetail.SetInfo(cards[0]);
            isDirty = true;
            Invalidate(new Rectangle(65, 35, cardWidth * xCount + 200, 630));
        }

        private void UpdateButtonState()
        {
            bitmapButtonPre.Enabled = page > 0;
            bitmapButtonNext.Enabled = (page + 1) * cardCount < totalCount;
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
            cardDetail.SetInfo(-1);
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(65, 110, cardWidth*xCount, cardHeight*yCount));
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            page++;
            if (page > (totalCount - 1) / cardCount)
            {
                page--;
                return;
            }
            tar = -1;
            cardDetail.SetInfo(-1);
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(65, 110, cardWidth * xCount, cardHeight * yCount));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CardViewForm_MouseMove(object sender, MouseEventArgs e)
        {
            cardDetail.CheckMouseMove(e.X, e.Y);

            int truex = e.X - 65;
            int truey = e.Y - 110;
            if (truex > 0 && truex < xCount * cardWidth && truey > 0 && truey < yCount * cardHeight)
            {
                int temp = truex / cardWidth + truey / cardHeight * xCount + cardCount * page;
                if (temp != tar)
                {
                    if (temp < totalCount)
                    {
                        tar = temp;
                    }
                    else
                    {
                        tar = -1;
                    }
                    Invalidate(new Rectangle(65, 110, cardWidth * xCount, cardHeight * yCount));
                }
            }
            else
            {
                if (tar != -1)
                {
                    tar = -1;
                    Invalidate(new Rectangle(65, 110, cardWidth * xCount, cardHeight * yCount));
                }
            }
        }

        private void CardViewForm_Click(object sender, EventArgs e)
        {
            if (tar != -1)
            {
                cardDetail.SetInfo(cards[tar]);
                Invalidate(new Rectangle(65 + cardWidth * xCount, 35, 200, 525));
            }
        }

        public void DrawOnCardView(Graphics g, int cid, int x, int y, bool isSelected)
        {
            CardAssistant.DrawBase(g, cid, x, y, cardWidth, cardHeight, isSelected);
#if DEBUG
            int off = 0;
            if (DataType.Peoples.PeopleBook.IsPeopleDropCard(cid))
            {
                g.DrawImage(HSIcons.GetIconsByEName("cad1"), x + 5 + (off++ * 12), y + 30, 24, 24);
            }
            if (DataType.Tasks.TaskBook.HasCard(cid))
            {
                g.DrawImage(HSIcons.GetIconsByEName("cad3"), x + 5 + (off++ * 12), y + 30, 24, 24);
            }
            if (DeckBook.HasCard("rookie", cid) || HItemBook.IsGiftHasCard(cid))
            {
                g.DrawImage(HSIcons.GetIconsByEName("cad4"), x + 5 + (off * 12), y + 30, 24, 24);
            }
#endif
        }

        private void CardViewForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 全卡片 ", font, Brushes.White, 280, 8);
            font.Dispose();

            cardDetail.Draw(e.Graphics);
            e.Graphics.FillRectangle(Brushes.DarkGray, 67, 37, cardWidth * xCount - 4, 71);
            Image img = PicLoader.Read("System", "SearchBack.JPG");
            e.Graphics.DrawImage(img, 70, 40, cardWidth * xCount - 10, 65);
            img.Dispose();

            font = new Font("宋体", 9);
            e.Graphics.DrawString("分类", font,Brushes.White ,86,51);
            e.Graphics.DrawString("星级", font, Brushes.White, 231, 51);
            font.Dispose();

            if (show)
            {
                int pages = totalCount / cardCount + 1;
                int cardLimit = (page < pages - 1) ? cardCount : (totalCount % cardCount);
                int former = cardCount * page + 1;
                if (isDirty)
                {
                    tempImage.Dispose();
                    tempImage = new Bitmap(cardWidth * xCount, cardHeight * yCount);
                    Graphics g = Graphics.FromImage(tempImage);
                    for (int i = former - 1; i < former + cardLimit - 1; i++)
                    {
                        int ri = i % (xCount * yCount);
                        int x = (ri % xCount) * cardWidth;
                        int y = (ri / xCount) * cardHeight;
                        DrawOnCardView(g, cards[i], x, y, false);
                    }
                    g.Dispose();
                    isDirty = false;
                }
                e.Graphics.DrawImage(tempImage, 65, 110);
                if (tar != -1 && tar < totalCount)
                {
                    int ri = tar % (xCount * yCount);
                    int x = (ri % xCount) * cardWidth + 65;
                    int y = (ri/xCount)*cardHeight + 110;
                    DrawOnCardView(e.Graphics, cards[tar], x, y, true);
                }
            }
        }
    }
}