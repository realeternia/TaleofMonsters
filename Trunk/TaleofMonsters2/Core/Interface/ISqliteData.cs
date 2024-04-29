using System.Data.SQLite;

namespace TaleofMonsters.Core.Interface
{
    public interface ISqliteData
    {
        void Load(SQLiteConnection conn);
        void Save(SQLiteConnection conn);
    }
}
