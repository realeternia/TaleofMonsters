using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NarlonLib.Control;
using NarlonLib.Drawing;
using NarlonLib.Math;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.DataType.Decks;
using ConfigDatas;

namespace TaleofMonsters.Forms.MagicBook
{
    internal class CardDetail
    {
        private int x;
        private int y;
        private int height;
        private int cid;
        private int level;
        private int lastCell = -1;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private Card card;
        private UserControl userControl;
        private List<MonsterSkill> skillBases;
        private List<MonsterSkill> skills;

        public void SetInfo(DeckCard dcard)
        {
            cid = dcard.BaseId;
            level = dcard.Level;
            lastCell = -1;
            skillBases = new List<MonsterSkill>();
            skills = new List<MonsterSkill>();
            if (cid > 0)
            {
                card = CardAssistant.GetCard(cid);
                card.UpdateToLevel(dcard);
                if (card.GetCardType() == CardTypes.Monster)
                {
                    MonsterCard monsterCard = card as MonsterCard;
                    if (monsterCard != null)
                    {
                        for (int i = 0; i < ConfigData.GetMonsterConfig(monsterCard.CardId).Skills.Count; i++)
                        {
                            int skillId = ConfigData.GetMonsterConfig(monsterCard.CardId).Skills[i].X;
                            MonsterSkill monsterSkill = new MonsterSkill(skillId, ConfigData.GetMonsterConfig(monsterCard.CardId).Skills[i].Y, 0);
                            if (SkillBook.IsBasicSkill(skillId))
                            {
                                skillBases.Add(monsterSkill);
                            }
                            else
                            {
                                skills.Add(monsterSkill);
                            }
                        }
                    }
                }
            }
            tooltip.Hide(userControl);
        }

        public void SetInfo(int id)            
        {
            SetInfo(new DeckCard(1, id, 1, 0));
        }

        public CardDetail(UserControl control, int x, int y, int height)
        {
            card = SpecialCards.NullCard;
            userControl = control;
            this.x = x;
            this.y = y;
            this.height = height;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Thistle, x, y, 200, height);
            if (cid != -1)
            {
                card.DrawOnDeck(g, x, y, true);
                if (card.Quality > 0)//绘制勋章
                {
                    g.DrawImage(HSIcons.GetIconsByEName("mdl" + card.Quality), 148 + x, y +27, 24, 24);
                }
            }
        }

