using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using SharpDX.Win32;
using System.ComponentModel.Design.Serialization;

namespace PONG
{
    

    public class Game1 : Game
    {        
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        public Ball Kirby;
        int totalPlayers = 2;
        public Racket[] players;

        


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

            players[1] = new Racket(26, 57, Keys.W, Keys.S);
            players[2] = new Racket(973, 57, Keys.Up, Keys.Down); 
            for(int i = 0; i < totalPlayers; i++)
            {
                players[i].Initialize();
            }
            Kirby = new Ball(100, 200, 2, 0);
            Kirby.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {   
            
           _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            for(int i = 0; i < totalPlayers; i++)
            {
                players[i].LoadContent(Content);
            }
            Kirby = new Ball(100, 200, 2, 0);
            Kirby.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

             for(int i = 0; i < totalPlayers; i++)
            {
                players[i].Update(Kirby.hitbox);
            }
            //Kirby.intersectDetect(player1.intersect);
            //Kirby.intersectDetect(player2.intersect);
            Kirby.update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            

            _spriteBatch.Begin();

             for(int i = 0; i < totalPlayers; i++)
            {
                players[i].Draw(_spriteBatch);
            }
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

