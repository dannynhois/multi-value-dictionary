namespace mvdictionary;

public class CommandLineParser
{
    private IMultiValueDictionary<string> _dictionary;
    public string Command { get; set; } = string.Empty;
    public string Arg1 { get; set; } = string.Empty;
    public string Arg2 { get; set; } = string.Empty;

    public CommandLineParser(IMultiValueDictionary<string> dictionary)
    {
        _dictionary = dictionary;

    }

    public CommandLineParser SetValues(string userInput)
    {
        var split = userInput.Split(" ");
        Command = split[0].ToLower();
        if (split.Length > 1)
        {
            Arg1 = split[1];
        }
        if (split.Length > 2)
        {
            Arg2 = split[2];
        }
        return this;
    }

    public void Parse()
    {
        var index = 1;

        switch (this.Command)
        {
            case "add":
                var added = _dictionary.AddItem(this.Arg1, this.Arg2);
                if (added)
                {
                    Console.WriteLine("Added");
                }
                break;
            case "remove":
                if (_dictionary.RemoveItem(this.Arg1, this.Arg2))
                {
                    Console.WriteLine("Removed");
                }
                break;
            case "removeall":
                if (_dictionary.RemoveAllItem(this.Arg1))
                {
                    Console.WriteLine("Removed");
                }
                break;
            case "clear":
                if (_dictionary.Clear())
                {
                    Console.WriteLine("Cleared");
                }
                break;
            case "keys":
                var keys = _dictionary.GetKeys();
                if (keys.Length > 0)
                {
                    foreach (var key in keys)
                    {
                        Console.WriteLine($"{index}) {key}");
                        index++;
                    }
                }
                break;
            case "members":
                var members = _dictionary.GetMembers(this.Arg1);
                if (members.Length > 0)
                {
                    foreach (var member in members)
                    {
                        Console.WriteLine($"{index}) {member}");
                        index++;
                    }
                }
                break;
            case "allmembers":
                var allMembers = _dictionary.GetAllMembers();
                if (allMembers.Length > 0)
                {
                    foreach (var member in allMembers)
                    {
                        Console.WriteLine($"{index}) {member}");
                        index++;
                    }
                }
                break;
            case "items":
                var allItems = _dictionary.GetAllItems();
                if (allItems.Length > 0)
                {
                    foreach (var item in allItems)
                    {
                        Console.WriteLine($"{index}) {item}");
                        index++;
                    }
                }
                break;
            case "keyexists":
                Console.WriteLine(_dictionary.KeyExists(this.Arg1));
                break;
            case "memberexists":
                Console.WriteLine(_dictionary.MemberExists(this.Arg1,this.Arg2));
                break;
            default:
                Console.WriteLine("Invalid command");
                break;
        }
    }
}