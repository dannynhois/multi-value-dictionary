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
}