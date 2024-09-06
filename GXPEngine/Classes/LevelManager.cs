using System;
using System.Collections.Generic;
using GXPEngine;
class LevelManager : GameObject
{
    public List<LineSegment> Lines => _lines;
    public List<Block> Blocks => blocks;

    private MyGame game;
    private Ball ball;

    private int startTriangleX = 22;
    private int startTriangleY = 115;
    private int startSquareX = 35;
    private int startBlockY = 20;
    private int spacingX = 150;
    private int spacingY = 30;
    private int shiftY = 125;
    private int border = 50;
    private int squareSize = 50;
    private int triangleSize = 75;
    private int blockHealth = 50;
    private int playerSpeed = 7;
    private int playerRadius = 13;
    private int ballAmount = 10;

    private bool spawnedTriangle = false;


    private List<Block> blocks = new List<Block>();
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

            //Shift the blocks down and check if the blocks are underneath the bottom boundary
            foreach(Block block in blocks)
            {
                block.y += shiftY;

                if(block.y + block.height >= bottomLine.start.y)
                {
                    // game over
                    Console.WriteLine($"Game Over");
                }
            }

            foreach(Block block in blocks)
            {
 
            }

            //Spawn a new row of blocks
            if(!spawnedTriangle)
            {
                for(int x = 0; x < 10; x++)
                {
                    Block block = new Triangle(new Vec2(startTriangleX + spacingX * x, 20 + spacingY), triangleSize, triangleSize, blockHealth);
                    blocks.Add(block);
                    game.AddChild(block);
                    spawnedTriangle = true;
                }
            } else
            {
                for(int x = 0; x < 10; x++)
                {
                    Block block = new Square(new Vec2(startSquareX + spacingX * x, 35 + spacingY), squareSize, squareSize, blockHealth);
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

    private void HandleBlockDestroyed(Block block)
    {
        block.OnDestroyed -= HandleBlockDestroyed;
        blocks.Remove(block);
    }

    public void SpawnBlocksAndTriangles()
    {
        /*        square = new Square(new Vec2(300, 400), squareSize, squareSize, blockHealth);
                blocks.Add(square);
                game.AddChild(square);

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
                Block blockSquare = new Square(new Vec2(startSquareX + spacingX * x, startBlockY + spacingY), squareSize, squareSize, blockHealth);
                blockSquare.OnDestroyed += HandleBlockDestroyed;
                blocks.Add(blockSquare);
                game.AddChild(blockSquare);

                Block blockTriangle = new Triangle(new Vec2(startTriangleX + spacingX * x, startTriangleY + spacingY), triangleSize, triangleSize, blockHealth);
                blockTriangle.OnDestroyed += HandleBlockDestroyed;
                blocks.Add(blockTriangle);
                game.AddChild(blockTriangle);
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
