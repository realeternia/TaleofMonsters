using System;
using System.Collections.Generic;
using System.Drawing;
using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemEffect;
using TaleofMonsters.Controler.Battle.Data.MemFlow;
using TaleofMonsters.Controler.Battle.Data.MemMap;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.CardPieces;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Weapons;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Effects;
using TaleofMonsters.DataType.Equips.Addons;
using TaleofMonsters.DataType.Others;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Core;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Loader;
using NarlonLib.Core;

namespace TaleofMonsters.Controler.Battle.Data.MemMonster
{
    internal class LiveMonster:IMonster
    {
        private PlayerIndex pPos;
        private int action;
        private int timeGo;
        private int life;
        private int oldLife;
        private int level;
        private int id; //唯一的id
        private int lastDamagerId;
        private int ghostTime;
        private Point position;
        private Monster avatar;

        private AttrModifyData atk;
        private AttrModifyData def;
        private AttrModifyData matk;
        private AttrModifyData mdef;
        private AttrModifyData hit;
        private AttrModifyData dhit;
        private AttrModifyData spd;
        private AttrModifyData skl;
        private int pDamageAbsorb;
        private int cardManaRateW;//武器卡的魔法消耗比例
        private int cardManaRateS;//魔法卡的魔法消耗比例

        private TrueWeapon weapon;
        private WeaponAttrRate weaponRate;

        private int attackType;
        private int ats;
        private Dictionary<int, MemBaseBuff> buffs;
        private List<IMonsterAuro> auroList;//光环
        private int tileBuff;//地形技能
        private SimpleSet<int> immuneBuffs;
        private List<MemBaseSkill> skills;
        private SimpleSet<SkillMarks> specialMarks;
        private int[] antiMagic;//魔法抗性

        private bool dropAdd;
        private int hpReg;
        private int magicRatePlus;
        private int tileEffectPlus;

        private Point targetPosition; //攻击目标位置

        private HpBar hpBar;

        #region 属性
        public int Id
        {
            get { return id; }
        }

        public int Level
        {
            get { return level; }
        }

        public Monster Avatar
        {
            get { return avatar; }
        }

        public int Life
        {
            get { return Math.Max(life, 0); }
            set { life = value; if (life > avatar.Hp) life = avatar.Hp;
            hpBar.Rate = life * 100 / avatar.Hp;                
            }
        }

        public int LossLife
        {
            get { int value = oldLife - life; oldLife = life; return value; }
        }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public Point CenterPosition
        {
            get { return new Point(position.X + 40, position.Y + 40); }
        }

        public int Xoff
        {
            get { return Math.Abs(position.X - 400)/100 - 1; }
        }

        public int Action
        {
            get { return action; }
            set { action = Math.Max(value, 0); }
        }

        public PlayerIndex PPos
        {
            get { return OwnerPlayer.PPos; }
        }

        public bool IsAlive
        {
            get { return life > 0; }
        }

        public Dictionary<int, MemBaseBuff> Buffs
        {
            get { return buffs; }
        }

        public List<MemBaseSkill> Skills
        {
            get { return skills; }
        }

        public TrueWeapon TWeapon
        {
            get { return weapon; }
            set { weapon = value; }
        }

        public int RealAtk
        {
            get
            {
                float diff = (atk.Source + atk.Adder) * (1 + atk.Multiter) - atk.Source;
                if (HasSpecialMark(SkillMarks.Atk_Def_Bonus))
                {
                    diff = diff * 3 / 2;
                }
                return Math.Max((int)(atk.Source + diff), 0);
            }
        }

        public int RealDef
        {
            get
            {
                float diff = (def.Source + def.Adder) * (1 + def.Multiter) - def.Source;
                if (HasSpecialMark(SkillMarks.Atk_Def_Bonus))
                {
                    diff = diff * 3 / 2;
                }
                return Math.Max((int)(def.Source + diff), 0);
            }
        }

        public int RealMAtk
        {
            get
            {
                return (int)Math.Max((matk.Source + matk.Adder) * (1 + matk.Multiter) * (100 + magicRatePlus) / 100, 0);
            }
        }

        public int RealMDef
        {
            get
            {
                float diff = (mdef.Source + mdef.Adder) * (1 + mdef.Multiter) - mdef.Source;
                return Math.Max((int)(mdef.Source + diff), 0);
            }
        }

        public int RealHit
        {
            get
            {
                if (HasSpecialMark(SkillMarks.Lock_Hit))
                {
                    return (int)Math.Min((hit.Source + hit.Adder) * (1 + hit.Multiter), hit.Source);
                }
                return (int)Math.Max((hit.Source + hit.Adder) * (1 + hit.Multiter), 0);
            }
        }

        public int RealDhit
        {
            get
            {
                if (HasSpecialMark(SkillMarks.Lock_Dhit))
                {
                    return (int)Math.Min((dhit.Source + dhit.Adder) * (1 + dhit.Multiter), dhit.Source);
                }
                return (int)Math.Max((dhit.Source + dhit.Adder) * (1 + dhit.Multiter), 0);
            }
        }

        public int RealSkl
        {
            get { return (int)((skl.Source + skl.Adder) * (1 + skl.Multiter)); }
        }

        public int RealSpd
        {
            get { return (int)((spd.Source + spd.Adder) * (1 + spd.Multiter)); }
        }

