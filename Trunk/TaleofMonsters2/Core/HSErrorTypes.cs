namespace TaleofMonsters.Core
{
    internal class HSErrorTypes
    {
        public const int OK = 0;
        public const int CommonError = 1;

        public const int BattleNoUseCard = 101;
        public const int BattleNoUseSpellCard = 102;
        public const int BattleLackMana = 103;
        public const int BattleLackAnger = 104;
        public const int BattleHeroOnlyOne = 105;
        public const int BattleSkillInCD = 106;

        public const int DeckCardOnlyThree = 1000;
        public const int DeckIsFull = 1001;

        public static string GetDescript(int id)
        {
            return ConfigDatas.ConfigData.GetErrorConfig(id).Des;
        }
    }
}
