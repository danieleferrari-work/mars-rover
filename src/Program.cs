public class Program
{
    public static void Main(string[] args)
    {
        Dictionary<Position, Obstacle> obstacles = new Dictionary<Position, Obstacle>() {
            { new Position(1,1), new Obstacle("rock") },
            { new Position(4,3), new Obstacle("water") }
        };
        Planet planet = new Planet("mars", 10, obstacles);
        Rover rover = new Rover(planet, new Position(0, 0), Directions.N);

        Console.WriteLine("WELCOME TO MARS ROVER REMOTE CONTROL SYSTEM" + (char)(0174));

        string? commands;
        do
        {
            Console.WriteLine("Type commands to control the rover.");
            Console.WriteLine("Type \"c\" to show allowed commands.");
            Console.WriteLine("Type \"s\" to show world status.");
            Console.WriteLine("Type \"q\" to quit.");
            commands = Console.ReadLine();

            if (string.IsNullOrEmpty(commands))
                break;

            commands = commands.ToLower();
            try
            {
                if (commands == "c")
                {
                    Console.WriteLine("\n--------------------");
                    Console.WriteLine("\"f\": forward");
                    Console.WriteLine("\"b\": backward");
                    Console.WriteLine("\"l\": turn left");
                    Console.WriteLine("\"r\": turn right");
                    Console.WriteLine("--------------------\n");
                }
                else if (commands == "s")
                {
                    Console.WriteLine("\nplanet: " + planet.name);
                    Console.WriteLine("size: " + planet.size + "x" + planet.size);
                    planet.PrintStatus(rover);

                }
                else if (commands != "q")
                {
                    rover.DoCommands(commands);
                    planet.PrintStatus(rover);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (commands != "q");

        Console.WriteLine("THANKS FOR USING MARS ROVER REMOTE CONTROL SYSTEM" + (char)(0174));
    }
}