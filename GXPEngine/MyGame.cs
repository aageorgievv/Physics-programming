using GXPEngine;
using System.Drawing;

public class MyGame : Game
{

    LevelManager levelManager;
    static MyGame game;
    Ball ball;

    public MyGame() : base(1024, 768, false)
    {
        levelManager = new LevelManager(this);
        levelManager.SpawnBalls();
        levelManager.SpawnBlocks();
    }


    void Update()
    {

    }

    static void Main()
    {
        new MyGame().Start();
    }
}