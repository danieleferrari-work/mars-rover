public class Program
{
    public static void Main(string[] args)
    {
        HashSet<Position> obstacles = new HashSet<Position>() { new Position(1, 1), new Position(4, 3) };
        World world = new World(10, obstacles);
        Rover rover = new Rover(world, new Position(0,0), Directions.N);
        
        world.PrintStatus(rover);
    }
}