namespace Taverna.Scripts.List;

public class RandomList<T> : List<T>
{

    private T? lastResult;


    public T Get()
    {
        return lastResult = this[ListUtils.RandomGenerator.Next( Count )];
    }

    public T GetExcept( T exception )
    {
        return GetExcept( [exception] );
    }

    public T GetExcept( List<T> listExceptions )
    {
        List<T> range = this.Except( listExceptions ).ToList();

        int index = ListUtils.RandomGenerator.Next( 0 , range.Count );
        return lastResult = range.ElementAt( index );
    }

    public T GetExceptLastResult()
    {
        if (lastResult == null)
        {
            return lastResult = Get();
        }

        return lastResult = GetExcept( lastResult );
    }

    public void FYatesShuffle()
    {
        int n = Count;
        while (n > 1)
        {
            n--;
            int k = ListUtils.RandomGenerator.Next( n + 1 );
            (this[n], this[k]) = (this[k], this[n]);
        }
    }
}

public static class EnumerableExtensions
{
    public static RandomList<T> ToRandomList<T>( this List<T> list )
    {
        RandomList<T> randomList = [.. list];
        return randomList;
    }

    public static RandomList<T> ToRandomList<T>( this IEnumerable<T> list )
    {
        RandomList<T> randomList = [.. list];
        return randomList;
    }
}
