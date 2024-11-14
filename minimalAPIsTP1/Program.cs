using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using minimalAPIsTP1;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton(new TodoListService());

var app = builder.Build();

app.MapGet("/todos", ([FromServices] TodoListService service) => Results.Ok(service.GetAll()));

app.MapGet("/todos/{id:int}", ([FromRoute] int id, [FromServices] TodoListService service) =>
{
  var task = service.GetById(id);
  if (task is not null)
  {
    return Results.Ok(task);
  }
  else
  {
    return Results.NotFound();
  }
});

app.MapGet("/todos/active", ([FromServices] TodoListService service) =>
{
  var activeTasks = service.GetAll().Where((t) => t.endDate == null);
  return Results.Ok(activeTasks);
});


app.MapPost("/todos", ([FromBody] string title, [FromServices] TodoListService service) =>
{
  return Results.Ok(service.Add(title));
});

app.MapDelete("/todos/{id:int}", ([FromRoute] int id, [FromServices] TodoListService service) =>
{

  var result = service.Delete(id);
  if (result)
  {
    return Results.NoContent();
  }
  return Results.NotFound();
});

app.MapPut("/todos/{id:int}", ([FromRoute] int id, [FromBody] minimalAPIsTP1.Task task, [FromServices] TodoListService service) =>
{
  service.Update(id, task);
  return Results.NoContent();
});


await app.RunAsync();

