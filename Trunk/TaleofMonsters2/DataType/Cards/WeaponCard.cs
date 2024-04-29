using System.Drawing;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards.Weapons;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.Core;

namespace TaleofMonsters.DataType.Cards
{
    sealed class WeaponCard : Card
    {
        private readonly Weapon weapon;
        private DeckCard card;

        internal WeaponCard(Weapon weapon)
        {
            this.weapon = weapon;
        }

        internal override int CardId
        {
            get { return weapon.Id; }
        }

        internal override int Star
        {
            get { return weapon.WeaponConfig.Star; }
        }

        internal override int Type
        {
            get { return weapon.WeaponConfig.Type; }
        }

        internal override int Cost
        {
            get { return weapon.WeaponConfig.Cost; }
        }

        internal override string Name
        {
            get { return weapon.WeaponConfig.Name; }
        }

        internal override byte Quality
        {
            get { return card.Quality; }
        }

        internal override string ENameShort
        {
            get { return weapon.WeaponConfig.EnameShort; }
        }

        internal override int Res
        {
            get { return weapon.WeaponConfig.Res; }
        }

        internal override Image GetCardImage(int width, int height)
        {
            return WeaponBook.GetWeaponImage(weapon.Id, width , height);
        }

        internal override CardTypes GetCardType()
        {
            return CardTypes.Weapon;
        }

        internal override void UpdateToLevel(DeckCard dc)
        {
            card = dc;
            if (card.Level > 1)
            {
                weapon.UpgradeToLevel(card.Level, card.Quality);
            }
        }

