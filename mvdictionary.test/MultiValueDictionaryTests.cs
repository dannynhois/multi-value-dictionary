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
        
        dictionary.AddItems("danny","me");
        dictionary.AddItems("danny","you");
        
        Assert.IsTrue(dictionary.Items.ContainsKey("danny"));
        Assert.IsTrue(dictionary.Items["danny"].Count == 2);
    }
    [Test]
    public void MultiValueDictionary_AddShouldNotAddDuplicate()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();
        
        dictionary.AddItems("danny","me");
        dictionary.AddItems("danny","me");
        dictionary.AddItems("john","me");
        
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
        
        dictionary.AddItems("danny","me");
        dictionary.AddItems("danny","ME");

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

        dictionary.AddItems("danny","me");
        dictionary.AddItems("danny","ME");
        dictionary.AddItems("john","me");
        var keys = dictionary.GetKeys();
        var expected = new string[] { "danny", "john" };

        Assert.AreEqual(expected, keys);
    }
    [Test]
    public void MultiValueDictionary_GetMembersShouldReturnEmpty()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItems("danny","me");
        dictionary.AddItems("danny","ME");
        dictionary.AddItems("john","me");
        var members = dictionary.GetMembers("david");
        Assert.IsEmpty(members);
    }
    
    [Test]
    public void MultiValueDictionary_GetMembersShouldReturnMembers()
    {
        // Given
        var dictionary = new MultiValueDictionary<string>();

        dictionary.AddItems("danny","me");
        dictionary.AddItems("danny","ME");
        dictionary.AddItems("john","me");
        var members = dictionary.GetMembers("danny");
        var expected = new string[] { "me", "ME" };

        Assert.AreEqual(expected, members);
    }
}