        public string Arrow
        {
            get
            {
                if (weapon.CardId==0 || weapon.Avatar.WeaponConfig.Arrow == "null")
                    return avatar.MonsterConfig.Arrow;
                return weapon.Avatar.WeaponConfig.Arrow;
            }
        }

        public bool IsHero
        {
            get { return avatar.MonsterConfig.Race == (int)MonsterRaces.Hero; }
        }

        public Player OwnerPlayer
        {
            get { return pPos == PlayerIndex.Player1 ? PlayerManager.LeftPlayer : PlayerManager.RightPlayer; }
        }

        public IPlayer Owner
        {
            get { return pPos == PlayerIndex.Player1 ? PlayerManager.LeftPlayer : PlayerManager.RightPlayer; }
        }

        public Player Rival
        {
            get { return pPos == PlayerIndex.Player1 ? PlayerManager.RightPlayer : PlayerManager.LeftPlayer; }
        }

        public int HpReg
        {
            get { return hpReg; }
            set { hpReg = value; }
        }

        public int MagicRatePlus
        {
            get { return magicRatePlus; }
            set { magicRatePlus = value; }
        }

        public int TileEffectPlus
        {
            get { return tileEffectPlus; }
            set { tileEffectPlus = value; }
        }

        public int GhostTime
        {
            get { return ghostTime; }
            set { ghostTime = value; }
        }

        public bool IsGhost
        {
            get { return ghostTime > 0; }
        }

        public Point TargetPosition
        {
            get { return targetPosition; }
            set { targetPosition = value; }
        }
        #endregion

        public LiveMonster(DeckCard card, Monster mon, Point point, PlayerIndex pos)
        {
            hpBar = new HpBar();

            id = card.Id;
            level = card.Level;
            avatar = mon;
            if (avatar.MonsterConfig.Race != (int)MonsterRaces.Hero)
                avatar.UpgradeToLevel(card.Level, card.Quality);          
            Life = avatar.Hp;
            oldLife = life;
            position = point;
            pPos = pos;
            ats = 10;
            action = 0;
            timeGo = 0;

            weapon = new TrueWeapon();
            attackType = (int)MonsterAttrs.None;

            SetBasicData();

            TargetPosition = new Point(-1, -1);//清除位置标记
        }

        private void SetBasicData()
        {
            buffs = new Dictionary<int, MemBaseBuff>();
            auroList = new List<IMonsterAuro>();
            skills = SkillAssistant.GetMemSkillDataForMonster(this);
            immuneBuffs = new SimpleSet<int>();
            antiMagic = new int[8];
            specialMarks = new SimpleSet<SkillMarks>();

            atk = new AttrModifyData(avatar.Atk);
            def = new AttrModifyData(avatar.Def);
            matk = new AttrModifyData(avatar.MAtk);
            mdef = new AttrModifyData(avatar.MDef);
            hit = new AttrModifyData(avatar.Hit);
            dhit = new AttrModifyData(avatar.Dhit);
            spd = new AttrModifyData(avatar.Spd);
            skl = new AttrModifyData(avatar.Skl);

            SkillAssistant.CheckInitialEffect(this);

            if (avatar.MonsterConfig.Type != (int)MonsterRaces.Hero)
                EAddonBook.UpdateMonsterData(this, OwnerPlayer.State.monsterskills.Keys(), OwnerPlayer.State.monsterskills.Values());
        }

        public bool BeHited(LiveMonster src)
        {
            SkillAssistant.CheckBurst(src, this);
            int hitrate = SkillAssistant.GetHit(src, this);
            if (hitrate > MathTool.GetRandom(100))
            {
                HitDamage damage;
                damage = SkillAssistant.GetDamage(src, this);
                CheckDamageBuffEffect(src, damage);
                Life -= damage.Value;
                SkillAssistant.CheckHitEffectAfter(src, this, damage);
                if (damage.Value > 0)
                {
                    CheckRecoverOnHit();
                    lastDamagerId = src.Id;
                }
                return true;
            }
            return false;
        }

        public void Dying()
        {
            ghostTime = 1;
            targetPosition = new Point(-1, -1);
            BattleInfo.Instance.MemMap.GetMouseCell(position.X,position.Y).UpdateOwner(-id);
            if (avatar.MonsterConfig.Race == (int)MonsterRaces.Hero)
            {
                if (pPos == PlayerIndex.Player2)
                {
                    if (Rival is HumanPlayer)
                    {
                        UserProfile.Profile.OnKillMonster(avatar.MonsterConfig.Star, avatar.MonsterConfig.Race, avatar.MonsterConfig.Type);
                    }
                    BattleInfo.Instance.LeftHeroKill++;
                }
                else
                {
                    BattleInfo.Instance.RightHeroKill++;
                }
            }
            else
            {
                if (Rival.HasBuff(BuffEffectTypes.UnitDieMana))
                    Rival.Mana += Rival.MaxMana*3/100;

                if (pPos == PlayerIndex.Player2)
                {
                    if (Rival is HumanPlayer)
                    {
                        int itemId = CardPieceBook.CheckPiece(avatar.Id);
                        if (itemId == 0 && dropAdd)
                        {
                            itemId = CardPieceBook.CheckPiece(avatar.Id);
                        }
                        if (itemId > 0)
                        {
                            BattleInfo.Instance.AddItemGet(itemId);
                            UserProfile.InfoBag.AddItem(itemId, 1);
                            FlowWordQueue.Instance.Add(new FlowItemInfo(itemId, position, 20, 50), true);
                        }
                        UserProfile.Profile.OnKillMonster(Avatar.MonsterConfig.Star, Avatar.MonsterConfig.Race, Avatar.MonsterConfig.Type);
                    }
                    BattleInfo.Instance.LeftKill++;
                }
                else
                    BattleInfo.Instance.RightKill++;
            }

            if (lastDamagerId != 0)
            {
                int expGet = (int)Math.Sqrt(level * avatar.MonsterConfig.Star) / 2 + 1; //杀死怪物经验
                OwnerPlayer.AddBottleExp(expGet);
                FlowWordQueue.Instance.Add(new FlowExpInfo(expGet, Position, 20, 50), true);

                Rival.AddExtraInfo(PlayerExtraInfo.Soul, (int) Math.Sqrt(level*avatar.MonsterConfig.Star) + 2); //杀死怪物获得魂
            }
        }

