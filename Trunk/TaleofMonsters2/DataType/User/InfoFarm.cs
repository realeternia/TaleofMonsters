using System;
using System.IO;
using NarlonLib.Core;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Others;

namespace TaleofMonsters.DataType.User
{
    internal class InfoFarm : INlSerlizable
    {
        private FarmState[] farmState;

        public InfoFarm()
        {
            farmState = new FarmState[SysConstants.PlayFarmCount];
            farmState[0] = new FarmState(0, 0);
            for (int i = 1; i < farmState.Length; i++)
            {
                farmState[i] = new FarmState(-1, 0);
            }
        }

        public FarmState GetFarmState(int id)
        {
            return farmState[id];
        }

        public void SetFarmState(int id, FarmState state)
        {
            farmState[id] = state;
        }

        public int GetFarmAvailCount()
        {
            int count = 0;
            foreach (FarmState state in farmState)
            {
                if (state.type != -1)
                {
                    count++;
                }
            }
            return count;
        }

        public bool UseSeed(int type, int dura)
        {
            for (int i = 0; i < 9; i++)
            {
                if (farmState[i].type == 0)
                {
                    farmState[i].type = type;
                    farmState[i].time = TimeTool.DateTimeToUnixTime(DateTime.Now) + dura;
                    return true;
                }
            }
            return false;
        }

        #region INlSerlizable ³ÉÔ±

        public void Write(BinaryWriter bw)
        {
            bw.Write(farmState.Length);
            for (int i = 0; i < farmState.Length; i++)
            {
                farmState[i].Write(bw);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            farmState = new FarmState[count];
            for (int i = 0; i < count; i++)
            {
                farmState[i] = new FarmState();
                farmState[i].Read(br);
            }
        }

        #endregion
    }
}
