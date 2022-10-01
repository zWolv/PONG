using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PONG
{
    public class Text
    {
        public int placeX;
        public int placeY;
        public int score;
        public string scoreBoardText = "";
        private SpriteFont scoreDisplay;

        public Text(int _placeX,int _placeY)
        {
            placeX = _placeX;
            placeY = _placeY;
        }

        public void LoadContent(ContentManager content)
        {
            scoreDisplay = content.Load<SpriteFont>("Score");
        }

        public void Update(Ball bal, int canvasWidth, int canvasHeight, Racket who)
        {
            bal.scoreUpdate = true;
            if (bal._location.X < 0)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            } else if (bal._location.X > canvasWidth)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            } else if (bal._location.Y > canvasHeight && !bal.tweeRackets)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            } else if (bal._location.Y < 0 && !bal.tweeRackets)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            }
                

               

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(scoreDisplay, score.ToString(), new Vector2(placeX, placeY), Color.Pink);
        }

    }
}