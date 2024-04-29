using System.Data.Common;
using System.Data.SQLite;
using TaleofMonsters.Controler.World;

namespace TaleofMonsters.DataType.User
{
    class UserProfile
    {
        private static Profile profile;
        private static string profileName;

        public static string ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }

        public static Profile Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        public static InfoBasic InfoBasic
        {
            get { return profile.InfoBasic; }
        }

        public static InfoBag InfoBag
        {
            get { return profile.InfoBag; }
        }

        public static InfoCard InfoCard
        {
            get { return profile.InfoCard; }
        }

        public static InfoSkill InfoSkill
        {
            get { return profile.InfoSkill; }
        }

        public static InfoTask InfoTask
        {
            get { return profile.InfoTask; }
        }

        public static InfoRival InfoRival
        {
            get { return profile.InfoRival; }
        }

        public static InfoRecord InfoRecord
        {
            get { return profile.InfoRecord; }
        }

        public static InfoWorld InfoWorld
        {
            get { return profile.InfoWorld; }
        }

        static public void LoadFromDB(int pid)
        {
            profile = new Profile(pid);
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();
                profile.Load(conn);
                conn.Close();
            }
        }

        static public void SaveToDB()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                profile.Save(conn);
                trans.Commit();
                conn.Close();
            }
        }

        static public string GetHabitData()
        {
            return WorldInfoManager.LoadLastUserName();
        }

        static public void SaveHabitData(string name)
        {
            WorldInfoManager.SaveLastUserName(name);
        }
    }
}
