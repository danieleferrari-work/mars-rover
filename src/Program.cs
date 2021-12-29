public class Program
{
    public static void Main(string[] args)
    {
        Dictionary<Position, Obstacle> obstacles = new Dictionary<Position, Obstacle>() {
            { new Position(3,1), new Obstacle("rock") },
            { new Position(4,3), new Obstacle("water") },
            { new Position(3,3), new Obstacle("water") },
            { new Position(2,3), new Obstacle("water") },
            { new Position(1,0), new Obstacle("alien") },
        };
        Planet planet = new Planet("mars", 5, obstacles);
        Rover rover = new Rover(planet, new Position(0, 0), Directions.N);

        Console.WriteLine("WELCOME TO MARS ROVER REMOTE CONTROL SYSTEM" + (char)(0174));

        string? commands;
        do
        {
            Console.WriteLine("Type commands to control the rover.");
            Console.WriteLine("Type \"" + Constants.COMMAND_PRINT_COMMANDS + "\" to show allowed commands.");
            Console.WriteLine("Type \"" + Constants.COMMAND_PRINT_STATUS + "\" to show world status.");
            Console.WriteLine("Type \"" + Constants.COMMAND_QUIT + "\" to quit.");
            commands = Console.ReadLine();

            if (string.IsNullOrEmpty(commands))
                continue;

            commands = commands.ToLower();
            try
            {
                if (commands == Constants.COMMAND_PRINT_COMMANDS)
                {
                    Console.WriteLine("\n--------------------");
                    Console.WriteLine("\"" + Constants.C_FORWARD + "\": forward");
                    Console.WriteLine("\"" + Constants.C_BACKWARD + "\": backward");
                    Console.WriteLine("\"" + Constants.C_LEFT + "\": turn left");
                    Console.WriteLine("\"" + Constants.C_RIGHT + "\": turn right");
                    Console.WriteLine("--------------------\n");
                }
                else if (commands == Constants.COMMAND_PRINT_STATUS)
                {
                    Console.WriteLine("\nplanet: " + planet.name);
                    Console.WriteLine("size: " + planet.size + "x" + planet.size);
                    planet.PrintStatus(rover);

                }
                else if (commands != Constants.COMMAND_QUIT)
                {
                    rover.DoCommands(commands);
                    planet.PrintStatus(rover);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (commands != Constants.COMMAND_QUIT);

        Console.WriteLine("THANKS FOR USING MARS ROVER REMOTE CONTROL SYSTEM" + (char)(0174));
    }
}