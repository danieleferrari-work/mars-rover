public class Position : IEquatable<Position>
{
    public int x;
    public int y;


    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Position(Position position)
    {
        this.x = position.x;
        this.y = position.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public override bool Equals(object obj) => this.Equals(obj as Position);

    public bool Equals(Position other)
    {
        if (other.GetType() != typeof(Position))
            return false;
        bool eq = (((Position)other).x == x && ((Position)other).y == y);
        return eq;
    }
    public static bool operator ==(Position lhs, Position rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            return false;
        }
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Position lhs, Position rhs) => !(lhs == rhs);
}