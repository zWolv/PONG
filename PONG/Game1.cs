using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace PONG
{
    public class Game1 : Game
    {
        //spritebatch en graphicsdevicemanager references
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        // spritefont voor tekst
        SpriteFont spriteFont;
        // list voor ballen
        public List<Ball> ballen = new List<Ball>();
        // list voor spelers in verschillende gamemodes
        public List<Racket> tweePlayers = new List<Racket>();
        public List<Racket> vierPlayers = new List<Racket>();
        // variabele voor grootte speelwindow
        static int canvasWidth = 1000;
        static int canvasHeight = 500;
        // knoppen op begin- en eindscherm
        public Buttons tweeSpelers;
        public Buttons vierSpelers;
        public Buttons speedUp;
        public Buttons gameOver;
        // list voor scoredisplay
        public List<Score> score2player = new List<Score>();
        public List<Score> score4player = new List<Score>();
        //rect om de background te "clearen"
        Texture2D rect;
        //string voor winnaar
        string winner = "";

        //gamestates voor switch cases binnen de game loop
        public enum gameStates
        {
            Menu,
            TweeSpelers,
            VierSpelers,
            SpeedUp,
            GameOver,
        }

        // start-gamestate
        public gameStates currentGameState = gameStates.Menu;

        //constructor met verwijzingen
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

            //de knoppen van de menus 
            tweeSpelers = new Buttons(335, 30, gameStates.TweeSpelers, "Twee Spelers");
            vierSpelers = new Buttons(335, 180, gameStates.VierSpelers, "Vier Spelers");
            speedUp = new Buttons(335, 330, gameStates.SpeedUp, "Speedup mode");
            gameOver = new Buttons(335, 150, gameStates.Menu, "Terug naar menu");
            //twee rackets toevoegen aan bijbehorende list
            tweePlayers.Add(new Racket(0, 0, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
            tweePlayers.Add(new Racket(canvasWidth - 53, (canvasHeight / 2) - 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));
            // vier rackets toevoegen aan bijbehorende list
            vierPlayers.Add(new Racket(0, (canvasHeight / 2) - 57, Keys.W, Keys.S, Racket.direction.vertical, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket(canvasHeight - 53, (canvasHeight / 2) - 57, Keys.Up, Keys.Down, Racket.direction.vertical, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket((canvasHeight / 2) - 57, 0, Keys.Right, Keys.Left, Racket.direction.horizontal, canvasWidth, canvasHeight));
            vierPlayers.Add(new Racket((canvasHeight / 2) - 57, canvasHeight - 53, Keys.H, Keys.G, Racket.direction.horizontal, canvasWidth, canvasHeight));
            // bal toevoegen aan bijbehorende list
            ballen.Add(new Ball(400, canvasHeight / 2));
            //scoredisplay toevoegen aan bijbehorende list
            score2player.Add(new Score(100, canvasHeight / 2));
            score2player.Add(new Score(canvasWidth - 100, canvasHeight / 2));

            score4player.Add(new Score(100, canvasHeight / 2));
            score4player.Add(new Score(canvasHeight - 100, canvasHeight / 2));
            score4player.Add(new Score(canvasHeight / 2, 100));
            score4player.Add(new Score(canvasHeight / 2, canvasHeight - 100));

            //bal initialiseren
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

            //spritefont verwijzing
            spriteFont = Content.Load<SpriteFont>("Score");
            //rect een texture geven
            rect = new Texture2D(GraphicsDevice, canvasWidth, canvasHeight);
            //content voor knoppen laden
            tweeSpelers.LoadContent(Content);
            vierSpelers.LoadContent(Content);
            speedUp.LoadContent(Content);
            gameOver.LoadContent(Content);
            //content voor tweespeler mode laden
            foreach (Racket p in tweePlayers)
            {
                p.LoadContent(Content);
            }
            //content voor ballen laden
            foreach (Ball b in ballen)
            {
                b.LoadContent(Content);
            }
            //content voor vierspeler mode laden
            foreach (Racket p in vierPlayers)
            {
                p.LoadContent(Content);
            }
            //content voor scores laden
            foreach (Score num in score2player)
            {
                num.LoadContent(Content);
            }

            foreach(Score num in score4player)
            {
                num.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //gamestate switchcase om gamemodes etc te gebruiken
                switch (currentGameState)
                {
                // gamestate voor het startmenu
                    case gameStates.Menu:
                        tweeSpelers.Update(this);
                        vierSpelers.Update(this);
                        speedUp.Update(this);
                    break;
                // gamestate voor de tweespeler mode
                case gameStates.SpeedUp:
                case gameStates.TweeSpelers:
                    //bal collision checken
                    foreach (Racket p in tweePlayers)
                    {
                        foreach (Ball b in ballen)
                        {
                            p.intersectDetection(b.hitbox);
                        }
                    }

                    //bij bal collision balvelocity updaten
                    foreach (Ball b in ballen)
                    {
                        foreach (Racket p in tweePlayers)
                        {
                            b.intersectDetect(p.intersect);
                        }
                    }

                    //bal positie updaten
                    foreach (Ball b in ballen)
                    {
                        foreach (Racket p in tweePlayers)
                        {
                            b.tweeSpelers(canvasWidth, canvasHeight, this);
                        }
                    }

                    //positie van de rackets updaten
                    foreach (Racket p in tweePlayers)
                    {
                        p.movement();
                    }

                    //score per racket updaten als nodig is
                    for(int i = 0;i < 2; i++)
                    {
                        score2player[i].Update(ballen[0], canvasWidth, canvasHeight, tweePlayers[i], i, this);
                    }
                    break;
                //gamestate voor vierspeler mode
                case gameStates.VierSpelers:
                    //grootte scherm aanpassen
                    _graphics.PreferredBackBufferWidth = _graphics.PreferredBackBufferHeight;
                    _graphics.ApplyChanges();
                    //canvasgrootte updaten
                    canvasHeight = _graphics.PreferredBackBufferHeight;
                    canvasWidth = _graphics.PreferredBackBufferWidth;
                    //bal collision checken
                    foreach (Racket p in vierPlayers)
                        {
                            foreach (Ball b in ballen)
                            {
                                p.intersectDetection(b.hitbox);
                            }
                        }

                    //update balvelocity bij collision
                    foreach (Ball b in ballen)
                    {
                        foreach (Racket p in vierPlayers)
                        {
                            b.intersectDetect(p.intersect);
                        }
                    }

                    //check of rackets met andere rackets colliden
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

                    //positie van ballen updaten
                    foreach (Ball b in ballen)
                    {
                        foreach (Racket p in vierPlayers)
                        {
                            b.vierSpelers(canvasWidth, canvasHeight, this);
                        }
                    }

                    //positie van rackets updaten
                    foreach (Racket p in vierPlayers)
                    {
                        p.movement();
                    }



                    //score van spelers(op basis van racket) updaten
                    for (int i = 0; i < 4; i++)
                    {
                        score4player[i].Update(ballen[0], canvasWidth, canvasHeight, vierPlayers[i], i, this);
                    }
                    break;
                    // gamestate voor gameover scherm
                    case gameStates.GameOver:
                        gameOver.Update(this);
                    //reset windowgrootte
                    _graphics.PreferredBackBufferWidth = 2 * canvasHeight;
                    _graphics.ApplyChanges();
                    canvasHeight = _graphics.PreferredBackBufferHeight;
                    canvasWidth = _graphics.PreferredBackBufferWidth;

                    //reset alle scores en save de speler die heeft gewonnen
                    for (int i = 0; i < 4;i++)
                        {
                            if (score4player[i].score == 5)
                            {
                                switch (i)
                                {
                                    case 0:
                                        winner = "Speler Links";
                                    break;
                                    case 1:
                                        winner = "Speler Rechts";
                                    break;
                                    case 2:
                                        winner = "Speler Boven";
                                    break;
                                    case 3:
                                        winner = "Speler Onder";
                                    break;
                                    default:
                                    break;
                                }
                            }
                            score4player[i].Reset();
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            if (score2player[i].score == 5)
                            {
                                switch (i)
                                {
                                    case 0:
                                        winner = "Speler Links";
                                    break;
                                    case 1:
                                        winner = "Speler Rechts";
                                    break;
                                    default:
                                    break;
                                }
                            }

                            score2player[i].Reset();
                        }
                    break;
                    default:
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
                //teken het menu
                    case gameStates.Menu:
                        _spriteBatch.Begin();
                    //teken de vierspeler mode knop
                        vierSpelers.Draw(_spriteBatch);
                    //teken de tweespeler mode knop
                        tweeSpelers.Draw(_spriteBatch);
                    //teken de speedup mode knop
                        speedUp.Draw(_spriteBatch);
                        _spriteBatch.End();
                break;
                //teken de tweespeler mode
                case gameStates.SpeedUp:
                case gameStates.TweeSpelers:
                    _spriteBatch.Begin();
                    //teken de spelers
                    foreach (Racket p in tweePlayers)
                    {
                        p.Draw(_spriteBatch);
                    }

                    //teken de ballen
                    foreach (Ball b in ballen)
                    {
                        b.Draw(_spriteBatch);
                    }

                    //teken de scores
                    foreach (Score num in score2player)
                    {
                        num.Draw(_spriteBatch);
                    }
                    _spriteBatch.End();

                break;
                //teken de vierspeler mode
                case gameStates.VierSpelers:
                    _spriteBatch.Begin();

                    //teken de rackets
                    foreach (Racket p in vierPlayers)
                    {
                        p.Draw(_spriteBatch);
                    }

                    //teken de ballen
                    foreach (Ball b in ballen)
                    {
                        b.Draw(_spriteBatch);
                    }

                    //teken de scores
                    foreach(Score num in score4player)
                    {
                        num.Draw(_spriteBatch);
                    }

                    _spriteBatch.End();

                    
                break;
                //teken het gameoverscherm
                case gameStates.GameOver:
                    
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(spriteFont, winner + " is de winnaar!", new Vector2(360, 100), Color.Black);
                    //teken de achtergrond over de gespeelde gamemode
                    _spriteBatch.Draw(rect, new Vector2(0), Color.DarkBlue);
                    //teken de terugknop
                    gameOver.Draw(_spriteBatch);
                    _spriteBatch.End();
                break;
                default:
                break;
            }

            base.Draw(gameTime);
        }

        static void Main()
        {
            //maak game aan
            Game1 game = new Game1();
            //run de game
            game.Run();
        }
    }
}


