
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PONG
{
   public class Ball
    {
        int x;
        int y;
        int rad;
        public Ball(int _x,int _y,int _rad)
        {
            x      = _x;
            y      = _y;
            rad    = _rad;
        }
    }
}
