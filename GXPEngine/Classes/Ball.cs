using GXPEngine;


class Ball : EasyDraw
{
    public int _radius = 10;
    private float _speed = 5;
    private float _bounciness = 1f;

    public Vec2 _position;
    private Vec2 _oldPosition;
    public Vec2 _velocity;

    private LevelManager level;

    public Ball(LevelManager pLevel, Vec2 position, int radius, float speed) : base(radius * 2, radius * 2, false)
    {

        this.level = pLevel;
        this._position = position;
        this._radius = radius;
        this._speed = speed;

        x = _position.x;
        y = _position.y;

        SetOrigin(radius, radius);
        Draw(255, 255, 255);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {

        if(Input.GetKeyUp(Key.SPACE))
        {
            _speed = Utils.Random(4f, 8f);
            Vec2 ballToMouse = new Vec2(Input.mouseX - _position.x, Input.mouseY - _position.y);
            _velocity = ballToMouse.Normalized() * _speed;
        }


        _oldPosition = _position;
        _position += _velocity;

        x = _position.x;
        y = _position.y;

        CheckBoundaryCollisions();
        CheckBlockOverlaps();
        //CheckTriangleOverlaps();
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Ellipse(0, 0, width - 1, height - 1);
    }

    void CheckBlockOverlaps()
    {
        int numBlocks = level.GetNumberOfBlocks();

        for(int i = 0; i < level.GetNumberOfBlocks(); i++)
        {
            Block block = level.GetBlock(i);
            if(AreBlocksOverlapping(block))
            {
                ResolveBlockCollision(block);
            }

            if(block._hitPoints <= 0)
            {
                block.Kill();
                level.Blocks.Remove(block);
            }
        }
    }

    /*    void CheckTriangleOverlaps()
        {
            for(int i = 0; i < level.GetNumberOfTriangles(); i++)
            {
                Triangle triangle = level.GetTriangle(i);

                foreach(CollisionFrame frame in triangle.CollisionFrames)
                {
                    CheckTriangleCollisions(frame);
                }
            }
        }*/

    bool AreBlocksOverlapping(Block block)
    {
        // Basic AABB (Axis-Aligned Bounding Box) collision detection
        return Mathf.Abs((block._position.x + block.width / 2f) - (_position.x + _radius)) < (_radius + block.width / 2f) &&
               Mathf.Abs((block._position.y + block.height / 2f) - (_position.y + _radius)) < (_radius + block.height / 2f);
    }

    void ResolveBlockCollision(Block block)
    {
        // Calculate overlap in both axes
        float overlapX = (_radius + block.width / 2f) - Mathf.Abs((block._position.x + block.width / 2f) - (_position.x + _radius));
        float overlapY = (_radius + block.height / 2f) - Mathf.Abs((block._position.y + block.height / 2f) - (_position.y + _radius));

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
    }

    void CheckTriangleCollisions(CollisionFrame collisionFrame)
    {
        /*        Vec2 difference = new Vec2(_position.x - collisionFrame.start.x, _position.y - collisionFrame.start.y);
                Vec2 lineNormal = (collisionFrame.end - collisionFrame.start).Normal();
                float ballDistance = difference.Dot(lineNormal);

                //compare distance with ball radius
                if(ballDistance < _radius)
                {
                    _velocity.Reflect(lineNormal, 1);
                }*/
    }

    void CheckBoundaryCollisions()
    {

        foreach(LineSegment line in level.Lines)
        {
            Vec2 difference = new Vec2(_position.x - line.start.x, _position.y - line.start.y);
            Vec2 lineNormal = (line.end - line.start).Normal();
            float ballDistance = difference.Dot(lineNormal);

            //compare distance with ball radius
            if(ballDistance < _radius)
            {
                _velocity.Reflect(lineNormal, 1);
            }
        }

        /*        if(_position.x - _radius < level.LeftXBoundary)
                {
                    float impactLeft = level.LeftXBoundary + _radius;
                    float timeOfImpact = (impactLeft - _oldPosition.x) / (_position.x - _oldPosition.x);

                    _position.x = _oldPosition.x + timeOfImpact * _velocity.x;
                    _position.y = _oldPosition.y + timeOfImpact * _velocity.y;
                    _velocity.x = -_bounciness * _velocity.x;
                } else if(_position.x + _radius > level.RightXBoundary)
                {
                    float impactRight = level.RightXBoundary - _radius;
                    float timeOfImpact = (impactRight - _oldPosition.x) / (_position.x - _oldPosition.x);
                    _position.x = _oldPosition.x + timeOfImpact * _velocity.x;
                    _position.y = _oldPosition.y + timeOfImpact * _velocity.y;
                    _velocity.x = -_bounciness * _velocity.x;
                }

                if(_position.y - _radius < level.TopYBoundary)
                {
                    float impactTop = level.TopYBoundary + _radius;
                    float timeOfImpact = (impactTop - _oldPosition.y) / (_position.y - _oldPosition.y);
                    _position.x = _oldPosition.x + timeOfImpact * _velocity.x;
                    _position.y = _oldPosition.y + timeOfImpact * _velocity.y;
                    _velocity.y = -_bounciness * _velocity.y;
                } else if(_position.y + _radius > level.BottomYBoundary)
                {
                    float impactBottom = level.BottomYBoundary - _radius;
                    float timeOfImpact = (impactBottom - _oldPosition.y) / (_position.y - _oldPosition.y);
                    _position.x = _oldPosition.x + timeOfImpact * _velocity.x;
                    _position.y = _oldPosition.y + timeOfImpact * _velocity.y;
                    _velocity.y = -_bounciness * _velocity.y;
                }*/
    }
}