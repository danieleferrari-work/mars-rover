public record struct Position(int x, int y)
{
    public Position(Position position) : this(position.x, position.y) { }
}