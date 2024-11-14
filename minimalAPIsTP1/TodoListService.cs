using Microsoft.AspNetCore.Http.Features;

namespace minimalAPIsTP1;

public class TodoListService
{
  private List<Task> todoList = new List<Task> { };

  public List<Task> GetAll() => todoList;

  public Task? GetById(int id) => todoList.Find(t => t.id == id);

  public Task Add(string title)
  {
    var id = todoList.Count > 0 ? todoList.Max(a => a.id) + 1 : 0;
    var task = new Task(id, title, DateTime.Now);
    todoList.Add(task);
    return task;
  }

  public void Update(int id, Task task)
  {
    Delete(id);
    todoList.Add(new Task(id, task.title, task.startDate, task.endDate));
  }

  public bool Delete(int id)
  {
    var task = GetById(id);
    if (task is not null)
    {
      todoList.Remove(task);
      return true;
    }
    return false;
  }
}
