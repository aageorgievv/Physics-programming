using System.Collections.Generic;
using GXPEngine;
class LevelManager : GameObject
{
    public float LeftXBoundary
    {
        get { return _leftXBoundary; }
    }
    public float RightXBoundary
    {
        get { return _rightXBoundary; }
    }
    public float TopYBoundary
    {
        get { return _topYBoundary; }
    }
    public float BottomYBoundary
    {
        get { return _bottomYBoundary; }
    }

    private MyGame game;
    private Ball ball;
    private Block block;

    float _leftXBoundary = 0;
    float _rightXBoundary = 0;
    float _topYBoundary = 0;
    private float _bottomYBoundary = 0;
    float border = 50;

    List<Block> blocks = new List<Block>();
    public LevelManager(MyGame game)
    {
        this.game = game;
        game.AddChild(this);

        _leftXBoundary = 0;
        _rightXBoundary = game.width + 1;
        _topYBoundary = 0;
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
        ball = new Ball(this, new Vec2(game.width / 2, 700), 10, 5);
        game.AddChild(ball);
    }

    public void SpawnBlocks()
    {
        for(int y = 0; y < 6; y++)
        {
            for(int x = 0; x < 18; x++)
            {
                block = new Block(new Vec2(80 + 50 * x, 50 + 50 * y), 20);
                blocks.Add(block);
                game.AddChild(block);
            }
        }
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
