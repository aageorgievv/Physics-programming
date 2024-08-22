using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using GXPEngine;
class LevelManager : GameObject
{
    /*    public float LeftXBoundary => _leftXBoundary;
        public float RightXBoundary => _rightXBoundary;
        public float TopYBoundary => _topYBoundary;
        public float BottomYBoundary => _bottomYBoundary;*/
    public List<LineSegment> Lines => _lines;
    public List<Block> Blocks => blocks;

    private MyGame game;
    private Ball ball;
    private Block block;
    private Triangle triangle;

    private float _leftXBoundary = 0;
    private float _rightXBoundary = 0;
    private float _topYBoundary = 0;
    private float _bottomYBoundary = 0;

    private int spacingX = 100;
    private int spacingY = 90;
    private int border = 50;
    private int blockWidth = 50;
    private int blockHeight = 50;
    private int blockHealth = 50;
    private int playerSpeed = 7;
    private int playerRadious = 10;
    private int ballAmount = 20;


    private List<Block> blocks = new List<Block>();
    private List<Triangle> triangles = new List<Triangle>();
    private List<LineSegment> _lines = new List<LineSegment>();
    private List<Ball> balls = new List<Ball>();

    private LineSegment bottomLine;

    private CollisionFrame frame;

    public LevelManager(MyGame game)
    {
        this.game = game;
        game.AddChild(this);

        CreateABoundary(LineSide.Top);
        CreateABoundary(LineSide.Bottom);
        CreateABoundary(LineSide.Left);
        CreateABoundary(LineSide.Right);
        CreateABoundary(LineSide.BottomLeft);
        CreateABoundary(LineSide.BottomRight);
    }

    void Update()
    {
        CheckBalls();
    }

    void CheckBalls()
    {
        if(balls.Count == 0)
        {
            // all balls are destroyed

            ball.hasShot = false;

            foreach(Block block in blocks)
            {
                block.y += spacingY;
            }

            foreach(Block block in blocks)
            {
                if(block.y + block.height >= bottomLine.start.y)
                {
                    // game over
                    Console.WriteLine($"Game Over");
                }
            }

            for(int x = 0; x < 10; x++)
            {
                block = new Block(new Vec2(35 + spacingX * x, 20 + spacingY * y), blockWidth, blockHeight, blockHealth);
                blocks.Add(block);
                game.AddChild(block);
            }

            SpawnBalls();
        }
    }

    public void SpawnBalls()
    {
        for(int i = 0; i < 1 /*ballAmount*/; i++)
        {
            ball = new Ball(this, new Vec2(game.width / 2f, 600), playerRadious, playerSpeed);
            ball.OnDestroyed += HandleBallDestroyed;
            game.AddChild(ball);
            balls.Add(ball);
        }
    }

    private void HandleBallDestroyed(Ball ball)
    {
        ball.OnDestroyed -= HandleBallDestroyed;
        balls.Remove(ball);

    }

    public void SpawnBlocksAndTriangles()
    {
        //triangle = new Triangle(new Vec2(400 + spacingX * x, 200 + spacingY * y), 100, 100, 50);
        //triangles.Add(triangle);
        //game.AddChild(triangle);

        for(int y = 0; y < 4; y++)
        {
            for(int x = 0; x < 10; x++)
            {
                //block = new Block(new Vec2(35 + spacingX * x, 20 + spacingY * y), blockWidth, blockHeight, blockHealth);
                //blocks.Add(block);
                //game.AddChild(block);

                triangle = new Triangle(new Vec2(35 + spacingX * x, 20 + spacingY * y), 50, 50, 25);
                triangles.Add(triangle);
                game.AddChild(triangle);
            }
        }
    }

    public int GetNumberOfBlocks()
    {
        return blocks.Count;
    }



    public Block GetBlock(int index)
    {
        if(index >= 0 && index < blocks.Count)
        {
            return blocks[index];
        }
        return null;
    }

    public int GetNumberOfTriangles()
    {
        return triangles.Count;
    }

    public Triangle GetTriangle(int index)
    {
        if(index >= 0 && index < triangles.Count)
        {
            return triangles[index];
        }
        return null;
    }

    void CreateABoundary(LineSide side)
    {
        LineSegment line = null;
        switch(side)
        {
            case LineSide.Top:
                line = new LineSegment(game.width, 0, 0, 0, side, 0xffffffff, 1);
                break;
            case LineSide.Bottom:
                line = new LineSegment(0, game.height - 50, game.width, game.height - 50, side, 0xffff0000, 1);
                bottomLine = line;
                break;
            case LineSide.Left:
                line = new LineSegment(1, 0, 1, game.height, side, 0xffffffff, 1);
                break;
            case LineSide.Right:
                line = new LineSegment(game.width, 0, game.width, game.height, side, 0xffffffff, 1);
                break;
            case LineSide.BottomLeft:
                line = new LineSegment(0, game.height * 0.8f, game.width * 0.2f, game.height, side, 0xffffffff, 1);
                break;
            case LineSide.BottomRight:
                line = new LineSegment(game.width, game.height * 0.8f, game.width * 0.8f, game.height, side, 0xffffffff, 1);
                break;
        }
        game.AddChild(line);
        Lines.Add(line);
    }
}
