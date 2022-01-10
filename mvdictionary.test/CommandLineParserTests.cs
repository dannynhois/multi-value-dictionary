using Moq;
using NUnit.Framework;

namespace mvdictionary.test;

public class CommandLineParserTests
{
    private Mock<IMultiValueDictionary> _dictionary;
    
    [SetUp]
    public void Setup()
    {
        _dictionary = new Mock<IMultiValueDictionary>();
    }
    
    [Test]
    public void CommandLineParser_AddShouldRespectCasingOnValues()
    {
        // Given
        var parser = new CommandLineParser(_dictionary.Object);
        
        parser.SetValues("ADD danny me").Parse();
        parser.SetValues("add danny ME").Parse();
        parser.SetValues("addx danny ME").Parse();

        _dictionary.Verify(x => x.AddItems(It.IsAny<string>(),It.IsAny<string>()),Times.Exactly(2));
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
}