using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using GXPEngine;


public abstract class BaseClass
{
    // ok ill do it simpler, cause this is just gonna get complicated otherwise

    public abstract int SomeNumber { get; }


    public abstract int SomeFunctionThatReturnsANumber();

}


public class DerivedClass : BaseClass
{
    public override int SomeNumber => 5;
    public override int SomeFunctionThatReturnsANumber()
    {
        throw new System.NotImplementedException();
    }
}

public class SomeOtherDerivedClass : BaseClass
{
    public override int SomeNumber => 10;
    public override int SomeFunctionThatReturnsANumber()
    {
        throw new System.NotImplementedException();
    }
}


public class SomeOtherRandomClass
{
    public void SomeFunction()
    {
        BaseClass baseClass = new DerivedClass();
        BaseClass derivedClass = new SomeOtherDerivedClass();


    }
}




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

    public override List<CollisionFrame> CollisionFrames => collisionFrames;
    public override List<LineCap> CollisionCaps => collisionCaps;

    private List<CollisionFrame> collisionFrames = new List<CollisionFrame>();
    private List<LineCap> collisionCaps = new List<LineCap>();

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

        collisionFrames.Add(Top);
        collisionFrames.Add(Bottom);
        collisionFrames.Add(Left);
        collisionFrames.Add(Right);

        TopRightCap = new LineCap(new Vec2(_position.x + width, _position.y));
        TopLeftCap = new LineCap(new Vec2(_position.x, _position.y));
        BottomRightCap = new LineCap(new Vec2(_position.x + width, _position.y + height));
        BottomLeftCap = new LineCap(new Vec2(_position.x, _position.y + height));

        AddChild(TopRightCap);
        AddChild(TopLeftCap);
        AddChild(BottomRightCap);
        AddChild(BottomLeftCap);

        collisionCaps.Add(TopRightCap);
        collisionCaps.Add(TopLeftCap);
        collisionCaps.Add(BottomRightCap);
        collisionCaps.Add(BottomLeftCap);

    }

    void UpdateLineCaps()
    {
        TopRightCap._position = new Vec2(_position.x + width, _position.y);
        TopLeftCap._position = new Vec2(_position.x, _position.y);
        BottomRightCap._position = new Vec2(_position.x + width, _position.y + height);
        BottomLeftCap._position = new Vec2(_position.x, _position.y + height);
    }
}