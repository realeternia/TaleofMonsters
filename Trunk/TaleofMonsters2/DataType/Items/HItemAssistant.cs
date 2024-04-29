using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.DataType.Cards.Weapons;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.DataType.Items
{
    static class HItemAssistant
    {
        public static bool UseItemsById(int id, int type)
        {
            HItemConfig itemConfig = ConfigData.GetHItemConfig(id);
            if (itemConfig.Id == ConfigData.NoneHItem.Id)
                return false;

            if (type == HItemTypes.Common)
            {
                if (itemConfig.SubType == HItemTypes.Gift)
                    return UseGift(id);
                if (itemConfig.SubType == HItemTypes.Ore)
                    return UseOre(itemConfig.UseEffect);
                if (itemConfig.SubType == HItemTypes.Item)
                    return UseItem(itemConfig.UseEffect);
                if (itemConfig.SubType == HItemTypes.RandomCard)
                    return UseScard(itemConfig.UseEffect);
                if (itemConfig.SubType == HItemTypes.People)
                    return UsePcard(itemConfig.UseEffect);
            }
            else if (type == HItemTypes.Fight)
            {
                if (itemConfig.SubType == HItemTypes.Fight)
                    return UseFightItem(itemConfig.UseEffect);
            }
            else if (type == HItemTypes.Seed)
            {
                if (itemConfig.SubType == HItemTypes.Seed)
                    return UseSeedItem(itemConfig.UseEffect);
            }

            return false;
        }

        private static bool UseGift(int id)
        {
            RLIItemRateCountList items = ConfigData.GetItemGiftConfig(id).Items;

            int roll = MathTool.GetRandom(1, 101);
            for (int i = 0; i < items.Count; i++)
            {
                RLIItemRateCount item = items[i];

                if (roll < item.RollMin || roll > item.RollMax)
                    continue;
                if (item.Type == 1)
                {
                    UserProfile.InfoBag.AddItem(item.Id, item.Count);
                }
                else if (item.Type == 2)
                {
                    UserProfile.InfoBag.AddEquip(item.Id);
                }
                else if (item.Type == 3)
                {
                    UserProfile.InfoCard.AddCard(item.Id, item.Count);
                }
            }

            return true;
        }

        private static bool UseOre(int[] effects)
        {
            int[] rt = {0, 0, 0, 0, 0, 0, 0};
            rt[effects[0] - 1] = effects[1];
            UserProfile.InfoBag.AddResource(rt);

            return true;
        }

        private static bool UseFightItem(int[] effects)
        {
            int etype = effects[0];
            int evalue = effects[1];
            int evalue2 = 0;
            if (effects.Length >= 3) evalue2 = effects[2];

            switch (etype)
            {
                case 1: PlayerManager.LeftPlayer.Anger += evalue; break;
                case 2: PlayerManager.LeftPlayer.Mana += evalue; break;
                case 3: PlayerManager.LeftPlayer.Anger += evalue; PlayerManager.LeftPlayer.Mana += evalue2; break;

                case 5: PlayerManager.LeftPlayer.AddCard(new ActiveCard(new DeckCard(Controler.World.WorldInfoManager.GetCardFakeId(), MonsterBook.GetRandMonsterId(), 1, 0))); break;
                case 6: PlayerManager.LeftPlayer.AddCard(new ActiveCard(new DeckCard(Controler.World.WorldInfoManager.GetCardFakeId(),WeaponBook.GetRandWeaponId(), 1, 0))); break;
                case 7: PlayerManager.LeftPlayer.AddCard(new ActiveCard(new DeckCard(Controler.World.WorldInfoManager.GetCardFakeId(), SpellBook.GetRandSpellId(), 1, 0))); break;

                case 11: PlayerManager.LeftPlayer.AddExtraInfo(PlayerExtraInfo.Nature, evalue); break;

                case 101: PlayerManager.LeftPlayer.DirectDamage += evalue; break;
             //   case 102: PlayerManager.RightPlayer.AddBuff((int)BuffIds.Eye, 1, evalue); break;
            }

            return true;
        }

        private static bool UseSeedItem(int[] effects)
        {
            return UserProfile.Profile.InfoFarm.UseSeed(effects[0], effects[1]);
        }

        private static bool UseItem(int[] effects)
        {
            int etype = effects[0];
            int evalue = effects[1];

            switch (etype)
            {
                case 1: UserProfile.InfoBasic.AddExp(evalue); break;
                case 2: UserProfile.InfoBasic.AddSkillValueById(evalue, 1); break;
                case 3: UserProfile.InfoBasic.ap += evalue; break;
                case 4: if (UserProfile.InfoBag.GetDayItem(evalue)) return false;
                    UserProfile.InfoBag.SetDayItem(evalue); break;
            }

            return true;
        }

        private static bool UseScard(int[] effects)
        {
            int timeCount = 1;

            while (true)
            {
                int id;
                int type = MathTool.GetRandom(10);
                if (type < 6)
                {
                    id = MonsterBook.GetRandMonsterId();
                }
                else if (type < 8)
                {
                    id = WeaponBook.GetRandWeaponId();
                }
                else
                {
                    id = SpellBook.GetRandSpellId();
                }
                int lvinfo = effects[CardAssistant.GetCard(id).Star - 1];
                if (timeCount >= lvinfo && lvinfo != 0)
                {
                    UserProfile.InfoCard.AddCard(id);
                    break;
                }
                timeCount++;
            }

            return true;
        }

        private static bool UsePcard(int[] effects)
        {
            UserProfile.InfoRival.SetRivalAvail(effects[0]);

            return true;
        }
    }
}
