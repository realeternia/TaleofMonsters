using System.Drawing;
using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.DataType.Cards.Spells;

namespace TaleofMonsters.Controler.Battle.Data
{
    internal class MemBaseSpell
    {
        protected int spellId;
        protected int level;
        protected Spell spellInfo;
        protected string hintWord;

        public MemBaseSpell(Spell spl)
        {
            spellId = spl.Id;
            spellInfo = spl;
            hintWord = "";
        }

        public string HintWord
        {
            get { return hintWord; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public SpellConfig SpellConfig
        {
            get { return spellInfo.SpellConfig; }
        }

        public void CheckSpellEffect(PlayerIndex pPos, LiveMonster target, Point mouse)
        {
            if (spellInfo.SpellConfig.Effect != null)
            {
                Player p1 = pPos == PlayerIndex.Player1 ? PlayerManager.LeftPlayer : PlayerManager.RightPlayer;
                Player p2 = pPos == PlayerIndex.Player2 ? PlayerManager.LeftPlayer : PlayerManager.RightPlayer;
                spellInfo.SpellConfig.Effect(BattleInfo.Instance.MemMap, p1, p2, target, mouse, level);
            }
        }
    }
}
