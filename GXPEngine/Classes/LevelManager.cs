using System;
using System.Collections.Generic;
using System.Reflection;
using GXPEngine;
class LevelManager : GameObject
{
    public List<LineSegment> Lines => _lines;

    private MyGame game;
    private Ball ball;
    private Block block;
    private Triangle triangle;

    private int spacingX = 80;
    private int spacingY = 80;

    List<Block> blocks = new List<Block>();
    List<Triangle> triangles = new List<Triangle>();
    List<LineSegment> _lines = new List<LineSegment>();
    public LevelManager(MyGame game)
    {
        this.game = game;
        game.AddChild(this);

        CreateABoundary("Top");
        CreateABoundary("Bottom");
        CreateABoundary("Left");
        CreateABoundary("Right");
    }

    void Update()
    {

    }

    public void SpawnBalls()
    {
        ball = new Ball(this, new Vec2(game.width / 2f, 700), 10, 5);
        game.AddChild(ball);
    }

    public void SpawnBlocksAndTriangles()
    {
        for(int y = 0; y < 6; y++)
        {
            for(int x = 0; x < 12; x++)
            {
                block = new Block(new Vec2(50 + spacingX * x, 50 + spacingX * y), 50, 50, 50);
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

    void CreateABoundary(string side)
    {
        LineSegment line = null;
        switch (side)
        {
            case "Top":
                line = new LineSegment(game.width, 0, 0, 0, 0xffffffff, 1);
                break;
            case "Bottom":
                line = new LineSegment(0, game.height - 50, game.width, game.height - 50, 0xffffffff, 1);
                break;
            case "Left":
                line = new LineSegment(1, 0, 1, game.height, 0xffffffff, 1);
                break;
            case "Right":
                line = new LineSegment(game.width, game.height, game.width, 0, 0xffffffff, 1);
                break;
        }
        game.AddChild(line);
        Lines.Add(line);
    }
}