        public void CheckMouseMove(int mousex, int mousey)
        {
            if (cid == -1)
                return;

            int truex = mousex - x;
            int truey = mousey - y;
            bool isIn = false;
            int basel = 210;
            if (MathTool.IsPointInRegion(truex, truey, 148, 27, 172, 51, false) &&card.Quality>=1)
            {
                isIn = true;
                if (lastCell != 10)
                {
                    lastCell = 10;
                    Image image = GetCardQualityImage(card.Quality);
                    tooltip.Show(image, userControl, mousex, mousey);
                    return;
                }
            }
            if (card.GetCardType() == CardTypes.Monster)
            {
                MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(cid);
                if (MathTool.IsPointInRegion(truex, truey, 58, basel - 40, 82, basel - 16, false))
                {
                    isIn = true;
                    if (lastCell != 1)
                    {
                        lastCell = 1;
                        Image image = DrawTool.GetImageByString("种族："+HSTypes.I2Race(monsterConfig.Race), 100);
                        tooltip.Show(image, userControl, mousex, mousey);
                        return;
                    }
                }
                else if (MathTool.IsPointInRegion(truex, truey, 86, basel - 40, 110, basel - 16, false))
                {
                    isIn = true;
                    if (lastCell != 2)
                    {
                        lastCell = 2;
                        Image image = GetTerrainImage(monsterConfig.Type, monsterConfig.Tile);
                        tooltip.Show(image, userControl, mousex, mousey);
                        return;
                    }
                }
                else if (MathTool.IsPointInRegion(truex, truey, 8, basel + 223, 188, basel + 263, false))
                {
                    int thisCell = truex < 8 ? -1 : (truex - 8) / 45;
                    isIn = true;
                    if (lastCell!=thisCell)
                    {
                        lastCell = thisCell;
                        if (thisCell < skills.Count)
                        {
                            Image image = GetSkillDesImage(skills[thisCell].SkillId, skills[thisCell].Percent);
                            tooltip.Show(image, userControl, mousex, mousey);
                            return;
                        }
                    }
                }
                else if (MathTool.IsPointInRegion(truex, truey, 58, basel + 204, 238, basel + 219, false))
                {
                    int thisCell = truex < 58 ? -1 : (truex - 58)/30;
                    isIn = true;
                    if (lastCell != thisCell)
                    {
                        lastCell = thisCell;
                        if (thisCell < skillBases.Count)
                        {
                            Image image = GetSkillDesImage(skillBases[thisCell].SkillId, skillBases[thisCell].Percent);
                            tooltip.Show(image, userControl, mousex, mousey);
                            return;
                        }
                    }
                }
            }
            else if (card.GetCardType() == CardTypes.Weapon)
            {
                WeaponConfig weaponConfig = ConfigData.GetWeaponConfig(cid);
                if (MathTool.IsPointInRegion(truex, truey, 58, basel - 40, 82, basel - 16, false))
                {
                    isIn = true;
                    if (lastCell != 1)
                    {
                        lastCell = 1;
                        Image image = DrawTool.GetImageByString("类型：" + HSTypes.I2Attr(weaponConfig.Type), 100);
                        tooltip.Show(image, userControl, mousex, mousey);
                        return;
                    }
                }
                else if (MathTool.IsPointInRegion(truex, truey, 86, basel - 40, 110, basel - 16, false))
                {
                    isIn = true;
                    if (lastCell != 2)
                    {
                        lastCell = 2;
                        Image image = DrawTool.GetImageByString("属性："+HSTypes.I2Attr(weaponConfig.Attr), 100);
                        tooltip.Show(image, userControl, mousex, mousey);
                        return;
                    }
                }
                else if (MathTool.IsPointInRegion(truex, truey, 8, basel + 208, 53, basel + 248, false))
                {
                    isIn = true;
                    int thisCell = truex < 8 ? -1 : (truex - 8) / 45;
                    if (lastCell != thisCell)
                    {
                        lastCell = thisCell;
                        if (weaponConfig.SkillId != 0)
                        {
                            Image image = GetSkillDesImage(weaponConfig.SkillId, weaponConfig.Percent);
                            tooltip.Show(image, userControl, mousex, mousey);
                            return;
                        }
                    }
                }
            }
            else if (card.GetCardType() == CardTypes.Spell)
            {
                SpellConfig spellConfig = ConfigData.GetSpellConfig(cid);
                if (MathTool.IsPointInRegion(truex, truey, 58, basel - 40, 82, basel - 16, false))
                {
                    isIn = true;
                    if (lastCell != 1)
                    {
                        lastCell = 1;
                        Image image = DrawTool.GetImageByString("类型：" + HSTypes.I2Attr(spellConfig.Type), 100);
                        tooltip.Show(image, userControl, mousex, mousey);
                        return;
                    }
                }
                else if (MathTool.IsPointInRegion(truex, truey, 86, basel - 40, 110, basel - 16, false))
                {
                    isIn = true;
                    if (lastCell != 2)
                    {
                        lastCell = 2;
                        Image image = DrawTool.GetImageByString("属性：" + HSTypes.I2Attr(spellConfig.Attr), 100);
                        tooltip.Show(image, userControl, mousex, mousey);
                        return;
                    }
                }
            }
            if (!isIn && lastCell != -1)
            {
                lastCell = -1;
                tooltip.Hide(userControl);
            }
        }

        private Image GetSkillDesImage(int sid, int rate)
        {
            Skill skl = new Skill(sid);
            skl.UpgradeToLevel(level);

            string headtext = skl.Name;
            if (rate != 100)
                headtext = string.Format("{0}({1}%发动)", skl.Name, rate);
            string desc = skl.Descript;
            //foreach (int buffId in skl.SkillConfig.Buff)
            //{
            //    if (buffId > 0)
            //    {
            //        Buff buffdata = new Buff(buffId);
            //        buffdata.UpgradeToLevel(level);
            //        desc += string.Format("$ >>{0}:{1}", buffdata.BuffConfig.Name, buffdata.Descript);
            //    }
            //}todo 补buff说明
            return DrawTool.GetImageByString(headtext, desc, 200, Color.LightBlue);
        }

        internal Image GetTerrainImage(int monType, int type)
        {
            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine("属性："+HSTypes.I2Attr(monType), "White", 20);
            tipData.AddLine();
            tipData.AddTextNewLine("弱化：", "White");
            for (int i = 1; i <= 8; i++)
            {
                int tp = 1 << i - 1;
                if ((type & tp) != tp)
                {
                    tipData.AddImage(HSIcons.GetIconsByEName("atd" + i));
                }
            }
            return tipData.Image;
        }

        internal Image GetCardQualityImage(int quality)
        {
            ControlPlus.TipImage tipData = new ControlPlus.TipImage();
            tipData.AddTextNewLine("稀有度：" + HSTypes.I2CardQuality(quality), "White", 20);
            tipData.AddLine();
            tipData.AddTextNewLine(string.Format("  全属性提升{0}%", quality*6), "Lime");
            return tipData.Image;
        }
    }
}
