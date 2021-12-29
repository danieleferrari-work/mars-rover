public class Rover
{
    public Position position { get; private set; }
    public Directions direction { get; private set; }

    private Planet _landingWorld;


    #region PUBLIC METHODS


    public Rover(Planet landingWorld, Position startPosition, Directions startDirection)
    {
        this._landingWorld = landingWorld;
        this.position = landingWorld.GetCorrectPosition(startPosition);
        this.direction = startDirection;
    }

    public void DoCommands(string commands)
    {
        Console.WriteLine("\nDoCommands [" + commands.ToUpper() + "]");

        CommandData? commandData = null;

        foreach (char c in commands.ToLower())
        {
            try
            {
                commandData = DoCommand(c.ToString());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (commandData != null && !commandData.isValid)
            {
                if (commandData.obstacle.HasValue)
                    Console.WriteLine(commandData.obstacle.Value + " at " + commandData.moveTo);
                break;
            }
        }
    }

    private CommandData DoCommand(string command) => command switch
    {
        Constants.C_FORWARD => Move(forward: true),
        Constants.C_BACKWARD => Move(forward: false),
        Constants.C_RIGHT => Turn(clockwise: true),
        Constants.C_LEFT => Turn(clockwise: false),

        _ => throw new ArgumentOutOfRangeException(nameof(command),
            $"Not expected command value: {command}"),
    };


    #endregion PUBLIC METHODS


    #region PRIVATE METHODS

    private CommandData Move(bool forward)
    {
        Position newPosition = position.Move(direction, forward);
        newPosition = _landingWorld.GetCorrectPosition(newPosition);

        CommandData moveData;

        if (_landingWorld.IsObstructed(newPosition))
        {
            moveData = new CommandData(
                false,
                position,
                newPosition,
                direction,
                direction,
                _landingWorld.GetObstacleAt(newPosition));
        }
        else
        {
            moveData = new CommandData(
                true,
                position,
                newPosition,
                direction,
                direction
            );

            position = newPosition;
            Console.WriteLine((forward ? "MoveForward\t> " : "MoveBackward\t> ") + position);
        }

        return moveData;
    }

    private CommandData Turn(bool clockwise)
    {
        Directions newDirection = Utils.Turn(direction, clockwise);

        CommandData moveData = new CommandData(
            true,
            position,
            position,
            direction,
            newDirection
        );

        direction = moveData.directionTo;
        Console.WriteLine((clockwise ? "TurnRight\t> " : "TurnLeft\t> ") + direction);

        return moveData;
    }

    #endregion PRIVATE METHODS
}