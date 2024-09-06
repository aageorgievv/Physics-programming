using System;
using System.Diagnostics;
using GXPEngine;
using GXPEngine.Core;


class Ball : EasyDraw
{

    public event System.Action<Ball> OnDestroyed;

    public bool hasShot = false;

    public int _radius;

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
        AimAndRotate();
        Move();
        CheckBoundaryCollisions();
        CheckBlockOverlaps();
        CheckTriangleOverlaps();
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
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Ellipse(0, 0, width - 1, height - 1);
    }

    void DrawBounds()
    {
        Vector2[] bounds = GetExtents();
        Gizmos.DrawLine(bounds[0].x, bounds[0].y, bounds[1].x, bounds[1].y);
        Gizmos.DrawLine(bounds[1].x, bounds[1].y, bounds[2].x, bounds[2].y);
        Gizmos.DrawLine(bounds[2].x, bounds[2].y, bounds[3].x, bounds[3].y);
        Gizmos.DrawLine(bounds[3].x, bounds[3].y, bounds[0].x, bounds[0].y);
    }

    void AimAndRotate()
    {
        if(hasShot == false)
        {
            Gizmos.DrawLine(x, y, Input.mouseX, Input.mouseY);

            Vec2 ballToMouse = new Vec2(Input.mouseX - x, Input.mouseY - y);
            float angle = ballToMouse.GetAngleDegrees();
            ball.rotation = angle;
            // Console.WriteLine($"Angle: {angle}");
        }
    }

    void CheckBlockOverlaps()
    {
        for(int i = 0; i < level.GetNumberOfBlocks(); i++)
        {
            Square square = level.GetBlock(i);

            foreach(CollisionFrame frame in square.CollisionFrames)
            {
                CircleVSLineCollision(frame, square);
            }

            foreach(LineCap cap in square.CollisionCaps)
            {
                CheckCircleVsCircleCollision(cap, square);
            }
        }
    }

    void CheckTriangleOverlaps()
    {
        for(int i = 0; i < level.GetNumberOfTriangles(); i++)
        {
            Triangle triangle = level.GetTriangle(i);

            foreach(CollisionFrame frame in triangle.CollisionFrames)
            {
                CircleVSLineCollision(frame, triangle);
            }

            foreach(LineCap cap in triangle.CollisionCaps)
            {
                CheckCircleVsCircleCollision(cap, triangle);
            }
        }
    }

    void CheckBoundaryCollisions()
    {

        foreach(LineSegment line in level.Lines)
        {
            CircleVSLineCollision(line, null);

        }
    }

    void CheckCircleVsCircleCollision(LineCap cap, Object owner)
    {
        Vec2 capToCircle = _position - cap._position;

        //discrete collision check
/*        if(capToCircle.Length() < _radius + cap._radius)
        {
            _velocity.Reflect(capToCircle.Normalized(), 1);
            HitBlock(owner);
        }*/

        //Continuous collision check
        float combinedRadius = (_radius + cap._radius);
        float a = _velocity.Dot(_velocity);
        float b = 2 * capToCircle.Dot(_velocity);
        float c = capToCircle.Dot(capToCircle) - combinedRadius * combinedRadius;

        float d = b * b - 4 * (a * c);

        float t = (-b - d * d) / (2 * a);

        Vec2 POI = _position + _velocity * t;

        if(a == 0)
        {
            return;
        }

        if (c < 0)
        {
            if (b < 0)
            {
                _velocity.Reflect(capToCircle.Normalized(), 1);
                HitBlock(owner);
            }
        }

        if(d < 0)
        {
            return;
        }

        if(0 <= t)
        {
            if(t < 1)
            {
                _position = POI;
                _velocity.Reflect(capToCircle.Normalized(), 1);
                HitBlock(owner);
            }
        }
    }

    void CircleVSLineCollision(LineSegment line, Object owner)
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


        /* Gizmos.DrawLine(start.x, start.y, start.x + startToBall.x, start.y + startToBall.y);
         Gizmos.DrawLine(start.x, start.y, start.x + startToBallProjection.x, start.y + startToBallProjection.y, null, 0xFFFF0000);*/

        Vec2 oldDistance = start + startToBallProjection - _oldPosition;
        float a = Mathf.Abs(oldDistance.Dot(lineNormal)) - _radius;
        float b = Mathf.Abs(-_velocity.Dot(lineNormal));
        float t = a / b;

        Vec2 pointOfImpact = _oldPosition + t * _velocity;


        float dot = startToBall.Dot(lineVector.Normalized());

        if(dot < 0 || dot > lineVector.Length())
        {
            return;
        }


        //compare distance with ball radius
        if(ballDistance < _radius)
        {
            _position = pointOfImpact;
            _velocity.Reflect(lineNormal, 1);
            HitBlock(owner);
            if(line.side == LineSide.Bottom)
            {
                LateDestroy();
                OnDestroyed?.Invoke(this);

                if(line.side == LineSide.Bottom)
                {
                    LateDestroy();
                    OnDestroyed?.Invoke(this);
                }
            }
        }
    }

    void HitBlock(Object owner)
    {
        if(owner is Square block)
        {
            block.TakeDamage(1);

            if(block.IsDead)
            {
                game.RemoveChild(block);
                level.Blocks.Remove(block);
            }
        }

        if(owner is Triangle triangle)
        {
            triangle.TakeDamage(1);

            if(triangle.IsDead)
            {
                game.RemoveChild(triangle);
                level.Triangles.Remove(triangle);
            }
        }
    }
}