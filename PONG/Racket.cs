using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;

namespace PONG
{
    public class Racket
    {
        int x1;
        int x2;
        int y2;
        int y1;
        private Texture2D _sprite;
        private Vector2 _pos;
       
        
        

       public Racket(int _x1,int _y1,int _x2,int _y2)
        {
            x1 = _x1;
            y1 = _y1;
            x2 = _x2;
            y2 = _y2;
        }

       public void movement() {
       
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

        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_sprite,_pos, Color.White);

            _spriteBatch.End();
        }


    } 
}
