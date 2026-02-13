using TodoApi.Models;
using TodoApi.Repositories;
using Microsoft.Extensions.Logging;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Todo CreateTodo(Todo todo);
        List<Todo> GetAllTodos(int pageNumber, int pageSize);
        Todo GetTodoById(int id);
        Todo UpdateTodo(int id, Todo todo);
        bool DeleteTodo(int id);
    }

    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly ILogger<TodoService> _logger;

        public TodoService(ITodoRepository repository, ILogger<TodoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Todo CreateTodo(Todo todo)
        {
            try
            {
                return _repository.CreateTodo(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo");
                throw;
            }
        }

        public List<Todo> GetAllTodos(int pageNumber, int pageSize)
        {
            try
            {
                return _repository.GetAllTodos(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all todos");
                throw;
            }
        }

        public Todo GetTodoById(int id)
        {
            try
            {
                return _repository.GetTodoById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting todo by id {id}");
                throw;
            }
        }

        public Todo UpdateTodo(int id, Todo todo)
        {
            try
            {
                return _repository.UpdateTodo(id, todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating todo {id}");
                throw;
            }
        }

        public bool DeleteTodo(int id)
        {
            try
            {
                return _repository.DeleteTodo(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting todo {id}");
                throw;
            }
        }
    }
}
