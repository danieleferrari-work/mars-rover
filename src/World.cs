using System.Linq;
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

    public int IndexOfPosition(Position position)
    {
        return position.y * density + position.x;
    }

    public Position PositionOfIndex(int index)
    {
        int x = index % density;
        int y = index / density;
        // Console.WriteLine(index + " => " + x + "  " + y);
        return new Position(x, y);
    }

    public override string ToString()
    {
        string separatorString = "";
        for (int i = 0; i < density; i++)
            separatorString += "-";
        separatorString+= "\n";

        string res = separatorString;

        for (int i = 0; i < Math.Pow(density, 2); i++)
        {
            if (IsObstructed(PositionOfIndex(i)))
                res += "x";
            else
                res += "□";
            if ((i + 1) % density == 0)
                res += "\n";
        }

        res += separatorString;
        return res;
    }
}