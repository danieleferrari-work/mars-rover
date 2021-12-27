public record struct Position
{
    public int x;
    public int y;


    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Position(Position position)
    {
        this.x = position.x;
        this.y = position.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }
}