using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using GXPEngine;
class LevelManager : GameObject
{

    public List<LineSegment> Lines => _lines;
    public List<LineCap> Caps => _caps;
    public List<Block> Blocks => blocks;

    private MyGame game;
    private Ball ball;
    private Block block;
    private Triangle triangle;

    private int startTriangleX = 22;
    private int startTriangleY = 115;
    private int startBlockX = 35;
    private int startBlockY = 20;
    private int spacingX = 150;
    private int spacingY = 30;
    private int shiftY = 125;
    private int border = 50;
    private int blockSize = 50;
    private int triangleSize = 75;
    private int brickHealth = 1;
    private int playerSpeed = 7;
    private int playerRadius = 13;
    private int ballAmount = 20;

    private bool spawnedTriangle = false;


    private List<Block> blocks = new List<Block>();
    private List<Triangle> triangles = new List<Triangle>();
    private List<LineSegment> _lines = new List<LineSegment>();
    private List<LineCap> _caps = new List<LineCap>();
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
                block.y += shiftY;

            }

            foreach(Block block in blocks)
            {
                if(block.y + block.height >= bottomLine.start.y)
                {
                    // game over
                    Console.WriteLine($"Game Over");
                }
            }

            foreach(Triangle triangle in triangles)
            {
                triangle.y += shiftY;
            }

            foreach(Triangle triangle in triangles)
            {
                if(triangle.y + triangle.height >= bottomLine.start.y)
                {
                    // game over
                    Console.WriteLine($"Game Over");
                }
            }

            if(!spawnedTriangle)
            {
                for(int x = 0; x < 10; x++)
                {
                    triangle = new Triangle(new Vec2(startTriangleX + spacingX * x, 20 + spacingY), triangleSize, triangleSize, brickHealth);
                    triangles.Add(triangle);
                    game.AddChild(triangle);
                    spawnedTriangle = true;
                }
            } else
            {
                for(int x = 0; x < 10; x++)
                {
                    block = new Block(new Vec2(startBlockX + spacingX * x, 35 + spacingY), blockSize, blockSize, brickHealth);
                    blocks.Add(block);
                    game.AddChild(block);
                    spawnedTriangle = false;
                }
            }


            SpawnBalls();
        }
    }

    public void SpawnBalls()
    {
        for(int i = 0; i < ballAmount; i++)
        {
            ball = new Ball(this, new Vec2(game.width / 2f, 500), playerRadius, playerSpeed);
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
/*        block = new Block(new Vec2(300, 400), blockSize, blockSize, brickHealth);
        blocks.Add(block);
        game.AddChild(block);

        triangle = new Triangle(new Vec2(350 + spacingX * x, 200 + spacingY * y), 100, 100, 50);
        triangles.Add(triangle);
        game.AddChild(triangle);


        CollisionFrame frame = new CollisionFrame(200, 300, 600, 300);
        AddChild(frame);
        Lines.Add(frame);*/

        for(int y = 0; y < 1; y++)
        {
            for(int x = 0; x < 10; x++)
            {
                block = new Block(new Vec2(startBlockX + spacingX * x, startBlockY + spacingY), blockSize, blockSize, brickHealth);
                blocks.Add(block);
                game.AddChild(block);
            }
        }

        for(int y = 0; y < 1; y++)
        {
            for(int x = 0; x < 10; x++)
            {

                triangle = new Triangle(new Vec2(startTriangleX + spacingX * x, startTriangleY + spacingY), triangleSize, triangleSize, brickHealth);
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
