using System;
using System.Drawing;
using GXPEngine;
class Block : EasyDraw
{
    private int _hitPoints;
    private int offSet = 5;

    public Vec2 _position;

    private EasyDraw hitPointNumber;

    CollisionFrame Top;
    CollisionFrame Bottom;
    CollisionFrame Left;
    CollisionFrame Right;

    public Block(Vec2 position, int width, int height, int hitPoints) : base(width, height, false)
    {
        this._position = position;
        this._hitPoints = hitPoints;

        x = _position.x;
        y = _position.y;

        SetOrigin(0f, 0f);
        Draw(0, 100, 0);
        AddCollisionFrame();
        DrawHitPoints();
    }

    void Update()
    {

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
        hitPointNumber = new EasyDraw(width, height);
        hitPointNumber.Fill(Color.Yellow);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.TextSize(30);
        hitPointNumber.Text(" " + _hitPoints , width / 2f - offSet, height / 2f + offSet);
        AddChild(hitPointNumber);
    }

    public void AddCollisionFrame()
    {


/*        Top = new CollisionFrame(new Vec2(0, 0), new Vec2(width, 0), 0xff00ff00, 3);
        Bottom = new CollisionFrame(new Vec2(0, height), new Vec2(width, height), 0xff00ff00, 3);
        Right = new CollisionFrame(new Vec2(width, 0), new Vec2(width, height), 0xff00ff00, 3);
        Left = new CollisionFrame(new Vec2(0, 0), new Vec2(0, height), 0xff00ff00, 3);

        AddChild(Top);
        AddChild(Bottom);
        AddChild(Right);
        AddChild(Left);*/
    }
}