using System.Xml.Serialization;
using GXPEngine;
class Block : EasyDraw
{
    private int _radius = 20;

    private Vec2 _position;

    CollisionFrame Top;
    CollisionFrame Bottom;
    CollisionFrame Left;
    CollisionFrame Right;

    public Block(Vec2 position, int radius, float speed = 0) : base(radius * 2 + 1, radius * 2 + 1)
    {
        this._position = position;
        this._radius = radius;

        x = _position.x;
        y = _position.y;

        Draw(0, 100, 0);

        AddCollisionFrame();
    }

    void Update()
    {

    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Rect(_radius, _radius, 2 * _radius, 2 * _radius);
    }

    public void AddCollisionFrame()
    {


        Top = new CollisionFrame(new Vec2(0 + _radius * 2, 0 ), new Vec2(0, 0), 0xff00ff00, 3);
        Bottom = new CollisionFrame(new Vec2(0 - 2, 0 + _radius * 2), new Vec2(0 + _radius * 2 + 1, 0 + _radius * 2), 0xff00ff00, 3);
        Right = new CollisionFrame(new Vec2(0 + _radius * 2, 0 - 1), new Vec2(0 + _radius * 2, 0 + _radius * 2), 0xff00ff00, 3);
        Left = new CollisionFrame(new Vec2(0, 0 - 1), new Vec2(0, 0 + _radius * 2 + 1), 0xff00ff00, 3);

        AddChild(Top);
        AddChild(Bottom);
        AddChild(Right);
        AddChild(Left);
    }
}