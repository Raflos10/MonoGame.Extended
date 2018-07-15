using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using TiledMaps.Systems;

namespace TiledMaps
{
    public class MainGame : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private World _world;
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;

        public MainGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _map = Content.Load<TiledMap>("test-map-1");
            _mapRenderer = new TiledMapRenderer(GraphicsDevice, _map);

            var font = Content.Load<BitmapFont>("Sensation");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _world = new WorldBuilder()
                .AddSystem(new MapRenderingSystem(GraphicsDevice))
                .Build();
        }

        protected override void UnloadContent()
        {
            _spriteBatch.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _world.Draw(gameTime);
            _mapRenderer.Draw();

            base.Draw(gameTime);
        }
    }
}
