using GXPEngine.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class LineCap : GameObject
    {
        public Vec2 _position;
        public int _radius;
        private uint _color = 0xffffffff;

        public LineCap(Vec2 position, int radius = 1, uint color = 0xffff0000)
        {
            _position = position;
            _radius = radius;
            _color = color;
        }

        override protected void RenderSelf(GLContext glContext)
        {
            if(game != null)
            {
                Gizmos.DrawCross(_position.x, _position.y, _radius, null, _color);
            }
        }
    }
}
