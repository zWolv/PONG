
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
        public Vector2 _location;
        public Vector2 _velocity;

        public Ball(int _x, int _y, int _speedX, int _speedY)
        {
            x = _x;
            y = _y;
            speedX = _speedX;
            speedY = _speedY;
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

        public void update()
        {
            _location = Vector2.Add(_location, _velocity);

            if (_location.X < 0 || _location.X > 600)
            {
                _velocity.X++;
                _velocity.X = -1 * (_velocity.X);
            }
        }

        public void LoadContent(ContentManager content)
        {
            _kirbyBall = content.Load<Texture2D>("KirbyBallSprite");
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_kirbyBall, _location, null, Color.White, 0, new Vector2(0), 0.07f, SpriteEffects.None, 0f);
        }
    }
}
