using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace PONG
{
    public class Game1 : Game
    {
        //spritebatch en graphicsdevicemanager references
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        // list voor ballen
        public List<Ball> ballen = new List<Ball>();
        // list voor spelers in verschillende gamemodes
        public List<Racket> tweePlayers = new List<Racket>();
        // variabele voor grootte speelwindow
        static int canvasWidth = 1000;
        static int canvasHeight = 500;
        // knoppen op begin- en eindscherm
        public Buttons tweeSpelers;
        public Buttons speedUp;
        public Buttons tweeBallen
        public Buttons gameOver;
        // list voor scoredisplay
        public List<Score> score = new List<Score>();
        //rect om de background te "clearen"
        Texture2D rect;

        //gamestates voor switch cases binnen de game loop
        public enum gameStates
        {
            Menu,
            TweeSpelers,
            SpeedUp,
            TweeBallen,
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
            tweeSpelers = new Buttons(335, 60, gameStates.TweeSpelers, "Standaard mode");
            speedUp = new Buttons(335, 210, gameStates.SpeedUp, "Speedup mode");
            tweeBallen = new Buttons(335, 360, gameStates.TweeBallen, "Twee-ballen mode");
            gameOver = new Buttons(335, 150, gameStates.Menu, "Terug naar menu");
            //twee rackets toevoegen aan bijbehorende list
            tweePlayers.Add(new Racket(26, 57, Keys.W, Keys.S, canvasWidth, canvasHeight));
            tweePlayers.Add(new Racket(973, 57, Keys.Up, Keys.Down, canvasWidth, canvasHeight));
            // bal toevoegen aan bijbehorende list
            ballen.Add(new Ball(canvasWidth / 2, canvasHeight / 2));
            ballen.Add(new Ball(canvasWidth / 2, canvasHeight / 2));
            //scoredisplay toevoegen aan bijbehorende list
            score.Add(new Score(100, canvasHeight / 2));
            score.Add(new Score(canvasWidth - 100, canvasHeight / 2));

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

            //rect een texture geven
            rect = new Texture2D(GraphicsDevice, canvasWidth, canvasHeight);
            //content voor knoppen laden
            tweeSpelers.LoadContent(Content);
            speedUp.LoadContent(Content);
            tweeBallen.LoadContent(Content);
            gameOver.LoadContent(Content);
            //content voor tweespeler mode laden
            foreach (Racket p in tweePlayers)
            {
                p.LoadContent(Content, GraphicsDevice);
            }
            //content voor ballen laden
            foreach (Ball b in ballen)
            {
                b.LoadContent(Content);
            }
            //content voor scores laden
            foreach (Score num in score)
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

                    //positie van de rackets updaten
                    foreach (Racket p in tweePlayers)
                    {
                        p.movement();
                    }

                    //bal positie updaten
                    foreach (Ball b in ballen)
                    {
                        foreach (Racket p in tweePlayers)
                        {
                            b.tweeSpelers(canvasWidth, canvasHeight, this);
                        }

                    }

                    //score per racket updaten als nodig is
                    for(int i = 0;i < 2; i++)
                    {
                        foreach (Ball b in ballen)
                        {
                            score[i].Update(b, canvasWidth, canvasHeight, tweePlayers[i], i, this);
                        }
                    }
                    break;
                    // gamestate voor gameover scherm
                    case gameStates.GameOver:
                        gameOver.Update(this);
                        //reset alle scores
                        for(int i = 0; i < 2;i++)
                        {
                            score[i].Reset();
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
                //teken het menu
                    case gameStates.Menu:
                        _spriteBatch.Begin();
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
                    for(int i = 0; i < 2; i++)
                    {
                        score[i].Draw(_spriteBatch);
                    }
                    _spriteBatch.End();

                    break;
                    //teken het gameoverscherm
                case gameStates.GameOver:
                    
                    _spriteBatch.Begin();
                    //teken de achtergrond over de gespeelde gamemode
                    _spriteBatch.Draw(rect, new Vector2(0), Color.DarkBlue);
                    //teken de terugknop
                    gameOver.Draw(_spriteBatch);
                    _spriteBatch.End();
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


