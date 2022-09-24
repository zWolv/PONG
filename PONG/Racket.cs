using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PONG
{
    public class Racket
    {
        int x1;
        int y1;
        public Texture2D _sprite;
        private Vector2 spriteOrigin;
        private Vector2 _pos;
        Keys player_up;
        Keys player_down;
        public bool intersect;

            public Racket(int _x1,int _y1, Keys _player_up, Keys _player_down)
            {
                x1 = _x1;
                y1 = _y1;
                player_up = _player_up;
                player_down = _player_down;
            }

            public void movement() 
            {
                KeyboardState state = Keyboard.GetState();

                if(_pos.Y <= 383) { 
                    if(state.IsKeyDown(player_down))
                    {
                        _pos.Y += 5;
                    }
                }

                if(_pos.Y >= 1) {
                    if (state.IsKeyDown(player_up))
                    {
                        _pos.Y -= 5;
                    }           
                }   
            }
       
            public Rectangle hitbox
            {
                get
                    {
                        Rectangle racketBounds = _sprite.Bounds;
                        racketBounds.Offset(_pos - spriteOrigin);
                        return racketBounds;
                    }
            }
        
            public void LoadContent(ContentManager content)
            {
                _sprite = content.Load<Texture2D>("batje");
                spriteOrigin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);
                _pos = new Vector2(x1, y1);
                _pos = _pos - spriteOrigin;
            }

            public void Update(Rectangle bal)
            {
                movement();

                if (hitbox.Intersects(bal))
                {
                    intersect = true;
                } 
                else
                {
                    intersect = false;
                }           
            }

            public void Draw(SpriteBatch _spriteBatch)
            {
                _spriteBatch.Draw(_sprite,_pos, Color.White);

            }
    } 
}
