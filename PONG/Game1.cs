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
        public Texture2D _kirbyBall;
        public Vector2 _location;

        public Game1()
        {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            _location = new Vector2(100, 50);
            base.Initialize();
        }

        protected override void LoadContent()
        {   
            
           _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            _kirbyBall = Content.Load<Texture2D>("KirbyBallSprite");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(_kirbyBall, _location, null, Color.White, 0, new Vector2(0), 0.1f, SpriteEffects.None, 0f);
            
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

