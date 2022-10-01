using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
        static int canvasWidth = 1000;
        static int canvasHeight = 500;
        public Buttons tweeSpelers;
        public Buttons vierSpelers;
        public List<Text> score = new List<Text>();

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

            tweeSpelers = new Buttons(335, 60, gameStates.TweeSpelers, "Twee Spelers");
            vierSpelers = new Buttons(335, 210, gameStates.VierSpelers, "Vier Spelers");
            tweePlayers.Add(new Racket(26, 57, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
            tweePlayers.Add(new Racket(973, 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));

            vierPlayers.Add(new Racket(26, 57, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(973, 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(500, 56, Keys.Right, Keys.Left, Racket.direction.horizontal, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(500, 503, Keys.H, Keys.G, Racket.direction.horizontal, canvasWidth, canvasHeight));

            ballen.Add(new Ball(559, canvasHeight / 2, 0, 0.1f));

            score.Add(new Text(100, canvasHeight / 2));
            score.Add(new Text(canvasWidth - 100, canvasHeight / 2));
            score.Add(new Text(canvasWidth / 2, 100));
            score.Add(new Text(canvasWidth / 2, canvasHeight - 100));

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
            vierSpelers.LoadContent(Content);
            foreach (Racket p in tweePlayers)
            {
                p.LoadContent(Content, GraphicsDevice);
            }
            foreach (Ball b in ballen)
            {
                b.LoadContent(Content);
            }
            foreach (Racket p in vierPlayers)
            {
                p.LoadContent(Content, GraphicsDevice);
            }
            foreach(Text num in score)
            {
                num.LoadContent(Content);
            }
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
                        vierSpelers.Update(this);
                    break;
                case gameStates.TweeSpelers:
                    foreach (Racket p in tweePlayers)
                    {
                        p.Update();
                    }

                    foreach (Racket p in tweePlayers)
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
                        foreach (Racket p in tweePlayers)
                        {
                            b.tweeSpelers(canvasWidth, canvasHeight);
                        }

                    }

                    for(int i = 0;i < 2; i++)
                    {
                        score[i].Update(ballen[0], canvasWidth, canvasHeight, tweePlayers[i], i);
                    }
                    break;
                case gameStates.VierSpelers:

                    foreach (Racket p in vierPlayers)
                    {
                        p.Update();
                    }

                    //foreach (Racket self in vierPlayers)
                    //{
                    //    foreach (Racket other in vierPlayers)
                    //    {
                    //        if (self != other)
                    //        {
                    //            self.internalIntersect(self, other);
                    //        }
                    //    }
                    //}

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
                        foreach (Racket p in vierPlayers)
                        {
                            b.vierSpelers(canvasWidth, canvasHeight);
                        }

                    }

                    for (int i = 0; i < 4; i++)
                    {
                        score[i].Update(ballen[0], canvasWidth, canvasHeight, vierPlayers[i], i);
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
                        vierSpelers.Draw(_spriteBatch);
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

                    for(int i = 0; i < 2; i++)
                    {
                        score[i].Draw(_spriteBatch);
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

                    foreach(Text num in score)
                    {
                        num.Draw(_spriteBatch);
                    }

                    foreach (Text num in score)
                    {
                        num.Draw(_spriteBatch);
                    }
                    _spriteBatch.End();

                    
                    break;
                case gameStates.GameOver:
                    break;
            }

            base.Draw(gameTime);
        }

        static void Main()
        {
            Game1 game = new Game1();
            game.Run();
        }
    }
}


