using System.Collections.Generic;
using ConfigDatas;
using NarlonLib.Math;

namespace TaleofMonsters.DataType.Formulas
{
    internal class FormulaBook
    {
        static public double GetPhysicalDamage(int atk, int def)
        {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            rules.Add("atk", atk.ToString());
            rules.Add("def", def.ToString());
            return MathTool.GetFormulaResult(ConfigData.GetFormulaConfig((int)FormulaIds.PDamage).Rule, rules);
        }

        static public double GetMagicDamage(int atk)
        {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            rules.Add("atk", atk.ToString());
            return MathTool.GetFormulaResult(ConfigData.GetFormulaConfig((int)FormulaIds.MDamage).Rule, rules);
        }

        static public double GetHitRate(int hit, int dhit)
        {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            rules.Add("hit", hit.ToString());
            rules.Add("dit", dhit.ToString());
            return MathTool.GetFormulaResult(ConfigData.GetFormulaConfig((int)FormulaIds.Hit).Rule, rules);
        }
    }
}
