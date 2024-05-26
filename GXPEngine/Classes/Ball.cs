using System;
using System.Drawing.Drawing2D;
using GXPEngine;
class Ball : EasyDraw
{
    private int _radius = 20;

    private float _speed = 10;
    private float _bounciness = 1f;

    public Vec2 _position;
    private Vec2 _oldPosition;
    public Vec2 _velocity;

    private LevelManager level;

    public Ball(LevelManager pLevel, Vec2 position, int radius, float speed) : base(radius * 2 + 1, radius * 2 + 1)
    {
        this.level = pLevel;
        this._position = position;
        this._radius = radius;
        this._speed = speed;

        SetOrigin(_radius, _radius);
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
            Vec2 ballToMouse = new Vec2(Input.mouseX - _position.x, Input.mouseY - _position.y);
            _velocity = ballToMouse.Normalized() * _speed;
        }

        _oldPosition = _position;
        _position += _velocity;

        x = _position.x;
        y = _position.y;

        CheckForCollisions();
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
    }

    void CheckForCollisions()
    {

        if(_position.x - _radius < level.LeftXBoundary)
        {
            float impactLeft = level.LeftXBoundary + _radius;
            float timeOfImpact = (impactLeft - _oldPosition.x) / (_position.x - _oldPosition.x);

            _position.x = _oldPosition.x + timeOfImpact * _velocity.x;
            _position.y = _oldPosition.y + timeOfImpact * _velocity.y;
            _velocity.x = -_bounciness * _velocity.x;
        }
        
        else if (_position.x + _radius > level.RightXBoundary)
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
        }

        else if (_position.y + _radius > level.BottomYBoundary)
        {
            float impactBottom = level.BottomYBoundary - _radius;
            float timeOfImpact = (impactBottom - _oldPosition.y) / (_position.y - _oldPosition.y);
            _position.x = _oldPosition.x + timeOfImpact * _velocity.x;
            _position.y = _oldPosition.y + timeOfImpact * _velocity.y;
            _velocity.y = -_bounciness * _velocity.y;
        }
    }
}

