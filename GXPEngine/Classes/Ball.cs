﻿using System;
using System.Diagnostics;
using GXPEngine;
using GXPEngine.Core;


class Ball : EasyDraw
{
    public event System.Action<Ball> OnDestroyed;

    public bool hasShot = false;

    public int _radius = 10;

    private float _speed = 5;
    private float _bounciness = 1f;

    public Vec2 _position
    {
        get { return new Vec2(x, y); }
        set { x = value.x; y = value.y; }
    }
    private Vec2 _oldPosition;
    public Vec2 _velocity;

    private LevelManager level;

    private Sprite ball;

    public Ball(LevelManager pLevel, Vec2 position, int radius, float speed) : base(radius * 2, radius * 2, false)
    {

        this.level = pLevel;
        this._position = position;
        this._radius = radius;
        this._speed = speed;

        //Draw(255, 255, 255);
        SetOrigin(width / 2, height / 2);

        ball = new Sprite("Assets/circle.png");
        ball.SetOrigin(ball.width / 2, ball.height / 2);
        AddChild(ball);
        ball.scale = (float)radius / ball.width * 2;
    }

    void Update()
    {
        Move();
        //DrawBounds();
    }

    void Move()
    {

        if(Input.GetKeyUp(Key.SPACE) && hasShot == false)
        {
            _speed = Utils.Random(4f, 8f);
            Vec2 ballToMouse = new Vec2(Input.mouseX - x, Input.mouseY - y);
            _velocity = ballToMouse.Normalized() * _speed;
            hasShot = true;
        }

        _oldPosition = _position;
        _position += _velocity;

        CheckBoundaryCollisions();
        CheckBlockOverlaps();
        CheckTriangleOverlaps();
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Ellipse(0, 0, width - 1, height - 1);

        //_texture = new Texture2D("Assets/circle.png");
    }

    void DrawBounds()
    {
        Vector2[] bounds = GetExtents();
        Gizmos.DrawLine(bounds[0].x, bounds[0].y, bounds[1].x, bounds[1].y);
        Gizmos.DrawLine(bounds[1].x, bounds[1].y, bounds[2].x, bounds[2].y);
        Gizmos.DrawLine(bounds[2].x, bounds[2].y, bounds[3].x, bounds[3].y);
        Gizmos.DrawLine(bounds[3].x, bounds[3].y, bounds[0].x, bounds[0].y);
        Gizmos.DrawCross(x, y, 50, color: 0xffff00ff);
    }

    void CheckBlockOverlaps()
    {
/*        for(int i = 0; i < level.GetNumberOfBlocks(); i++)
        {
            Block block = level.GetBlock(i);

            foreach(CollisionFrame frame in block.CollisionFrames)
            {
                CheckBrickCollisions(frame);
            }
        }*/
    }

    void CheckTriangleOverlaps()
    {
        for(int i = 0; i < level.GetNumberOfTriangles(); i++)
        {
            Triangle triangle = level.GetTriangle(i);

            foreach(CollisionFrame frame in triangle.CollisionFrames)
            {
                CheckBrickCollisions(frame);
            }
        }
    }

/*    bool AreBlocksOverlapping(Block block)
    {
        // Basic AABB (Axis-Aligned Bounding Box) collision detection. The method calculates the distance between the centers of the ball and the block along the x and y axes.
        return Mathf.Abs((block.x + block.width / 2f) - (_position.x + _radius)) < (_radius + block.width / 2f) &&
               Mathf.Abs((block.y + block.height / 2f) - (_position.y + _radius)) < (_radius + block.height / 2f);
    }

    void ResolveBlockCollision(Block block)
    {
        // Calculate overlap in both axes
        float overlapX = (_radius + block.width / 2f) - Mathf.Abs((block.x + block.width / 2f) - (_position.x + _radius));
        float overlapY = (_radius + block.height / 2f) - Mathf.Abs((block.y + block.height / 2f) - (_position.y + _radius));

        // Adjust position to resolve collision
        if(overlapX <= overlapY)
        {
            // Reverse velocity along X axis
            block._hitPoints -= 1;
            _velocity.x = -_velocity.x;
        } else
        {
            // Reverse velocity along Y axis
            block._hitPoints -= 1;
            _velocity.y = -_velocity.y;
        }
    }*/

    void CheckBrickCollisions(CollisionFrame collisionFrame)
    {
        CircleVSLineCollision(collisionFrame);
    }

    void CircleVSLineCollision(LineSegment line)
    {
        Vector2 startTransformed = line.TransformPoint(line.start.x, line.start.y);
        Vector2 endTransformed = line.TransformPoint(line.end.x, line.end.y);
        Vec2 start = new Vec2(startTransformed.x, startTransformed.y);
        Vec2 end = new Vec2(endTransformed.x, endTransformed.y);


        Vec2 startToBall = new Vec2(x - start.x, y - start.y);
        Vec2 lineVector = end - start;

        Vec2 lineNormal = (end - start).Normal();
        float ballDistance = Mathf.Abs(startToBall.Dot(lineNormal));
        Vec2 startToBallProjection = startToBall.Project(lineVector);

        /*Gizmos.DrawLine(start.x, start.y, start.x + startToBall.x, start.y + startToBall.y);
        Gizmos.DrawLine(start.x, start.y, start.x + startToBallProjection.x, start.y + startToBallProjection.y, null, 0xFFFF0000);*/

        Vec2 oldDistance = start + startToBallProjection - _oldPosition;
        float a = Mathf.Abs(oldDistance.Dot(lineNormal)) - _radius;
        float b = Mathf.Abs(-_velocity.Dot(lineNormal));
        float t = a / b;

        Vec2 pointOfImpact = _oldPosition + t * _velocity;


        float dot = startToBall.Dot(lineVector.Normalized());

        if (dot < 0 || dot > lineVector.Length())
        {
            return;
        }


        //compare distance with ball radius
        if(ballDistance < _radius)
        {
            _position = pointOfImpact;
            _velocity.Reflect(lineNormal, 1);
            if(line.side == LineSide.Bottom)
            {
                LateDestroy();
                OnDestroyed?.Invoke(this);
            }
        }
    }



    void CheckBoundaryCollisions()
    {

        foreach(LineSegment line in level.Lines)
        {
            CircleVSLineCollision(line);
        }
    }
}