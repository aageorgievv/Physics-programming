using GXPEngine;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
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
        Console.WriteLine("Division ok? {0} (value={1}, should be (2,4)", summ4, outCome4);

        float degrees = Vec2.RadToDeg(1.5708f);
        Console.WriteLine("RadToDeg ok? {0} (value={1}, should be (90)", (int)degrees == 90, degrees);

        float radians = Vec2.DegToRad(90);
        bool isRadCorrect = Math.Abs(radians - 1.570796) < 1e-6f;
        Console.WriteLine("DegToRad ok? {0} (value={1}, should be (1.570796)", isRadCorrect, radians);

        Vec2 getUnitVectorRad = Vec2.GetUnitVectorRad(Mathf.PI);
        getUnitVectorRad.RoundSmallValues();
        bool isUnitVectorRad1 = getUnitVectorRad.x == -1 && getUnitVectorRad.y == 0;
        Console.WriteLine("GetUnitVectorRad1 ok? {0} (value={1}, should be (-1,0))", isUnitVectorRad1, getUnitVectorRad);

        Vec2 getUnitVectorDeg = Vec2.GetUnitVectorDeg(0); 
        getUnitVectorDeg.RoundSmallValues();
        bool isUnitVectorDeg = getUnitVectorDeg.x == 1 && getUnitVectorDeg.y == 0;
        Console.WriteLine("-getUnitVectorDeg1 ok? {0} (value={1}, should be (1,0))", isUnitVectorDeg, getUnitVectorDeg);

        Vec2 angleRadians = new Vec2(3, 4);
        angleRadians.SetAngleRadians(Mathf.PI);
        angleRadians.RoundSmallValues();
        bool isAngleRadians = angleRadians.x == -5 && angleRadians.y == 0;
        Console.WriteLine("SetAngleRadians ok? {0} (value={1}, should be (-5,0))", isAngleRadians, angleRadians);

        Vec2 angleDegrees = new Vec2(3, 4);
        angleDegrees.SetAngleDegrees(90);
        angleDegrees.RoundSmallValues();
        bool isAngleDegrees = angleDegrees.x == 0 && angleDegrees.y == 5;
        Console.WriteLine("-SetAngleDegrees ok? {0} (value={1}, should be (0,5))", isAngleDegrees, angleDegrees)
            ;

        Vec2 someVector = new Vec2(1, 1); // 45 degrees expected
        float someAngleDegrees2 = someVector.GetAngleDegrees();
        bool isAngleDegrees2 = someAngleDegrees2 == 45;
        Console.WriteLine("GetAngleDegrees ok? {0} (value={1}, should be 45)", isAngleDegrees2, someAngleDegrees2);

        Vec2 someVector2 = new Vec2(1, 1); // 45 degrees expected
        float someAngleRadians2 = someVector2.GetAngleRadians();
        bool isAngleRadians2 = someAngleRadians2 == 0.7853982f;
        Console.WriteLine("GetAngleRadians ok? {0} (value={1}, should be 0.7853982)", isAngleRadians2, someAngleRadians2);

        Vec2 rotateDegrees = new Vec2(1, 0);
        rotateDegrees.RotateDegrees(45);
        Console.WriteLine("RotateDegrees ok?  (value={0}, should be (0.7071068, 0.7071068)", rotateDegrees);

        Vec2 rotateRadians = new Vec2(1, 0);
        rotateRadians.RotateRadians(Mathf.PI); //rotate by 180 degrees
        rotateRadians.RoundSmallValues();
        bool isRotateRad = rotateRadians.x == -1 && rotateRadians.y == 0;
        Console.WriteLine("RotateRadians ok? {0}  (value={1}, should be (-1,0)", isRotateRad, rotateRadians);

        Vec2 randomVec = Vec2.RandomUnitVector();
        float length = randomVec.Length();
        bool isRandomUnitVector = length == 1;
        Console.WriteLine("RandomUnitVector ok? {0} (value={1}, should be 1", isRandomUnitVector, length);

        Vec2 vec2 = new Vec2(3, 4);
        vec2.RotateAroundDegrees(new Vec2(1, 1), 90);
        bool isRotateAroundDeg = vec2.x == -2 && vec2.y == 3;
        Console.WriteLine("RotateAroundDegrees ok? {0} (value={1}, should be (-2, 3)", isRotateAroundDeg, vec2);

        Vec2 vec3 = new Vec2(3, 4);
        vec3.RotateAroundRadians(new Vec2(1, 1), Mathf.PI / 2);
        bool isRotateAroundRad = vec2.x == -2 && vec2.y == 3;
        Console.WriteLine("RotateAroundRadians ok? {0} (value={1}, should be (-2, 3)", isRotateAroundRad, vec3);

        Vec2 start = new Vec2(0, 0);
        Vec2 end = new Vec2(1, 1);
        Vec2 line = end - start;
        Vec2 normal = line.Normal();
        float lengthNormal = normal.Length();
        bool normalLength = lengthNormal == 1;
        Console.WriteLine("Normal ok? {0} (value={1}, should be 1", normalLength, lengthNormal);

        Vec2 first = new Vec2(1, 2);
        Vec2 second = new Vec2(3, 4);
        float dotProduct = first.Dot(second.Normalized());
        float secondLenght = second.Length();
        bool isDot = dotProduct == 2.2;
        Console.WriteLine("Dot ok? {0} (value={1}, secondLenght:{2} should be 2.2", isDot, dotProduct, secondLenght);

        //-----------------------

        Vec2 A = new Vec2(1, 2);
        Vec2 B = new Vec2(3, 4);
        Vec2 projected = A.Project(B);
        Console.WriteLine("Project ok? {0} (value={1}, should be (1.32f, 1.76)", isDot, projected);

        Vec2 vectorToReflect = new Vec2(1, 1);
        Vec2 floorNormal = new Vec2(0, 1);
        Vec2 reflectedVector = vectorToReflect;
        reflectedVector.Reflect(floorNormal, 1f);
        Console.WriteLine($"Reflecting {vectorToReflect} on normal {floorNormal} is {reflectedVector}");
    }

    static void Main()
    {
        new MyGame().Start();

    }
}