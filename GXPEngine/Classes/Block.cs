using System.Drawing;
using GXPEngine;
class Block : EasyDraw
{
    public int _hitPoints;
    private int offSet = 5;

    private EasyDraw hitPointNumber;

    CollisionFrame Top;
    CollisionFrame Bottom;
    CollisionFrame Left;
    CollisionFrame Right;

    public Block(Vec2 position, int width, int height, int hitPoints) : base(width, height, false)
    {
        this._hitPoints = hitPoints;

        x = position.x;
        y = position.y;

        hitPointNumber = new EasyDraw(width, height);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.TextSize(30);

        SetOrigin(0f, 0f);
        Draw(0, 100, 0);
        DrawHitPoints();
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
        hitPointNumber.Clear(Color.Green);
        hitPointNumber.Fill(Color.Yellow);
        hitPointNumber.Text(" " + _hitPoints , width / 2f - offSet, height / 2f + offSet);
        AddChild(hitPointNumber);
    }

    public void Kill()
    {
        Destroy();
    }
}