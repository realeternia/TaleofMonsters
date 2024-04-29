using System.Drawing;
using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.MemEffect;
using TaleofMonsters.Controler.Battle.Data.MemFlow;
using TaleofMonsters.Controler.Battle.Data.MemMap;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Effects;

namespace TaleofMonsters.DataType.Cards.Spells
{
    static class SpellAssistant
    {
        public static void CheckSpellEffect(PlayerIndex pos, LiveMonster target, DeckCard dcard,Point mouse)
        {
            Spell spell = new Spell(dcard.BaseId);
            SpellConfig spellConfig = spell.SpellConfig;
            spell.UpgradeToLevel(dcard.Level, dcard.Quality);
            MemBaseSpell spl = new MemBaseSpell(spell);
            spl.CheckSpellEffect(pos, target, mouse);
            if (SpellBook.HasEffect(spell.Id ,SpellSpecialEffects.Return))
            {
                ActiveCard card = new ActiveCard(Controler.World.WorldInfoManager.GetCardFakeId(), spell.Id, 1, 0);
                if (pos == PlayerIndex.Player1)
                    PlayerManager.LeftPlayer.AddCard(card);
                else
                    PlayerManager.RightPlayer.AddCard(card);
            }

            if (spl.HintWord!="")
                FlowWordQueue.Instance.Add(new FlowWord(spl.HintWord, mouse, 0, "Cyan", 26, 0, 0, 0, 15), false);
            if (BattleTargetManager.PlayEffectOnMonster(spellConfig.Target))
                EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(spellConfig.UnitEffect), target, false));
            if (BattleTargetManager.PlayEffectOnMouse(spellConfig.Target))
                EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(spellConfig.UnitEffect), mouse, false));
            if (spellConfig.AreaEffect != "")
            {
                RegionTypes regionType = BattleTargetManager.GetRegionType(spellConfig.Target[2]);
                foreach (MemMapPoint memMapPoint in BattleInfo.Instance.MemMap.Cells)
                {
                    if (BattleLocationManager.IsPointInRegionType(regionType, mouse.X, mouse.Y, memMapPoint.ToPoint(), spellConfig.Range))
                    {
                        EffectQueue.Instance.Add(new ActiveEffect(EffectBook.GetEffect(spellConfig.AreaEffect), memMapPoint.ToPoint()+new Size(50,50), false));
                    }
                }
            }
        }
    }
}
