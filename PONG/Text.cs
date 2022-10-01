using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PONG
{
    public class Text
    {
        public int placeX;
        public int placeY;
        public int score;
        public string Scoreboard;
        private SpriteFont scoreDisplay;

        public Text(int _placeX,int _placeY, int _score, string _Scoreboard)
        {
            placeX = _placeX;
            placeY = _placeY;
            score = _score;
            Scoreboard = _Scoreboard;
        }

        public void LoadContent(ContentManager content)
        {
            scoreDisplay = content.Load<SpriteFont>("Score");
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(scoreDisplay, "Score " + score1, place, Color.Pink);
        }

    }
}