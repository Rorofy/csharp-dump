using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MinimapApis;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton(new ArticleService());

var app = builder.Build();

app.MapGet("/get", () => "Hello GET!");
app.MapDelete("/delete", () => "Hello DELETE!");
app.MapPost("/post", () => "Hello POST!");
app.MapPut("/put", () => "Hello PUT!");
app.MapPatch("/patch", () => "Hello PATCH!");

app.MapGet("/article", () => new Article(1, "Marteau"));
app.MapGet("/articles/{id}", (int id, [FromServices] ArticleService service) =>
{
  var article = service.GetAll().Find(a => a.Id == id);
  if (article is not null)
  {
    return Results.Ok(article);
  }
  else
  {
    return Results.NotFound();
  }
});

app.MapPost("articles", (Article a, [FromServices] ArticleService service) =>
{
  var result = service.Add(a.Title);
  return Results.Ok(result);
});


app.MapGet("/personne/{nom}", (
  [FromRoute] string nom,
  [FromQuery] string? prenom,
  [FromHeader(Name = "Accept-Encoding")] string encoding) => Results.Ok($"{nom} {prenom} {encoding}"));

//app.MapGet("personne/identite", (Personne p) => Results.Ok(p));

app.MapPost("personne/identite", (Personne p) =>
{
  return Results.Ok(p);
});

await app.RunAsync();