        public void Next(TileMatchResult tileMatching)//附带判断地形因素
        {
            timeGo++;
            hpBar.Update();
            if ((timeGo % 4) == 3)
            {
                SkillAssistant.CheckAuroState(this, tileMatching);
            }
            if ((timeGo % 100) == 0)//一回合
            {
                int lpuse = Math.Min(2, Math.Min(OwnerPlayer.GetExtraInfo(PlayerExtraInfo.Nature),avatar.Hp == Life ? 0 : (avatar.Hp - Life)/10+1));
                if (lpuse > 0) //命造成的生命回复
                {
                    Life += lpuse * 200;
                    OwnerPlayer.AddExtraInfo(PlayerExtraInfo.Nature, -lpuse);
                }
                Life += hpReg;
            }
        }

        public bool Attack(bool hasAttack)
        {
            buffCount();
            if (HasBuff(BuffEffectTypes.NoAttack) || RealAtk == 0)
                return false;
            action += Ats;
            if (action >= 1000)
            {
                action = action - 1000 + MathTool.GetRandom(avatar.Spd);
                return !hasAttack;
            }
            return false;
        }

        public void CheckMagicDamage(HitDamage damage)
        {
            if (damage.Element>0&&antiMagic[damage.Element-1]>0)
            {
                damage.SetDamage(DamageTypes.Magic,Math.Max(damage.Value*(100 - antiMagic[damage.Element - 1])/100,0));
            }
        }

        private void buffCount()
        {
            List<int> toDelete = new List<int>();
            foreach (MemBaseBuff buff in buffs.Values)
            {
                buff.TimeLeft--;
                buff.TimeGo++;
                if ((buff.TimeGo % 100) == 99)
                    buff.OnRoundEffect(this);
                if (buff.TimeLeft <= 0)
                    toDelete.Add(buff.Id);
            }

            if (toDelete.Count>0)
            {
                foreach (int buffId in toDelete)
                {
                    buffs[buffId].OnRemoveBuff(this);
                    buffs.Remove(buffId);
                }
                toDelete.Clear();
            }
        }

        public void AddWeapon(DeckCard card)
        {
            if (weapon.CardId > 0)
                WeaponAssistant.CheckWeaponEffect(this, weapon, -1);
            Weapon wpn = new Weapon(card.BaseId);
            wpn.UpgradeToLevel(card.Level, card.Quality);
            weapon = new TrueWeapon(card, wpn);
            weapon.CheckWeaponRate(weaponRate);
            EAddonBook.UpdateWeaponData(weapon, OwnerPlayer.State.weaponskills.Keys(), OwnerPlayer.State.weaponskills.Values());
            WeaponAssistant.CheckWeaponEffect(this, weapon, 1);
        }

        public void DeleteWeapon()
        {
            WeaponAssistant.CheckWeaponEffect(this, weapon, -1);
            weapon = new TrueWeapon();
        }

        public bool HasBuff(BuffEffectTypes type)
        {
            foreach (MemBaseBuff buff in buffs.Values)
            {
                if (BuffBook.HasEffect(buff.Id,type))
                {
                    return true;
                }
            }
            return false;
        }

        public void DelBuff(BuffEffectTypes type)
        {
            foreach (MemBaseBuff buff in buffs.Values)
            {
                if (BuffBook.HasEffect(buff.Id, type))
                {
                    buffs.Remove(buff.Id);
                    return;
                }
            }            
        }

        private void CheckRecoverOnHit()
        {
            foreach (MemBaseBuff buff in buffs.Values)
            {
                if (buff.BuffConfig.EndOnHit)
                {
                    buff.TimeLeft = 0;
                }
            }            
        }

        public void CheckDamageBuffEffect(LiveMonster src, HitDamage dam)
        {
            if (dam.Dtype == DamageTypes.Physical)
            {
                if (pDamageAbsorb != 0)
                {
                    dam.SetDamage(DamageTypes.Physical, dam.Value * Math.Max(100 - pDamageAbsorb, 0) / 100);
                }

                if (HasBuff(BuffEffectTypes.Chaos) && MathTool.GetRandom(100) < 30)
                {
                    src.Life -= dam.Value;
                    dam.SetDamage(DamageTypes.Physical, 0);
                }
            }

        }

        public void CheckAuroEffect()
        {
            foreach (IMonsterAuro auro in auroList)
            {
                auro.CheckAuroState();
            }
        }

        public void Revive()
        {
            ghostTime = 0;
            BattleInfo.Instance.MemMap.GetMouseCell(position.X, position.Y).UpdateOwner(id);
            Life++;
        }

