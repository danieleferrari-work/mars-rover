using System.Collections;
using Xunit;

public class PositionTest
{
    [Theory]
    [ClassData(typeof(PositionTestData))]
    public void TestIndexConversion(int index, Position position)
    {
        // World world = new World(5);

        // Assert.Equal(position, world.PositionOfIndex(index));
        // Assert.Equal(index, world.IndexOfPosition(position));
    }


    public class PositionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 0, new Position(0, 0) };
            yield return new object[] { 12, new Position(2, 2) };
            yield return new object[] { 23, new Position(3, 4) };
            yield return new object[] { 24, new Position(4, 4) };
            yield return new object[] { 20, new Position(0, 4) };
            yield return new object[] { 14, new Position(4, 2) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

//4   20 21 22 23 24
//3   15 16 17 18 19
//2   10 11 12 13 14
//1   5  6  7  8  9
//0   0  1  2  3  4 
//
//    0  1  2  3  4

