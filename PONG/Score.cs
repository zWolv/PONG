/// Gemaakt door Thomas van Egmond en Steijn Hoks
///              8471533              5002311

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PONG
{
    public class Score
    {
        //inputpositie
        public int placeX;
        public int placeY;
        //de score zelf
        public int score;
        //spritefont voor tekenen
        private SpriteFont scoreDisplay;

        public Score(int _placeX,int _placeY)
        {
            //geef de waardes van het geïnstancieerde object mee aan de class
            placeX = _placeX;
            placeY = _placeY;
        }

        //laad de spritefont
        public void LoadContent(ContentManager content)
        {
            scoreDisplay = content.Load<SpriteFont>("Score");
        }

        public void balScoreUpdate(Ball bal)
        {
            bal._location = bal._startLocation;
            bal._startVelocity.X = bal.rnd.Next(-3, 3);
            if (bal._startVelocity.X == 0)
            {
                bal._startVelocity.X = 1;
            }
            bal._startVelocity.Y = bal.maxVelocity - bal._startVelocity.X;
            bal._velocity = bal._startVelocity;

            score++;
        }
        //check of de bal buiten het veld is en bij welk racket de score moet
        public void Update(Ball bal, int canvasWidth, int canvasHeight, Racket who, int listItem, Game1 game)
        {
            if(game.currentGameState == Game1.gameStates.TweeSpelers || game.currentGameState == Game1.gameStates.SpeedUp)
            {
                if (bal._location.X < -1 * bal._kirbyBall.Width && listItem == 1)
                {
                    balScoreUpdate(bal);
                }
                else if (bal._location.X > canvasWidth && listItem == 0)
                {
                    balScoreUpdate(bal);
                }
            } else if(game.currentGameState == Game1.gameStates.VierSpelers)
            {
                if (bal._location.X < -1 * bal._kirbyBall.Width && listItem == 1)
                {
                    balScoreUpdate(bal);
                }
                else if (bal._location.X > canvasHeight && listItem == 0)
                {
                    balScoreUpdate(bal);
                }
                else if (bal._location.Y > canvasHeight && !bal.tweeRackets && listItem == 2)
                {
                    balScoreUpdate(bal);
                }
                else if (bal._location.Y < -1 * bal._kirbyBall.Height && !bal.tweeRackets && listItem == 3)
                {
                    balScoreUpdate(bal);
                }
            }

            //einde van de game als iemand 5 punten heeft
            if (score == 5)
            {
                game.currentGameState = Game1.gameStates.GameOver;
            }
        }

        //reset de score als de game eindigt
        public void Reset()
        {
            score = 0;
        }

        //teken de score
        public void Draw(SpriteBatch _spriteBatch)
        {
                _spriteBatch.DrawString(scoreDisplay, score.ToString(), new Vector2(placeX, placeY), Color.Pink);
        }

    }
}