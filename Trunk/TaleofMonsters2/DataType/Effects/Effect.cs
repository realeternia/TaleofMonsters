namespace TaleofMonsters.DataType.Effects
{
    internal class Effect
    {
        string soundName;
        EffectFrame[] frames;

        public string SoundName
        {
            get { return soundName; }
            set { soundName = value; }
        }

        public EffectFrame[] Frames
        {
            get { return frames; }
            set { frames = value; }
        }
    }

    class Effects
    {
        public static Effect None = new Effect();
    }
}
