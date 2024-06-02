using GXPEngine;
using System;
using System.Drawing;

public class MyGame : Game
{

    LevelManager levelManager;
    static MyGame game;
    Ball ball;

    public MyGame() : base(1024, 768, false, false)
    {
        levelManager = new LevelManager(this);
        levelManager.SpawnBlocksAndTriangles();
        levelManager.SpawnBalls();

        DoTests();
    }


    void Update()
    {
        if(Input.GetKey(Key.W))
        {
            targetFps = 10;

        } else
        {
            targetFps = 60;
        }
    }

    static void DoTests()
    {
        Vec2 v1 = new Vec2(3, 4);
        float len = v1.Length(); // should be 5
        Console.WriteLine("Length ok? {0} (value={1}, should be 5)", len == 5, len);

        Vec2 v2 = new Vec2(3, 4);
        v2.Normalize();
        Console.WriteLine("Normalize ok?  Result:{0} ", v2);

        Vec2 v3 = new Vec2(3, 4);
        Vec2 len3 = v3.Normalized();
        Console.WriteLine("Normalized ok? Result:{0}", len3);

        Vec2 left = new Vec2(3, 4);
        Vec2 right = new Vec2(7, 6);
        Vec2 outCome = left + right;
        Console.WriteLine("Addition ok? Result:{0}", outCome);

        Vec2 xy = new Vec2(3, 4);
        xy.SetXY(6, 2);
        Console.WriteLine($"Position: {xy}");

        Vec2 left2 = new Vec2(5, 6);
        Vec2 right2 = new Vec2(2, 2);
        Vec2 outCome2 = left2 - right2;
        Console.WriteLine("Subtraction ok? Result:{0}", outCome2);

        Vec2 left3 = new Vec2(3, 4);
        float number = 5;
        Vec2 outCome3 = left3 * number;
        Console.WriteLine("Addition ok? Result:{0}", outCome3);

        float degrees = Vec2.RadToDeg(1.75f * Mathf.PI);
        Console.WriteLine("Rad result: " + degrees);

        float piFraction = Vec2.DegToRad(225) / Mathf.PI;
        Console.WriteLine("PI " + piFraction);

        Vec2 degUnit = Vec2.GetUnitVectorDeg(45);
        Console.WriteLine("GetUnitVectorDeg " + degUnit);

        Vec2 angleDegrees = new Vec2(1, 0);
        angleDegrees.SetAngleDegrees(45f);
        Console.WriteLine("SetAngleDegrees " + angleDegrees);

        Vec2 angleRadians = new Vec2(1, 0);
        angleRadians.SetAngleRadians(0.785398163f);
        Console.WriteLine("SetAngleRadians " + angleRadians);

        float deg = angleDegrees.GetAngleDegrees();
        Console.WriteLine("GetAngleDegrees " + deg);

        float rad = angleRadians.GetAngleRadians();
        Console.WriteLine("GetAngleRadians " + rad);

        Vec2 radUnit = Vec2.GetUnitVectorRad(0.707f);
        Console.WriteLine("GetUnitVectorRad " + radUnit);

        Vec2 rotateDegrees = new Vec2(1, 0);
        rotateDegrees.RotateDegrees(45);
        Console.WriteLine("rotateDegrees ok? " + rotateDegrees);

        Vec2 rotateRadians = new Vec2(1, 0);
        rotateRadians.RotateRadians(0.785398f);
        Console.WriteLine("rotateRadians ok? " + rotateRadians);


    }

    static void Main()
    {
        new MyGame().Start();

    }
}