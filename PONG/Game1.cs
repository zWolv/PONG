using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// SharpDX.Direct2D1;
using SharpDX.MediaFoundation;
using SharpDX.Win32;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

namespace PONG
{
    public class Game1 : Game
    {        
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        public List<Ball> ballen = new List<Ball>();
        public List<Racket> players = new List<Racket>();
        int canvasWidth = 1000;
        int canvasHeight = 500;
        Texture2D test;
        int levens;

        enum gameStates
        {
            Menu,
            TweeSpelers,
            VierSpelers,
            GameOver,
        }

        gameStates currentGameState = gameStates.Menu;

        public Game1()
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

            protected override void Initialize()
            {
                // TODO: Add your initialization logic here
                _graphics.PreferredBackBufferWidth = canvasWidth;
                _graphics.PreferredBackBufferHeight = canvasHeight;
                _graphics.ApplyChanges();
                ballen.Add(new Ball(canvasWidth / 2, canvasHeight / 2, 5, 0));
                players.Add(new Racket(26, 57, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
                players.Add(new Racket(973, 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));
                players.Add(new Racket(500, 56, Keys.Right, Keys.Left, Racket.direction.horizontal, canvasWidth, canvasHeight));
            players.Add(new Racket(500, 477, Keys.H, Keys.G, Racket.direction.horizontal, canvasWidth, canvasHeight));
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

            test = Content.Load<Texture2D>("batje");
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

                KeyboardState state = Keyboard.GetState();
                if(state.IsKeyDown(Keys.D1))
            {
                levens++;
            } else if (state.IsKeyDown(Keys.D2))
            {
                levens--;
            }
            // TODO: Add your update logic here
            foreach (Racket self in players)
            {
                foreach (Racket other in players)
                {
                    if (self != other)
                    {
                        self.internalIntersect(self, other);
                    }
                }
            }

            foreach (Racket p in players)
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
                GraphicsDevice.Clear(Color.DarkBlue);

            // TODO: Add your drawing code here

                switch (currentGameState)
                {
                    case gameStates.Menu:
                    break;
                    case gameStates.TweeSpelers:
                    break;
                    case gameStates.VierSpelers:
                    break;
                    case gameStates.GameOver:
                    break;
                }

                _spriteBatch.Begin();

                foreach (Racket p in players)
                {
                    p.Draw(_spriteBatch);
                }

                foreach(Ball b in ballen)
                {
                    b.Draw(_spriteBatch);
                }

                for(int i = 0; i < levens; i++)
                {
                 _spriteBatch.Draw(test, new Vector2(60 + (i * 60), 70), Color.White);
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

