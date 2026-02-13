using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        Todo CreateTodo(Todo todo);
        List<Todo> GetAllTodos(int pageNumber, int pageSize);
        Todo GetTodoById(int id);
        Todo UpdateTodo(int id, Todo todo);
        bool DeleteTodo(int id);
    }
}