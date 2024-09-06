using GXPEngine;

public class Block : EasyDraw
{
    public bool IsDead => _hitPoints <= 0;
    public int _hitPoints { get; set; }

    public int offSetX = 5;
    public int offSetY = 25;

    public EasyDraw hitPointNumber;

    public Vec2 _position
    {
        get { return new Vec2(x, y); }
        set { x = value.x; y = value.y; }
    }

    public Block(Vec2 position, int width, int height, int hitPoints,  bool addCollider = true) : base(width, height, false)
    {
        _position = position;
        this._hitPoints = hitPoints;

        x = position.x;
        y = position.y;

    }
    public void TakeDamage(int damage)
    {
        if(damage <= 0)
        {
            return;
        }

        _hitPoints -= damage;

        if(IsDead)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy();
    }
}