        public void AddSkill(int sid, int slevel, int rate)
        {
            foreach (MemBaseSkill memSkill in skills)
            {
                if (memSkill.SkillId == sid)
                {
                    skills.Remove(memSkill);
                    break;
                }
            }

            Skill skill = new Skill(sid);
            skill.UpgradeToLevel(slevel);
            MemBaseSkill skillbase = new MemBaseSkill(skill, rate);
            skillbase.Level = slevel;
            skillbase.Self = this;
            skillbase.CheckInitialEffect();
            skills.Add(skillbase);
        }

        public void RemoveSkill(int sid)
        {
            foreach (MemBaseSkill memSkill in skills)
            {
                if (memSkill.SkillId == sid)
                {
                    skills.Remove(memSkill);
                    break;
                }
            }
        }

        public void AddSpecialMark(SkillMarks mark)
        {
            if (!HasSpecialMark(mark))
                specialMarks.Add(mark);
        }

        public bool HasSpecialMark(SkillMarks mark)
        {
            return specialMarks.Has(mark);
        }

		public void AddStrengthLevel(int value)
		{
			int basedata = value * MathTool.GetSqrtMulti10(avatar.MonsterConfig.Star);
		    Atk += (double) basedata/10;
		    Def += (double) basedata/10;
			MaxHp=avatar.Hp + 3 * basedata / 10;
		}

        public void Draw(Graphics g2, Color uponColor)
        {
            Bitmap image = new Bitmap(100, 100);
            Graphics g = Graphics.FromImage(image);

            if (!IsGhost)
            {
                g.DrawImage(avatar.MonsterConfig.Race == (int)MonsterRaces.Hero ? OwnerPlayer.HeroImage : MonsterBook.GetMonsterImage(avatar.Id, 100, 100), 0, 0, 100, 100);
                if (uponColor != Color.White)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(150, uponColor));
                    g.FillRectangle(brush, 0, 0, 100, 100);
                    brush.Dispose();
                }
                Pen pen;
                if (targetPosition.X >= 0 && targetPosition.Y >= 0)//攻击中
                {
                    pen = new Pen(Brushes.Gold, 3);
                }
                else
                {
                    pen = new Pen(pPos == PlayerIndex.Player2 ? Brushes.Blue : Brushes.Red, 3);
                }
                Font font = new Font("Arial", 7, FontStyle.Regular);
                Font font2 = new Font("Arial", 6, FontStyle.Regular);
                Font fontLevel = new Font("Arial", 10, FontStyle.Bold);
                g.DrawRectangle(pen, 1, 1, 98, 98);
                pen.Dispose();

                hpBar.Draw(g);

                g.FillPie(Brushes.Gray, 75, 15, 20, 20, 0, 360);
                g.FillPie(Brushes.Yellow, 75, 15, 20, 20, 0, action * 9 / 25);

                const string stars = "★★★★★★★★★★";
                g.DrawString(stars.Substring(10 - avatar.MonsterConfig.Star), font2, Brushes.Yellow, 0, 0);
#if DEBUG
                g.DrawString(id.ToString(), font, Brushes.White, 0, 20);
#endif
                g.DrawString("AT", font, Brushes.Red, 0, 81);
                g.FillRectangle(Brushes.Red, 15, 84, avatar.Atk * 2, 5);
                g.DrawString("DF", font, Brushes.Blue, 0, 88);
                g.FillRectangle(Brushes.Blue, 15, 91, avatar.Def * 4, 5);
                g.DrawString(level.ToString(), fontLevel, Brushes.DarkBlue, level < 10 ? 81 : 77, 18);
                g.DrawString(level.ToString(), fontLevel, Brushes.LightPink, level<10? 80:76,17);
                font.Dispose();
                font2.Dispose();
                fontLevel.Dispose();

                if (weapon.CardId>0)
                    g.DrawImage(weapon.GetImage(16, 16), 5, 30, 16, 16);
                int index = 0;
                MemBaseBuff[] copybuffs = new MemBaseBuff[buffs.Count];
                buffs.Values.CopyTo(copybuffs, 0);
                foreach (MemBaseBuff attr in copybuffs)
                {
                    g.DrawImage(BuffBook.GetBuffImage(attr.Id), new Rectangle(5 + 18 * index, 50, 16, 16), new Rectangle(((attr.TimeGo / 3) % 2) * 16, 0, 16, 16), GraphicsUnit.Pixel);
                    index++;
                }
            }
            else
            {
                Image img = PicLoader.Read("System", "Rip.PNG");
                g.DrawImage(img, 19, 11, 63, 78);
                img.Dispose();

                g.FillRectangle(Brushes.Red, 0, 2, 100, 5);
                g.FillRectangle(Brushes.Cyan, 0, 2, Math.Min(ghostTime/3, 100), 5);
            }

