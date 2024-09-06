using System.Collections.Generic;
using System.Drawing;
using GXPEngine;
class Square : Block
{
    CollisionFrame Top;
    CollisionFrame Bottom;
    CollisionFrame Left;
    CollisionFrame Right;

    public LineCap TopRightCap;
    public LineCap TopLeftCap;
    public LineCap BottomRightCap;
    public LineCap BottomLeftCap;

    public List<CollisionFrame> CollisionFrames = new List<CollisionFrame>();
    public List<LineCap> CollisionCaps = new List<LineCap>();

    public Square(Vec2 position, int width, int height, int hitPoints) : base(position, width, height, hitPoints, false)
    {
        hitPointNumber = new EasyDraw(width, height);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.TextSize(30);

        Draw(0,100, 0);
        AddCollisionFrame();
    }

    void Update()
    {
        DrawHitPoints();
        UpdateLineCaps();
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        ShapeAlign(CenterMode.Min, CenterMode.Min); 
        Rect(0, 0, width, height);
    }

    void DrawHitPoints()
    {
        hitPointNumber.Clear(Color.Empty);
        hitPointNumber.Fill(Color.Yellow);
        hitPointNumber.Text(" " + _hitPoints , width / 2f - offSetX, height / 2f + offSetX);
        AddChild(hitPointNumber);
    }

    public void AddCollisionFrame()
    {
        Top = new CollisionFrame(new Vec2(0, 0), new Vec2(width, 0), 0xff00ff00, 3);
        Bottom = new CollisionFrame(new Vec2(0, height), new Vec2(width, height), 0xff00ff00, 3);
        Left = new CollisionFrame(new Vec2(0, 0), new Vec2(0, height), 0xff00ff00, 3);
        Right = new CollisionFrame(new Vec2(width, 0), new Vec2(width, height), 0xff00ff00, 3);

        AddChild(Top);
        AddChild(Bottom);
        AddChild(Left);
        AddChild(Right);

        CollisionFrames.Add(Top);
        CollisionFrames.Add(Bottom);
        CollisionFrames.Add(Left);
        CollisionFrames.Add(Right);

        TopRightCap = new LineCap(new Vec2(_position.x + width, _position.y));
        TopLeftCap = new LineCap(new Vec2(_position.x, _position.y));
        BottomRightCap = new LineCap(new Vec2(_position.x + width, _position.y + height));
        BottomLeftCap = new LineCap(new Vec2(_position.x, _position.y + height));

        AddChild(TopRightCap);
        AddChild(TopLeftCap);
        AddChild(BottomRightCap);
        AddChild(BottomLeftCap);

        CollisionCaps.Add(TopRightCap);
        CollisionCaps.Add(TopLeftCap);
        CollisionCaps.Add(BottomRightCap);
        CollisionCaps.Add(BottomLeftCap);

    }

    void UpdateLineCaps()
    {
        TopRightCap._position = new Vec2(_position.x + width, _position.y);
        TopLeftCap._position = new Vec2(_position.x, _position.y);
        BottomRightCap._position = new Vec2(_position.x + width, _position.y + height);
        BottomLeftCap._position = new Vec2(_position.x, _position.y + height);
    }
}