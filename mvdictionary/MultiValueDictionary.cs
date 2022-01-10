namespace mvdictionary;

public interface IMultiValueDictionary
{
    void AddItems(string key, string value);
    string[] GetKeys();
    string[] GetMembers(string key);
}

public class MultiValueDictionary : IMultiValueDictionary
{
    public Dictionary<string, HashSet<string>> Items { get; } = new Dictionary<string, HashSet<string>>();

    public void AddItems(string key, string value)
    {
        if (!Items.ContainsKey(key))
        {
            var hash = new HashSet<string>() { value };
            Items.Add(key,hash);
            Console.WriteLine("Added");
        }
        else
        {
            var hash = Items[key] ?? new ();
            var added = hash.Add(value);
            if (!added)
            {
                Console.WriteLine("ERROR, member already exists for key");
                return;
            }
            else
            {
                Console.WriteLine("Added");
            }
        }

    }

    public string[] GetKeys()
    {
        if (Items.Keys.Count == 0)
        {
            Console.WriteLine("(empty set)");
        }
        return Items.Keys.ToArray();
    }
    public string[] GetMembers(string key)
    {
        if (!Items.ContainsKey(key))
        {
            Console.WriteLine("ERROR, key doesn't exist");
            return Array.Empty<string>();
        }

        var results = Items[key] ?? new HashSet<string>();
        return results.ToArray();
    }
}