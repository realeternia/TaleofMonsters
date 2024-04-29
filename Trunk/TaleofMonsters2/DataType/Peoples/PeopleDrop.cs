using System.Collections.Generic;
using ConfigDatas;
using NarlonLib.Math;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.DataType.Peoples
{
    class PeopleDrop
    {
        private List<int> dropIds;
        private List<DropResource> resources = new List<DropResource>();

        public PeopleDrop(int id)
        {
            dropIds = new List<int>();
            foreach (DropCardConfig dropCardConfig in ConfigData.DropCardDict.Values)
            {
                if (dropCardConfig.Pid==id)
                {
                    dropIds.Add(dropCardConfig.Id);
                }   
            }

            PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(id);

            DropResource gold = new DropResource();
            gold.id = 1;
            gold.percent = 100;
            gold.min = PeopleBook.IsPeople(id) ? peopleConfig.Level : peopleConfig.Level * 3 + 12;
            gold.max = PeopleBook.IsPeople(id) ? peopleConfig.Level * 3 / 2 : peopleConfig.Level * 5 + 20;
            resources.Add(gold);
            foreach (int rid in peopleConfig.Reward)
            {
                DropResource drop = new DropResource();
                drop.id = rid;
                drop.percent = drop.id <= 3 ? peopleConfig.Level * 5 / 4 + 5 : peopleConfig.Level / 2 + 2;
                drop.min = 1;
                drop.max = 1;
                resources.Add(drop);
            }
        }

        public int GetDropCard(IntPair[] cardRates)
        {
            RandomMaker maker = new RandomMaker();
            foreach (int dropId in dropIds)
            {
                DropCardConfig dropCardConfig = ConfigData.GetDropCardConfig(dropId);
                maker.Add(dropCardConfig.CardId, dropCardConfig.Rate * 10);
            }
            foreach (IntPair intPair in cardRates)
            {
                maker.Add(intPair.type, intPair.value);
            }
            return maker.Process(1)[0];
        }

        public int[] GetDropResource()
        {
            int[] rt = { 0, 0, 0, 0, 0, 0, 0 };
            foreach (DropResource info in resources)
            {
                int percent = info.percent;
                if (UserProfile.InfoBag.GetDayItem(HItemSpecial.DoubleGoldItem))
                {
                    percent *= 2;
                }
                if (MathTool.GetRandom(100) < percent)
                {
                    rt[info.id - 1] = MathTool.GetRandom(info.min, info.max + 1);
                }
            }
            return rt;
        }
    }
}
