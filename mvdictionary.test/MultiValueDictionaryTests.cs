using NUnit.Framework;

namespace mvdictionary.test;

public class MultiValueDictionaryTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void MultiValueDictionary_AddShouldAddItem()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();
        
        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","you");
        
        Assert.IsTrue(dictionary.Items.ContainsKey("danny"));
        Assert.IsTrue(dictionary.Items["danny"].Count == 2);
    }
    [Test]
    public void MultiValueDictionary_AddShouldNotAddDuplicate()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();
        
        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","me");
        dictionary.AddItem("john","me");
        
        Assert.IsTrue(dictionary.Items.ContainsKey("danny"));
        Assert.IsTrue(dictionary.Items["danny"].Count == 1);
        Assert.IsTrue(dictionary.Items["john"].Count == 1);

    }
    [Test]
    public void MultiValueDictionary_AddShouldRespectCasingOnValues()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();
        var parser = new CommandLineParser(dictionary);
        
        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");

        Assert.IsTrue(dictionary.Items.ContainsKey("danny"));
        Assert.IsTrue(dictionary.Items["danny"].Count == 2);
    }
    
    [Test]
    public void MultiValueDictionary_GetKeysShouldReturnEmptyArrayInitially()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        var keys = dictionary.GetKeys();

        Assert.IsEmpty(keys);
    }
    
    [Test]
    public void MultiValueDictionary_GetKeysShouldReturnKeys()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");
        dictionary.AddItem("john","me");
        var keys = dictionary.GetKeys();
        var expected = new string[] { "danny", "john" };

        Assert.AreEqual(expected, keys);
    }
    [Test]
    public void MultiValueDictionary_GetMembersShouldReturnEmpty()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");
        dictionary.AddItem("john","me");
        var members = dictionary.GetMembers("david");
        Assert.IsEmpty(members);
    }
    
    [Test]
    public void MultiValueDictionary_GetMembersShouldReturnMembers()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");
        dictionary.AddItem("john","me");
        var members = dictionary.GetMembers("danny");
        var expected = new string[] { "me", "ME" };

        Assert.AreEqual(expected, members);
    }
    
    [Test]
    public void MultiValueDictionary_RemoveItemShouldReturnTrueWithValidValues()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");
        var removed = dictionary.RemoveItem("danny", "me");
        var members = dictionary.GetMembers("danny");
        Assert.AreEqual(true, removed);
        Assert.AreEqual(new string[]{"ME"}, members);
    }
    
    [Test]
    public void MultiValueDictionary_RemoveItemShouldReturnFalseWithInvalidValues()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");
        var removed = dictionary.RemoveItem("john", "me");
        Assert.AreEqual(false, removed);
        
        var members = dictionary.GetMembers("danny");
        var expected = new string[] { "me", "ME" };
        Assert.AreEqual(expected, members);
    }
    [Test]
    public void MultiValueDictionary_RemoveItemShouldReturnFalseWithWithDuplicateValues()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("danny","me");
        dictionary.AddItem("danny","ME");
        var removed = dictionary.RemoveItem("danny", "me");
        Assert.AreEqual(true, removed);
        removed = dictionary.RemoveItem("danny", "me");
        Assert.AreEqual(false, removed);
        
    }
    
    [Test]
    public void MultiValueDictionary_RemoveAllItemShouldReturnTrue()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        var keys = dictionary.GetKeys();
        Assert.IsNotEmpty(keys);
        var removed = dictionary.RemoveAllItem("foo");
        Assert.AreEqual(true, removed);
        
        var members = dictionary.GetMembers("foo");
        var expected = new string[] { };
        Assert.AreEqual(expected, members);
        
        keys = dictionary.GetKeys();
        Assert.IsEmpty(keys);
    }
    
    [Test]
    public void MultiValueDictionary_ClearShouldRemoveAllValues()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","zip");
        var keys = dictionary.GetKeys();
        Assert.IsNotEmpty(keys);
        var cleared = dictionary.Clear();
        Assert.AreEqual(true, cleared);
        
        keys = dictionary.GetKeys();
        Assert.IsEmpty(keys);
        
        cleared = dictionary.Clear();
        Assert.AreEqual(true, cleared);
    }
    
    [Test]
    public void MultiValueDictionary_KeyExistsShouldReturnTrueForValid()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","zip");
        Assert.AreEqual(true, dictionary.KeyExists("foo"));
        
    }
    
    [Test]
    public void MultiValueDictionary_KeyExistsShouldReturnFalseForInvalid()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","zip");
        Assert.AreEqual(false, dictionary.KeyExists("nope"));
    }
    
    [Test]
    public void MultiValueDictionary_MemberExistsShouldReturnFalseForInvalidKey()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","zip");
        Assert.AreEqual(false, dictionary.MemberExists("nope","bar"));
    }
    [Test]
    public void MultiValueDictionary_MemberExistsShouldReturnFalseForInvalidMember()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","zip");
        Assert.AreEqual(false, dictionary.MemberExists("foo","zip"));
    }
    
    [Test]
    public void MultiValueDictionary_MemberExistsShouldReturnTrueForValidMember()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","zip");
        Assert.AreEqual(true, dictionary.MemberExists("foo","bar"));
    }
    
    [Test]
    public void MultiValueDictionary_GetAllMembersShouldReturnAllMembers()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","bar");
        dictionary.AddItem("bang","baz");
        var expected = new string[] { "bar", "baz", "bar", "baz" };
        var members = dictionary.GetAllMembers();

        Assert.AreEqual(expected, members);
    }
    [Test]
    public void MultiValueDictionary_GetAllMembersShouldReturnEmpty()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();
        var members = dictionary.GetAllMembers();

        Assert.IsEmpty(members);
    }
    [Test]
    public void MultiValueDictionary_GetAllItemsShouldReturnAllItems()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItem("foo","bar");
        dictionary.AddItem("foo","baz");
        dictionary.AddItem("bang","bar");
        dictionary.AddItem("bang","baz");
        var expected = new string[] { "foo: bar", "foo: baz", "bang: bar", "bang: baz" };
        var members = dictionary.GetAllItems();

        Assert.AreEqual(expected, members);
    }
    [Test]
    public void MultiValueDictionary_GetAllItemshouldReturnEmpty()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();
        var members = dictionary.GetAllItems();

        Assert.IsEmpty(members);
    }
    
}