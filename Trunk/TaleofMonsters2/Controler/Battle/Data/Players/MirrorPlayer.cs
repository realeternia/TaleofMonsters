using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.DataType.Cards.Monsters;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    class MirrorPlayer : Player
    {
        public MirrorPlayer(int id, ActiveCards cpcards, PlayerIndex pPos)
        {
            peopleId = id;

            PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(id);
            level = peopleConfig.Level;
            playerState = new PlayerState(id);
            int[] equips ={ 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            CalculateEquipAndSkill(equips, new int[] {});
            this.pPos = pPos;
            for (int i = 0; i < 6; i++)
                cards[i] = ActiveCards.NoneCard;
            activeCards = cpcards.GetCopy();
            heroData = new Monster(peopleConfig.KingCard);
            heroData.UpgradeToLevel(level, 0);
            heroImage = MonsterBook.GetMonsterImage(heroData.Id, 180, 180);
            InitBase();
        }
    }
}
