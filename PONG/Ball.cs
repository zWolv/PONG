
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PONG
{
    public class Ball
    {
        float x;
        float y;
        float speedX;
        float speedY;
        public Texture2D _kirbyBall;
        private Vector2 spriteOrigin;
        public Vector2 _location;
        public Vector2 _startLocation;
        public Vector2 _velocity;
        public Vector2 _startVelocity;
        bool intersect;
        Random rnd = new Random();
        public string Scoreboard;
        public bool tweeRackets = true;
        Vector2 oldVelocity;

        public Ball(float _x, float _y, float _speedX, float _speedY)
        {
            x = _x;
            y = _y;
            speedX = _speedX;
            speedY = _speedY;
        }

        public Rectangle hitbox
        {
            get
            {
                Rectangle balBounds = _kirbyBall.Bounds;
                balBounds.Offset(_location);
                return balBounds;
            }
        }

        public void Initialize()
        {

            _location = new Vector2(x, y);
            _startLocation = new Vector2(x - 25, y - 25);
            _velocity = new Vector2(speedX, speedY);
            _startVelocity = new Vector2(speedX, speedY);
        }

        public void intersectDetect(bool intersect)
        {
            if (intersect)
            {
                this.intersect = true;
            }
        }

        public void vierSpelers(int canvasWidth, int canvasHeight)
        {
            if (this.intersect && _location.Y < 53)
            {
                this.intersect = false;
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

        public void tweeSpelers(int canvasWidth, int canvasHeight)
        {
            if(tweeRackets)
            {
                if (_location.Y < 0 || _location.Y > canvasHeight - _kirbyBall.Height)
                {
                    _velocity.Y *= -1;
                }
            }

            if (this.intersect && _location.X < 53)
            {
                this.intersect = false;

                if(_velocity.X < 0 && _location.X > 27)
                {
                    _velocity.Y = rnd.Next(-5,5);
                    _velocity.X *= -1;
                    _velocity.X = 7 - Math.Abs(_velocity.Y);
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
                    _velocity.X = (7 - Math.Abs(_velocity.Y)) * -1;
                } else if (_location.X > canvasWidth - (53 + (_kirbyBall.Width / 2)))
                {
                    _velocity.Y *= -1;
                }
            }

            _location = Vector2.Add(_location, _velocity);
        }

            public void LoadContent(ContentManager content)
            {
                _kirbyBall = content.Load<Texture2D>("KirbyBallSprite");
                spriteOrigin = new Vector2(_kirbyBall.Width / 2, _kirbyBall.Height / 2);
                _location = _location - spriteOrigin;
            }

            public void Draw(SpriteBatch _spriteBatch)
            {
                //_spriteBatch.DrawString(scoreDisplay, "Score " + _score1, new Vector2(Game1.canvasWidth / 10, Game1.canvasHeight / 10), Color.Black);
                _spriteBatch.Draw(_kirbyBall, _location, null, Color.White);
            }
    }
}
