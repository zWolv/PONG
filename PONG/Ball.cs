
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        int x;
        int y;
        int speedX;
        int speedY;
        public Texture2D _kirbyBall;
        private Vector2 spriteOrigin;
        public Vector2 _location;
        public Vector2 _velocity;
        bool intersect;

        public Ball(int _x, int _y, int _speedX, int _speedY)
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
        public void update()
        {
            if (intersect)
            {
                _velocity.X = _velocity.X * -1;
            }

            _location = Vector2.Add(_location, _velocity);

            if (_location.X < 0 || _location.X > 600)
            {
                _velocity.X++;
                _velocity.X = -(_velocity.X);
            }
        }

        public void LoadContent(ContentManager content)
        {
            _kirbyBall = content.Load<Texture2D>("KirbyBallSprite");
            spriteOrigin = new Vector2(_kirbyBall.Width / 2, _kirbyBall.Height / 2);
            _location = _location - spriteOrigin;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_kirbyBall, _location, null, Color.White, 0, new Vector2(0), 0.07f, SpriteEffects.None, 0f);
        }
    }
}
