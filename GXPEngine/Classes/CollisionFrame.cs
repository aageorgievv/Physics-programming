using GXPEngine;
using System.Security.Policy;
public class CollisionFrame : LineSegment
{

    public CollisionFrame(Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1) : base(pStart, pEnd, LineSide.None, pColor, pLineWidth)
    {
    }
}

