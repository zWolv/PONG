using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using SharpDX.Win32;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace PONG
{
    public class Game1 : Game
    {        
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        public List<Ball> ballen = new List<Ball>();
        public List<Racket> players = new List<Racket>();

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

                players.Add(new Racket(26, 57, Keys.W, Keys.S));
                players.Add(new Racket(973, 57, Keys.Up, Keys.Down));
                ballen.Add(new Ball(700, 50, 2, 0));
                foreach(Ball b in ballen)
                {
                    b.Initialize();
                }
                //Always leave this at the bottom
                base.Initialize();
            }

            protected override void LoadContent()
            {   
               _spriteBatch = new SpriteBatch(GraphicsDevice);

                // TODO: use this.Content to load your game content here
                foreach(Racket p in players)
                {
                    p.LoadContent(Content);
                }
                foreach(Ball b in ballen)
                {
                    b.LoadContent(Content);
                }
            }

            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here
                foreach(Racket p in players)
                {   
                    foreach(Ball b in ballen)
                    {
                        p.Update(b.hitbox);
                    }
                    
                }
                
                foreach(Ball b in ballen)
                {
                    foreach(Racket p in players)
                    {
                        b.intersectDetect(p.intersect);
                    }
                }
                
            
                foreach(Ball p in ballen)
                {
                    p.Update();
                }
            
                base.Update(gameTime);
            }

            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.White);

                // TODO: Add your drawing code here

                _spriteBatch.Begin();

                foreach(Racket p in players)
                {
                    p.Draw(_spriteBatch);
                }

                foreach(Ball b in ballen)
                {
                    b.Draw(_spriteBatch);
                }
           
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

