using Moq;
using NUnit.Framework;

namespace mvdictionary.test;

public class CommandLineParserTests
{
    private Mock<IMultiValueDictionary<string>> _dictionary;
    
    [SetUp]
    public void Setup()
    {
        _dictionary = new Mock<IMultiValueDictionary<string>>();
    }
    
    [Test]
    public void CommandLineParser_AddShouldRespectCasingOnValues()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        
        parser.SetValues("ADD danny me").Parse();
        parser.SetValues("add danny ME").Parse();
        parser.SetValues("addx danny ME").Parse();

        _dictionary.Verify(x => x.AddItem(It.IsAny<string>(),It.IsAny<string>()),Times.Exactly(2));
    }
    
    [Test]
    public void CommandLineParser_GetKeysShouldLogEmpty()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("keys ignore").Parse();

        _dictionary.Verify(x => x.GetKeys(),Times.AtLeastOnce);
    }
    [Test]
    public void CommandLineParser_GetKeysShouldLog()
    {
        // Given
        _dictionary.Setup(x => x.GetKeys()).Returns(new string[] { "danny", "john" });
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("keys ignore").Parse();

        _dictionary.Verify(x => x.GetKeys(),Times.AtLeastOnce);
    }
    [Test]
    public void CommandLineParser_GetMembersShouldLog()
    {
        // Given
        _dictionary.Setup(x => x.GetMembers(It.IsAny<string>())).Returns(new string[] { "danny", "john" });
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("members ignore").Parse();

        _dictionary.Verify(x => x.GetMembers(It.IsAny<string>()),Times.AtLeastOnce);
    }
    
    [Test]
    public void CommandLineParser_KeysCallsCorrectFunction()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("keys ignore").Parse();
        _dictionary.Verify(x => x.GetKeys(),Times.AtLeastOnce);
    }
    [Test]
    public void CommandLineParser_MembersCallsCorrectFunction()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("members key").Parse();
        _dictionary.Verify(x => x.GetMembers("key"),Times.AtLeastOnce);
    }
    [Test]
    public void CommandLineParser_AddCallsCorrectFunction()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("add foo bar").Parse();
        _dictionary.Verify(x => x.AddItem("foo","bar"),Times.AtLeastOnce);
    }
    [Test]
    public void CommandLineParser_RemoveCallsCorrectFunction()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("remove foo bar").Parse();
        _dictionary.Verify(x => x.RemoveItem("foo","bar"),Times.AtLeastOnce);
    }
    
    [Test]
    public void CommandLineParser_RemoveAllCallsCorrectFunction()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("REMOVEALL foo").Parse();
        _dictionary.Verify(x => x.RemoveAllItem("foo"),Times.AtLeastOnce);
    }
    [Test]
    public void CommandLineParser_ClearCallsCorrectFunction()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        parser.SetValues("CLEAR foo").Parse();
        _dictionary.Verify(x => x.Clear(),Times.AtLeastOnce);
    }
    
}