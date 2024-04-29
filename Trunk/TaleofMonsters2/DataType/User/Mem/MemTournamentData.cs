using System.Collections.Generic;
using ConfigDatas;
using TaleofMonsters.Controler.Battle;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Peoples;
using TaleofMonsters.Forms.TourGame;
using TaleofMonsters.Core;

namespace TaleofMonsters.DataType.User.Mem
{
    internal class MemTournamentData : INlSerlizable
    {
        public int id;
        public bool engage;
        public int[] pids;
        public MatchResult[] results;

        private int currentMid;
        private int currentRvId;
        private bool currentAutoGiveUp;

        public MemTournamentData()
        {
        }

        public MemTournamentData(int tid)
        {
            id = tid;
            TournamentConfig tournamentConfig = ConfigData.GetTournamentConfig(tid);
            pids = new int[tournamentConfig.PlayerCount];
            results = new MatchResult[tournamentConfig.MatchCount];
        }

        public void CheckMatch(int mid, bool autoGiveup)
        {
            TournamentConfig tournamentConfig = ConfigData.GetTournamentConfig(id);

            TournamentMatchConfig match = ConfigData.GetTournamentMatchConfig(mid);
            int left;
            int right;
            if (match.LeftType == 1)
            {
                left = pids[match.LeftValue];
            }
            else
            {
                left = results[match.LeftValue].winner;
            }
            if (match.RightType == 1)
            {
                right = pids[match.RightValue];
            }
            else
            {
                right = results[match.RightValue].winner;
            }

            currentMid = mid;
            currentAutoGiveUp = autoGiveup;
            currentRvId = left == -1 ? right : left;

            results[mid] = new MatchResult();
            if (left != -1 && right != -1)
            {
                FastBattle.Instance.StartGame(left, right, tournamentConfig.Map, -1);
                results[mid].winner = FastBattle.Instance.LeftWin ? left : right;
                results[mid].loser = FastBattle.Instance.LeftWin ? right : left;
            }
            else
            {
                PeopleBook.Fight(currentRvId, tournamentConfig.Map, -1, 1, onWin, onLose);
            }
        }

        private void onWin()
        {
            if (!currentAutoGiveUp)
            {
                results[currentMid].winner = -1;
                results[currentMid].loser = currentRvId;
            }
        }

        private void onLose()
        {
            results[currentMid].winner = currentRvId;
            results[currentMid].loser = -1;
        }

        public List<int[]> GetRanks()
        {
            List<int[]> ranks = new List<int[]>();
            for (int i = 0; i < pids.Length; i++)
            {
                int[] dat = new int[] { 0, 0, 0, 0 };
                dat[0] = pids[i];
                foreach (MatchResult matchResult in results)
                {
                    if (matchResult.winner == dat[0])
                    {
                        dat[1]++;
                        dat[2]++;
                        dat[3] += 3;
                    }
                    else if (matchResult.loser == dat[0])
                    {
                        dat[1]++;
                    }
                }
                ranks.Add(dat);
            }
            ranks.Sort(new CompareByWin());
            return ranks;
        }

        public void Award()
        {
            List<int[]> ranks = GetRanks();
            TournamentConfig tournamentConfig = ConfigData.GetTournamentConfig(id);
            for (int i = 0; i < tournamentConfig.Awards.Length; i++)
            {
                UserProfile.InfoWorld.UpdatePeopleRank(ranks[i][0], tournamentConfig.Awards[i]);
            }

            for (int i = 0; i < tournamentConfig.Resource.Count; i++)
            {
                if (ranks[i][0] == -1)
                {
                    if (tournamentConfig.Resource[i].Id== 99)
                    {
                        UserProfile.InfoBag.AddDiamond(tournamentConfig.Resource[i].Value);
                    }
                    else
                    {
                        UserProfile.InfoBag.AddResource((GameResourceType)tournamentConfig.Resource[i].Id, tournamentConfig.Resource[i].Value);
                    }
                }
            }

            engage = false;
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(id);
            bw.Write(engage);
            bw.Write(pids.Length);
            foreach (int pid in pids)
            {
                bw.Write(pid);
            }
                bw.Write(results.Length);
            foreach (MatchResult result in results)
            {
                result.Write(bw);
            }
        }

        public void Read(System.IO.BinaryReader br)
        {
            id = br.ReadInt32();
            engage = br.ReadBoolean();
            int count = br.ReadInt32();
            pids = new int[count];
            for (int i = 0; i < count; i++)
            {
                pids[i] = br.ReadInt32();
            }
            count = br.ReadInt32();
            results = new MatchResult[count];
            for (int i = 0; i < count; i++)
            {
                results[i] = new MatchResult();
                results[i].Read(br);
            }
        }

        #endregion
    }

    internal class CompareByWin : IComparer<int[]>
    {
        #region IComparer<int[]> 成员

        public int Compare(int[] x, int[] y)
        {
            if (x[2] != y[2])
                return y[2] - x[2];
            if (x[1] != y[1])
                return x[1] - y[1];
            return x[0] - y[0];
        }

        #endregion
    }
}
