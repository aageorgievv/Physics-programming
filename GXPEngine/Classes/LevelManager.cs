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

    float _leftXBoundary = 0;
    float _rightXBoundary = 0;
    float _topYBoundary = 0;
    float _bottomYBoundary = 0;

    float border = 50;
    public LevelManager(MyGame game)
    {
        this.game = game;
        game.AddChild(this);


        //Add offset after collisions are ready
        _leftXBoundary = border;
        _rightXBoundary = game.width - border;
        _topYBoundary = border;
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
        ball = new Ball(this ,new Vec2(game.width / 2, 700), 10, 5);

        game.AddChild(ball);
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
