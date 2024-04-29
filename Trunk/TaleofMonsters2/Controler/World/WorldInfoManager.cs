using System.Data.SQLite;

namespace TaleofMonsters.Controler.World
{
    class WorldInfoManager
    {
        private static int cardid;
        private static int cardfakeid = 100;

        public static int GetPlayerPid(string name)
        {
            int pid = 0;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select Id from mem_player where Name=@Name";
                cmd.Parameters.Add(new SQLiteParameter("Name", name));
                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pid = int.Parse(dr["Id"].ToString());
                }
                dr.Close();
            }
            return pid;
        }

        public static int GetMaxPlayerPid()
        {
            int pid = 0;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select max(Id)+1 from mem_player";
                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0].ToString() =="")
                    {
                        return 10000;
                    }
                    pid = int.Parse(dr[0].ToString());
                }
                dr.Close();
            }
            return pid;
        }

        public static int GetCardFakeId()
        {
            ++cardfakeid;
            if (cardfakeid>500000)
            {
                cardfakeid = 1;
            }
            return cardfakeid;
        }

        public static int GetCardUniqueId()
        {
            cardid++;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update system_config set value=@value where key='cardid'";
                cmd.Parameters.Add(new SQLiteParameter("value", cardid.ToString()));
                cmd.ExecuteNonQuery();
            }
            return cardid;
        }

        public static void LoadCardUniqueId()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select value from system_config where key='cardid'";
                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cardid = int.Parse(dr[0].ToString());
                }
                dr.Close();
            }
        }

        public static void SaveLastUserName(string name)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update system_config set value=@value where key='lastuser'";
                cmd.Parameters.Add(new SQLiteParameter("value", name));
                cmd.ExecuteNonQuery();
            }
        }

        public static string LoadLastUserName()
        {
            string name = "";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select value from system_config where key='lastuser'";
                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    name = dr[0].ToString();
                }
                dr.Close();
            }
            return name;
        }
    }
}
