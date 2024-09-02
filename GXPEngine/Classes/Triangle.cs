﻿using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine;
class Triangle : EasyDraw
{
    public int _hitPoints { get; set; }

    private int offSetX = 5;
    private int offSetY = 25;

    public Vec2 _position;

    private EasyDraw hitPointNumber;

    public CollisionFrame Left;
    public CollisionFrame Bottom;
    public CollisionFrame Right;

    public List<CollisionFrame> CollisionFrames = new List<CollisionFrame>();

    public Triangle(Vec2 position, int width, int height, int hitPoints) : base(width, height, false)
    {
        this._position = position;
        this._hitPoints = hitPoints;

        x = _position.x;
        y = _position.y;

        hitPointNumber = new EasyDraw(width, height);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.TextSize(30);

        Draw(100, 100 ,100);
        AddCollisionFrame();
        
    }

    void Update()
    {
        DrawHitPoints();
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Triangle(width / 2f, 0, width, height, 0, height);
    }

    void DrawHitPoints()
    {
        hitPointNumber.Clear(Color.Empty);
        hitPointNumber.Fill(Color.Yellow);
        hitPointNumber.TextAlign(CenterMode.Center, CenterMode.Center);
        hitPointNumber.Text(" " + _hitPoints, width / 2f - offSetX, height / 2 - 5 + offSetY);
        AddChild(hitPointNumber);

    }

    public void AddCollisionFrame()
    {

        Bottom = new CollisionFrame(new Vec2(0, height), new Vec2(width, height), 0xff00ff00, 3);
        Right = new CollisionFrame(new Vec2(width / 2f, 0), new Vec2(width, height), 0xff00ff00, 3);
        Left = new CollisionFrame(new Vec2(width / 2f, 0), new Vec2(0, height), 0xff00ff00, 3);
        
        AddChild(Bottom);
        AddChild(Right);
        AddChild(Left);

        CollisionFrames.Add(Bottom);
        CollisionFrames.Add(Left);
        CollisionFrames.Add(Right);
    }
}