namespace Taverna.Scripts.List;

public static class ListUtils
{
    private static readonly Random _randomGenerator = new();

    public static Random RandomGenerator
    {
        get
        {
            return _randomGenerator;
        }
    }

    public static T GetRandom<T>( this List<T> list )
    {
        return list[_randomGenerator.Next( list.Count )];
    }
}
