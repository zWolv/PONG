using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
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
        // list voor ballen
        public List<Ball> ballen = new List<Ball>();
        // list voor spelers in verschillende gamemodes
        public List<Racket> tweePlayers = new List<Racket>();
        public List<Racket> vierPlayers = new List<Racket>();   
        int canvasWidth = 1000;
        int canvasHeight = 500;
        public Buttons tweeSpelers;

        public enum gameStates
        {
            Menu,
            TweeSpelers,
            VierSpelers,
            GameOver,
        }

        public gameStates currentGameState = gameStates.Menu;

        public Game1()
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //set the screen size
            _graphics.PreferredBackBufferWidth = canvasWidth;
            _graphics.PreferredBackBufferHeight = canvasHeight;
            _graphics.ApplyChanges();

            tweeSpelers = new Buttons(400, 250, gameStates.TweeSpelers);
            tweePlayers.Add(new Racket(26, 57, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
            tweePlayers.Add(new Racket(973, 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));

            vierPlayers.Add(new Racket(26, 57, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(973, 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(500, 56, Keys.Right, Keys.Left, Racket.direction.horizontal, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(500, 477, Keys.H, Keys.G, Racket.direction.horizontal, canvasWidth, canvasHeight));
            ballen.Add(new Ball(canvasWidth / 2, canvasHeight / 2, 1, 1));
            foreach (Ball b in ballen)
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
                
                        tweeSpelers.LoadContent(Content);
                        foreach (Racket p in tweePlayers)
                        {
                            p.LoadContent(Content, GraphicsDevice);
                        }
                        foreach (Ball b in ballen)
                        {
                            b.LoadContent(Content);
                        }
                        foreach(Racket p in vierPlayers)
                        {
                            p.LoadContent(Content, GraphicsDevice);
                        }

                //for (int i = 2; i < 4; i++)
                //{
                //    vierPlayers[i].hitbox.Width = vierPlayers[i]._sprite.Height;
                //    vierPlayers[i].hitbox.Height = vierPlayers[i]._sprite.Width;
                //    vierPlayers[i].hitbox.Offset(vierPlayers[i]._pos - vierPlayers[i].spriteOrigin - new Vector2((float)vierPlayers[i]._sprite.Width, 0));
                //}
            }

            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

            // TODO: Add your update logic here
                switch (currentGameState)
                {
                    case gameStates.Menu:
                        tweeSpelers.Update(this);
                    break;
                    case gameStates.TweeSpelers:
                        foreach (Racket p in tweePlayers)
                        {
                            p.Update();
                        }

                        foreach(Racket p in tweePlayers)
                        {
                            foreach (Ball b in ballen)
                            {
                                p.intersectDetection(b.hitbox);
                            }
                        }


                        foreach (Ball b in ballen)
                        {
                            foreach (Racket p in tweePlayers)
                            {
                                b.intersectDetect(p.intersect);
                            }
                        }

                        foreach (Ball b in ballen)
                        {   
                            foreach(Racket p in tweePlayers)
                            {
                                b.Update(canvasWidth, canvasHeight);
                            }
                            
                        }

                        
                    break;
                case gameStates.VierSpelers:
                        foreach (Racket self in vierPlayers)
                        {
                            foreach (Racket other in vierPlayers)
                            {
                                if (self != other)
                                {
                                    self.internalIntersect(self, other);
                                }
                            }
                        }

                        foreach (Racket p in vierPlayers)
                        {
                            foreach (Ball b in ballen)
                            {
                                p.intersectDetection(b.hitbox);
                            }
                        }


                        foreach (Ball b in ballen)
                        {
                            foreach (Racket p in vierPlayers)
                            {
                                b.intersectDetect(p.intersect);
                            }
                        }


                        foreach (Ball b in ballen)
                        {   
                            foreach(Racket p in vierPlayers)
                            {
                                b.Update(canvasWidth, canvasHeight);
                            }
                                
                        }
                    break;

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
                        _spriteBatch.Begin();
                        tweeSpelers.Draw(_spriteBatch);
                        _spriteBatch.End();
                    break;
                    case gameStates.TweeSpelers:
                        _spriteBatch.Begin();
                        foreach (Racket p in tweePlayers)
                        {
                            p.Draw(_spriteBatch);
                        }

                        foreach (Ball b in ballen)
                        {
                            b.Draw(_spriteBatch);
                        }
                        _spriteBatch.End();
                    break;
                    case gameStates.VierSpelers:
                        _spriteBatch.Begin();

                        foreach (Racket p in vierPlayers)
                        {
                            p.Draw(_spriteBatch);
                        }

                        foreach (Ball b in ballen)
                        {
                            b.Draw(_spriteBatch);
                        }

                        _spriteBatch.End();
                    break;
                    case gameStates.GameOver:
                    break;
                }

                

                base.Draw(gameTime);
            }

            static void  Main()
            {
               Game1 game = new Game1();
               game.Run();
            }
    }
}

