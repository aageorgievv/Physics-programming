using System;
using System.Collections.Generic;
using System.Reflection;
using GXPEngine;
class LevelManager : GameObject
{
    public float LeftXBoundary => _leftXBoundary;
    public float RightXBoundary => _rightXBoundary;
    public float TopYBoundary => _topYBoundary;
    public float BottomYBoundary => _bottomYBoundary;
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
    private int spacingY = 80;
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

    public LevelManager(MyGame game)
    {
        this.game = game;
        game.AddChild(this);

        CreateABoundary(LineSide.Top);
        CreateABoundary(LineSide.Bottom);
        CreateABoundary(LineSide.Left);
        CreateABoundary(LineSide.Right);

/*        _leftXBoundary = 1;
        _rightXBoundary = game.width;
        _topYBoundary = 1;
        _bottomYBoundary = game.height - border;

        CreateVisualXBoundary(_leftXBoundary);
        CreateVisualXBoundary(_rightXBoundary);
        CreateVisualYBoundary(_topYBoundary);
        CreateVisualYBoundary(_bottomYBoundary);*/
    }

    void Update()
    {
        if (balls.Count == 0)
        {
            // all balls are destroyed


            foreach (Block block in blocks)
            {
                block.y += spacingY;
            }

            foreach(Block block in blocks)
            {
                if (block.y + block.height >= bottomLine.start.y)
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
        for (int i = 0; i < ballAmount; i++)
        {
            ball = new Ball(this, new Vec2(game.width / 2f, 700), playerRadious, playerSpeed);
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
        for(int y = 0; y < 4; y++)
        {
            for(int x = 0; x < 10; x++)
            {
                block = new Block(new Vec2(35 + spacingX * x, 20 + spacingY * y), blockWidth, blockHeight, blockHealth);
                blocks.Add(block);
                game.AddChild(block);

/*                triangle = new Triangle(new Vec2(670 + spacingX * x, 350 + spacingY * y), 100, 100, 50);
                triangles.Add(triangle);
                game.AddChild(triangle);*/
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
        switch (side)
        {
            case LineSide.Top:
                line = new LineSegment(game.width, 0, 0, 0, LineSide.Top, 0xffffffff, 1);
                break;
            case LineSide.Bottom:
                line = new LineSegment(0, game.height - 50, game.width, game.height - 50, LineSide.Bottom, 0xffffffff, 1);
                break;
            case LineSide.Left:
                line = new LineSegment(1, 0, 1, game.height, LineSide.Left, 0xffffffff, 1);
                break;
            case LineSide.Right:
                line = new LineSegment(game.width, game.height, game.width, 0, LineSide.Left, 0xffffffff, 1);
                bottomLine = line;
                break;
        }
        game.AddChild(line);
        Lines.Add(line);
    }

/*        void CreateVisualXBoundary(float xBoundary)
    {
        game.AddChild(new LineSegment(xBoundary, 0, xBoundary, game.height, 0xffffffff, 1));
    }

    void CreateVisualYBoundary(float yBoundary)
    {
        game.AddChild(new LineSegment(0, yBoundary, game.width, yBoundary, 0xffffffff, 1));
    }*/
}
