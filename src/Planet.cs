public class Planet
{
    public string name { get; private set; }
    public int size { get; private set; }
    private Dictionary<Position, Obstacle> _obstacles = new Dictionary<Position, Obstacle>();

    public Planet(string name, int density)
    {
        this.name = name;
        this.size = density;
    }

    public Planet(string name, int density, Dictionary<Position, Obstacle> obstacles)
    {
        this.name = name;
        this.size = density;
        this._obstacles = new Dictionary<Position, Obstacle>(obstacles);
    }

    public bool IsObstructed(Position position)
    {
        return _obstacles.ContainsKey(position);
    }

    public Obstacle? GetObstacleAt(Position position)
    {
        if (!_obstacles.ContainsKey(position))
            return null;
        return _obstacles[position];
    }

    public Position GetCorrectPosition(Position rawPosition)
    {
        Position correctPosition = new Position(rawPosition.x % size, rawPosition.y % size);

        if (correctPosition.x < 0)
            correctPosition.x = size + correctPosition.x;
        if (correctPosition.y < 0)
            correctPosition.y = size + correctPosition.y;
        if (correctPosition.x > size - 1)
            correctPosition.x = correctPosition.x - size;
        if (correctPosition.y > size - 1)
            correctPosition.y = correctPosition.y - size;

        return correctPosition;
    }

    public void PrintStatus(Rover rover)
    {
        string separatorString = "";
        for (int i = 0; i < size; i++)
            separatorString += "-";
        separatorString += "\n";

        string res = separatorString;
    
        for (int y = size - 1; y >= 0; y--)
            for (int x = 0; x < size; x++)
            {
                Position position = new Position(x, y);
                Obstacle? obstacle = GetObstacleAt(position);
                if (obstacle.HasValue)
                {
                    string obstacleName = obstacle.Value.name;
                    if (!string.IsNullOrEmpty(obstacleName))
                        res += obstacleName.Substring(0, 1).ToUpper(); // obstacle name's first letter
                }
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
                    res += " ";

                if (x == size - 1)
                    res += "\n";
            }

        res += separatorString;
        Console.WriteLine(res);
    }
}