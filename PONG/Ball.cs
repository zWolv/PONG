
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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

        public Ball(int _x, int _y, float _speedX, float _speedY)
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
            _startLocation = new Vector2(x, y);
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

        public void Update(int canvasWidth, int canvasHeight)
        {

            if (_location.Y < 0 || _location.Y > canvasHeight - _kirbyBall.Height)
            {
                _velocity.Y *= -1;
            }

            if (this.intersect && _location.X < 53)
            {
                this.intersect = false;
                if(_velocity.X < 0 && _location.X > 27)
                {
                    _velocity.X *= -1;
                    _velocity.Y = rnd.Next(-7, 7);
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
                    _velocity.Y = rnd.Next(-7, 7);
                } else if (_location.X > canvasWidth - (53 + (_kirbyBall.Width / 2)))
                {
                    _velocity.Y *= -1;
                }
            }
                

            if (_location.X < 0 - _kirbyBall.Width)
            {
                _location = _startLocation;
                _velocity = _startVelocity;
                //_score2++;
            }
            else if (_location.X > 1000 + _kirbyBall.Width)
            {
                _location = _startLocation;
                _velocity = _startVelocity * -1;
                //_score1++;
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
