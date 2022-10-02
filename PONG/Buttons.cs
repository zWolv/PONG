using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PONG
{
    public class Buttons
    {
        //texture van de knop
        Texture2D _sprite;
        //vectorpositie van de knop
        Vector2 pos;
        //inputpositie van de knop
        int x1;
        int y1;
        //mousestates met debounce
        MouseState previousMouseState = Mouse.GetState();
        MouseState mouseState = Mouse.GetState();
        //spritefont voor de text in de knop
        SpriteFont spriteFont;
        //gamestates van de game
        Game1.gameStates gameState;
        //string voor de text in de knop
        string text;
        //offset voor de text
        Vector2 offset;

        public Buttons(int _x1,int _y1, Game1.gameStates _gameState, string _text)
        {
            //geef de waardes van het geïnstancieerde object mee aan de class
            x1 = _x1;
            y1 = _y1;
            gameState = _gameState;
            text = _text;
        }

        //laad de sprites en geef posities
        public void LoadContent(ContentManager content)
        {
            _sprite = content.Load<Texture2D>("buttonBounds");
            spriteFont = content.Load<SpriteFont>("Score");
            pos = new Vector2(x1, y1);
            offset = new Vector2(80,55);
        }

        //check of er geklikt is en update de gamestate
        public void Update(Game1 game)
        {
            //update mousestates
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            
            //klikregistratie binnen de sprite
            if (mouseState.Position.X > pos.X && mouseState.Position.X < pos.X + _sprite.Width && mouseState.Position.Y > pos.Y && mouseState.Position.Y < pos.Y + _sprite.Height)
            {   
                //debounce -- 1x klikken registreert 1 keer
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    game.currentGameState = gameState;
                }
            }
                

        }

        //teken de knop met tekst
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, pos, Color.White);
            spriteBatch.DrawString(spriteFont, text, pos + offset, Color.Black);
        }

    }
}
