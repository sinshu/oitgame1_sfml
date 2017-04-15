using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace OitGame1
{
    static class Program
    {
        static void Main(string[] args)
        {
            var playerCount = 4;
            var style = Styles.Close | Styles.Titlebar;
            using (var window = new RenderWindow(new VideoMode(640, 480), "OitGame1", style))
            using (var graphics = new SfmlGraphics(window))
            using (var audio = new SfmlAudio())
            using (var input = new SfmlInput(playerCount))
            {
                window.Closed += (sender, e) => ((RenderWindow)sender).Close();
                window.SetFramerateLimit(60);

                var world = new GameWorld(playerCount);
                world.Audio = audio;

                while (window.IsOpen)
                {
                    window.DispatchEvents();

                    input.Update();

                    world.Update(input.Current);
                    world.Draw(graphics);
                }
            }
        }
    }
}
