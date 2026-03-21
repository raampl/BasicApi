# BasicApi

A minimal REST API built with ASP.NET Core for managing Todo items.

## Overview

BasicApi provides a simple CRUD interface for Todo items using ASP.NET Core Minimal APIs. It stores data in-memory and exposes standard REST endpoints.

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                      Client Request                          │
│                   (HTTP Client/Postman)                      │
└─────────────────────────┬───────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────┐
│                    Minimal API Layer                         │
│  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐        │
│  │ GET     │  │ GET     │  │ POST    │  │ PUT     │  DELETE│
│  │ /todos  │  │ /todos/ │  │ /todos  │  │ /todos/ │  /todos│
│  │         │  │   {id}  │  │         │  │   {id}  │  /{id} │
│  └────┬────┘  └────┬────┘  └────┬────┘  └────┬────┘  └──┬───┘
└───────┼────────────┼────────────┼────────────┼──────────┼────┘
        │            │            │            │          │
        └────────────┴────────────┼────────────┴──────────┘
                                 │
                                 ▼
┌─────────────────────────────────────────────────────────────┐
│                    In-Memory Store                          │
│                    List<Todo>                               │
└─────────────────────────────────────────────────────────────┘
```

## Endpoints

| Method | Endpoint       | Description              |
|--------|----------------|--------------------------|
| GET    | /todos         | List all todos           |
| GET    | /todos/{id}    | Get a specific todo       |
| POST   | /todos         | Create a new todo         |
| PUT    | /todos/{id}    | Update an existing todo   |
| DELETE | /todos/{id}    | Delete a todo             |

## Data Model

```csharp
public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsDone { get; set; }
}
```

## Running the API

```bash
dotnet run
```

The API will be available at `http://localhost:5000` (or the port configured in `launchSettings.json`).

## Example Requests

```bash
# List all todos
curl http://localhost:5000/todos

# Get a single todo
curl http://localhost:5000/todos/1

# Create a todo
curl -X POST http://localhost:5000/todos \
  -H "Content-Type: application/json" \
  -d '{"title": "New task", "isDone": false}'

# Update a todo
curl -X PUT http://localhost:5000/todos/1 \
  -H "Content-Type: application/json" \
  -d '{"title": "Updated task", "isDone": true}'

# Delete a todo
curl -X DELETE http://localhost:5000/todos/1
```
