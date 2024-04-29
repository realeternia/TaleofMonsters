using System.Collections.Generic;
using System.Drawing;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemEffect;
using TaleofMonsters.Controler.Battle.Data.MemFlow;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.DataType.Effects;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Others;
using TaleofMonsters.DataType.Skills;

namespace TaleofMonsters.Controler.Battle.DataTent
{
    internal class MonsterQueue
    {
        static MonsterQueue instance;
        public static MonsterQueue Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonsterQueue();
                }
                return instance;
            }
        }

        public static void Init()
        {
            instance = new MonsterQueue();
        }

        private List<LiveMonster> monsters = new List<LiveMonster>();
        private int leftCount;
        private int rightCount;

        public List<LiveMonster> Enumerator
        {
            get { return monsters; }
        }

        public LiveMonster this[int index]
        {
            get { return monsters[index]; }
            set { monsters[index] = value; }
        }

        public int Count
        {
            get { return monsters.Count; }
        }

        public int LeftCount
        {
            get { return leftCount; }
        }

        public int RightCount
        {
            get { return rightCount; }
        }

        public void Add(LiveMonster mon)
        {
            mon.OwnerPlayer.State.CheckMonsterEvent(true, mon.Avatar);
            string cover = mon.Avatar.MonsterConfig.Cover;
            if (cover != "")
            {
                ActiveEffect ef = new ActiveEffect(EffectBook.GetEffect(cover), mon, true);
                ef.Repeat = true;
                EffectQueue.Instance.Add(ef);
            }
            monsters.Add(mon);
            if (mon.PPos == PlayerIndex.Player1)
            {
                leftCount++;
            }
            else
            {
                rightCount++;
            }
        }

        public void Remove(LiveMonster mon)
        {
            mon.OwnerPlayer.State.CheckMonsterEvent(false, mon.Avatar);
            if (BattleInfo.Instance.MemMap.GetMouseCell(mon.Position.X, mon.Position.Y).Owner == -mon.Id)
                BattleLocationManager.UpdateCellOwner(mon.Position.X, mon.Position.Y, 0);
            monsters.Remove(mon);                                
        }

        public void Remove(int monid)
        {
            foreach (LiveMonster monster in monsters)
            {
                if (monster.Id == monid)
                {
                    Remove(monster);
                    return;
                }
            }
        }

        public void AddInitialAction()
        {
            List<MonsterIdSpeed> lives = new List<MonsterIdSpeed>();
            foreach (LiveMonster mon in monsters)
            {
                MonsterIdSpeed spd = new MonsterIdSpeed();
                spd.id = mon.Id;
                spd.speed = mon.Avatar.Spd;
                spd.mark = mon.Level;
                lives.Add(spd);
            }
            lives.Sort(new CompareBySpeed());
            for (int i = 0; i < lives.Count; i++)
            {
                LiveMonster tar = GetMonsterByUniqueId(lives[i].id);
                tar.Action += (11 - i)*75;
            }
        }

        public LiveMonster GetMonsterByUniqueId(int id)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].Id == System.Math.Abs(id))
                {
                    return monsters[i];
                }
            }
            return null;
        }

        public int NextAction()
        {
            leftCount = 0;
            rightCount = 0;
            List<int> removeMids = new List<int>();            
            foreach (LiveMonster mon in monsters)
            {
                if (mon.Rival.DirectDamage > 0)
                    mon.Life -= mon.Rival.DirectDamage;
                int loss = mon.LossLife;
                if (loss > 0)
                {
                    FlowWordQueue.Instance.Add(new FlowWord(loss.ToString(), mon.CenterPosition, 5, "red", 0, -10), false);//掉血显示
                }
                if (!mon.IsAlive)
                {
                    if (!mon.IsGhost)
                    {
                        mon.Dying();
                    }
                    else
                    {
                        mon.GhostTime++;
                        if (mon.GhostTime>=300) //300 is ghost time 3round
                        {
                            removeMids.Add(mon.Id);
                        }
                    }
                }
                else
                {
                    if (mon.PPos == PlayerIndex.Player2) rightCount = RightCount + 1;
                    else leftCount = LeftCount + 1;
                }
            }
            PlayerManager.LeftPlayer.DirectDamage = 0;//伤害清除
            PlayerManager.RightPlayer.DirectDamage = 0;

            foreach (int mid in removeMids)
            {
                Remove(mid);
            }

            int hitMonsterId = 0;//每回合仅有一个单位可以发起攻击
            foreach (LiveMonster roundMonster in monsters)
            {
                if (roundMonster.IsGhost) 
                    continue;

                roundMonster.TargetPosition = new Point(-1, -1);//清除位置标记

                int tile = BattleInfo.Instance.MemMap.GetMouseCell(roundMonster.Position.X, roundMonster.Position.Y).Tile;
                TileMatchResult match = TileBook.MatchTile(tile,roundMonster.Avatar.MonsterConfig.Type, roundMonster.Avatar.MonsterConfig.Tile);
                roundMonster.Next(match);
                PlayerIndex side = roundMonster.PPos;
                if (roundMonster.HasBuff(BuffEffectTypes.Rebel))//控制
                {
                    side = side == PlayerIndex.Player1 ? PlayerIndex.Player2 : PlayerIndex.Player1;
                }
                int eid = BattleInfo.Instance.MemMap.GetEmemyId(roundMonster.Id, side, roundMonster.Position.Y);
                if (eid != 0)
                {
                    if (roundMonster.Attack(hitMonsterId > 0))
                    {
                        hitMonsterId = roundMonster.Id;
                        LiveMonster target = GetMonsterByUniqueId(eid);
                        if (target != null)
                        {
                            SkillAssistant.CheckTimelySkillEffect(roundMonster);
                            if (SkillAssistant.CheckSpecial(roundMonster))
                            {
                                return 0;//特殊技能出发
                            }
                            roundMonster.TargetPosition = new Point(target.PPos == PlayerIndex.Player1 ? target.Position.X + 100 : target.Position.X - 70, target.Position.Y + 15);
                            EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(roundMonster.Arrow), target, false));
                            bool isMiss = !target.BeHited(roundMonster);
                            if (isMiss)
                                FlowWordQueue.Instance.Add(new FlowWord("Miss!", new Point(roundMonster.TargetPosition.X + 40, roundMonster.TargetPosition.Y + 40), 0, "red", -10, 0), false);
                        }
                    }
                }
            }
            return hitMonsterId;
        }
    }
}
