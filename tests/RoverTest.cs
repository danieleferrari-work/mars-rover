using System.Collections;
using Xunit;

public class RoverTest
{
    [Theory]
    [ClassData(typeof(RoverDoCommandsTestData))]
    public void TestCommands(Position startPosition, Directions startDirection, string command, Position endPosition, Directions endDirection)
    {
        HashSet<Position> obstacles = new HashSet<Position>() { new Position(1, 1), new Position(4, 3) };
        World world = new World(5, obstacles);
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
            // // simple move forward and backward
            yield return new object[] { new Position(0, 0), Directions.N, "F", new Position(0, 1), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.N, "B", new Position(0, 4), Directions.N };
            yield return new object[] { new Position(1, 3), Directions.N, "B", new Position(1, 2), Directions.N };
            yield return new object[] { new Position(3, -1), Directions.N, "F", new Position(3, 0), Directions.N };

            // multi move forward/backward
            yield return new object[] { new Position(0, 0), Directions.N, "FF", new Position(0, 2), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.N, "BB", new Position(0, 3), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.N, "BF", new Position(0, 0), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.N, "FBF", new Position(0, 1), Directions.N };

            // simple rotations
            yield return new object[] { new Position(0, 0), Directions.N, "L", new Position(0, 0), Directions.W };
            yield return new object[] { new Position(0, 0), Directions.N, "R", new Position(0, 0), Directions.E };
            yield return new object[] { new Position(0, 0), Directions.W, "R", new Position(0, 0), Directions.N };

            // multi rotations
            yield return new object[] { new Position(0, 0), Directions.N, "LL", new Position(0, 0), Directions.S };
            yield return new object[] { new Position(0, 0), Directions.N, "RR", new Position(0, 0), Directions.S };
            yield return new object[] { new Position(0, 0), Directions.N, "LR", new Position(0, 0), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.W, "LR", new Position(0, 0), Directions.W };
            yield return new object[] { new Position(0, 0), Directions.W, "LLR", new Position(0, 0), Directions.S };
            yield return new object[] { new Position(0, 0), Directions.S, "LLLL", new Position(0, 0), Directions.S };

            // multi rotation and move
            yield return new object[] { new Position(0, 0), Directions.N, "LF", new Position(4, 0), Directions.W };
            yield return new object[] { new Position(0, 0), Directions.N, "RF", new Position(1, 0), Directions.E };
            yield return new object[] { new Position(0, 0), Directions.N, "RFRFRF", new Position(0, 4), Directions.W };

            // test spherical world
            yield return new object[] { new Position(3, 3), Directions.N, "FFFFFFFFFFFFFFF", new Position(3, 3), Directions.N };
            yield return new object[] { new Position(3, 2), Directions.E, "FFFFFFFFFFFFFFF", new Position(3, 2), Directions.E };

            // obstacle 
            // endPosition is not (1,1) because there is an obstacle in (1,1)
            yield return new object[] { new Position(0, 0), Directions.N, "FRF", new Position(0, 1), Directions.E };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

