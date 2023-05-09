using System.Text.RegularExpressions;
namespace MoogleEngine;

public class GetFile
{
   
    public static List<Documents> Docs = new List<Documents>();
    readonly static string Folder = @"E:\content2";
    static public List<string> paths = Directory.GetFiles(Folder, "*.txt").ToList();
    static public int CantidadDoc = paths.Count;
   
   
   
    public static List<string> Allwords = new List<string>();
    public static int TotalWords;
    public GetFile()
    {
     foreach (string path in paths)
        {
            string aux = File.ReadAllText(path);
            string[] words = Regex.Split(aux.ToLower(), @"\W+");
            Docs.Add(new Documents(path, Path.GetFileNameWithoutExtension(path), aux , words , new List<double>(), new List<double>(),"", 0));
            Allwords.AddRange(words);
        } 
      Allwords = Allwords.Distinct().ToList();
      TotalWords = Allwords.Count;
    }
    public static void Clean()
    {
        foreach (Documents doc in Docs)
        {
            doc.TF.Clear();
            doc.TFIDF.Clear();
            Busqueda.QueryTF = Enumerable.Repeat(0, GetFile.TotalWords).ToList();
            Busqueda.Docxword = Enumerable.Repeat(0, GetFile.TotalWords).ToList();
            Busqueda.Suggest.Clear();
            Suggestion.Suggest = "";
        }
    }
}