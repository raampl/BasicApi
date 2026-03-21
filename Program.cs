var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory store
var todos = new List<Todo>
{
    new Todo { Id = 1, Title = "Learn minimal APIs", IsDone = false },
    new Todo { Id = 2, Title = "Build a REST endpoint", IsDone = false }
};

// GET /todos  - list
app.MapGet("/todos", () =>
{
    return Results.Ok(todos);
});

// GET /todos/{id}  - single
app.MapGet("/todos/{id:int}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

// POST /todos  - create
app.MapPost("/todos", (Todo newTodo) =>
{
    var nextId = todos.Count == 0 ? 1 : todos.Max(t => t.Id) + 1;
    newTodo.Id = nextId;
    todos.Add(newTodo);
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

// PUT /todos/{id}  - update
app.MapPut("/todos/{id:int}", (int id, Todo updated) =>
{
    var existing = todos.FirstOrDefault(t => t.Id == id);
    if (existing is null) return Results.NotFound();

    existing.Title = updated.Title;
    existing.IsDone = updated.IsDone;
    return Results.NoContent();
});

// DELETE /todos/{id}  - delete
app.MapDelete("/todos/{id:int}", (int id) =>
{
    var existing = todos.FirstOrDefault(t => t.Id == id);
    if (existing is null) return Results.NotFound();

    todos.Remove(existing);
    return Results.NoContent();
});

app.Run();