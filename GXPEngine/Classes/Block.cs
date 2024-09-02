using System.Collections.Generic;
using System.Drawing;
using GXPEngine;
class Block : EasyDraw
{
    public int _hitPoints { get; set; }

    private int offSet = 5;

    private EasyDraw hitPointNumber;

    CollisionFrame Top;
    CollisionFrame Bottom;
    CollisionFrame Left;
    CollisionFrame Right;

    public List<CollisionFrame> CollisionFrames = new List<CollisionFrame>();

    public Block(Vec2 position, int width, int height, int hitPoints) : base(width, height, false)
    {
        this._hitPoints = hitPoints;

        x = position.x;
        y = position.y;

        hitPointNumber = new EasyDraw(width, height);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.TextSize(30);

        Draw(0,100, 0);
        AddCollisionFrame();
    }

    void Update()
    {
        DrawHitPoints();
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
        hitPointNumber.Text(" " + _hitPoints , width / 2f - offSet, height / 2f + offSet);
        AddChild(hitPointNumber);
    }

    public void Kill()
    {
        Destroy();
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

    }
}