using FluentAssertions;
using NetLead.Helpers;

namespace NetLeadTests;
public class WordFrequencyAnalyzerTests
{
    private readonly WordFrequencyAnalyzer _sut = new();

    [Fact]
    public void Returns_Top_3_Words_Correctly()
    {
        var result = _sut.GetTopWords("The quick brown fox jumps over the lazy dog. The dog was not amused.", 3);

        var expected = new List<(string, int)>
        {
            ("the", 3),
            ("dog", 2),
            ("quick", 1)
        };

        result[0].Should().BeEquivalentTo(expected[0]);
        result[1].Should().BeEquivalentTo(expected[1]);
    }

    [Fact]
    public void Handles_Case_Insensitivity()
    {
        var result = _sut.GetTopWords("Apple apple APPLE", 1);
        result[0].Should().BeEquivalentTo(("apple", 3));
    }

    [Fact]
    public void Ignores_Punctuation()
    {
        var result = _sut.GetTopWords("Hello, world! Hello...", 1);
        result[0].Should().BeEquivalentTo(("hello", 2));
    }

    [Fact]
    public void Returns_Empty_List_On_Empty_Input()
    {
        var result = _sut.GetTopWords("", 5);
        result.Should().BeEmpty();
    }

    [Fact]
    public void Returns_All_Words_When_Top_N_Exceeds_Count()
    {
        var result = _sut.GetTopWords("One two three", 10);
        result.Count.Should().Be(3);
    }
}
