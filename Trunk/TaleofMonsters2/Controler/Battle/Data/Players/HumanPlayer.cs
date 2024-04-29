using System;
using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Equips.Addons;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.User.Mem;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    class HumanPlayer : Player
    {
        public HumanPlayer(PlayerIndex pPos)
        {
            peopleId = 0;
            level = UserProfile.InfoBasic.level;
            playerState = new PlayerState();
            this.pPos = pPos;
            for (int i = 0; i < 6; i++)
                cards[i] = ActiveCards.NoneCard;

            DeckCard[] cd = new DeckCard[50];
            for (int i = 0; i < 50; i++)
            {
                int id = UserProfile.InfoCard.SelectedDeck.GetCardAt(i);
                cd[i] = UserProfile.InfoCard.GetDeckCardById(id);
            }
            activeCards = new ActiveCards(cd);

            CalculateEquipAndSkill(UserProfile.InfoBag.equipon, UserProfile.InfoSkill.skillCommons.ToArray());
            for (int i = 0; i < 5; i++)
            {
                if (UserProfile.InfoSkill.skillUse[i] <= 0)
                    continue;
                HeroSkillSpecialConfig skillSpecialConfig = ConfigData.GetHeroSkillSpecialConfig(UserProfile.InfoSkill.skillUse[i]);
                skills[i] = new MonsterSkill(skillSpecialConfig.SkillId, skillSpecialConfig.Rate, skillSpecialConfig.Level);
            }
            EAddonBook.UpdateMasterData(playerState.masterskills.Keys(), playerState.masterskills.Values());
            heroData = new Monster(playerState, skills);
            heroData.UpgradeToLevel(level, 0);
            heroImage = PicLoader.Read("Hero", string.Format("{0}{1}.JPG", playerState.job, playerState.sex == 1 ? "m" : "f"));
            InitBase();
        }

        public override void AddResource(GameResourceType type, int number)
        {
            UserProfile.InfoBag.AddResource(type, number);
        }

        public override void AddBottleExp(int addon)
        {
            int rexp = UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.HeroExpPoint) + addon;
            UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.HeroExpPoint, Math.Min(ExpTree.GetNextRequiredCard(level), rexp));
        }

        public override void InitialCards()
        {
#if DEBUG
            AddCard(new ActiveCard(World.WorldInfoManager.GetCardFakeId(), 30043, 1, 0));
#endif
            for (int i = 0; i < 4; i++)
            {
                GetNextCard();
            }

#if DEBUG
        //    monsters[0] = new ActiveCard(World.WorldInfoManager.GetCardFakeId(), 10074, 1, 0);
#endif
            DeckCard heroDeckCard = new DeckCard(World.WorldInfoManager.GetCardFakeId(), 99, level, 0);
            if (playerState.job != 0)
            {
                LiveMonster lm = new LiveMonster(heroDeckCard, heroData, BattleLocationManager.GetHeroPoint(pPos, heroDeckCard.Id), pPos);
                lm.TileEffectPlus = playerState.GetTotalAttr(PlayerAttrs.Adp)/ 20;
                MonsterQueue.Instance.Add(lm);
                BattleLocationManager.UpdateCellOwner(lm.Position.X, lm.Position.Y, lm.Id);
                hero = lm;
            }
        }
    }
}
