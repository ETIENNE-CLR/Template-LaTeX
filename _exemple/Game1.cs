using GameProject.Enums;
using GameProject.Managers;
using GameProject.Views.Screens;
using GameProject.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
    internal class Game1 : Game
    {
        // Propriétés de la classe...
        public const string NAME_GAME = "Game Template";
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ScreenManager screenManager { get; private set; }

        // Constructeur de la classe...
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Méthodes de la classe...
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            const double RATIO = 5.0 / 8.0;
            const int BASE_WIDTH = 1080;

            screenManager = new ScreenManager(_graphics, new Dictionary<ScreenNames, MonogameWindow>()
            {
                { ScreenNames.TITLE_SCREEN, new TitleScreen(RATIO, BASE_WIDTH) },
                { ScreenNames.GAME_SCREEN, new GameScreen(RATIO, BASE_WIDTH) },
            });
            screenManager.SetScreen(ScreenNames.GAME_SCREEN);

            // Init window
            Window.Title = NAME_GAME;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AssetsManager.Load(GraphicsDevice);
            screenManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            screenManager.CurrentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            screenManager.CurrentScreen.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
