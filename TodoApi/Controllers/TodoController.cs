using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _todoService.CreateTodo(todo);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllTodos()
        {
            var todos = _todoService.GetAllTodos();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updated = _todoService.UpdateTodo(id, todo);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var deleted = _todoService.DeleteTodo(id);
            if (!deleted)
                return NotFound();
            return Ok(new { message = "Todo deleted successfully" });
        }
    }
}
