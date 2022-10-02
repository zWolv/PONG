using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PONG
{
    public class Racket
    {
        //positie
        int x1;
        int y1;
        //sprite voor rackets
        public Texture2D _sprite;
        //corrigeren voor linksboven tekenen
        public Vector2 spriteOrigin;
        //positie
        public Vector2 _pos;
        // input voor de speler
        Keys player_up_right;
        Keys player_down_left;
        //de hitbox voor collision
        public Rectangle hitbox;
        //bool voor collision
        public bool intersect;
        //enum voor richting van het racket
       public enum direction
        {
            horizontal,
            vertical
        }
        //richting van het racket
        public direction richting;
        //grootte van het speelscherm
        int width;
        int height;
        //bools of het racket ergens heen kan bewegen
        bool canMoveUp = true;
        bool canMoveDown = true;
        bool canMoveLeft = true;
        bool canMoveRight = true;
        Texture2D rectTexture;

        public Racket(int _x1, int _y1, Keys _player_up_right, Keys _player_down_left, direction _richting, int _screenWidth, int _screenHeight)
        {
            //geef de waardes van het geïnstancieerde object mee aan de class
            x1 = _x1;
            y1 = _y1;
            player_up_right = _player_up_right;
            player_down_left = _player_down_left;
            richting = _richting;
            height = _screenHeight;
            width = _screenWidth;

        }

        //update de positie van een racket
        public void movement()
        {
            //check of een knop ingedrukt wordt
            KeyboardState state = Keyboard.GetState();

            //check welke richting de racket beweegt -- geldt ook voor onderstaande statements
            if (richting == direction.vertical)
            {
                //check of racket binnen scherm is -- geldt ook voor onderstaande statements
                if (_pos.Y <= height - 116)
                {
                    //check of er input is -- geldt ook voor onderstaande statements
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

        //check of rackets met zichzelf colliden en niet meer kunnen bewegen in bepaalde richting
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

        //bounding box voor de verticale rackets
        public Rectangle boundingBoxVertical
        {
            get
            {
                hitbox = _sprite.Bounds;
                hitbox.Offset(_pos);
                return hitbox;
            }
        }

        //bounding box voor horizontale rackets
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

        // laad de sprites -- geef ze een positie
        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            _sprite = content.Load<Texture2D>("batje");
            spriteOrigin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);
            _pos = new Vector2(x1, y1);
            _pos = _pos - spriteOrigin;

            //test voor hitbox weergave
            rectTexture = new Texture2D(device, _sprite.Bounds.Width, _sprite.Bounds.Height);
            Color[] pixels = new Color[_sprite.Bounds.Width * _sprite.Bounds.Height];
            for (int y = 0; y < _sprite.Bounds.Height; y++)
                for (int x = 0; x < _sprite.Bounds.Width; x++)
                {
                    pixels[x + y * _sprite.Bounds.Width] = Color.Red;
                }

            rectTexture.SetData<Color>(pixels);

        }

        // update de rackets -- overbodig?
        public void Update()
        {
            movement();
        }

        //teken de sprites van de rackets
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

        // check collision met de bal
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
