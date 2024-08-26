using GXPEngine;
using System;
using System.Drawing;
using GXPEngine.Core;

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
        float len = v1.Length();
        Console.WriteLine("Length ok? {0} (value={1}, should be 5)", len == 5, len);

        Vec2 vector = new Vec2(3, 4);
        vector.Normalize();
        bool isNormalize = vector.x == 0.6f && vector.y == 0.8f;
        Console.WriteLine("Normalize ok? {0} (value={1}, should be (0.6,0.8)", isNormalize, vector);

        Vec2 v3 = new Vec2(3, 4);
        Vec2 len2 = v3.Normalized();
        bool isNormalized = vector.x == 0.6f && vector.y == 0.8f;
        Console.WriteLine("Normalized ok? {0} (value={1}, should be (0.6,0.8)", isNormalized, len2);

        Vec2 ball = new Vec2(3, 4);
        ball.SetXY(4, 6);
        bool isXY = ball.x == 4 && ball.y == 6;
        Console.WriteLine("SetXY ok? {0} (value={1}, should be (4,6)", isXY, ball);

        Vec2 left = new Vec2(3, 4);
        Vec2 right = new Vec2(7, 6);
        Vec2 outCome = left + right;
        bool summ = outCome.x == 10 && outCome.y == 10;
        Console.WriteLine("Addition ok? {0} (value={1}, should be (10,10)", summ, outCome);

        Vec2 left2 = new Vec2(13, 14);
        Vec2 right2 = new Vec2(3, 4);
        Vec2 outCome2 = left2 - right2;
        bool summ2 = outCome2.x == 10 && outCome2.y == 10;
        Console.WriteLine("Subtraction ok? {0} (value={1}, should be (10,10)", summ2, outCome2);

        Vec2 v4 = new Vec2(4, 8);
        Vec2 outCome3 = v4 * 2;
        bool summ3 = outCome3.x == 8 && outCome3.y == 16;
        Console.WriteLine("Multiplication ok? {0} (value={1}, should be (8,16)", summ3, outCome3);

        Vec2 v5 = new Vec2(4, 8);
        Vec2 outCome4 = v5 / 2;
        bool summ4 = outCome4.x == 2 && outCome4.y == 4;
        Console.WriteLine("Division ok? {0} (value={1}, should be (2,14)", summ4, outCome4);

        float degrees = Vec2.RadToDeg(1.5708f);
        Console.WriteLine("RadToDeg ok? {0} (value={1}, should be (90)", (int)degrees == 90, degrees);

        float radians = Vec2.DegToRad(90);
        Console.WriteLine("DegToRad ok? (value={0}, should be (1.570796)", radians);

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

        Vec2 start = new Vec2(0, 0);
        Vec2 end = new Vec2(1, 1);
        Vec2 line = end - start;
        Vec2 normal = line.Normal();
        Console.WriteLine($"The normal of {line} is {normal}");

        Vec2 first = new Vec2(0.707f, 0.707f);
        Vec2 second = new Vec2(1, 0);
        float dotProduct = first.Dot(second);
        Console.WriteLine($"The dot product of {first} on {second} is {dotProduct}");

        Vec2 vectorToReflect = new Vec2(1, 1);
        Vec2 floorNormal = new Vec2(0, 1);
        Vec2 reflectedVector = vectorToReflect;
        reflectedVector.Reflect(floorNormal, 1f);
        Console.WriteLine($"Reflecting {vectorToReflect} on normal {floorNormal} is {reflectedVector}");

        Vec2 randomUnitVector = Vec2.RandomUnitVector();
        Console.WriteLine($"Is random unit vector {randomUnitVector} a unit vector? - {randomUnitVector.Length() == 1f}");
    }

    static void Main()
    {
        new MyGame().Start();

    }
}