        internal override void DrawOnDeck(Graphics g, int offX, int offY, bool isShowPicture)
        {
            int basel = 0;
            if (isShowPicture)
            {
                g.DrawImage(WeaponBook.GetWeaponImage(weapon.Id,180,180), offX+20,offY+ 20, 160, 180);
                Image back = PicLoader.Read("System", "CardPage.PNG");
                g.FillPie(PaintTool.GetBrushByAttribute(weapon.WeaponConfig.Type), offX - 10, offY + 190, 40, 40, 270, 90);
                g.DrawImage(back, offX + 10, offY + 10, 180, 200);
				back.Dispose();

                Font font = new Font("宋体", 10, FontStyle.Regular);
                g.DrawString(("★★★★★★★★★★").Substring(10 - weapon.WeaponConfig.Star), font, Brushes.Yellow, offX + 30, offY + 30);
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
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(lineBack, offX + 10, basel + 75 + i * 30, 180, 15);
            }
            g.FillRectangle(headerBack, offX + 10, basel + 185, 180, 20);
            headerBack.Dispose();
            lineBack.Dispose();

            Font fontblack = new Font("黑体", 12, FontStyle.Bold);
            Font fontsong = new Font("宋体", 10, FontStyle.Regular);
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, 50, 0));
            g.DrawString(weapon.WeaponConfig.Name, fontblack, Brushes.White, offX + 10, basel + 2);
            if (card.Name != "")
            {
                int wid = (int)g.MeasureString(weapon.WeaponConfig.Name, fontblack).Width;
                g.DrawString(string.Format("{0}", card.Name), fontsong, Brushes.DarkGreen, offX + 12 + wid, basel + 5);
            }
            g.DrawImage(HSIcons.GetIconsByEName("wep" + (weapon.WeaponConfig.Type - 8)), 60 + offX, basel - 40, 24, 24);
            g.DrawImage(HSIcons.GetIconsByEName("atr" + weapon.WeaponConfig.Attr), 88 + offX, basel - 40, 24, 24);
            g.DrawString(string.Format("Lv{0:00}", card.Level), fontsong, Brushes.Indigo, 13 + offX, basel + 22);
            g.DrawString(string.Format("({0}/{1})", card.Exp, ExpTree.GetNextRequiredCard(card.Level)), fontsong, Brushes.RoyalBlue, 44 + offX, basel + 22);
            g.DrawString("数据", fontblack, Brushes.White, offX + 10, basel + 42);           
            g.DrawString(string.Format("攻击 {0,3:D}", weapon.Atk), fontsong, sb, offX + 10, basel + 61);
            PaintTool.DrawValueLine(g, weapon.Atk / 2, 70 + offX, basel + 62, 115, 10);
            g.DrawString(string.Format("防御 {0,3:D}", weapon.Def), fontsong, sb, offX + 10, basel + 76);
            PaintTool.DrawValueLine(g, weapon.Def / 2, 70 + offX, basel + 77, 115, 10);
            g.DrawString(string.Format("魔攻 {0,3:D}", weapon.MAtk), fontsong, sb, 10 + offX, basel + 91);
            PaintTool.DrawValueLine(g, weapon.MAtk / 2, 70 + offX, basel + 92, 115, 10);
            g.DrawString(string.Format("魔防 {0,3:D}", weapon.MDef), fontsong, sb, 10 + offX, basel + 106);
            PaintTool.DrawValueLine(g, weapon.MDef / 2, 70 + offX, basel + 107, 115, 10);
            g.DrawString(string.Format("命中 {0,3:D}", weapon.Hit), fontsong, sb, 10 + offX, basel + 121);
            PaintTool.DrawValueLine(g, weapon.Hit / 2, 70 + offX, basel + 122, 115, 10);
            g.DrawString(string.Format("回避 {0,3:D}", weapon.Dhit), fontsong, sb, 10 + offX, basel + 136);
            PaintTool.DrawValueLine(g, weapon.Dhit, 70 + offX, basel + 137, 115, 10);
            g.DrawString(string.Format("技巧 {0,3:D}", weapon.Skl), fontsong, sb, 10 + offX, basel + 151);
            PaintTool.DrawValueLine(g, weapon.Skl / 2, 70 + offX, basel + 152, 115, 10);
            g.DrawString(string.Format("速度 {0,3:D}", weapon.Spd), fontsong, sb, 10 + offX, basel + 166);
            PaintTool.DrawValueLine(g, weapon.Spd/2, 70 + offX, basel + 167, 115, 10);
            g.DrawString("技能", fontblack, Brushes.White, offX + 10, basel + 187);
            if (weapon.WeaponConfig.SkillId != 0)
                g.DrawImage(SkillBook.GetSkillImage(weapon.WeaponConfig.SkillId), offX + 10, basel + 208, 40, 40);
            fontblack.Dispose();
            fontsong.Dispose();
            sb.Dispose();
        }

        internal override void DrawOnStateBar(Graphics g)
        {
            Font fontsong = new Font("宋体", 10, FontStyle.Regular);
            SolidBrush sb = new SolidBrush(Color.FromArgb(255, 255, 255));
            SolidBrush sg = new SolidBrush(Color.FromArgb(0, 255, 150));
            g.DrawImage(HSIcons.GetIconsByEName("abl1"), 0, 0);
            g.DrawString(weapon.Atk.ToString().PadLeft(3,' '), fontsong, sb, 22, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl2"), 50, 0);
            g.DrawString(weapon.Def.ToString().PadLeft(3, ' '), fontsong, sb, 72, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl11"), 100, 0);
            g.DrawString(weapon.MAtk.ToString().PadLeft(3, ' '), fontsong, sb, 122, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl12"), 150, 0);
            g.DrawString(weapon.MDef.ToString().PadLeft(3, ' '), fontsong, sb, 172, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl4"), 200, 0);
            g.DrawString(weapon.Hit.ToString().PadLeft(3, ' '), fontsong, sb, 222, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl6"), 250, 0);
            g.DrawString(weapon.Dhit.ToString().PadLeft(3, ' '), fontsong, sb, 272, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl5"), 300, 0);
            g.DrawString(weapon.Skl.ToString().PadLeft(3, ' '), fontsong, sb, 322, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl3"), 350, 0);
            g.DrawString(weapon.Spd.ToString().PadLeft(3, ' '), fontsong, sb, 372, 4);

            if (weapon.WeaponConfig.SkillId > 0)
            {
                g.DrawImage(HSIcons.GetIconsByEName("abl9"), 400, 0);
                g.DrawString(ConfigDatas.ConfigData.GetSkillConfig(weapon.WeaponConfig.SkillId).Name, fontsong, sg, 422, 4);
            }
            fontsong.Dispose();
            sb.Dispose();
            sg.Dispose();
        }

        internal override Image GetPreview(CardPreviewType type, int[] parms)
        {
            const string stars = "★★★★★★★★★★";
            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine(weapon.WeaponConfig.Name, "White", 20);
            tipData.AddTextNewLine(stars.Substring(10 - weapon.WeaponConfig.Star), "Yellow", 20);
            tipData.AddLine();
            tipData.AddTextNewLine("类型/属性", "White");
            tipData.AddImage(HSIcons.GetIconsByEName("wep" + (weapon.WeaponConfig.Type - 8)));
            tipData.AddImage(HSIcons.GetIconsByEName("atr" + weapon.WeaponConfig.Attr));
            tipData.AddTextNewLine(string.Format("攻击加成 {0,2:D}  防御加成 {1,2:D}", weapon.Atk, weapon.Def), "Lime");
            tipData.AddTextNewLine(string.Format("命中加成 {0,2:D}  回避加成 {1,2:D}", weapon.Hit, weapon.Dhit), "Lime");
            tipData.AddTextNewLine(string.Format("魔攻加成 {0,2:D}", weapon.MAtk), "Lime");
            tipData.AddTextNewLine("技能", "White");
            if (weapon.WeaponConfig.SkillId > 0)
            {
                tipData.AddImage(SkillBook.GetSkillImage(weapon.WeaponConfig.SkillId));
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
