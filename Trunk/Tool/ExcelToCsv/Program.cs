using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelToCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles("./Xlsx");
            StreamWriter swTotal = new StreamWriter("./ConfigData/ConfigData.cs", false, Encoding.Default);

            swTotal.WriteLine("using System;");
            swTotal.WriteLine("using System.Collections.Generic;");
            swTotal.WriteLine("using NarlonLib.Math;");            
            swTotal.WriteLine("namespace ConfigDatas");
            swTotal.WriteLine("{");
            swTotal.WriteLine("\tpublic class ConfigData");
            swTotal.WriteLine("\t{");
            string loadStr = "";
            try
            {
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Extension == ".xlsx")
                    {
                        string keyname = fileInfo.Name.Substring(0, fileInfo.Name.Length - 5);
                        if (keyname.Length <= 1 || keyname[0] == '~')
                        {
                            continue;
                        }
                        Console.WriteLine(keyname);
                        loadStr += string.Format("\t\t\tLoad{0}();\r\n", keyname);
                        //StreamWriter sw = new StreamWriter(string.Format("./Config/{0}.csv",keyname), false, Encoding.Default);
                        //DataTable datas = GetExcelToDataTableBySheet(fileInfo.FullName, keyname + "$");
                        //sw.WriteLine(GetString(datas.Rows[0].ItemArray));
                        //for (int i = 3; i < datas.Rows.Count; i++)
                        //{
                        //    sw.WriteLine(GetString(datas.Rows[i].ItemArray));
                        //}
                        //sw.Close();

                        StreamWriter sw = new StreamWriter(string.Format("./ConfigData/{0}.cs", keyname), false, Encoding.Default);
                        DataTable datas = GetExcelToDataTableBySheet(fileInfo.FullName, keyname + "$");
                        object[] infos = datas.Rows[0].ItemArray;
                        object[] types = datas.Rows[1].ItemArray;
                        if (infos[0].ToString() != "Id")
                        {
                            infos = datas.Rows[2].ItemArray;
                        }

                        sw.WriteLine("namespace ConfigDatas");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class " + keyname + "Config");
                        sw.WriteLine("\t{");
                        string[] infoStr;
                        infoStr = new string[infos.Length];
                        for (int i = 0; i < infos.Length; i++)
                        {
                            sw.WriteLine("\t\tpublic {0} {1};", ConvertType(types[i]), infos[i]);
                            infoStr[i] = ConvertType(types[i]) + " " + infos[i];
                        }
                        sw.WriteLine("\t\tpublic " + keyname + "Config(){}");
                        sw.WriteLine("\t\tpublic " + keyname + "Config(" + string.Join(",", infoStr) + ")");
                        sw.WriteLine("\t\t{");
                        for (int i = 0; i < infos.Length; i++)
                        {
                            sw.WriteLine("\t\t\tthis.{0}= {0};", infos[i]);
                        }
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        swTotal.WriteLine(string.Format("\t\tpublic static Dictionary<int, {0}Config> {0}Dict= new Dictionary<int, {0}Config>();", keyname));
                        swTotal.WriteLine(string.Format("\t\tpublic static {0}Config None{0}= new {0}Config();", keyname));
                        swTotal.WriteLine(string.Format("\t\tpublic static {0}Config Get{0}Config(int id)", keyname));
                        swTotal.WriteLine("\t\t{");
                        swTotal.WriteLine(string.Format("\t\t\tif ({0}Dict.ContainsKey(id))", keyname));
                        swTotal.WriteLine(string.Format("\t\t\t\treturn {0}Dict[id];", keyname));
                        swTotal.WriteLine("\t\t\telse");
                        swTotal.WriteLine(string.Format("\t\t\t\treturn None{0};", keyname));
                        swTotal.WriteLine("\t\t}");
                        swTotal.WriteLine(string.Format("\t\tprivate static void Load{0}()", keyname));
                        swTotal.WriteLine("\t\t{");
                        for (int i = 3; i < datas.Rows.Count; i++)
                        {
                            swTotal.WriteLine(string.Format("\t\t\t{0}Dict.Add({1}, new {0}Config({2}));", keyname, datas.Rows[i].ItemArray[0], GetString(datas.Rows[i].ItemArray, datas.Rows[1].ItemArray)));
                        }
                        swTotal.WriteLine("\t\t}");

                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();                
            }

            swTotal.WriteLine("\t\tpublic static void LoadData()");
            swTotal.WriteLine("\t\t{");
            swTotal.WriteLine(loadStr);
            swTotal.WriteLine("\t\t}");
            swTotal.WriteLine("\t}");
            swTotal.WriteLine("}");
            swTotal.Close();
        }

        private static string ConvertType(object tp)
        {
            switch (tp.ToString())
            {
                case "SkillActiveType": return "int";
                case "Color": return "int";
            }
            return tp.ToString();
        }

        private static bool NeedParse(string tp)
        {
            if (tp.Substring(0, 2) == "RL")
            {
                return true;
            } 
            switch (tp)
            {
                case "SkillActiveType": return true;
            }
            return false;
        }

        private static string GetString(object[] datas,object[] types)
        {
            string[] strs=new string[datas.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                if (NeedParse(types[i].ToString()))
                {
                    strs[i] = string.Format("{0}.Parse(\"{1}\")", types[i], datas[i]);
                }
                else if (types[i].ToString() == "BuffEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format("delegate(IMonster o,int lv){{{0}}}", datas[i]);
                }
                else if (types[i].ToString() == "SkillInitialEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format("delegate(IMonster s,int lv){{{0}}}", datas[i]);
                }
                else if (types[i].ToString() == "SkillHitEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format("delegate(IMonster s,IMonster d,ref int hit,int lv){{{0}}}", datas[i]);
                }
                else if (types[i].ToString() == "SkillDamageEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format(
                                      "delegate(IMonster s,IMonster d,bool isActive,HitDamage damage, ref int minDamage, ref bool deathHit,int lv){{{0}}}",
                                      datas[i]);
                }
                else if (types[i].ToString() == "SkillAfterHitEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format(
                                      "delegate(IMonster s,IMonster d,HitDamage damage, bool deadHit,int lv){{{0}}}",
                                      datas[i]);
                }
                else if (types[i].ToString() == "SkillBurstCheckDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format(
                                      "delegate(IMonster s,IMonster d,bool isActive){{if({0})return true;return false;}}",
                                      datas[i]);
                }
                else if (types[i].ToString() == "SkillTimelyEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format("delegate(IMonster s,int lv){{{0}}}", datas[i]);
                }
                else if (types[i].ToString() == "SpellEffectDelegate")
                {
                    strs[i] = datas[i].ToString() == ""
                                  ? "null"
                                  : string.Format(
                                      "delegate(IMap m, IPlayer p, IPlayer r, IMonster t,System.Drawing.Point mouse,int lv){{{0}}}",
                                      datas[i]);
                }
                else if (types[i].ToString() == "FormatStringDelegate")
                {
                    if (datas[i].ToString() == "")
                    {
                        strs[i] = "";
                    }
                    else
                    {
                        strs[i] = datas[i].ToString();
                        Regex regex = new Regex(@"{.*?}");
                        MatchCollection mc = regex.Matches(datas[i].ToString());
                        int index = 0;
                        if (mc.Count > 0)
                        {
                            strs[i] = "\"" + datas[i] + "\"";
                            string[] datas2 = new string[mc.Count];
                            foreach (Match match in mc)
                            {
                                datas2[index] = match.Value.Substring(1, match.Value.Length - 2);

                                int pindex = strs[i].IndexOf(match.Value);
                                strs[i] = strs[i].Substring(0, pindex) + "{" + index++ + ":0}" +
                                          strs[i].Substring(pindex + match.Value.Length);
                            }
                            strs[i] = string.Format("{0},{1}", strs[i], string.Join(",", datas2));
                        }
                        else
                        {
                            strs[i] = "\"" + strs[i] + "\"";
                        }
                    }
                    strs[i] = string.Format("delegate(int lv){{ return string.Format({0});}}", strs[i]);
                }
                else if (types[i].ToString() == "string[]") //����
                {
                    string[] infos = datas[i].ToString().Split(';');
                    strs[i] = datas[i].ToString() == ""
                                  ? string.Format("new {0}{{}}", types[i])
                                  : string.Format("new {0}{{\"{1}\"}}", types[i], string.Join("\",\"", infos));
                }
                else if (types[i].ToString().Contains("[]")) //����
                {
                    string[] infos = datas[i].ToString().Split(';');
                    strs[i] = datas[i].ToString() == ""
                                  ? string.Format("new {0}{{}}", types[i])
                                  : string.Format("new {0}{{{1}}}", types[i], string.Join(",", infos));
                }
                else if (types[i].ToString() == "Color")
                {
                    string[] infos = datas[i].ToString().Split(';');
                    strs[i] = datas[i].ToString() == ""
                                  ? string.Format("new {0}{{}}", types[i])
                                  : string.Format("new {0}{{{1}}}", types[i], string.Join(",", infos));
                }
                else
                {
                    strs[i] =( (string) types[i] == "string" ? "\"" + datas[i] + "\"" : datas[i].ToString());
                }
            }
            return string.Join(",", strs);
        }

        public static DataTable GetExcelToDataTableBySheet(string fileFullPath, string sheetName)
        {
            //string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + FileFullPath +sheetNameed Properties='Excel 8.0; HDR=NO; IMEX=1'"; //������ֻ�ܲ���Excel2007֮ǰ(.xls)�ļ�  
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + fileFullPath + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; //�����ӿ��Բ���.xls��.xlsx�ļ�  
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter odda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", sheetName), conn);                    //("select * from [Sheet1$]", conn);  
            odda.Fill(ds, sheetName);
            conn.Close();
            return ds.Tables[0];
        } 
    }
}
