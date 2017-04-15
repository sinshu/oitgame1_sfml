using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace OitGame1
{
    class SfmlGraphics : IGameGraphics, IDisposable
    {
        private RenderWindow window;
        private Texture[] textures;
        private Sprite[] sprites;

        public SfmlGraphics(RenderWindow window)
        {
            this.window = window;

            window.Clear(new Color(128, 128, 128));
            window.Display();

            try
            {
                LoadTextures();
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public void Begin()
        {
        }

        public void End()
        {
            window.Display();
        }

        public void SetColor(int a, int r, int g, int b)
        {
        }

        public void DrawRectangle(int x, int y, int width, int height)
        {
        }

        public void DrawImage(GameImage image, int x, int y)
        {
            var sprite = sprites[(int)image];
            sprite.Position = new Vector2f(x, y);
            window.Draw(sprite);
        }

        public void DrawImage(GameImage image, int width, int height, int row, int col, int x, int y)
        {
            var sprite = sprites[(int)image];
            sprite.TextureRect = new IntRect(width * col, height * row, width, height);
            sprite.Origin = new Vector2f(0, 0);
            sprite.Position = new Vector2f(x, y);
            sprite.Rotation = 0;
            window.Draw(sprite);
        }

        public void DrawImage(GameImage image, int width, int height, int row, int col, int x, int y, int rotation)
        {
            var sprite = sprites[(int)image];
            sprite.TextureRect = new IntRect(width * col, height * row, width, height);
            var ox = width / 2;
            var oy = height / 2;
            sprite.Origin = new Vector2f(ox, oy);
            sprite.Position = new Vector2f(x + ox, y + oy);
            sprite.Rotation = (float)rotation / 512 * 360;
            window.Draw(sprite);
        }

        public void Test(int x, int y)
        {
        }

        public void Dispose()
        {
            if (sprites != null)
            {
                foreach (var sprite in sprites)
                {
                    if (sprite != null)
                    {
                        sprite.Dispose();
                    }
                }
                sprites = null;
            }
            if (textures != null)
            {
                foreach (var texture in textures)
                {
                    if (texture != null)
                    {
                        texture.Dispose();
                    }
                }
                textures = null;
            }
        }

        private void LoadTextures()
        {
            var textureCount = Enum.GetValues(typeof(GameImage)).Length;
            textures = new Texture[textureCount];
            sprites = new Sprite[textureCount];
            for (var i = 0; i < textureCount; i++)
            {
                var path = "images/" + Enum.GetName(typeof(GameImage), i) + ".png";
                Console.WriteLine(path);
                var texture = new Texture(path);
                textures[i] = texture;
                sprites[i] = new Sprite(texture);
            }
        }
    }
}
