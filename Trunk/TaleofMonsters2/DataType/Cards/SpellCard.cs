using System;
using System.Drawing;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Decks;
using ConfigDatas;

namespace TaleofMonsters.DataType.Cards
{
    sealed class SpellCard : Card
    {
        private readonly Spell spell;
        private DeckCard card;

        internal SpellCard(Spell spell)
        {
            this.spell = spell;
        }

        internal override int CardId
        {
            get { return spell.Id; }
        }

        internal override int Star
        {
            get { return spell.SpellConfig.Star; }
        }

        internal override int Type
        {
            get { return spell.SpellConfig.Type; }
        }

        internal override int Cost
        {
            get { return spell.SpellConfig.Cost; }
        }

        internal override string Name
        {
            get { return spell.SpellConfig.Name; }
        }

        internal override byte Quality
        {
            get { return card.Quality; }
        }

        internal override string ENameShort
        {
            get { return spell.SpellConfig.EnameShort; }
        }

        internal override int Res
        {
            get { return spell.SpellConfig.Res; }
        }

        internal override Image GetCardImage(int width, int height)
        {
            return SpellBook.GetSpellImage(spell.Id, width, height);
        }

        internal override CardTypes GetCardType()
        {
            return CardTypes.Spell;
        }

        internal override void UpdateToLevel(DeckCard dc)
        {
            card = dc;
            if (card.Level>1)
            {
                spell.UpgradeToLevel(card.Level, card.Quality);
            }
        }

        internal override void DrawOnDeck(Graphics g, int offX, int offY, bool isShowPicture)
        {
            SpellConfig spellConfig = spell.SpellConfig;
            int basel = 0;
            if (isShowPicture)
            {
                g.DrawImage(SpellBook.GetSpellImage(spell.Id, 180, 180), offX + 20,offY+ 20, 160, 180);
                Image back = PicLoader.Read("System", "CardPage.PNG");
                g.FillPie(PaintTool.GetBrushByAttribute(spellConfig.Type), offX - 10, offY + 190, 40, 40, 270, 90);
                g.DrawImage(back, offX + 10, offY + 10, 180, 200);
				back.Dispose();

                Font font = new Font("宋体", 10, FontStyle.Regular);
                g.DrawString(("★★★★★★★★★★").Substring(10 - spellConfig.Star), font, Brushes.Yellow, offX + 30, offY + 30);
                font.Dispose();
                basel = 210;
            }
            basel += offY;

            Brush headerBack = new SolidBrush(Color.FromArgb(190, 175, 160));
            Brush lineBack = new SolidBrush(Color.FromArgb(215, 210, 200));
            g.FillRectangle(headerBack, offX + 10, basel, 180, 20);
            for (int i = 0; i < 1; i++)
            {
                g.FillRectangle(lineBack, offX + 10, basel + 20 + i * 30, 180, 15);
            }
            g.FillRectangle(headerBack, offX + 10, basel + 40, 180, 20);

            g.FillRectangle(lineBack, offX + 10, basel + 60, 180, 45);
            headerBack.Dispose();
            lineBack.Dispose();

            Font fontblack = new Font("黑体", 12, FontStyle.Bold);
            Font fontsong = new Font("宋体", 10, FontStyle.Regular);
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, 50, 0));
            g.DrawString(spellConfig.Name, fontblack, Brushes.White, offX + 10, basel + 2);
            if (card.Name!="")
            {
                int wid = (int)g.MeasureString(spellConfig.Name, fontblack).Width;
                g.DrawString(string.Format("{0}", card.Name), fontsong, Brushes.DarkGreen, offX + 12 + wid, basel + 5);
            }
            g.DrawImage(HSIcons.GetIconsByEName("spl" + (spellConfig.Type - 12)), 60 + offX, basel - 40, 24, 24);
            g.DrawImage(HSIcons.GetIconsByEName("atr" + spellConfig.Attr), 88 + offX, basel - 40, 24, 24);
          //  g.DrawImage(AttackRanges.AttackRangeBook.GetAttackRangeImage(monster.arange), 116 + offx, basel - 40, 24, 24);
            g.DrawString(string.Format("Lv{0:00}", card.Level), fontsong, Brushes.Indigo, 13 + offX, basel + 22);
            g.DrawString(string.Format("({0}/{1})", card.Exp, ExpTree.GetNextRequiredCard(card.Level)), fontsong, Brushes.RoyalBlue, 44 + offX, basel + 22);
            g.DrawString("效果", fontblack, Brushes.White, offX + 10, basel + 42);
            string des = spell.Descript;
            if (des.Length < 13)
			{
                g.DrawString(des, fontsong, sb, offX + 10, basel + 61);
			}
            else if (des.Length < 26) 
			{
                g.DrawString(des.Substring(0, 13), fontsong, sb, offX + 10, basel + 61);
                g.DrawString(des.Substring(13), fontsong, sb, offX + 10, basel + 76);
            }
            else
            {
                g.DrawString(des.Substring(0, 13), fontsong, sb, offX + 10, basel +61);
                g.DrawString(des.Substring(13, 13), fontsong, sb, offX + 10, basel + 76);
                g.DrawString(des.Substring(26), fontsong, sb, offX + 10, basel + 91);
            }
            fontblack.Dispose();
            fontsong.Dispose();
            sb.Dispose();
        }

        internal override void DrawOnStateBar(Graphics g)
        {
            Font fontsong = new Font("宋体", 10, FontStyle.Regular);
            SolidBrush sb = new SolidBrush(Color.FromArgb(255, 255, 255));
            g.DrawString(spell.Descript, fontsong, sb, 4, 4);
            fontsong.Dispose();
            sb.Dispose();
        }

        internal override Image GetPreview(CardPreviewType type, int[] parms)
        {
            const string stars = "★★★★★★★★★★";
            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine(spell.SpellConfig.Name, "White", 20);
            tipData.AddTextNewLine(stars.Substring(10 - spell.SpellConfig.Star), "Yellow", 20);
            tipData.AddLine();
            tipData.AddTextNewLine("类型/属性", "White");
            tipData.AddImage(HSIcons.GetIconsByEName("wep" + (spell.SpellConfig.Type - 12)));
            tipData.AddImage(HSIcons.GetIconsByEName("atr" + spell.SpellConfig.Attr));
            string des = spell.Descript;
            while (true)
            {
                tipData.AddTextNewLine(des.Substring(0, Math.Min(des.Length, 15)), "Lime");
                if (des.Length <= 15)
                    break;
                des = des.Substring(15);
            }

            if (type == CardPreviewType.Shop)
            {
                tipData.AddLine();
                tipData.AddTextNewLine("价格", "White");
                for (int i = 0; i < 7; i++)
                {
                    if (parms[i] > 0)
                    {
                        tipData.AddText(" " + parms[i].ToString(), HSTypes.I2ResourceColor(i));
                        tipData.AddImage(HSIcons.GetIconsByEName("res" + (i + 1)));
                    }
                }
            } 
            return tipData.Image;
        }
    }
}
