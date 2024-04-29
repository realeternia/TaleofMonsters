namespace TaleofMonsters.Core
{
    internal class ExpTree
    {
        public const int MaxLevel = 99;

        static public int GetNextRequired(int level)
        {
            return ConfigDatas.ConfigData.GetExpConfig(level).Exp;
        }

        static public int GetNextRequiredCard(int level)
        {
            return ConfigDatas.ConfigData.GetExpConfig(level).PetExp;
        }
    }
}
