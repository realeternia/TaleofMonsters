using System.Drawing;
using ConfigDatas;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.Core;

namespace TaleofMonsters.DataType.Cards
{
    internal sealed class MonsterCard : Card
    {
        private readonly Monster monster;
        private DeckCard card;

        internal MonsterCard(Monster monster)
        {
            this.monster = monster;
        }

        internal override int CardId
        {
            get { return monster.Id; }
        }

        internal override int Star
        {
            get { return monster.MonsterConfig.Star; }
        }

        internal override int Type
        {
            get { return monster.MonsterConfig.Type; }
        }

        internal override int Cost
        {
            get { return monster.MonsterConfig.Cost; }
        }

        internal override string Name
        {
            get { return monster.MonsterConfig.Name; }
        }

        internal override byte Quality
        {
            get { return card.Quality; }
        }

        internal override string ENameShort
        {
            get { return monster.MonsterConfig.EnameShort; }
        }

        internal override int Res
        {
            get { return monster.MonsterConfig.Res; }
        }

        internal Monster GetMonster()
        {
            return monster;
        }

        internal override Image GetCardImage(int width, int height)
        {
            return MonsterBook.GetMonsterImage(monster.Id, width, height);
        }

        internal override CardTypes GetCardType()
        {
            return CardTypes.Monster;
        }

        internal override void UpdateToLevel(DeckCard dc)
        {
            card = dc;
            if (card.Level> 1)
            {
                monster.UpgradeToLevel(card.Level, card.Quality);
            }
        }

        internal override void DrawOnDeck(Graphics g, int offX, int offY, bool isShowPicture)
        {
            int basel = 0;
            if (isShowPicture)
            {
                g.DrawImage(MonsterBook.GetMonsterImage(monster.Id, 180, 180), offX + 20, offY + 20, 160, 180);
                Image back = PicLoader.Read("System", "CardPage.PNG");
                g.FillPie(PaintTool.GetBrushByAttribute(monster.MonsterConfig.Type), offX - 10, offY + 190, 40, 40, 270, 90);
                g.DrawImage(back, offX + 10, offY+10, 180, 200);
                back.Dispose();

                Font font = new Font("宋体", 10, FontStyle.Regular);
                g.DrawString(("★★★★★★★★★★").Substring(10 - monster.MonsterConfig.Star), font, Brushes.Yellow, offX + 30, offY + 30);
                font.Dispose();
                basel = 210;
            }

            basel += offY;
            Brush headerBack = new SolidBrush(Color.FromArgb(190, 175, 160));
            Brush lineBack = new SolidBrush(Color.FromArgb(215, 210, 200));
            g.FillRectangle(headerBack, offX+10, basel, 180, 20);
            for (int i = 0; i < 1; i++)
            {
                g.FillRectangle(lineBack, 10 + offX, basel + 20 + i * 30, 180, 15);
            }
            g.FillRectangle(headerBack, 10 + offX, basel + 40, 180, 20);
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(lineBack, 10 + offX, basel + 75 + i * 30, 180, 15);
            }
            g.FillRectangle(headerBack, 10 + offX, basel + 200, 180, 20);
            headerBack.Dispose();
            lineBack.Dispose();