            g.Dispose();
            int size = BattleInfo.Instance.MemMap.CardSize;
            if (targetPosition.X >= 0 && targetPosition.Y >= 0)//攻击中
            {
                g2.DrawImage(image, new Rectangle(targetPosition.X, targetPosition.Y, 70, 70), 0, 0, 100, 100, GraphicsUnit.Pixel);
            }
            else
            {
                g2.DrawImage(image, new Rectangle(position.X, position.Y, size, size), 0, 0, 100, 100, GraphicsUnit.Pixel);
            }
            image.Dispose();
        }

        /// <summary>
        /// 模拟tooltip实现卡片的点击说明
        /// </summary>
        public void DrawCardToolTips(Graphics g)
        {
            Font fontsong = new Font("宋体", 9, FontStyle.Regular);
            Font fontsong2 = new Font("宋体", 10, FontStyle.Bold);

            int size = BattleInfo.Instance.MemMap.CardSize;
            int stagewid = BattleInfo.Instance.MemMap.StageWidth;
            int stageheg = BattleInfo.Instance.MemMap.StageHeight;
            int x = position.X + size;
            int y = position.Y;
            int wid = 180, heg = 113;
            List<MonsterTipData> datas = new List<MonsterTipData>();
            for (int i = 0; i < skills.Count; i++)
            {
                string tp = string.Format("{0}:{1}{2}", skills[i].SkillInfo.Name, skills[i].SkillInfo.Descript, skills[i].Percent == 100 ? "" : string.Format("({0}%)", skills[i].Percent));
                if (tp.Length > 22)
                {
                    wid = Math.Max(wid, (int)g.MeasureString(tp.Substring(0, 20), fontsong).Width + 18);
                    datas.Add(new MonsterTipData("Skill", skills[i].SkillId, tp.Substring(0, 20)));
                    datas.Add(new MonsterTipData("Skill2", skills[i].SkillId, "     " + tp.Substring(20)));
                }
                else
                {
                    wid = Math.Max(wid, (int)g.MeasureString(tp, fontsong).Width + 18);
                    datas.Add(new MonsterTipData("Skill", skills[i].SkillId, tp));
                }
            }
            if (weapon.CardId>0)
            {
                string tp = string.Format("{0}:{1}", weapon.Avatar.WeaponConfig.Name, weapon.Avatar);
                datas.Add(new MonsterTipData("Weapon", weapon.CardId, tp));
                wid = Math.Max(wid, (int)g.MeasureString(tp, fontsong).Width + 18);
            }
            if (!IsGhost)//鬼不显示buff
            {
                MemBaseBuff[] memBasebuffInfos = new MemBaseBuff[buffs.Count];
                buffs.Values.CopyTo(memBasebuffInfos, 0);
                foreach (MemBaseBuff buffdata in memBasebuffInfos)
                {
                    Buff buff = buffdata.BuffInfo;
                    string tp = "";
                    if (buff.BuffConfig.Type[1] == 's')
                        tp = string.Format("{0}(剩余{1:0.0}回合)", buff.BuffConfig.Name, ((double)buffdata.TimeLeft) / 100);
                    else if (buff.BuffConfig.Type[1] == 'a')
                        tp = string.Format("{0}({1})", buff.BuffConfig.Name, buff.Descript);
                    datas.Add(new MonsterTipData("Buff", buff.Id, tp));
                    wid = Math.Max(wid, (int)g.MeasureString(tp, fontsong).Width + 18);
                }
            }

            heg += datas.Count * 16;

            if (x + wid > stagewid)
                x = position.X - wid;
            if (y + heg > stageheg)
                y = stageheg - heg - 1;

            g.FillRectangle(Brushes.Black, x, y, wid, heg);
            g.FillRectangle(Brushes.DimGray, x, y, wid, 18);

            g.DrawRectangle(Pens.White, x, y, wid, heg);
            g.DrawString(string.Format("Lv{1}{0}({2}属性{3})", avatar.MonsterConfig.Name, level, HSTypes.I2Attr(avatar.MonsterConfig.Type), HSTypes.I2Race(avatar.MonsterConfig.Race)), fontsong2, Brushes.White, x + 3, y + 3);
            g.DrawString(string.Format("攻击 {0,3:D}", avatar.Atk), fontsong, Brushes.White, x + 3, y + 19);
            int temp;
            if (attackType == (int)MonsterAttrs.None)
            {
                temp = RealAtk - avatar.Atk;
                if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 55, y + 19);
                else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 55, y + 19);
            }
            else //魔法攻击时的显示
            {
                g.DrawLine(Pens.Red, x + 34, y + 32, x + 54, y + 20);
                g.DrawString(string.Format("={0,2:D}", RealAtk), fontsong, Brushes.Cyan, x + 55, y + 19);
            }
            g.DrawString(string.Format("防御 {0,3:D}", avatar.Def), fontsong, Brushes.White, x + 93, y + 19);
            temp = RealDef - avatar.Def;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 145, y + 19);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 145, y + 19);

            g.DrawString(string.Format("魔攻 {0,3:D}", avatar.MAtk), fontsong, Brushes.White, x + 3, y + 35);
            temp = RealMAtk - avatar.MAtk;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 55, y + 35);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 55, y + 35);
            g.DrawString(string.Format("魔防 {0,3:D}", avatar.MDef), fontsong, Brushes.White, x + 93, y + 35);
            temp = RealMDef - avatar.MDef;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 145, y + 35);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 145, y + 35);

            g.DrawString(string.Format("命中 {0,3:D}", avatar.Hit), fontsong, Brushes.White, x + 3, y + 51);
            temp = RealHit - avatar.Hit;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 55, y + 51);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 55, y + 51);
            g.DrawString(string.Format("回避 {0,3:D}", avatar.Dhit), fontsong, Brushes.White, x + 93, y + 51);
            temp = RealDhit - avatar.Dhit;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 145, y + 51);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 145, y + 51);

            g.DrawString(string.Format("速度 {0,3:D}", avatar.Spd), fontsong, Brushes.White, x + 3, y + 67);
            temp = RealSpd - avatar.Spd;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 55, y + 67);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 55, y + 67);
            g.DrawString(string.Format("技巧 {0,3:D}", avatar.Skl), fontsong, Brushes.White, x + 93, y + 67);
            temp = RealSkl - avatar.Skl;
            if (temp > 0) g.DrawString(string.Format("+{0,2:D}", temp), fontsong, Brushes.Lime, x + 145, y + 67);
            else if (temp < 0) g.DrawString(string.Format("{0,2:D}", temp), fontsong, Brushes.Red, x + 145, y + 67);

            g.DrawString("生命值", fontsong, Brushes.White, x + 3, y + 83);
            g.DrawString(string.Format("{0} / {1}", Life, avatar.Hp), fontsong, Life == avatar.Hp ? Brushes.Lime : Brushes.Red, x + 48, y + 83);

            g.DrawString("限制", fontsong, Brushes.White, x + 3, y + 99);
            int va = 1;
            for (int i = 1; i <= 8; i++)
            {
                int tp = 1 << i - 1;
                if ((avatar.MonsterConfig.Tile & tp) != tp)
                {
                    g.DrawImage(HSIcons.GetIconsByEName("atd" + i), x + 20 + (va++) * 16, y + 98, 15, 15);
                }
            }

            int index = 0;
            foreach (MonsterTipData monsterTipData in datas)
            {
                if (monsterTipData.type == "Skill")
                {
                    int skillid = monsterTipData.key;
                    bool isBasic = SkillBook.IsBasicSkill(skillid);
                    if (!isBasic)
                        g.DrawImage(SkillBook.GetSkillImage(skillid), x + 3, y + 113 + index * 16, 16, 16);
                    g.DrawString(monsterTipData.content, fontsong, isBasic ? Brushes.LightGreen : Brushes.LightSalmon, x + 20, y + 115 + index * 16, StringFormat.GenericTypographic);
                }
                else if (monsterTipData.type == "Skill2")
                {
                    int skillid = monsterTipData.key;
                    bool isBasic = SkillBook.IsBasicSkill(skillid);
                    g.DrawString(monsterTipData.content, fontsong, isBasic ? Brushes.LightGreen : Brushes.LightSalmon, x + 20, y + 115 + index * 16, StringFormat.GenericTypographic);
                }
                else if (monsterTipData.type == "Weapon")
                {
                    g.FillRectangle(Brushes.DarkBlue, x+1, y + 113 + index * 16, wid-1, 16);
                    g.DrawImage(weapon.GetImage(16, 16), x + 3, y + 113 + index * 16, 16, 16);
                    g.DrawString(monsterTipData.content, fontsong, Brushes.LightBlue, x + 20, y + 115 + index * 16, StringFormat.GenericTypographic);                   
                }
                else if (monsterTipData.type == "Buff")
                {
                    int buffid = monsterTipData.key;
                    BuffConfig buff = buffs[buffid].BuffConfig;
                    g.FillRectangle(Brushes.DarkSlateGray, x+1, y + 113 + index * 16, wid-1, 16);
                    g.DrawImage(BuffBook.GetBuffImage(buffid), new Rectangle(x + 3, y + 113 + index * 16, 16, 16), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
                    g.DrawString(monsterTipData.content, fontsong, buff.Type[0] == 'n' ? Brushes.Red : Brushes.Lime, x + 20, y + 115 + index * 16, StringFormat.GenericTypographic);
                }
                index++;
            }
            fontsong.Dispose();
            fontsong2.Dispose();
            datas.Clear();
        }

        #region IMonster 成员

        public int CardId
        {
            get { return avatar.Id; }
        }

        public int WeaponId
        {
            get { return weapon.CardId; }
        }

        public void AddHp(double addon)
        {
            Life += (int)addon;
        }

        public void AddHpRate(double value)
        {
            Life += (int)(MaxHp * value);
        }

        public void AddAuro(int buff, int lv, int ele, int rac, int ran, int tar)
        {
            auroList.Add(new MonsterCommonAuro(this, buff, lv, ele, rac, ran, tar));
        }

        public void AddAuro(int buff, int lv, int mid)
        {
            auroList.Add(new MonsterSpecificAuro(this, buff, lv, mid));
        }

        public void AddAntiMagic(string type, int value)
        {
            if (type=="All")
            {
                for (int i = 0; i < antiMagic.Length; i++)
                {
                    antiMagic[i] += value;
                }
            }
            else
            {
                antiMagic[(int)Enum.Parse(typeof(MonsterAttrs), type) - 1] += value;    
            }            
        }

        public void AddBuff(int buffId, int blevel, int dura)
        {
            if (immuneBuffs.Has(buffId))
            {
                return;
            }

            if (BuffBook.IsImmune(buffId, Avatar.MonsterConfig.Race))
            {
                return;
            }

            if (buffs.ContainsKey(buffId))
            {
                buffs[buffId].TimeLeft = Math.Max(buffs[buffId].TimeLeft, dura);
            }
            else
            {
                Buff buffdata = new Buff(buffId);
                buffdata.UpgradeToLevel(blevel);
                MemBaseBuff buff = new MemBaseBuff(buffdata, dura);
                buff.Level = blevel;
                //buff.CheckBuffEffect(this, 1);
                buff.OnAddBuff(this);
                buffs.Add(buffId, buff);
            }
        }

        public void AddItem(int itemId)
        {
            if (OwnerPlayer is HumanPlayer)
            {
                BattleInfo.Instance.AddItemGet(itemId);
                UserProfile.InfoBag.AddItem(itemId, 1);
                FlowWordQueue.Instance.Add(new FlowItemInfo(itemId, Position, 20, 50), true);
            }
        }

        public void Transform(int monId)
        {
            DeckCard card = weapon.Card;
            DeleteWeapon();
            int lifp = Life * 100 / avatar.Hp;

            OwnerPlayer.State.CheckMonsterEvent(false, avatar);
            avatar = new Monster(monId);
            OwnerPlayer.State.CheckMonsterEvent(true, avatar);
            SetBasicData();            
            if (card != null && card.BaseId > 0)
            {
                AddWeapon(card);
            }
            Life = avatar.Hp * lifp / 100;
        }

        public void DeleteRandomCard()
        {
            OwnerPlayer.DeleteRandomCardFor(null);
        }

        public void AddCardRate(int monId, int rate)
        {
            if (OwnerPlayer is HumanPlayer)
            {
                BattleInfo.Instance.AddCardRate(monId, rate);
            }
        }

        public void AddResource(int type, int count)
        {
            OwnerPlayer.AddResource((GameResourceType)type, count);
            FlowWordQueue.Instance.Add(new FlowResourceInfo(type + 1, count, position, 20, 50), false);
        }

        public void AddActionRate(double value)
        {
            Action += (int)(SysConstants.BattleActionLimit*value);
        }

        public void StealWeapon(IMonster target)
        {
            if (target is LiveMonster)
            {
                AddWeapon((target as LiveMonster).weapon.Card);
                target.BreakWeapon();
            }
        }

        public void BreakWeapon()
        {
            if (weapon.CardId > 0)
                DeleteWeapon();
        }

        public void AddCardExp(int addon)
        {
            OwnerPlayer.AddBottleExp(addon);
        }

        public void WeaponReturn(int type)
        {
            if (TWeapon.Avatar.WeaponConfig.Type == type)
            {
                ActiveCard card = new ActiveCard(weapon.Card);
                OwnerPlayer.AddCard(card);
                DeleteWeapon();
            }
        }

        public void AddTile(int tile)
        {
            Avatar.MonsterConfig.Tile |= tile;
        }

        public void AddRandSkill()
        {
            AddSkill(SkillBook.GetRandSkillId(), level / 2, 100);
        }

        public void AddWeaponAttr(int type, double atkRate, double defRate, double hitRate, double dhitRate, double magicRate)
        {
            weaponRate = new WeaponAttrRate();
            weaponRate.Type = type;
            weaponRate.Atk = atkRate;
            weaponRate.Def = defRate;
            weaponRate.Hit = hitRate;
            weaponRate.Dhit = dhitRate;
            weaponRate.Magic = magicRate;
        }

        public void AddImmune(int buffId)
        {
            if (!immuneBuffs.Has(buffId))
            {
                immuneBuffs.Add(buffId);
            }
        }

        public void AddSpecialMark(int mark)
        {
            AddSpecialMark((SkillMarks) mark);
        }

        //a,队友，e，敌人，s，自身，t，目标</param>
        public IMonster[] GetRangeUnit(int type, int range, string filter, IMonster d, string effect)
        {
            RegionTypes rtype = (RegionTypes)type;
            LiveMonster target = d as LiveMonster;
            if (target == null)
            {
                return new IMonster[0]; 
            }

            int size = BattleInfo.Instance.MemMap.CardSize;
            foreach (MemMapPoint memMapPoint in BattleInfo.Instance.MemMap.Cells)
            {
                if (BattleLocationManager.IsPointInRegionType(rtype, target.Position.X, target.Position.Y, memMapPoint.ToPoint(), range))
                {
                    EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(effect), new Point(memMapPoint.X + size / 2, memMapPoint.Y + size / 2), true));
                }
            }

            List<IMonster> units = new List<IMonster>();
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost)
                    continue;

                if (Id == mon.Id && !filter.Contains("s"))
                    continue;

                if (target.id == mon.Id && !filter.Contains("t"))
                    continue;

                if (mon.PPos == PPos && !filter.Contains("a") || mon.PPos != PPos && !filter.Contains("e"))
                    continue;

                if (BattleLocationManager.IsPointInRegionType(rtype, target.Position.X, target.Position.Y, mon.Position, range))
                    units.Add(mon);
            }
            return units.ToArray();
        }

        public IMonster[] GetBehindUnit(IMonster d, string effect)
        {
            LiveMonster target = d as LiveMonster;
            if (target == null)
            {
                return new IMonster[0];
            }

            List<IMonster> units = new List<IMonster>();
            int size = BattleInfo.Instance.MemMap.CardSize;
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost || mon.Id == target.Id)
                    continue;

                if (mon.PPos != target.PPos)
                    continue;

                if (mon.Position.Y != target.Position.Y)
                    continue;

                if (PPos == PlayerIndex.Player1)
                {
                    if (mon.Position.X < target.Position.X || mon.Position.X > target.Position.X + size + 10)
                    {
                        continue;
                    }
                }
                else
                {
                    if (mon.Position.X > target.Position.X || mon.Position.X < target.Position.X - size - 10)
                    {
                        continue;
                    }
                }

                EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(effect), new Point(mon.Position.X + size / 2, mon.Position.Y + size / 2), true));
                units.Add(mon);
            }
            return units.ToArray();
        }

        public IMonster[] GetTypeUnit(IMonster d, string effect)
        {
            LiveMonster target = d as LiveMonster;
            if (target == null)
            {
                return new IMonster[0]; 
            }

            List<IMonster> units = new List<IMonster>();
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost || mon.Id == target.Id)
                    continue;

                if (mon.PPos == target.PPos && mon.Avatar.Id == target.Avatar.Id)
                {
                    EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(effect), mon, true));
                    units.Add(mon);
                }
            }
            return units.ToArray();
        }

        public int GetTileCount(int type)
        {
            return BattleInfo.Instance.MemMap.GetTileCount(type);
        }

        public int GetMonsterCount(int mid)
        {
            return OwnerPlayer.State.GetMonsterCountById(mid);
        }

        public int GetMonsterCountByType(int type)
        {
            return OwnerPlayer.State.GetMonsterCountByType((MonsterCountTypes)(type+10));
        }

        public AttrModifyData Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        public AttrModifyData DHit
        {
            get { return dhit; }
            set { dhit = value; }
        }

        public AttrModifyData Def
        {
            get { return def; }
            set { def = value; }
        }

        public AttrModifyData Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        public AttrModifyData Spd
        {
            get { return spd; }
            set { spd = value; }
        }

        public int Ats
        {
            get { return ats; }
            set { ats = value; }
        }

        public AttrModifyData MAtk
        {
            get { return matk; }
            set { matk = value; }
        }

        public AttrModifyData MDef
        {
            get { return mdef; }
            set { mdef = value; }
        }

        public AttrModifyData Skl
        {
            get { return skl; }
            set { skl = value; }
        }

        public int PDamageAbsorb 
        {
            get { return pDamageAbsorb; }
            set { pDamageAbsorb = value; }
        }

        public int CardManaRateW
        {
            get { return cardManaRateW; }
            set { cardManaRateW = value; OwnerPlayer.UpdateCardMana(); }
        }

        public int CardManaRateS
        {
            get { return cardManaRateS; }
            set { cardManaRateS = value; OwnerPlayer.UpdateCardMana(); }
        }

        public int Star
        {
            get { return avatar.MonsterConfig.Star; }
        }

        public int TileBuff
        {
            get { return tileBuff; }
            set { tileBuff = value; }
        }

        public double HpRate
        {
            get { return (double)life*100/avatar.Hp; }
        }

        public int MaxHp
        {
            get { return avatar.Hp; }
            set
            {
                int realvalue = Math.Max(value, 1);
                int hpper = Life * 100 / avatar.Hp;
                avatar.Hp = realvalue;
                Life = realvalue * hpper / 100;
            }
        }

        public int Hp
        {
            get { return Life; }
        }

        public bool IsTileMatching
        {
            get { return HasBuff(BuffEffectTypes.Tile); }
        }

        public bool IsElement(string ele)
        {
            return (int) Enum.Parse(typeof (MonsterAttrs), ele) == avatar.MonsterConfig.Type;
        }

        public bool IsRace(string rac)
        {
            return (int)Enum.Parse(typeof(MonsterRaces), rac) == avatar.MonsterConfig.Race;
        }

        public bool IsNight
        {
            get { return TimeState.Instance.IsNight; }
        }

        public bool HasSkill(int sid)
        {
            foreach (var sk in skills)
            {
                if (sk.SkillId == sid)
                    return true;
            }
            return false;
        }

        public int CardNumber
        {
            get { return OwnerPlayer.GetCardNumber(); }
        }

        public int AttackType
        {
            get { return attackType; }
            set { attackType = value; }
        }

        public int HeroAtk
        {
            get { return OwnerPlayer.HeroData.Atk; }
        }

        public int HeroDef
        {
            get { return OwnerPlayer.HeroData.Def; }
        }

        private int skillParm;
        public int SkillParm
        {
            get { return skillParm; }
            set { skillParm = value; }
        }

        public bool DropAdd
        {
            get { return dropAdd; }
            set { dropAdd = value; }
        }

        public void ClearDebuff()
        {
            foreach (MemBaseBuff buff in buffs.Values)
            {
                if (buff.Type == "ns")
                    buff.TimeLeft = 0;
            }
        }

        public void ExtendDebuff(int count)
        {
            foreach (MemBaseBuff buff in buffs.Values)
            {
                if (buff.Type == "ns")
                    buff.TimeLeft += count;
            }
        }

        public bool HasBuff(int buffid)
        {
            foreach (MemBaseBuff buff in buffs.Values)
            {
                if (buff.Id == buffid)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetToPosition(string type)
        {
            Point dest = BattleLocationManager.GetMonsterNearPoint(Position, type, pPos == PlayerIndex.Player2);
            if (dest.X != -1 && dest.Y != -1)
            {
                BattleLocationManager.SetToPosition(this, dest);
            }
        }

        public void OnMagicDamage(int damage, int element)
        {
            Life -= SkillAssistant.GetMagicDamage(this, new HitDamage(damage, element, DamageTypes.Magic));
        }

        public void SuddenDeath()
        {
            if (Star >= 6)
            {
                return;
            }
            Life = 0;
        }

        #endregion

    }
}
