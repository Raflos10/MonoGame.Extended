using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Systems;

namespace TiledMaps.Systems
{
    public class MapRenderingSystem : DrawSystem
    {
        private readonly SpriteBatch _spriteBatch;

        public MapRenderingSystem(GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.FillRectangle(100, 100, 100, 100, Color.AliceBlue);
            _spriteBatch.End();
        }
    }

}