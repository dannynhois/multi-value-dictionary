namespace mvdictionary;

public interface IMultiValueDictionary<T>
{
    bool AddItems(string key, T value);
    string[] GetKeys();
    T[] GetMembers(string key);
}

public class MultiValueDictionary<T> : IMultiValueDictionary<T>
{
    public Dictionary<string, HashSet<T>> Items { get; } = new Dictionary<string, HashSet<T>>();

    public bool AddItems(string key, T value)
    {
        if (!Items.ContainsKey(key))
        {
            var hash = new HashSet<T>() { value };
            Items.Add(key,hash);
        }
        else
        {
            var hash = Items[key] ?? new ();
            var added = hash.Add(value);
            if (!added)
            {
                Console.WriteLine("ERROR, member already exists for key");
                return false;
            }

        }

        return true;

    }

    public string[] GetKeys()
    {
        if (Items.Keys.Count == 0)
        {
            Console.WriteLine("(empty set)");
        }
        return Items.Keys.ToArray();
    }
    public T[] GetMembers(string key)
    {
        if (!Items.ContainsKey(key))
        {
            Console.WriteLine("ERROR, key doesn't exist");
            return Array.Empty<T>();
        }

        var results = Items[key] ?? new HashSet<T>();
        return results.ToArray();
    }
}