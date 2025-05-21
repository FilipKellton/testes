using System.Text.RegularExpressions;

namespace NetLead.Helpers;

public class WordFrequencyAnalyzer
{
    public List<(string Word, int Count)> GetTopWords(string text, int topN)
    {
        if (string.IsNullOrWhiteSpace(text) || topN <= 0)
        {
            return [];
        }

        var words = Regex.Matches(text.ToLower(), @"\b[a-z]+\b").Select(match => match.Value);

        var wordCounts = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (wordCounts.TryGetValue(word, out int value))
            {
                wordCounts[word] = ++value;
            }
            else
            {
                wordCounts[word] = 1;
            }
        }

        return wordCounts
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .Take(topN)
            .Select(kv => (kv.Key, kv.Value))
            .ToList();
    }
}
