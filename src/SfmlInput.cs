using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace OitGame1
{
    class SfmlInput : IGameInput, IDisposable
    {
        private static readonly PlayerKeySetting[] playerKeySettings;

        private int playerCount;

        private GameCommand[] commands;

        static SfmlInput()
        {
            playerKeySettings = new PlayerKeySetting[4];
            playerKeySettings[0] = new PlayerKeySetting(Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.Up, Keyboard.Key.Down);
            playerKeySettings[1] = new PlayerKeySetting(Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.W, Keyboard.Key.S);
            playerKeySettings[2] = new PlayerKeySetting(Keyboard.Key.F, Keyboard.Key.H, Keyboard.Key.T, Keyboard.Key.G);
            playerKeySettings[3] = new PlayerKeySetting(Keyboard.Key.J, Keyboard.Key.L, Keyboard.Key.I, Keyboard.Key.K);
        }

        public SfmlInput(int playerCount)
        {
            this.playerCount = playerCount;
            commands = new GameCommand[playerCount];
            for (var i = 0; i < commands.Length; i++)
            {
                commands[i] = new GameCommand(false, false, false, false);
            }
        }

        public void Update()
        {
        }

        public IList<GameCommand> Current
        {
            get
            {
                for (var i = 0; i < playerCount; i++)
                {
                    var keySetting = GetKeySetting(i);
                    var left = Keyboard.IsKeyPressed(keySetting.left);
                    var right = Keyboard.IsKeyPressed(keySetting.right);
                    var jump = Keyboard.IsKeyPressed(keySetting.jump);
                    var start = Keyboard.IsKeyPressed(keySetting.start);
                    commands[i] = new GameCommand(left, right, jump, start);
                }
                return commands;
            }
        }

        public bool Quit()
        {
            return false;
        }

        private static PlayerKeySetting GetKeySetting(int playerIndex)
        {
            return playerKeySettings[playerIndex % playerKeySettings.Length];
        }

        private class PlayerKeySetting
        {
            public readonly Keyboard.Key left;
            public readonly Keyboard.Key right;
            public readonly Keyboard.Key jump;
            public readonly Keyboard.Key start;

            public PlayerKeySetting(Keyboard.Key left, Keyboard.Key right, Keyboard.Key jump, Keyboard.Key start)
            {
                this.left = left;
                this.right = right;
                this.jump = jump;
                this.start = start;
            }
        }

        public void Dispose()
        {
        }
    }
}
