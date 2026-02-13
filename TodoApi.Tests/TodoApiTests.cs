using Xunit;
using Moq;
using TodoApi.Services;
using TodoApi.Models;
using TodoApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TodoApi.Tests;

public class TodoApiTests
{
    private readonly Mock<ITodoRepository> _mockRepository;
    private readonly Mock<ILogger<TodoService>> _mockLogger;
    private readonly ITodoService _todoService;
    private readonly TodoController _todoController;

    public TodoApiTests()
    {
        _mockRepository = new Mock<ITodoRepository>();
        _mockLogger = new Mock<ILogger<TodoService>>();
        _todoService = new TodoService(_mockRepository.Object, _mockLogger.Object);
        _todoController = new TodoController(_todoService);
    }

    [Fact]
    public void CreateTodo_ReturnsTodo_WhenValid()
    {
        var todo = new Todo { Title = "Test", Description = "Test Description", IsCompleted = false };
        _mockRepository.Setup(r => r.CreateTodo(It.IsAny<Todo>())).Returns(todo);
        var result = _todoService.CreateTodo(todo);
        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public void CreateTodo_ReturnsNull_WhenRepositoryReturnsNull()
    {
        _mockRepository.Setup(r => r.CreateTodo(It.IsAny<Todo>())).Returns((Todo)null);
        var result = _todoService.CreateTodo(new Todo { Title = "Test" });
        Assert.Null(result);
    }

    [Fact]
    public void GetAllTodos_ReturnsTodosList_WhenTodosExist()
    {
        var todos = new List<Todo> { new Todo { Id = 1, Title = "Test1" }, new Todo { Id = 2, Title = "Test2" } };
        _mockRepository.Setup(r => r.GetAllTodos(1, 10)).Returns(todos);
        var result = _todoService.GetAllTodos(1, 10);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetAllTodos_ReturnsEmptyList_WhenNoTodosExist()
    {
        _mockRepository.Setup(r => r.GetAllTodos(1, 10)).Returns(new List<Todo>());
        var result = _todoService.GetAllTodos(1, 10);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetTodoById_ReturnsTodo_WhenTodoExists()
    {
        var todo = new Todo { Id = 1, Title = "Test" };
        _mockRepository.Setup(r => r.GetTodoById(1)).Returns(todo);
        var result = _todoService.GetTodoById(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public void GetTodoById_ReturnsNull_WhenTodoDoesNotExist()
    {
        _mockRepository.Setup(r => r.GetTodoById(1)).Returns((Todo)null);
        var result = _todoService.GetTodoById(1);
        Assert.Null(result);
    }

    [Fact]
    public void UpdateTodo_ReturnsUpdatedTodo_WhenTodoExists()
    {
        var todo = new Todo { Id = 1, Title = "Updated" };
        _mockRepository.Setup(r => r.UpdateTodo(1, It.IsAny<Todo>())).Returns(todo);
        var result = _todoService.UpdateTodo(1, todo);
        Assert.NotNull(result);
        Assert.Equal("Updated", result.Title);
    }

    [Fact]
    public void UpdateTodo_ReturnsNull_WhenTodoDoesNotExist()
    {
        _mockRepository.Setup(r => r.UpdateTodo(1, It.IsAny<Todo>())).Returns((Todo)null);
        var result = _todoService.UpdateTodo(1, new Todo { Title = "Updated" });
        Assert.Null(result);
    }

    [Fact]
    public void DeleteTodo_ReturnsTrue_WhenTodoExists()
    {
        _mockRepository.Setup(r => r.DeleteTodo(1)).Returns(true);
        var result = _todoService.DeleteTodo(1);
        Assert.True(result);
    }

    [Fact]
    public void DeleteTodo_ReturnsFalse_WhenTodoDoesNotExist()
    {
        _mockRepository.Setup(r => r.DeleteTodo(1)).Returns(false);
        var result = _todoService.DeleteTodo(1);
        Assert.False(result);
    }

    [Fact]
    public void Controller_CreateTodo_ReturnsOk_WhenValid()
    {
        var todo = new Todo { Title = "Test", Description = "Desc" };
        _mockRepository.Setup(r => r.CreateTodo(It.IsAny<Todo>())).Returns(todo);
        var result = _todoController.CreateTodo(todo) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Controller_CreateTodo_ReturnsBadRequest_WhenModelInvalid()
    {
        _todoController.ModelState.AddModelError("Title", "Required");
        var result = _todoController.CreateTodo(new Todo()) as BadRequestObjectResult;
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void Controller_GetAllTodos_ReturnsOkWithTodos()
    {
        var todos = new List<Todo> { new Todo { Id = 1, Title = "Test1" } };
        _mockRepository.Setup(r => r.GetAllTodos(1, 10)).Returns(todos);
        var result = _todoController.GetAllTodos(1, 10) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Controller_GetTodo_ReturnsOk_WhenTodoExists()
    {
        var todo = new Todo { Id = 1, Title = "Test" };
        _mockRepository.Setup(r => r.GetTodoById(1)).Returns(todo);
        var result = _todoController.GetTodo(1) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Controller_GetTodo_ReturnsNotFound_WhenTodoDoesNotExist()
    {
        _mockRepository.Setup(r => r.GetTodoById(1)).Returns((Todo)null);
        var result = _todoController.GetTodo(1) as NotFoundResult;
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public void Controller_UpdateTodo_ReturnsOk_WhenTodoExists()
    {
        var todo = new Todo { Id = 1, Title = "Updated" };
        _mockRepository.Setup(r => r.UpdateTodo(1, It.IsAny<Todo>())).Returns(todo);
        var result = _todoController.UpdateTodo(1, todo) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Controller_UpdateTodo_ReturnsNotFound_WhenTodoDoesNotExist()
    {
        _mockRepository.Setup(r => r.UpdateTodo(1, It.IsAny<Todo>())).Returns((Todo)null);
        var result = _todoController.UpdateTodo(1, new Todo { Title = "Updated" }) as NotFoundResult;
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public void Controller_UpdateTodo_ReturnsBadRequest_WhenModelInvalid()
    {
        _todoController.ModelState.AddModelError("Title", "Required");
        var result = _todoController.UpdateTodo(1, new Todo()) as BadRequestObjectResult;
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void Controller_DeleteTodo_ReturnsOk_WhenTodoExists()
    {
        _mockRepository.Setup(r => r.DeleteTodo(1)).Returns(true);
        var result = _todoController.DeleteTodo(1) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public void Controller_DeleteTodo_ReturnsNotFound_WhenTodoDoesNotExist()
    {
        _mockRepository.Setup(r => r.DeleteTodo(1)).Returns(false);
        var result = _todoController.DeleteTodo(1) as NotFoundResult;
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }
}
