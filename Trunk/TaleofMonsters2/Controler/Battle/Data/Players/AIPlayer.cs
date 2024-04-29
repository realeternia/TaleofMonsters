using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Equips.Addons;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    class AIPlayer : Player
    {
        public AIPlayer(int id, string deck, PlayerIndex pPos, int rlevel)
        {
            peopleId = id;

            level = rlevel;
            PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(id);
            level = peopleConfig.Level;
            playerState = new PlayerState(id);
            this.pPos = pPos;
            for (int i = 0; i < 6; i++)
                cards[i] = ActiveCards.NoneCard;

            DeckCard[] cds = DeckBook.GetDeckByName(deck, level);
            activeCards = new ActiveCards(cds);
            int[] equips ={ 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            CalculateEquipAndSkill(equips, new int[] { });
            if (playerState.job != 0)
            {
                EAddonBook.UpdateMasterData(playerState.masterskills.Keys(), playerState.masterskills.Values());
                heroData = new Monster(playerState, skills);
                heroData.UpgradeToLevel(level, 0);
                heroImage = PicLoader.Read("Hero", string.Format("{0}{1}.JPG", playerState.job, playerState.sex == 1 ? "m" : "f"));
            }
            else
            {
                heroData = new Monster(peopleConfig.KingCard);
                heroData.UpgradeToLevel(level, 0);
                heroImage = MonsterBook.GetMonsterImage(heroData.Id,180,180);
            }
            InitBase();
        }
    }
}
