using System.Text.RegularExpressions;
namespace MoogleEngine;

public class Busqueda
{
   public static string? query;
   public static List<string> Query = new List<string>();
   public static List<int> QueryTF = Enumerable.Repeat(0, GetFile.TotalWords).ToList();
   public static List<int> Docxword = Enumerable.Repeat(0, GetFile.TotalWords).ToList();
   public static List<Documents> OrdDocs = new List<Documents>();
   public static List<int> Suggest = new List<int>();
   int index = 0;
   int snippetwords = 50;

    public Busqueda(){
            if (query != null)
            {
                Query = new List<string>(Regex.Split(query, @"\W+"));
            }
            foreach (string word in Query)
            {
                int aux = GetFile.Allwords.IndexOf(word);
                if (aux != -1)
                {
                    QueryTF[aux] = 1;
                    Suggest.Add(1);
                }
                else
                {
                Suggest.Add(0);
                }
            }
            foreach (int binary in QueryTF)
            {
                foreach (Documents doc in GetFile.Docs)
                {
                    if (binary == 1)
                    {
                        double cantidad = doc.Words.Count(s => s == GetFile.Allwords[index]);
                        if (cantidad != 0)
                        {
                            Docxword[index] += 1;
                            doc.TF.Add(1 + Math.Log10(cantidad)); 
                        }
                        else
                        {
                            doc.TF.Add(0);
                        }
                    }
                    else
                    {
                        doc.TF.Add(0);
                    }
                }
                index++;
            }
            foreach (Documents doc in GetFile.Docs)
            {
                int index = 0;
                float score = 0;
                foreach (double tf in doc.TF)
                {
                    doc.TFIDF.Add(doc.TF[index] * Math.Log10((double)GetFile.CantidadDoc / (1 + Docxword[index])));  
                    score += (float)doc.TFIDF[index];
                    index++;
                }
                doc.Score = score;
                double max = doc.TFIDF.Max();
            if (max != 0)
            {
                string HighestTFIDF = GetFile.Allwords[(doc.TFIDF.IndexOf(max))];
                doc.Snippet = WordsAround(doc.Content, HighestTFIDF, snippetwords);
            }
               
            }
            OrdDocs = GetFile.Docs.OrderByDescending(a => a.Score).ToList();  
            OrdDocs.RemoveAll(a => a.Score <= 0);
    }
    public static string WordsAround(string input, string word, int numW) 
    {
        Match match = Regex.Match(input, @$"\W{word}\W");
        int index =match.Index; 
        List<string> start = input.Substring(0, index).Split(' ').ToList();
        start.Reverse();
        int aux = Math.Min(numW, start.Count);
        start.RemoveRange(aux-1, start.Count- aux);
        start.Reverse();
        string startstring = string.Join(" ", start);
        List<string> end = input.Substring(index).Split(' ').ToList();
        aux = Math.Min(numW, end.Count);
        end.RemoveRange(aux - 1, end.Count - aux);
        string endstring = string.Join(" ", end);
        string result = startstring + endstring;
        return result;
    }
} 