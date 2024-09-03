using GXPEngine.Core;

public enum LineSide
{
    None,
    Top,
    Bottom,
    Left,
    Right,
    BottomLeft,
    BottomRight
}

namespace GXPEngine
{
    /// <summary>
    /// Implements an OpenGL line
    /// </summary>
    public class LineSegment : GameObject
    {
        public LineSide side;
        public Vec2 start;
        public Vec2 end;

        public uint color = 0xffffffff;
        public uint lineWidth = 1;

        public LineSegment(float pStartX, float pStartY, float pEndX, float pEndY, LineSide side, uint pColor = 0xffffffff, uint pLineWidth = 1)
            : this(new Vec2(pStartX, pStartY), new Vec2(pEndX, pEndY), side, pColor, pLineWidth)
        {
        }

        public LineSegment(Vec2 pStart, Vec2 pEnd, LineSide pSide, uint pColor = 0xffffffff, uint pLineWidth = 1)
        {
            start = pStart;
            end = pEnd;
            color = pColor;
            lineWidth = pLineWidth;
            side = pSide;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //														RenderSelf()
        //------------------------------------------------------------------------------------------------------------------------
        override protected void RenderSelf(GLContext glContext)
        {
            if(game != null)
            {
                Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
            }
        }
    }
}