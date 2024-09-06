using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine;


class Triangle : Block
{
    public CollisionFrame Left;
    public CollisionFrame Bottom;
    public CollisionFrame Right;

    public LineCap TopCap;
    public LineCap BottomRightCap;
    public LineCap BottomLeftCap;

    public override List<CollisionFrame> CollisionFrames => collisionFrames;
    public override List<LineCap> CollisionCaps => collisionCaps;

    private List<CollisionFrame> collisionFrames = new List<CollisionFrame>();
    private List<LineCap> collisionCaps = new List<LineCap>();

    public Triangle(Vec2 position, int width, int height, int hitPoints) : base(position, width, height, hitPoints, false)
    {
        hitPointNumber = new EasyDraw(width, height);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.TextSize(30);
        AddChild(hitPointNumber);

        Draw(100, 100, 100);
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
        Triangle(width / 2f, 0, width, height, 0, height);
    }

    void DrawHitPoints()
    {
        hitPointNumber.Clear(Color.Empty);
        hitPointNumber.Fill(Color.Yellow);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.Text(" " + _hitPoints, width / 2f - offSetX, height / 2 - 5 + offSetY);
        

    }

    public void AddCollisionFrame()
    {
        Bottom = new CollisionFrame(new Vec2(0, height), new Vec2(width, height), 0xff00ff00, 3);
        Right = new CollisionFrame(new Vec2(width / 2f, 0), new Vec2(width, height), 0xff00ff00, 3);
        Left = new CollisionFrame(new Vec2(width / 2f, 0), new Vec2(0, height), 0xff00ff00, 3);

        AddChild(Bottom);
        AddChild(Right);
        AddChild(Left);

        collisionFrames.Add(Bottom);
        collisionFrames.Add(Left);
        collisionFrames.Add(Right);

        TopCap = new LineCap(new Vec2(_position.x + width / 2f, _position.y));
        BottomRightCap = new LineCap(new Vec2(_position.x + width, _position.y + height));
        BottomLeftCap = new LineCap(new Vec2(_position.x, _position.y + height));


        AddChild(TopCap);
        AddChild(BottomRightCap);
        AddChild(BottomLeftCap);

        collisionCaps.Add(TopCap);
        collisionCaps.Add(BottomRightCap);
        collisionCaps.Add(BottomLeftCap);
    }

    void UpdateLineCaps()
    {
                TopCap._position = new Vec2(_position.x + width / 2f, _position.y);
        BottomRightCap._position = new Vec2(_position.x + width, _position.y + height);
        BottomLeftCap._position = new Vec2(_position.x, _position.y + height);
    }
}