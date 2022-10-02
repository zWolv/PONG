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
        //grootte van het speelscherm
        int width;
        int height;


        public Racket(int _x1, int _y1, Keys _player_up_right, Keys _player_down_left, int _screenWidth, int _screenHeight)
        {
            //geef de waardes van het geïnstancieerde object mee aan de class
            x1 = _x1;
            y1 = _y1;
            player_up_right = _player_up_right;
            player_down_left = _player_down_left;
            height = _screenHeight;
            width = _screenWidth;

        }

        //update de positie van een racket
        public void movement()
        {
            //check of een knop ingedrukt wordt
            KeyboardState state = Keyboard.GetState();

           
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



        // laad de sprites -- geef ze een positie
        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            _sprite = content.Load<Texture2D>("batje");
            spriteOrigin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);
            _pos = new Vector2(x1, y1);
            _pos = _pos - spriteOrigin;
        }

        //teken de sprites van de rackets
        public void Draw(SpriteBatch _spriteBatch)
        {
                _spriteBatch.Draw(_sprite, _pos, Color.White);
            


        }

        // check collision met de bal
        public void intersectDetection(Rectangle bal)
        {
            if(boundingBoxVertical.Intersects(bal)) 
            {
                intersect = true;
            }
            else if(!boundingBoxVertical.Intersects(bal))
            {
                intersect = false;
            }
        }
    }
}
