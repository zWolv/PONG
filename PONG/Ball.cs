
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PONG
{
    public class Ball
    {
        //positie
        float x;
        float y;
        //snelheid
        float speedX;
        float speedY;
        //bal texture
        public Texture2D _kirbyBall;
        // corrigeren voor het linksboven tekenen
        private Vector2 spriteOrigin;
        //vectorpositie
        public Vector2 _location;
        //startpositie
        public Vector2 _startLocation;
        //snelheidsvector
        public Vector2 _velocity;
        //startsnelheid
        public Vector2 _startVelocity;
        //check of er collision is met input racket
        public float maxVelocity = 7;
        bool intersect;
        //random bounce
        Random rnd = new Random();
        //wordt er met 2 of meer rackets gespeeld
        public bool tweeRackets = true;

        public Ball(float _x, float _y, float _speedX, float _speedY)
        {
            //geef de waardes van het geïnstancieerde object mee aan de class
            x = _x;
            y = _y;
            speedX = _speedX;
            speedY = _speedY;
        }

        //hitbox voor collision
        public Rectangle hitbox
        {
            get
            {
                Rectangle balBounds = _kirbyBall.Bounds;
                balBounds.Offset(_location);
                return balBounds;
            }
        }

        //initialize startwaardes
        public void Initialize()
        {
            _startLocation = new Vector2(x - 25, y - 25);
            _velocity = new Vector2(speedX, speedY);
            _startVelocity = new Vector2(speedX, speedY);
        }

        //neem de intersect bool van rackets over
        public void intersectDetect(bool intersect)
        {
            if (intersect)
            {
                this.intersect = true;
            }
        }

        // bounce de bal van het racket als er collision is
        public void vierSpelers(int canvasWidth, int canvasHeight)
        {
            //check of de bal wel echt bij een racket is -- geldt ook voor onderstaande statements
            if (this.intersect && _location.Y < 53)
            {
                //zorg dat de intersect niet langer dan 1 cycle duurt -- geldt ook voor onderstaande statements
                this.intersect = false;
                //check de richting van de bal en of de bal al voorbij de helft van het batje is -- geldt ook voor onderstaande statements
                if (_velocity.Y < 0 && _location.Y > 27)
                {
                    _velocity.Y *= -1;
                    _velocity.X = rnd.Next(-2,2);
                }
                else if (_location.Y < 27)
                {
                    _velocity.X *= -1;
                }
            }
            else if (this.intersect && _location.Y > canvasHeight - (53 + _kirbyBall.Height))
            {
                this.intersect = false;
                if (_velocity.Y > 0 && _location.Y < canvasHeight - (53 + (_kirbyBall.Height / 2)))
                {
                    _velocity.Y *= -1;
                    _velocity.X = rnd.Next(-2,2);
                }
                else if (_location.Y > canvasHeight - (53 + (_kirbyBall.Height / 2)))
                {
                    _velocity.X *= -1;
                }
            }

            tweeRackets = false;
            tweeSpelers(canvasWidth, canvasHeight);
        }

        //bounce de bal van het racket als er collision is
        public void tweeSpelers(int canvasWidth, int canvasHeight)
        {
            //als er maar 2 rackets zijn, bounced de bal van de boven- en onderkant
            if(tweeRackets)
            {
                if (_location.Y < 0 || _location.Y > canvasHeight - _kirbyBall.Height)
                {
                    _velocity.Y *= -1;
                }
            }

            //zie vierSpelers()
            if (this.intersect && _location.X < 53)
            {
                this.intersect = false;

                if(_velocity.X < 0 && _location.X > 27)
                {
                    _velocity.Y = rnd.Next(-5,5);
                    _velocity.X *= -1;
                    _velocity.X = maxVelocity - Math.Abs(_velocity.Y);
                    maxVelocity *= 1.1f;
                } else if (_location.X < 27)
                {
                    _velocity.Y *= -1;
                }
            }      

            else if(this.intersect && _location.X > canvasWidth - (53 + _kirbyBall.Width))
            {   
                this.intersect = false;
                if(_velocity.X > 0 && _location.X < canvasWidth - (53 + (_kirbyBall.Width / 2)))
                {
                    _velocity.X *= -1;
                    _velocity.Y = rnd.Next(-5,5);
                    _velocity.X = (maxVelocity - Math.Abs(_velocity.Y)) * -1;
                    maxVelocity *= 1.1f;
                } else if (_location.X > canvasWidth - (53 + (_kirbyBall.Width / 2)))
                {
                    _velocity.Y *= -1;
                }
            }

            //update de positie van de bal
            _location = Vector2.Add(_location, _velocity);

            //reset de snelheid nadat er gescoord is
            if(_location.X < 0)
            {
                maxVelocity = 7;
            } else if(_location.X > canvasWidth)
            {
                maxVelocity = 7;
            }
        }


            
            // laad de content van de bal
            public void LoadContent(ContentManager content)
            {
                _kirbyBall = content.Load<Texture2D>("KirbyBallSprite");
                spriteOrigin = new Vector2(_kirbyBall.Width / 2, _kirbyBall.Height / 2);
                _location = _location - spriteOrigin;
            }
            
            //teken de sprite
            public void Draw(SpriteBatch _spriteBatch)
            {
                _spriteBatch.Draw(_kirbyBall, _location, null, Color.White);
            }
    }
}
