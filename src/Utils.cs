public static class Utils
{
    public static Directions Turn(Directions startDirection, bool clockwise) => startDirection switch
    {
        Directions.N => clockwise ? Directions.E : Directions.W,
        Directions.E => clockwise ? Directions.S : Directions.N,
        Directions.S => clockwise ? Directions.W : Directions.E,
        Directions.W => clockwise ? Directions.N : Directions.S,

        _ => throw new ArgumentOutOfRangeException(nameof(startDirection),
            $"Not expected direction value: {startDirection}"),
    };
}
