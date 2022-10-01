using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PONG
{
    public class Racket
    {
        int x1;
        int y1;
        public Texture2D _sprite;
        public Vector2 spriteOrigin;
        public Vector2 _pos;
        Keys player_up_right;
        Keys player_down_left;
        public Rectangle hitbox;
        public bool intersect;
       public enum direction
        {
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
        Texture2D rectTexture;

        public Racket(int _x1, int _y1, Keys _player_up_right, Keys _player_down_left, direction _richting, int _screenWidth, int _screenHeight)
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


            if (richting == direction.vertical)
            {
                if (_pos.Y <= height - 116)
                {
                    if (state.IsKeyDown(player_down_left))
                    {
                        _pos.Y += 5;
                    }
                }

                if (_pos.Y >= height - height + 1)
                {
                    if (state.IsKeyDown(player_up_right))
                    {
                        _pos.Y -= 5;
                    }
                }
            }

            if (richting == direction.horizontal)
            {
                if (_pos.X <= width)
                {
                    if (state.IsKeyDown(player_up_right))
                    {
                        _pos.X += 5;
                    }
                }

                if (_pos.X >= width - width + 114)
                {
                    if (state.IsKeyDown(player_down_left))
                    {
                        _pos.X -= 5;
                    }
                }

            }
        }

        public void internalIntersect(Racket self, Racket other)
        {

            if (richting == Racket.direction.horizontal)
            {
                if (self.hitbox.Intersects(other.hitbox))
                {
                    if (self._pos.X > other._pos.X)
                    {
                        canMoveLeft = false;
                    }
                    else if (self._pos.X < other._pos.X)
                    {
                        canMoveRight = false;
                    }


                }
                else if (richting == Racket.direction.vertical)
                {
                    if (self.hitbox.Intersects(other.hitbox))
                    {
                        if (self._pos.X < other._pos.X)
                        {
                            canMoveUp = false;
                        }
                        else if (self._pos.X > other._pos.X)
                        {
                            canMoveDown = false;
                        }
                    }
                }
            }


        }

        public Rectangle boundingBoxVertical
        {
            get
            {
                hitbox = _sprite.Bounds;
                hitbox.Offset(_pos);
                return hitbox;
            }
        }

        public Rectangle boundingBoxHorizontal
        {
            get
            {
                hitbox = _sprite.Bounds;
                hitbox.Offset(_pos - new Vector2((float)_sprite.Height, 5));
                hitbox.Height = _sprite.Width;
                hitbox.Width = _sprite.Height;
                return hitbox;
            }
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            _sprite = content.Load<Texture2D>("batje");
            spriteOrigin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);
            _pos = new Vector2(x1, y1);
            _pos = _pos - spriteOrigin;

            rectTexture = new Texture2D(device, _sprite.Bounds.Width, _sprite.Bounds.Height);
            Color[] pixels = new Color[_sprite.Bounds.Width * _sprite.Bounds.Height];
            for (int y = 0; y < _sprite.Bounds.Height; y++)
                for (int x = 0; x < _sprite.Bounds.Width; x++)
                {
                    pixels[x + y * _sprite.Bounds.Width] = Color.Red;
                }

            rectTexture.SetData<Color>(pixels);

        }

        public void Update()
        {
            movement();

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(rectTexture, _pos, null, Color.Red);
            if (richting == direction.vertical)
            {
                _spriteBatch.Draw(_sprite, _pos, Color.White);
            }
            else if (richting == direction.horizontal)
            {
                _spriteBatch.Draw(_sprite, _pos, null, Color.White, 1.5707f, new Vector2(0), 1f, SpriteEffects.None, 0f);
            }


        }

        public void intersectDetection(Rectangle bal)
        {
            if (boundingBoxHorizontal.Intersects(bal) && richting == direction.horizontal)
            {
                intersect = true;
                canMoveUp = false;
                canMoveDown = false;
                canMoveRight = false;
                canMoveLeft = false;
            }
            else if(boundingBoxVertical.Intersects(bal) && richting == direction.vertical) 
            {
                intersect = true;
                canMoveUp = false;
                canMoveDown = false;
                canMoveRight = false;
                canMoveLeft = false;
            }
            else if(!boundingBoxVertical.Intersects(bal) && !boundingBoxHorizontal.Intersects(bal))
            {
                intersect = false;
                canMoveUp = true;
                canMoveDown = true;
                canMoveRight = true;
                canMoveLeft = true;
            }
        }
    }
}
