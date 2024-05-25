using GXPEngine;                                
using System.Drawing;                           

public class MyGame : Game {

    private EasyDraw canvas;
	Ball ball;

    public MyGame() : base(1024, 768, false)     
	{
        ball = new Ball(new Vec2(width / 2, 700), 10, 5);	

		AddChild(ball);
    }


	void Update() {

	}

	static void Main()                          
	{
		new MyGame().Start();                   
	}
}