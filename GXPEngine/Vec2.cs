using System;
using GXPEngine;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public float Length() //+
    {
        return Mathf.Sqrt(x * x + y * y); 
    }

    public void Normalize() //+
    {
        float length = Length();
        if(length != 0)
        {
            x = x / length;
            y = y / length;
        }
    }

    public Vec2 Normalized()  //+
    {
        float length = Length();
        if (length != 0)
        {
            return new Vec2(x / length, y / length);
        }
        else
        {
            return new Vec2(0, 0);
        }
    }

    public void SetXY(float vectorX, float vectorY) //+
    {
        x = vectorX;
        y = vectorY;
    }

    public static Vec2 operator +(Vec2 left, Vec2 right) //+
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right) //+
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    public static Vec2 operator *(Vec2 vector, float scaleAmount) //+
    {
        return new Vec2(vector.x * scaleAmount, vector.y * scaleAmount);
    }
    public static Vec2 operator *(float scaleAmount, Vec2 vector) //+
    {
        return new Vec2(scaleAmount * vector.x, scaleAmount * vector.y);
    }

    public static Vec2 operator /(Vec2 vector, float divideAmount) //+
    {
        return new Vec2(vector.x / divideAmount, vector.y / divideAmount);
    }
    public static Vec2 operator /(float divideAmount, Vec2 vector) //+
    {
        return new Vec2(divideAmount / vector.x, divideAmount / vector.y);
    }

    public static float RadToDeg(float radians) //+
    {
        return radians * (180.0f / Mathf.PI);
    }

    public static float DegToRad(float degree) //+
    {
        return degree * (Mathf.PI / 180.0f);
    }

    public void RoundSmallValues(float threshold = 1e-6f)// +
    {
        if(Math.Abs(x) < threshold) x = 0;
        if(Math.Abs(y) < threshold) y = 0;
    }

    public static Vec2 GetUnitVectorRad(float radian) //+
    {
        Vec2 vector = new Vec2();
        vector.SetXY(Mathf.Cos(radian), Mathf.Sin(radian));
        return vector;
    }

    public static Vec2 GetUnitVectorDeg(float degree) //+
    {
        float radian = DegToRad(degree);
        return GetUnitVectorRad(radian);
    }

    public void SetAngleRadians(float angleRad) //+
    {
        Vec2 vector = GetUnitVectorRad(angleRad);
        float length = Length();
        vector *= length;
        x = vector.x;
        y = vector.y;
    }

    public void SetAngleDegrees(float angleDeg)//+
    {
        Vec2 vector = GetUnitVectorDeg(angleDeg);
        float length = Length();
        vector *= length;
        x = vector.x;
        y = vector.y;
    }

    public float GetAngleDegrees()  //+
    {
        return RadToDeg(Mathf.Atan2(y, x));
    }

    public float GetAngleRadians() //+
    {
        return Mathf.Atan2(y, x);
    }

    public void RotateDegrees(float angleDeg) //+
    {
        float radians = DegToRad(angleDeg);
        float newX = x * Mathf.Cos(radians) - y * Mathf.Sin(radians);
        float newY = x * Mathf.Sin(radians) + y * Mathf.Cos(radians);

        x = newX;
        y = newY;
    }
    public void RotateRadians(float angleRad) //+
    {
        float newX = x * Mathf.Cos(angleRad) - y * Mathf.Sin(angleRad);
        float newY = x * Mathf.Sin(angleRad) + y * Mathf.Cos(angleRad);

        x = newX; 
        y = newY;
    }

    public static Vec2 RandomUnitVector() //+
    {
        float randomAngle = Utils.Random(0, 360);
        return GetUnitVectorDeg(randomAngle);
    }

    public void RotateAroundDegrees(Vec2 rotationPoint, float angleDeg) //++
    {
        x -= rotationPoint.x;
        y -= rotationPoint.y;
        RotateDegrees(angleDeg);
        x += rotationPoint.x;
        y += rotationPoint.y;
    }

    public void RotateAroundRadians(Vec2 rotationPoint, float angleRad) //++
    {
        x -= rotationPoint.x;
        y -= rotationPoint.y;
        RotateRadians(angleRad);
        x += rotationPoint.x;
        y += rotationPoint.y;
    }

    public Vec2 Normal() //+
    {
        Vec2 v = new Vec2(-y, x);
        v.Normalize();
        return v;
    }

    public float Dot(Vec2 vector) //+
    {
        return (x * vector.x + y * vector.y);
    }

    public Vec2 Project(Vec2 B)
    {
        return this.Dot(B) / B.Dot(B) * B;
    }
     
    public void Reflect(Vec2 normal, float bounciness)
    {
        Vec2 velocityOut = new Vec2((1 + bounciness) * Dot(normal) * normal.x, (1 + bounciness) * Dot(normal) * normal.y);
        x -= velocityOut.x;
        y -= velocityOut.y;
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
}