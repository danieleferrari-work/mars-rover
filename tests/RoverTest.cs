using System.Collections;
using Xunit;

public class RoverTest
{
    [Theory]
    [ClassData(typeof(RoverDoCommandsTestData))]
    public void TestCommands(Position startPosition, Directions startDirection, string command, Position endPosition, Directions endDirection)
    {
        Dictionary<Position, Obstacle> obstacles = new Dictionary<Position, Obstacle>() {
            { new Position(1,1), new Obstacle("rock") },
            { new Position(4,3), new Obstacle("water") }
        };
        Planet world = new Planet("mars", 5, obstacles);
        Rover rover = new Rover(world, startPosition, startDirection);

        Console.WriteLine("START: " + startPosition + " " + startDirection);
        world.PrintStatus(rover);

        rover.DoCommands(command);
        Assert.Equal(endPosition, rover.position);
        Assert.Equal(endDirection, rover.direction);

        world.PrintStatus(rover);
        Console.WriteLine("END: " + rover.position + " " + rover.direction + "\n");
    }


    public class RoverDoCommandsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // simple move forward and backward
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_FORWARD, new Position(0, 1), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_BACKWARD, new Position(0, 4), Directions.N };
            yield return new object[]
                { new Position(1, 3), Directions.N, Constants.C_BACKWARD, new Position(1, 2), Directions.N };
            yield return new object[]
                { new Position(3, -1), Directions.N, Constants.C_FORWARD, new Position(3, 0), Directions.N };

            // simple rotations
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_LEFT, new Position(0, 0), Directions.W };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_RIGHT, new Position(0, 0), Directions.E };
            yield return new object[]
                { new Position(0, 0), Directions.W, Constants.C_RIGHT, new Position(0, 0), Directions.N };

            // multi move forward/backward
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_FORWARD + Constants.C_FORWARD, new Position(0, 2), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_BACKWARD + Constants.C_BACKWARD, new Position(0, 3), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_BACKWARD + Constants.C_FORWARD, new Position(0, 0), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_FORWARD + Constants.C_BACKWARD + Constants.C_FORWARD, new Position(0, 1), Directions.N };

            // multi rotations
            yield return new object[] {
                     new Position(0, 0), Directions.N, Constants.C_LEFT + Constants.C_LEFT, new Position(0, 0), Directions.S };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_RIGHT + Constants.C_RIGHT, new Position(0, 0), Directions.S };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_LEFT + Constants.C_RIGHT, new Position(0, 0), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.W, Constants.C_LEFT + Constants.C_RIGHT, new Position(0, 0), Directions.W };
            yield return new object[]
                { new Position(0, 0), Directions.W, Constants.C_LEFT + Constants.C_LEFT + Constants.C_RIGHT, new Position(0, 0), Directions.S };
            yield return new object[]
                { new Position(0, 0), Directions.S, Constants.C_LEFT + Constants.C_LEFT + Constants.C_LEFT + Constants.C_LEFT, new Position(0, 0), Directions.S };

            // multi rotation and move
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_LEFT + Constants.C_FORWARD, new Position(4, 0), Directions.W };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_RIGHT + Constants.C_FORWARD, new Position(1, 0), Directions.E };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_RIGHT + Constants.C_FORWARD + Constants.C_RIGHT + Constants.C_FORWARD + Constants.C_RIGHT + Constants.C_FORWARD, new Position(0, 4), Directions.W };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_LEFT + Constants.C_FORWARD + Constants.C_LEFT + Constants.C_LEFT, new Position(4, 0), Directions.E };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_LEFT + Constants.C_FORWARD + Constants.C_LEFT + Constants.C_RIGHT + Constants.C_FORWARD, new Position(3, 0), Directions.W };

            // spherical world
            yield return new object[]
                { new Position(3, 3), Directions.N, Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD, new Position(3, 3), Directions.N };
            yield return new object[]
                { new Position(3, 2), Directions.E, Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD, new Position(3, 2), Directions.E };
            yield return new object[]
                { new Position(3, 2), Directions.E, Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD + Constants.C_BACKWARD, new Position(3, 2), Directions.E };

            // obstacles
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_FORWARD + Constants.C_RIGHT + Constants.C_FORWARD, new Position(0, 1), Directions.E };
            yield return new object[]
                { new Position(1, 3), Directions.N, Constants.C_BACKWARD + Constants.C_BACKWARD, new Position(1, 2), Directions.N };
            yield return new object[]
                { new Position(3, 3), Directions.W, Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD, new Position(0, 3), Directions.W };
            yield return new object[]
                { new Position(3, 3), Directions.W, Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_RIGHT + Constants.C_RIGHT + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD, new Position(0, 3), Directions.W };
            yield return new object[]
                { new Position(3, 3), Directions.W, Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_RIGHT + Constants.C_RIGHT + Constants.C_BACKWARD, new Position(0, 3), Directions.E };
            yield return new object[]
                { new Position(3, 3), Directions.W, Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_RIGHT + Constants.C_RIGHT + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD + Constants.C_FORWARD, new Position(3, 3), Directions.E };

            // wrong commands
            yield return new object[]
                { new Position(0, 0), Directions.N, "x", new Position(0, 0), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_FORWARD + "2", new Position(0, 1), Directions.N };
            yield return new object[]
                { new Position(0, 0), Directions.N, Constants.C_RIGHT + "p", new Position(0, 0), Directions.E };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

