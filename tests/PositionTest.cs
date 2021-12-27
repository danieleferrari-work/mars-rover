using System.Collections;
using Xunit;

public class PositionTest
{
    [Theory]
    [ClassData(typeof(PositionTestData))]
    public void TestIndexConversion(int worldSize, Position rawPosition, Position expectedPosition)
    {
        World world = new World(worldSize);

        Assert.Equal(expectedPosition, world.GetCorrectPosition(rawPosition));
    }

    public class PositionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 5, new Position(0, 0), new Position(0, 0) };
            yield return new object[] { 5, new Position(-1, -1), new Position(4, 4) };
            yield return new object[] { 5, new Position(-10, -1), new Position(0, 4) };
            yield return new object[] { 1, new Position(0, 0), new Position(0, 0) };
            yield return new object[] { 1, new Position(10, -1), new Position(0, 0) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}