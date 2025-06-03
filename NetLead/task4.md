📊 Task 

Implement Word Frequency Analyzer Class

🎯 Objective

Create a class that analyzes a block of text and returns the top N most frequently occurring words, ordered by their repetition count in descending order.

🧩 Context

This utility will be used to extract insights from textual data by identifying the most common words. The solution should efficiently count word frequencies and return the results in a clean, ordered format.

✅ Specific Requirements

🔧 Class Requirements

Implement a class with the following structure:

`public class WordFrequencyAnalyzer
{
    public List<(string Word, int Count)> GetTopWords(string text, int topN);
}`

🔍 Behavior

Accepts:

A string of arbitrary text

An integer topN indicating how many top words to return

Returns:

A list of tuples: (Word, Count) ordered by frequency (highest to lowest)

Words should be treated case-insensitively (e.g., "Hello" and "hello" count as the same word)

Ignore punctuation and non-alphabetic characters

If topN is greater than the number of distinct words, return all available

If topN <= 0 or input text is null/empty, return an empty list