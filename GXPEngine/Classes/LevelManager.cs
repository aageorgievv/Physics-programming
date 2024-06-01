using System.Collections.Generic;
using GXPEngine;
class LevelManager : GameObject
{
    public float LeftXBoundary => _leftXBoundary;
    public float RightXBoundary => _rightXBoundary;
    public float TopYBoundary => _topYBoundary;
    public float BottomYBoundary => _bottomYBoundary;

    private MyGame game;
    private Ball ball;
    private Block block;
    private Triangle triangle;

    private int spacingX = 120;
    private int spacingY = 60;

    float _leftXBoundary = 0;
    float _rightXBoundary = 0;
    float _topYBoundary = 0;
    private float _bottomYBoundary = 0;
    float border = 50;

    List<Block> blocks = new List<Block>();
    List<Triangle> triangles = new List<Triangle>();
    public LevelManager(MyGame game)
    {
        this.game = game;
        game.AddChild(this);

        _leftXBoundary = 1;
        _rightXBoundary = game.width;
        _topYBoundary = 1;
        _bottomYBoundary = game.height - border;

        CreateVisualXBoundary(_leftXBoundary);
        CreateVisualXBoundary(_rightXBoundary);
        CreateVisualYBoundary(_topYBoundary);
        CreateVisualYBoundary(_bottomYBoundary);
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
        for(int y = 0; y < 1; y++)
        {
            for(int x = 0; x < 1; x++)
            {
                block = new Block(new Vec2(270 + spacingX * x, 350 + spacingY * y), 100, 100, 50);
                blocks.Add(block);
                game.AddChild(block);

                triangle = new Triangle(new Vec2(670 + spacingX * x, 350 + spacingY * y), 100, 100, 50);
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

    void CreateVisualXBoundary(float xBoundary)
    {
        game.AddChild(new LineSegment(xBoundary, 0, xBoundary, game.height, 0xffffffff, 1));
    }

    void CreateVisualYBoundary(float yBoundary)
    {
        game.AddChild(new LineSegment(0, yBoundary, game.width, yBoundary, 0xffffffff, 1));
    }
}
