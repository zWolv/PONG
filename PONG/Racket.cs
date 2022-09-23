using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System.ComponentModel.DataAnnotations;

namespace PONG
{
    public class Racket
    {
        int x1;
        int y1;
        public Texture2D _sprite;
        private Vector2 _pos;
        Keys player_up;
        Keys player_down;
        
        

       public Racket(int _x1,int _y1, Keys _player_up, Keys _player_down)
        {
            x1 = _x1;
            y1 = _y1;
            player_up = _player_up;
            player_down = _player_down;
        }

       public void movement() {
            KeyboardState state = Keyboard.GetState();


            if (state.IsKeyDown(player_up))
            {
                _pos.Y -= 5;
            } else if(state.IsKeyDown(player_down))
            {
                _pos.Y += 5;
            }
       }
       


        public void Initialize()
        {
            _pos = new Vector2(x1, y1);
        }
        
        public void LoadContent(ContentManager content)
        {
            
            _sprite = content.Load<Texture2D>("batje");
        }

        public void Update()
        {
            movement();
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_sprite,_pos, Color.White);

        }


    } 
}
