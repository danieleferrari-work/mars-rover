public class World
{
    private double radius;
    private HashSet<Position> obstacles = new HashSet<Position>();


    public World(double radius, HashSet<Position> obstacles)
    {
        this.radius = radius;
        this.obstacles = new HashSet<Position>(obstacles);
    }

    public bool IsObstructed(Position position)
    {
        return obstacles.Contains(position);
    }
}