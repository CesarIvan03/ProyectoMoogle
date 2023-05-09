namespace MoogleEngine;

public class Documents
{
    //Esta línea define un constructor para la clase Documents que toma varios argumentos.
    public Documents(string path, string title, string content, string[] words, List<double>TF, List<double>TFIDF, string snippet, float score)
    {

    //Estas líneas asignan los valores de los argumentos del constructor a las propiedades correspondientes de la clase Documents.
        this.Path = path;
        this.Title = title;
        this.Content = content;
        this.TF = TF;
        this.TFIDF = TFIDF;
        this.Snippet = snippet;
        this.Words = words;
        this.Score = score;

    }
    //Estas líneas definen las propiedades de la clase Documents.
    public string Path { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public List<double> TF { get; private set; }
    public List<double> TFIDF { get; private set; }
    public string Snippet { get; set; }
    public string[] Words { get; private set; }
    public float Score { get; set; }
}
 