            Font fontblack = new Font("黑体", 12, FontStyle.Bold);
            Font fontsong = new Font("宋体", 10, FontStyle.Regular);
            g.DrawString(monster.MonsterConfig.Name, fontblack, Brushes.White, offX + 10, basel + 2);
            if (card.Name != "")
            {
                int wid = (int)g.MeasureString(monster.MonsterConfig.Name, fontblack).Width;
                g.DrawString(string.Format("{0}", card.Name), fontsong, Brushes.DarkGreen, offX + 12 + wid, basel + 5);
            } 
            g.DrawImage(HSIcons.GetIconsByEName("rac" + monster.MonsterConfig.Race), 60 + offX, basel - 40, 24, 24);
            g.DrawImage(HSIcons.GetIconsByEName("atr" + monster.MonsterConfig.Type), 88 + offX, basel - 40, 24, 24);
            g.DrawString(string.Format("Lv{0:00}", card.Level), fontsong, Brushes.Indigo, 13 + offX, basel + 22);
            g.DrawString(string.Format("({0}/{1})", card.Exp, ExpTree.GetNextRequiredCard(card.Level)), fontsong, Brushes.RoyalBlue, 44 + offX, basel + 22);
            g.DrawString("数据", fontblack, Brushes.White, 10 + offX, basel + 42);
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, 50, 0));
            g.DrawString(string.Format("攻击 {0,3:D}", monster.Atk), fontsong, sb, 10 + offX, basel + 61);
            PaintTool.DrawValueLine(g, monster.Atk/2, 70 + offX, basel + 62, 115, 10);
            g.DrawString(string.Format("防御 {0,3:D}", monster.Def), fontsong, sb, 10 + offX, basel + 76);
            PaintTool.DrawValueLine(g, monster.Def/2, 70 + offX, basel + 77, 115, 10);
            g.DrawString(string.Format("魔攻 {0,3:D}", monster.MAtk), fontsong, sb, 10 + offX, basel + 91);
            PaintTool.DrawValueLine(g, monster.MAtk / 2, 70 + offX, basel + 92, 115, 10);
            g.DrawString(string.Format("魔防 {0,3:D}", monster.MDef), fontsong, sb, 10 + offX, basel + 106);
            PaintTool.DrawValueLine(g, monster.MDef / 2, 70 + offX, basel + 107, 115, 10);
            g.DrawString(string.Format("命中 {0,3:D}", monster.Hit), fontsong, sb, 10 + offX, basel + 121);
            PaintTool.DrawValueLine(g, monster.Hit / 2, 70 + offX, basel + 122, 115, 10);
            g.DrawString(string.Format("回避 {0,3:D}", monster.Dhit), fontsong, sb, 10 + offX, basel + 136);
            PaintTool.DrawValueLine(g, monster.Dhit / 2, 70 + offX, basel + 137, 115, 10);
            g.DrawString(string.Format("技巧 {0,3:D}", monster.Skl), fontsong, sb, 10 + offX, basel + 151);
            PaintTool.DrawValueLine(g, monster.Skl / 2, 70 + offX, basel + 152, 115, 10);
            g.DrawString(string.Format("速度 {0,3:D}", monster.Spd), fontsong, sb, 10 + offX, basel + 166);
            PaintTool.DrawValueLine(g, monster.Spd / 2, 70 + offX, basel + 167, 115, 10);
            g.DrawString(string.Format("生命 {0,3:D}", monster.Hp), fontsong, sb, 10 + offX, basel + 181);
            PaintTool.DrawValueLine(g, monster.Hp / 5, 70 + offX, basel + 182, 115, 10);

            g.DrawString("技能", fontblack, Brushes.White, 10 + offX, basel + 202);
            int skillindex = 0;
            int skillbaseindex = 0;
            for (int i = 0; i < ConfigData.GetMonsterConfig(monster.Id).Skills.Count; i++)
            {
                int skillId = ConfigData.GetMonsterConfig(monster.Id).Skills[i].X;
                SkillConfig skillConfig = ConfigData.GetSkillConfig(skillId);
                if (SkillBook.IsBasicSkill(skillId))
                {
                    g.DrawString(skillConfig.Name, fontsong, Brushes.SlateBlue, 60 + skillbaseindex*30 + offX, basel + 204);
                    skillbaseindex++;
                }
                else
                {
                    g.DrawImage(SkillBook.GetSkillImage(skillId), 10 + 45 * skillindex + offX, basel + 223, 40, 40);
                    skillindex++;
                }
            }

            fontsong.Dispose();
            fontblack.Dispose();
            sb.Dispose();
        }

        internal override void DrawOnStateBar(Graphics g)
        {
            Font fontsong = new Font("宋体", 10, FontStyle.Regular);
            SolidBrush sb = new SolidBrush(Color.FromArgb(255, 255, 255));
            g.DrawImage(HSIcons.GetIconsByEName("abl1"), 0, 0);
            g.DrawString(monster.Atk.ToString().PadLeft(3, ' '), fontsong, sb, 22, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl2"), 50, 0);
            g.DrawString(monster.Def.ToString().PadLeft(3, ' '), fontsong, sb, 72, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl11"), 100, 0);
            g.DrawString(monster.MAtk.ToString().PadLeft(3, ' '), fontsong, sb, 122, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl12"), 150, 0);
            g.DrawString(monster.MDef.ToString().PadLeft(3, ' '), fontsong, sb, 172, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl4"), 200, 0);
            g.DrawString(monster.Hit.ToString().PadLeft(3, ' '), fontsong, sb, 222, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl6"), 250, 0);
            g.DrawString(monster.Dhit.ToString().PadLeft(3, ' '), fontsong, sb, 272, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl5"), 300, 0);
            g.DrawString(monster.Skl.ToString().PadLeft(3, ' '), fontsong, sb, 322, 4);
            g.DrawImage(HSIcons.GetIconsByEName("abl3"), 350, 0);
            g.DrawString(monster.Spd.ToString().PadLeft(3, ' '), fontsong, sb, 372, 4);

            g.DrawImage(HSIcons.GetIconsByEName("abl8"), 400, 0);
            g.DrawString(monster.Hp.ToString().PadLeft(3, ' '), fontsong, sb, 422, 4);
            int offx = 0;
            if (monster.MonsterConfig.Skills.Count > 0)
            {
                g.DrawImage(HSIcons.GetIconsByEName("abl9"), 450, 0);
                for (int i = 0; i < monster.MonsterConfig.Skills.Count; i++)
                {
                    int skillId = monster.MonsterConfig.Skills[i].X;
                    SkillConfig skillConfig = ConfigData.GetSkillConfig(skillId);
                    g.DrawString(skillConfig.Name, fontsong, SkillBook.IsBasicSkill(skillId) ? Brushes.LightGreen : Brushes.Gold, 472 + offx, 4);
                    offx += (int)g.MeasureString(skillConfig.Name, fontsong).Width + 5;
                }
            }

            fontsong.Dispose();
            sb.Dispose();
        }

        internal override Image GetPreview(CardPreviewType type, int[] parms)
        {
            const string stars = "★★★★★★★★★★";
            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine(monster.MonsterConfig.Name, "White", 20);
            tipData.AddTextNewLine(stars.Substring(10 - monster.MonsterConfig.Star), "Yellow", 20);
            tipData.AddLine();
            tipData.AddTextNewLine("种族/属性", "White");
            tipData.AddImage(HSIcons.GetIconsByEName("rac" + monster.MonsterConfig.Race));
            tipData.AddImage(HSIcons.GetIconsByEName("atr" + monster.MonsterConfig.Type));
            tipData.AddTextNewLine(string.Format("攻击 {0,3:D}  防御 {1,3:D}", monster.Atk, monster.Def), "Lime");
            tipData.AddTextNewLine(string.Format("命中 {0,3:D}  回避 {1,3:D}", monster.Hit, monster.Dhit), "Lime");
            tipData.AddTextNewLine(string.Format("速度 {0,3:D}  魔力 {1,3:D}", monster.Spd, monster.MAtk), "Lime");
            tipData.AddTextNewLine(string.Format("生命 {0,3:D}", monster.Hp), "Lime");
            tipData.AddLine();
            tipData.AddTextNewLine("技能", "White");
            for (int i = 0; i < monster.MonsterConfig.Skills.Count; i++)
            {
                int skillId = monster.MonsterConfig.Skills[i].X;
                if (!SkillBook.IsBasicSkill(skillId))
                {
                    tipData.AddImage(SkillBook.GetSkillImage(skillId));
                }
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
