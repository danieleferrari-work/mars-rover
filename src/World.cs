public class World
{
    private int density;
    private HashSet<Position> obstacles = new HashSet<Position>();


    public World(int density)
    {
        this.density = density;
    }

    public World(int density, HashSet<Position> obstacles)
    {
        this.density = density;
        this.obstacles = new HashSet<Position>(obstacles);
    }

    public bool IsObstructed(Position position)
    {
        return obstacles.Any(p => p == position);
    }

    public Position GetCorrectPosition(Position rawPosition)
    {
        Position correctPosition = new Position(rawPosition.x % density, rawPosition.y % density);

        if (correctPosition.x < 0)
            correctPosition.x = density + correctPosition.x;
        if (correctPosition.y < 0)
            correctPosition.y = density + correctPosition.y;
        if (correctPosition.x > density - 1)
            correctPosition.x = correctPosition.x - density;
        if (correctPosition.y > density - 1)
            correctPosition.y = correctPosition.y - density;

        return correctPosition;
    }

    public void PrintStatus(Rover rover)
    {
        string separatorString = "";
        for (int i = 0; i < density; i++)
            separatorString += "-";
        separatorString += "\n";

        string res = separatorString;

        for (int y = density - 1; y >= 0; y--)
            for (int x = 0; x < density; x++)
            {
                Position position = new Position(x, y);
                if (IsObstructed(position)) // obstacle position
                    res += "x";
                else if (rover.position == position) // rover position
                {
                    if (rover.direction == Directions.N)
                        res += "^";
                    else if (rover.direction == Directions.E)
                        res += ">";
                    else if (rover.direction == Directions.S)
                        res += "v";
                    else
                        res += "<";
                }
                else // empty position
                    res += "□";

                if (x == density - 1)
                    res += "\n";
            }

        res += separatorString;
        Console.Write(res);
    }
}