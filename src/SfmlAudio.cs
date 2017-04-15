using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Audio;
using SFML.System;
using SFML.Window;

namespace OitGame1
{
    class SfmlAudio : IGameAudio, IDisposable
    {
        private int channelCount = 8;

        private SoundBuffer[] buffers;
        private Music music;
        private Sound[] channels;

        public SfmlAudio()
        {
            try
            {
                LoadSounds();
                channels = new Sound[channelCount];
                for (var i = 0; i < channels.Length; i++)
                {
                    channels[i] = new Sound();
                }
                music.Play();
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public void PlaySound(GameSound sound)
        {
            var channel = channels.FirstOrDefault(ch => ch.Status == SoundStatus.Stopped);
            if (channel == null)
            {
                Console.WriteLine("!");
                channel = channels.MaxBy(ch => ch.PlayingOffset.AsMicroseconds()).First();
            }
            channel.SoundBuffer = buffers[(int)sound];
            channel.Play();
        }

        public void Dispose()
        {
            if (channels != null)
            {
                foreach (var channel in channels)
                {
                    if (channel != null)
                    {
                        channel.Stop();
                        channel.Dispose();
                    }
                }
                channels = null;
            }
            if (buffers != null)
            {
                foreach (var buffer in buffers)
                {
                    if (buffer != null)
                    {
                        buffer.Dispose();
                    }
                }
                buffers = null;
            }
            if (music != null)
            {
                music.Dispose();
                music = null;
            }
        }

        private void LoadSounds()
        {
            var soundCount = Enum.GetValues(typeof(GameSound)).Length;
            buffers = new SoundBuffer[soundCount];
            for (var i = 0; i < soundCount; i++)
            {
                var path = "sounds/" + Enum.GetName(typeof(GameSound), i) + ".wav";
                Console.WriteLine(path);
                buffers[i] = new SoundBuffer(path);
            }
            music = new Music("sounds/Bgm.ogg");
            music.Loop = true;
        }
    }
}
