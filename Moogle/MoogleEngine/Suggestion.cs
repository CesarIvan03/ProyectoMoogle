namespace MoogleEngine;

using System;
using System.Collections.Generic;

public class Suggestion
{
    public static int index = 0;
    public static string Suggest = "";

    public Suggestion(List<string> searchTerms, List<string> allWords)
    {
        foreach (string term in searchTerms)
        {
            if (!allWords.Contains(term))
            {
                string suggestedWord = SuggestWord(term, allWords);
                Suggest += suggestedWord + " ";
            }
            else
            {
                Suggest += term + " ";
            }
        }
    }

    public string SuggestWord(string word, List<string> allWords)
    {
        int minDistance = int.MaxValue;
        string suggestedWord = "";

        foreach (string w in allWords)
        {
            int distance = Levenshtein(word, w);
            if (distance < minDistance)
            {
                minDistance = distance;
                suggestedWord = w;
            }
        }

        return suggestedWord;
    }

    public int Levenshtein(string word1, string word2)
    {
        int[,] distances = new int[word1.Length + 1, word2.Length + 1];

        for (int i = 0; i <= word1.Length; i++)
        {
            distances[i, 0] = i;
        }

        for (int j = 0; j <= word2.Length; j++)
        {
            distances[0, j] = j;
        }

        for (int i = 1; i <= word1.Length; i++)
        {
            for (int j = 1; j <= word2.Length; j++)
            {
                int cost = (word1[i - 1] == word2[j - 1]) ? 0 : 1;

                distances[i, j] = Math.Min(
                    Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost);
            }
        }

        return distances[word1.Length, word2.Length];
    }
}

