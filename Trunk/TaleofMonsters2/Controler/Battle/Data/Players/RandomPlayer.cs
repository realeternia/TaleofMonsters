using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.DataType.Cards.Weapons;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    class RandomPlayer : Player
    {
        public RandomPlayer(int id, PlayerIndex pPos)
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
            DeckCard[] cd = new DeckCard[50];
            for (int i = 0; i < 50; i++)
            {
                switch (MathTool.GetRandom(7))
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        cd[i] = new DeckCard(World.WorldInfoManager.GetCardFakeId(), MonsterBook.GetRandMonsterId(), 1, 0);
                        break;
                    case 5:
                        cd[i] = new DeckCard(World.WorldInfoManager.GetCardFakeId(), WeaponBook.GetRandWeaponId(), 1, 0);
                        break;
                    case 6:
                        cd[i] = new DeckCard(World.WorldInfoManager.GetCardFakeId(), SpellBook.GetRandSpellId(), 1, 0);
                        break;
                }
            }
            activeCards = new ActiveCards(cd);
            heroData = new Monster(peopleConfig.KingCard);
            heroData.UpgradeToLevel(level, 0);
            heroImage = MonsterBook.GetMonsterImage(heroData.Id, 180, 180);
            InitBase();
        }
    }
}
