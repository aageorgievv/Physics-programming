using System;
using System.Drawing.Drawing2D;
using GXPEngine;
class Ball : EasyDraw
{
    private int _radius = 20;

    private float _speed = 10;

    public Vec2 _position;
    public Vec2 _velocity;

    public Ball(Vec2 position, int radius, float speed) : base(radius * 2 + 1, radius * 2 + 1)
    {
        this._position = position;
        this._radius = radius;
        this._speed = speed;

        SetOrigin(_radius, _radius);
        Draw(128, 128, 128);
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetKeyUp(Key.SPACE))
        {
            Vec2 ballToMouse = new Vec2(Input.mouseX - _position.x, Input.mouseY - _position.y);
            _velocity = ballToMouse.Normalized() * _speed;
        }

        _position += _velocity;

        x = _position.x;
        y = _position.y;
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
    }
}

