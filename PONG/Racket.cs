using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PONG
{
    public class Racket
    {
        int x1;
        int y1;
        public Texture2D _sprite;
        private Vector2 spriteOrigin;
        private Vector2 _pos;
        Keys player_up_right;
        Keys player_down_left;

        public bool intersect;
        public enum direction {
        horizontal,
        vertical
        }
        public direction richting;
        int width;
        int height;
        bool canMoveUp = true;
        bool canMoveDown = true;
        bool canMoveLeft = true;
        bool canMoveRight = true;

            public Racket(int _x1,int _y1, Keys _player_up_right, Keys _player_down_left, direction _richting, int _screenWidth, int _screenHeight)
            {
                x1 = _x1;
                y1 = _y1;
                player_up_right = _player_up_right;
                player_down_left = _player_down_left;
                richting = _richting;
                height = _screenHeight;
                width = _screenWidth;
                
            }

            public void movement() 
            {   
                KeyboardState state = Keyboard.GetState();


                if(richting == direction.vertical)
                {
                    if(_pos.Y <= height - 116) 
                    { 
                        if(state.IsKeyDown(player_down_left))
                        {
                        _pos.Y += 5;
                        }
                    }

                    if(_pos.Y >= height - height + 1) 
                    {
                        if (state.IsKeyDown(player_up_right))
                        {   
                            _pos.Y -= 5;
                        }           
                    }   
                }

                if(richting == direction.horizontal)
                {
                    if(_pos.X <= width)
                    {
                        if(state.IsKeyDown(player_up_right)) 
                        { 
                            _pos.X += 5;
                        }
                    }

                    if(_pos.X >= width - width + 114) 
                    { 
                        if(state.IsKeyDown(player_down_left))
                        {
                            _pos.X -= 5;
                        }
                    }
                    
                }  
            }
            

            public void internalIntersect(Racket self, Racket other)
            {   

                if(richting == Racket.direction.horizontal)
                {
                    if (self.hitbox.Intersects(other.hitbox)) 
                        {
                        if (self._pos.X > other._pos.X)
                        {
                            canMoveLeft = false;
                        } 
                        else if(self._pos.X < other._pos.X)
                        {
                            canMoveRight = false;
                        }

                            
                }
                else if(richting == Racket.direction.vertical)
                {
                    if (self.hitbox.Intersects(other.hitbox))
                    {
                        if(self._pos.X < other._pos.X)
                        {
                            canMoveUp = false;
                        }
                        else if(self._pos.X > other._pos.X)
                        {
                            canMoveDown = false;
                        }
                    }
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
        
            public void LoadContent(ContentManager  content)
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
                    canMoveUp = false;
                    canMoveDown = false;
                    canMoveRight = false;
                    canMoveLeft = false;
                } 
                else
                {
                    intersect = false;
                    canMoveUp = true;
                    canMoveDown = true;
                    canMoveRight = true;
                    canMoveLeft = true;
            }           
            }

            public void Draw(SpriteBatch _spriteBatch)
            {
                if(richting == direction.vertical)
                {
                    _spriteBatch.Draw(_sprite,_pos,Color.White);
                } else if(richting == direction.horizontal)
                {
                    _spriteBatch.Draw(_sprite,_pos,null,Color.White, 1.5707f, new Vector2(0),1f,SpriteEffects.None,0f);
                }
                

            }
    } 
}
