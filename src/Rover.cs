public class Rover
{
    public Position position
    {
        get;
        private set;
    }

    public Directions direction
    {
        get;
        private set;
    }

    private World landingWorld;


    #region PUBLIC METHODS


    public Rover(World landingWorld, Position startPosition, Directions startDirection)
    {
        this.landingWorld = landingWorld;
        this.position = startPosition;
        this.direction = startDirection;
    }

    public void DoCommands(string commands)
    {
        Console.WriteLine("\nDoCommands [" + commands + "]");
        try
        {
            foreach (char c in commands.ToUpper())
            {
                if (c == 'F')
                    MoveForward();
                else if (c == 'B')
                    MoveBackward();
                else if (c == 'L')
                    TurnLeft();
                else if (c == 'R')
                    TurnRight();
                else
                    Console.WriteLine("command not found: " + c);
            }
        }
        catch (ObstructedPathException ex)
        {
            Console.WriteLine("!!! obstructed path");
        }
    }

    #endregion PUBLIC METHODS

    #region PRIVATE METHODS

    public void MoveForward()
    {
        Move(forward: true);
        Console.WriteLine("MoveForward \t> " + position);
    }

    private void MoveBackward()
    {
        Move(forward: false);
        Console.WriteLine("MoveBackward \t> " + position);
    }

    private void Move(bool forward)
    {
        int moveIntensity = forward ? 1 : -1;
        Position newPosition = new Position(position);

        if (direction == Directions.N)
            newPosition.y += moveIntensity;
        else if (direction == Directions.E)
            newPosition.x += moveIntensity;
        else if (direction == Directions.S)
            newPosition.y -= moveIntensity;
        else if (direction == Directions.W)
            newPosition.x -= moveIntensity;

        if (landingWorld.IsObstructed(newPosition))
            throw new ObstructedPathException();
        else
            position = newPosition;
    }

    private void TurnLeft()
    {
        Turn(clockswise: false);
        Console.WriteLine("TurnLeft \t> " + direction);
    }

    private void TurnRight()
    {
        Turn(clockswise: true);
        Console.WriteLine("TurnRight \t> " + direction);
    }

    private void Turn(bool clockswise)
    {
        int directionsCount = 4;
        int rotation = clockswise ? 1 : -1;
        int directionIndex = (int)direction;
        int newDirectionIndex = directionIndex + rotation;

        if (newDirectionIndex < 0)
            newDirectionIndex = directionsCount + (newDirectionIndex % directionsCount);
        else
            newDirectionIndex = newDirectionIndex % directionsCount;

        direction = (Directions)(newDirectionIndex);
    }

    #endregion PRIVATE METHODS
}