using System.Collections;
using Xunit;

public class RoverTest
{
    [Theory]
    [ClassData(typeof(RoverDoCommandsTestData))]
    public void TestCommands(Position startPosition, Directions startDirection, string command, Position endPosition, Directions endDirection)
    {
        HashSet<Position> obstacles = new HashSet<Position>() { new Position(1, 1), new Position(4, 3) };
        World world = new World(10, obstacles);
        Rover rover = new Rover(world, startPosition, startDirection);

        rover.DoCommands(command);
        Assert.Equal(endPosition, rover.position);
        Assert.Equal(endDirection, rover.direction);
    }



    public class RoverDoCommandsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // simple move forward and backward
            yield return new object[] { new Position(0, 0), Directions.N, "F", new Position(0, 1), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.N, "B", new Position(0, -1), Directions.N };
            yield return new object[] { new Position(1, 3), Directions.N, "B", new Position(1, 2), Directions.N };
            yield return new object[] { new Position(3, -1), Directions.N, "F", new Position(3, 0), Directions.N };

            // multi move forward/backward
            yield return new object[] { new Position(0, 0), Directions.N, "FF", new Position(0, 2), Directions.N };
            yield return new object[] { new Position(0, 0), Directions.N, "BB", new Position(0, -2), Directions.N };
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
            yield return new object[] { new Position(0, 0), Directions.N, "LF", new Position(-1, 0), Directions.W };
            yield return new object[] { new Position(0, 0), Directions.N, "RF", new Position(1, 0), Directions.E };
            yield return new object[] { new Position(0, 0), Directions.N, "RFRFRF", new Position(0, -1), Directions.W };

            // obstacle 
            // endPosition is not (1,1) because there is an obstacle in (1,1)
            yield return new object[] { new Position(0, 0), Directions.N, "FRF", new Position(0, 1), Directions.E }; 



        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

