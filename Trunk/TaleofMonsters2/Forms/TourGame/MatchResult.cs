using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.Forms.TourGame
{
    internal class MatchResult : INlSerlizable
    {
        public int winner;
        public int loser;

        public MatchResult()
        {
            
        }

        public MatchResult(int wp, int lp)
        {
            winner = wp;
            loser = lp;
        }

        public override string ToString()
        {
            return string.Format("{0};{1}", winner, loser);
        }

        public static string MatchResultToString(MatchResult result)
        {
            return result.ToString();
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(winner);
            bw.Write(loser);
        }

        public void Read(System.IO.BinaryReader br)
        {
            winner = br.ReadInt32();
            loser = br.ReadInt32();
        }

        #endregion
    }
}
