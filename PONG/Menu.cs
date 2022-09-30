using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PONG
{
    public class Menu
    {
        Texture2D _sprite;
        Vector2 pos;
        Vector2 spriteOrigin;
        int x1;
        int y1;

        public Menu(int _x1,int _y1)
        {
            x1 = _x1;
            y1 = _y1;
        }

        public void LoadContent(ContentManager content)
        {
            _sprite = content.Load<Texture2D>("buttonBounds");
            spriteOrigin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);
            pos = new Vector2(x1, y1);
            pos = pos - spriteOrigin;
        }

        public void Update()
        {
            MouseState previousMouseState;
            MouseState mouseState = Mouse.GetState();
            

            if(mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, pos, Color.White);
        }

    }
}
