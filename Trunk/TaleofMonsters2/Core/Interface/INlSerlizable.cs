using System.IO;

namespace TaleofMonsters.Core.Interface
{
    public interface INlSerlizable
    {
        void Write(BinaryWriter bw);
        void Read(BinaryReader br);
    }
}
