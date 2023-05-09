namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query)
     {
        GetFile.Clean();
        
        //verifica si la cadena de consulta es una cadena vacía.
        if (query == "")
        {
            //creamos un objeto SearchResult que contiene un solo objeto SearchItem que indica que se debe ingresar una consulta si la cadena de consulta es una cadena vacía.
            SearchItem[] items = new SearchItem[1];
            items[0] = new SearchItem("Por favor ingrese lo que necesite buscar", "" , 0);
            return new SearchResult(items, query);
        }
        else
        {
            //Estas líneas establecen la propiedad query de la clase Busqueda en la cadena de consulta en minúsculas, crean un objeto Busqueda y un objeto Suggestion.
            Busqueda.query = query.ToLower();
            Busqueda search = new Busqueda();
            Suggestion suggestion = new Suggestion();
           
           //verifica si la cadena de consulta es una cadena vacía.

            if (Busqueda.OrdDocs.Count == 0)
            {
                SearchItem[] items = new SearchItem[1];
                items[0] = new SearchItem("No existe ningun documento que coincida con la busqueda", "", 0);
                return new SearchResult(items, Suggestion.Suggest);
            }
            else {
                int r = Math.Min(Busqueda.OrdDocs.Count, 4);
                SearchItem[] items = new SearchItem[r];
                for (int i = 0; i < r; i++)
                {
                    items[i] = new SearchItem(Busqueda.OrdDocs[i].Title, Busqueda.OrdDocs[i].Snippet, Busqueda.OrdDocs[i].Score);
                }
                return new SearchResult(items, Suggestion.Suggest);
            }   
        }
    }
}
