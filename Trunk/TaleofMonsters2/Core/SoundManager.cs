using System.Collections.Generic;
using WMPLib;
using System.IO;

namespace TaleofMonsters.Core
{
    class SoundManager
    {
        private static WindowsMediaPlayer player;
        private static WindowsMediaPlayer bgmPlayer;
        private static Stack<string> bgmHistory;
        private static bool mute;

        public static void Play(string dir, string path)
        {
            string filePath = string.Format("Sound/{0}/{1}", dir, path);
            if (mute || !File.Exists(filePath))
            {
                return;
            }

            if (player == null)
            {
                player = new WindowsMediaPlayer();
            }

            player.URL = filePath;
            player.controls.play();
        }

        public static void PlayBGM(string path)
        {
            string filePath = string.Format("Bgm/{0}", path);
            if (mute || !File.Exists(filePath))
            {
                return;
            }

            if (bgmPlayer == null)
            {
                bgmPlayer = new WindowsMediaPlayer();
                bgmHistory = new Stack<string>();
            }

            bgmHistory.Push(path);
            bgmPlayer.URL = filePath;
            bgmPlayer.controls.play();
            bgmPlayer.settings.setMode("loop", true);
        }

        public static void PlayLastBGM()
        {
            if (bgmHistory == null || bgmHistory.Count == 0)
            {
                return;
            }

            string path = bgmHistory.Peek();
            string filePath = string.Format("Bgm/{0}", path);
            if (mute || !File.Exists(filePath))
            {
                return;
            }

            bgmHistory.Pop();
            bgmPlayer.URL = string.Format("Bgm/{0}", path);
            bgmPlayer.controls.play();
            bgmPlayer.settings.setMode("loop", true);
        }
    }
}
