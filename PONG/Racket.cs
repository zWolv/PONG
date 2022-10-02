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
        public Texture2D batje1;
        public Texture2D batje2;
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
                if (_pos.Y < height - 114)
                {
                    //check of er input is -- geldt ook voor onderstaande statements
                    if (canMoveDown && state.IsKeyDown(player_down_left))
                    {
                        _pos.Y += 5;
                        if(_pos.Y > height - 114)
                        {
                            _pos.Y = height - 114;
                        }
                    }
                }

                if (_pos.Y > 0)
                {
                    if (canMoveUp && state.IsKeyDown(player_up_right))
                    {
                        _pos.Y -= 5;
                        if(_pos.Y < 0)
                        {
                            _pos.Y = 0;
                        }
                    }
                }
            }

            if (richting == direction.horizontal)
            {
                if (_pos.X < width - batje2.Width)
                {
                    if (canMoveRight && state.IsKeyDown(player_up_right))
                    {
                        _pos.X += 5;
                        if(_pos.X > width - batje2.Width)
                        {
                            _pos.X = width - batje2.Width;
                        }
                    }
                }

                if (_pos.X > 0)
                {
                    if (canMoveLeft && state.IsKeyDown(player_down_left))
                    {
                        _pos.X -= 5;
                        if(_pos.X < 1)
                        {
                            _pos.X = 0;
                        }
                    }
                }

            }
        }

        //check of rackets met zichzelf colliden en niet meer kunnen bewegen in bepaalde richting
        public void internalIntersect(Racket self, Racket other)
        {
            if (self.richting == Racket.direction.horizontal)
            {
                if (other.richting == Racket.direction.vertical)
                {
                    if (self.boundingBoxHorizontaalBatje.Intersects(other.boundingBoxVerticaalBatje))
                    {
                        if (self._pos.X > other._pos.X)
                        {
                            self.canMoveLeft = false;
                        }
                        else if (self._pos.X < other._pos.X)
                        {
                            self.canMoveRight = false;
                        }
                        else if(self._pos.X == other._pos.X && self._pos.Y > other._pos.Y)
                        {
                            self.canMoveRight = true;
                            self.canMoveLeft = false;
                            other.canMoveDown = false;
                            other.canMoveUp = true;
                        } else if (self._pos.X == other._pos.X && self._pos.Y < other._pos.Y)
                        {
                            self.canMoveLeft = false;
                            self.canMoveRight = true;
                            other.canMoveUp = false;
                            other.canMoveDown = true;
                        }
                    }
                }
            }
            else if (self.richting == Racket.direction.vertical)
            {
                if (other.richting == Racket.direction.horizontal)
                {
                    if (self.boundingBoxVerticaalBatje.Intersects(other.boundingBoxHorizontaalBatje))
                    {
                        if (self._pos.Y < other._pos.Y)
                        {
                            self.canMoveDown = false;
                        }
                        else if (self._pos.Y > other._pos.Y)
                        {
                            self.canMoveUp = false;
                        }
                        else if (self._pos.Y == other._pos.Y && self._pos.X > other._pos.X)
                        {
                            self.canMoveDown = true;
                            other.canMoveRight = false;
                            other.canMoveLeft = true;
                            self.canMoveUp = false;
                        }
                        else if (self._pos.Y == other._pos.Y && self._pos.X > other._pos.X)
                        {
                            self.canMoveDown = true;
                            other.canMoveRight = true;
                            other.canMoveLeft = false;
                            self.canMoveUp = false;

                        }
                    }
                }
            }
        }


        //bounding box voor de verticale rackets
        public Rectangle boundingBoxHorizontaalBatje
        {
            get
            {
                hitbox = batje2.Bounds;
                hitbox.Offset(_pos);
                return hitbox;
            }
        }

        //bounding box voor horizontale rackets
        public Rectangle boundingBoxVerticaalBatje
        {
            get
            {
                hitbox = batje1.Bounds;
                hitbox.Offset(_pos);
                return hitbox;
            }
        }

        // laad de sprites -- geef ze een positie
        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            batje1 = content.Load<Texture2D>("batje");
            batje2 = content.Load<Texture2D>("batje2");
            _pos = new Vector2(x1, y1);
        }

        // update de rackets -- overbodig?
        public void Update()
        {
            movement();
        }

        //teken de sprites van de rackets
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (richting == direction.vertical)
            {
                _spriteBatch.Draw(batje1, _pos, Color.White);
            }
            else if (richting == direction.horizontal)
            {
                _spriteBatch.Draw(batje2, _pos, Color.White);
            }


        }

        // check collision met de bal
        public void intersectDetection(Rectangle balHitbox)
        {
            if (richting == direction.horizontal && boundingBoxHorizontaalBatje.Intersects(balHitbox))
            {
                intersect = true;
            }
            else if(richting == direction.vertical && boundingBoxVerticaalBatje.Intersects(balHitbox)) 
            {
                intersect = true;
            }
            else if(!boundingBoxVerticaalBatje.Intersects(balHitbox) && !boundingBoxHorizontaalBatje.Intersects(balHitbox))
            {
                intersect = false;
            } else
            {
                intersect = false;
            }
        }
    }
}
