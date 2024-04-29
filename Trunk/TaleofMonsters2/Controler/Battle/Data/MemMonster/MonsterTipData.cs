namespace TaleofMonsters.Controler.Battle.Data.MemMonster
{
    internal struct MonsterTipData
    {
        public string type;
        public int key;
        public string content;

        public MonsterTipData(string type, int key, string content)
        {
            this.type = type;
            this.key = key;
            this.content = content;
        }
    }
}
