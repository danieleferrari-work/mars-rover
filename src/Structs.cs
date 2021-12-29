public record struct Position(int x, int y)
{
    public Position(Position position) : this(position.x, position.y) { }
    public Position MoveN() { return new Position(x, y + 1); }
    public Position MoveS() { return new Position(x, y - 1); }
    public Position MoveE() { return new Position(x + 1, y); }
    public Position MoveW() { return new Position(x - 1, y); }

    public Position Move(Directions startDirection, bool forward) => startDirection switch
    {
        Directions.N => forward ? MoveN() : MoveS(),
        Directions.E => forward ? MoveE() : MoveW(),
        Directions.S => forward ? MoveS() : MoveN(),
        Directions.W => forward ? MoveW() : MoveE(),

        _ => throw new ArgumentOutOfRangeException(nameof(startDirection),
            $"Not expected direction value: {startDirection}"),
    };
}

public record struct Obstacle(string name)
{
    public Obstacle(Obstacle obstacle) : this(obstacle.name) { }
}

public record struct CommandData(
    bool isValid,
    Position moveFrom,
    Position moveTo,
    Directions directionFrom,
    Directions directionTo,
    Obstacle? obstacle = null
) { }

