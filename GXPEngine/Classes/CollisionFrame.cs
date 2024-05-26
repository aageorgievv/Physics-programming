using GXPEngine;
class CollisionFrame : LineSegment
{

    public CollisionFrame(float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
        : base(new Vec2(pStartX, pStartY), new Vec2(pEndX, pEndY), pColor, pLineWidth)
    {
    }

    public CollisionFrame(Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1) : base(pStart, pEnd, pColor, pLineWidth)
    {
    }
}

