using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PONG
{
    public class Buttons
    {
        Texture2D _sprite;
        Vector2 pos;
        int x1;
        int y1;
        MouseState previousMouseState = Mouse.GetState();
        MouseState mouseState = Mouse.GetState();
        SpriteFont spriteFont;
        Game1.gameStates gameState;
        string text;
        Vector2 center;

        public Buttons(int _x1,int _y1, Game1.gameStates _gameState, string _text)
        {
            x1 = _x1;
            y1 = _y1;
            gameState = _gameState;
            text = _text;
        }

        public void LoadContent(ContentManager content)
        {
            _sprite = content.Load<Texture2D>("buttonBounds");
            spriteFont = content.Load<SpriteFont>("Score");
            pos = new Vector2(x1, y1);
            center = new Vector2(100,95);
        }

        public void Update(Game1 game)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            
            
            if (mouseState.Position.X > pos.X && mouseState.Position.X < pos.X + _sprite.Width && mouseState.Position.Y > pos.Y && mouseState.Position.Y < pos.Y + _sprite.Height)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    game.currentGameState = gameState;
                }
            }
                

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, pos, Color.White);
            spriteBatch.DrawString(spriteFont, text, pos + center, Color.Black); 
        }

    }
}
