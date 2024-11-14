using System.Reflection;

namespace MinimapApis
{
  public class Personne
  {
    public string? nom { get; set; }
    public string? prenom { get; set; }

    // public static bool TryParse(string value, out Personne? person) {
    //   try {
    //     var data = value.Split(' ');
    //     person = new Personne {
    //       nom = data[0],
    //       prenom = data[1]
    //     };
    //     return true;
    //   } catch (Exception) {
    //     person = null;
    //     return false;
    //   }
    // }

    public static async ValueTask<Personne?> BindAsync(
      HttpContext context, ParameterInfo parameterInfo)
    {
      try
      {
        using var streamReader = new StreamReader(context.Request.Body);
        var body = await streamReader.ReadToEndAsync();
        var data = body.Split(' ');
        var person = new Personne
        {
          nom = data[0],
          prenom = data[1]
        };
        return person;
      }
      catch (Exception)
      {
        return null;
      }
    }
  }
}
