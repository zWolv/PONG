using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

        public void Update(Ball bal, int canvasWidth, int canvasHeight, Racket who, int listItem, Game1 game)
        {
            if (bal._location.X < 0 && listItem == 1)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            } else if (bal._location.X > canvasWidth && listItem == 0)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            } else if (bal._location.Y > canvasHeight && !bal.tweeRackets && listItem == 2)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            } else if (bal._location.Y < 0 && !bal.tweeRackets && listItem == 3)
            {
                bal._location = bal._startLocation;
                bal._velocity = bal._startVelocity;
                score++;
            }   

            if(score == 5)
            {
                game.currentGameState = Game1.gameStates.GameOver;
            }
        }

        public void Reset()
        {
            score = 0;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(scoreDisplay, score.ToString(), new Vector2(placeX, placeY), Color.Pink);
        }

    }
}