
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
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
        public Vector2 _velocity;
        bool intersect;

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
                        balBounds.Offset(_location - spriteOrigin);
                        return balBounds;
                    }
            }

            public void Initialize()
            {
                _location = new Vector2(x, y);
                _velocity = new Vector2(speedX, speedY);
            }

            public void movement()
            {
                update();
            }

            public void intersectDetect(bool intersect)
            {
                if(intersect)
                {
                    this.intersect = true;
                } 
            }

            public void Update()
            {
                if (intersect)
                {
                    _velocity.X = _velocity.X * -1;
                    intersect = false;
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
                _spriteBatch.Draw(_kirbyBall, _location, null, Color.White);
            }
    }
}
