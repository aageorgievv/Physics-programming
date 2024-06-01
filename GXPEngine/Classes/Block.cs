using System.Drawing;
using System.Xml.Serialization;
using GXPEngine;
class Block : EasyDraw
{
    public int _radius;
    private int _hitPoints;

    public Vec2 _position;

    private EasyDraw hitPointNumber;

    public Block(Vec2 position, int radius, int hitPoints, float speed = 0) : base(radius, radius)
    {
        this._position = position;
        this._radius = radius;
        this._hitPoints = hitPoints;

        x = _position.x;
        y = _position.y;

        SetOrigin(radius, radius);
        Draw(0, 100, 0);

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
        Rect(0, 0, width,height);
    }

    void DrawHitPoints()
    {
        hitPointNumber = new EasyDraw(50, 50);
        hitPointNumber.Fill(Color.Red);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.Text(" " + _hitPoints , 25, 25);
        AddChild(hitPointNumber);
    }
}