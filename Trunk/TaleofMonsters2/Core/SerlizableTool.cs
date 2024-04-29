using System.IO;
using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.Core
{
    internal class SerlizableTool
    {
       static public byte[] ToBytes(INlSerlizable iData)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    iData.Write(bw);
                }
                return ms.GetBuffer();
            }
        }

        static public void Parse(INlSerlizable iData, byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    iData.Read(br);
                }
            }
        }
    }
}
