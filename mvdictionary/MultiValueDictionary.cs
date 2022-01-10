namespace mvdictionary;

public interface IMultiValueDictionary<T>
{
    bool AddItem(string key, T value);
    bool RemoveItem(string key, T value);
    string[] GetKeys();
    T[] GetMembers(string key);
}

public class MultiValueDictionary<T> : IMultiValueDictionary<T>
{
    public Dictionary<string, HashSet<T>> Items { get; } = new Dictionary<string, HashSet<T>>();

    public bool AddItem(string key, T value)
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
    public bool RemoveItem(string key, T value)
    {
        if (!Items.ContainsKey(key))
        {
            Console.WriteLine("ERROR, key doesn't exist");
            return false;
        }

        var removed = Items[key].Remove(value);
        if (!removed)
        {
            Console.WriteLine("ERROR, member doesn't exist");
        }

        return removed;

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