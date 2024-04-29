namespace TaleofMonsters.DataType.User.Mem
{
    public enum MemPlayerRecordTypes
    {
        TotalWin = 1,
        TotalDig = 2,
        TotalPick = 3,
        TotalFish = 4,
        TotalKill = 5,
        TotalSummon = 6,
        TotalWeapon = 7,
        TotalSpell = 8,
        TotalOnline = 9,
        ContinueWin = 10,
        TotalKillByType = 100, // size 9
        TotalKillByRace = 200, //16
        TotalKillByLevel = 300, //9

        TotalSummonByType = 400, //17
        TotalSummonByRace = 500, //16
        TotalSummonByLevel = 600, //9

        LastCardShopTime = 1000,
        LastMergeTime = 1001,
        LastCardChangeTime = 1002,
        LastNpcPieceTime = 1003,
        LastQuestionTime = 1004,
        HeroExpPoint = 1100,
    }
}
