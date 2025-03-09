using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static readonly List<TodoItem> _todos = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Aprender Angular", IsCompleted = false },
            new TodoItem { Id = 2, Title = "Crear API", IsCompleted = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodos()
        {
            return Ok(_todos);
        }

        [HttpPost]
        public ActionResult<TodoItem> AddTodo([FromBody] TodoItem todo)
        {
            todo.Id = _todos.Max(t => t.Id) + 1;
            _todos.Add(todo);
            return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
        }
    }
}