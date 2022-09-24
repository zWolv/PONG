using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;

namespace PONG
{
    

    public class Game1 : Game
    {        
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        public Racket player1;
        public Racket player2;
        public Ball Kirby;

        public Game1()
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            player1 = new Racket(0, 0, Keys.W, Keys.S);
            player2 = new Racket(947, 0, Keys.Up, Keys.Down);
            player1.Initialize();
            player2.Initialize();
            Kirby = new Ball(100, 200, 2, 0);
            Kirby.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {   
            
           _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            player1.LoadContent(Content);
            player2.LoadContent(Content);
            Kirby.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player1.Update();
            player2.Update();
            Kirby.update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            

            _spriteBatch.Begin();

            player1.Draw(_spriteBatch);
            player2.Draw(_spriteBatch);
            Kirby.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

            static void  Main()
            {
               Game1 game = new Game1();
               game.Run();
            }

    }



}

