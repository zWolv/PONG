using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PONG
{
    public class Racket
    {
        int x1;
        int y1;
        public Texture2D _sprite;
        private Vector2 _pos;
       
        
        

       public Racket(int _x1,int _y1)
        {
            x1 = _x1;
            y1 = _y1;
        }

       public void movement(int x,int y) {
            _pos = new Vector2(x, y);


            KeyboardState state = Keyboard.GetState();


            if (state.IsKeyDown(Keys.S))
            {
                _pos.Y += 5;
            }
       }
       


        public void Initialize()
        {
            
        }
        
        public void LoadContent(ContentManager content)
        {
            
            _sprite = content.Load<Texture2D>("batje");
            
            _pos = new Vector2(30,50);
        }

        public void Update()
        {
            movement(x1, y1);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_sprite,_pos, Color.White);

        }


    } 
}
