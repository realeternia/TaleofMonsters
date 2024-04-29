using System.Drawing;
using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.Controler.Battle.Data.MemCard;

namespace TaleofMonsters.Controler.Battle
{
    internal class AIStrategy
    {
        internal static void AIProc(Player player, bool isGamePaused)
        {            
            if (isGamePaused)
                return;

            if (player.GetCardNumber() <= 0)
                return;

            if (MathTool.GetRandom(4) != 0)
                return;

            int row = BattleInfo.Instance.MemMap.RowCount;
            int size = BattleInfo.Instance.MemMap.CardSize;
            PlayerIndex pos = (player == PlayerManager.LeftPlayer) ? PlayerIndex.Player1:PlayerIndex.Player2;
            
            player.CardsDesk.SetSelectId(MathTool.GetRandom(player.GetCardNumber()) + 1);
            if (player.SelectCardId != 0)
            {
                ActiveCard card = player.CardsDesk.GetSelectCard();
                if (player.CardUsePreCheck(card) == HSErrorTypes.OK)
                {
                    if (card.CardType == CardTypes.Monster)
                    {
                        Point monPos = BattleLocationManager.GetMonsterPoint(PlayerIndex.Player2);
                        LiveMonster newMon = new LiveMonster(card.Card, new Monster(card.CardId), monPos, PlayerIndex.Player2);
                        MonsterQueue.Instance.Add(newMon);
                        BattleLocationManager.UpdateCellOwner(newMon.Position.X, newMon.Position.Y, newMon.Id);
                    }
                    if (card.CardType == CardTypes.Weapon)
                    {
                        int tar = -1;
                        for (int i = 0; i < MonsterQueue.Instance.Count; i++)
                        {
                            LiveMonster monster = MonsterQueue.Instance[i];
                            if (!monster.IsGhost && monster.PPos == pos && monster.TWeapon.CardId == 0 && monster.Life > monster.MaxHp / 2)
                            {
                                if (tar == -1 || monster.Avatar.MonsterConfig.Star > MonsterQueue.Instance[tar].Avatar.MonsterConfig.Star)
                                    tar = i;
                            }
                        }
                        if (tar == -1)
                            return;

                        if (player.HasBuff(BuffEffectTypes.BreakWeapon))
                        {
                            player.DelBuff(BuffEffectTypes.BreakWeapon);
                        }
                        else
                        {
                            MonsterQueue.Instance[tar].AddWeapon(card.Card);
                            if (pos== PlayerIndex.Player1)
                            {
                                BattleInfo.Instance.LeftWeaponAdd++;
                            }
                            else
                            {
                                BattleInfo.Instance.RightWeaponAdd++;
                            }
                        }
                    }
                    else if (card.CardType == CardTypes.Spell)
                    {
                        SpellConfig spellConfig = ConfigData.GetSpellConfig(card.CardId);
                        if (BattleTargetManager.IsSpellNullTarget(spellConfig.Target))
                        {
                            Point targetPoint = new Point(pos == PlayerIndex.Player1 ? MathTool.GetRandom(200, 300) : MathTool.GetRandom(600, 700), MathTool.GetRandom(size * 3 / 10, row * size - size * 3 / 10));
                            SpellAssistant.CheckSpellEffect(pos, null, card.Card, targetPoint);

                        }
                        else if (BattleTargetManager.IsSpellUnitTarget(spellConfig.Target))
                        {
                            int tar = -1;
                            for (int i = 0; i < MonsterQueue.Instance.Count; i++)
                            {
                                LiveMonster monster = MonsterQueue.Instance[i];
                                if (!monster.IsGhost && ((monster.PPos != pos && spellConfig.Target[1] == 'F') || (monster.PPos == pos && spellConfig.Target[1] != 'F')))
                                {
                                    if (tar == -1 || monster.Avatar.MonsterConfig.Star > MonsterQueue.Instance[tar].Avatar.MonsterConfig.Star)
                                        tar = i;
                                }
                            }
                            if (tar != -1)
                            {
                                Point targetPoint = MonsterQueue.Instance[tar].CenterPosition;
                                SpellAssistant.CheckSpellEffect(pos, MonsterQueue.Instance[tar], card.Card, targetPoint);
                            }
                            else
                            {
                                return;
                            }
                        }
                        //else if (BattleTargetManager.IsSpellGridTarget(spell.Target))
                        //{
                        //    Point targetPoint = BattleLocationManager.GetRandomPoint(new Point());
                        //    if (BattleLocationManager.GetPlaceMonster(targetPoint.X, targetPoint.Y) != null)
                        //    {
                        //        return;
                        //    }
                        //    SpellAssistant.CheckSpellEffect(isLeft, null, card.Card, targetPoint);
                        //}
                        //else if (BattleTargetManager.IsSpellRowTarget(spell.Target))
                        //{
                        //    Point targetPoint = BattleLocationManager.GetRandomPoint(new Point());
                        //    SpellAssistant.CheckSpellEffect(isLeft, null, card.Card, targetPoint);
                        //}
                        if (pos == PlayerIndex.Player1)
                        {
                            BattleInfo.Instance.LeftSpellAdd++;
                        }
                        else
                        {
                            BattleInfo.Instance.RightSpellAdd++;
                        }
                    }
                    player.OnUseCard(player.CardsDesk.GetSelectCard());
                    player.DeleteCardAt(player.SelectId);
                }
            }
        }
    }
}