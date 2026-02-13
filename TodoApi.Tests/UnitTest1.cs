using Xunit;
using Moq;
using TodoApi.Services;
using TodoApi.Models;
using TodoApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Repositories;
using Microsoft.Extensions.Logging;

namespace TodoApi.Tests;

public class UnitTest1
{
    private readonly Mock<ITodoRepository> _mockRepository;
    private readonly Mock<ILogger<TodoService>> _mockLogger;
    private readonly ITodoService _todoService;
    private readonly TodoController _todoController;

    public UnitTest1()
    {
        _mockRepository = new Mock<ITodoRepository>();
        _mockLogger = new Mock<ILogger<TodoService>>();
        _todoService = new TodoService(_mockRepository.Object, _mockLogger.Object);
        _todoController = new TodoController(_todoService);
    }

    [Fact]
    public void TestCreateTodo()
    {
        var todo = new Todo
        {
            Title = "Test",
            Description = "Test Description",
            IsCompleted = false
        };

        _mockRepository.Setup(r => r.CreateTodo(It.IsAny<Todo>())).Returns(todo);

        var result = _todoService.CreateTodo(todo);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public void TestGetTodo()
    {
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Test1", Description = "Desc1", IsCompleted = false },
            new Todo { Id = 2, Title = "Test2", Description = "Desc2", IsCompleted = true }
        };

        _mockRepository.Setup(r => r.GetAllTodos()).Returns(todos);

        var result = _todoService.GetAllTodos();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void TestUpdateTodo()
    {
        var todo = new Todo
        {
            Id = 1,
            Title = "Updated",
            Description = "Updated Description",
            IsCompleted = true
        };

        _mockRepository.Setup(r => r.UpdateTodo(1, It.IsAny<Todo>())).Returns(todo);

        var result = _todoService.UpdateTodo(1, todo);

        Assert.NotNull(result);
        Assert.Equal("Updated", result.Title);
    }

    [Fact]
    public void TestDeleteTodo()
    {
        _mockRepository.Setup(r => r.DeleteTodo(1)).Returns(true);

        var result = _todoService.DeleteTodo(1);

        Assert.True(result);
    }

    [Fact]
    public void TestControllerCreateTodo()
    {
        var todo = new Todo { Title = "Test", Description = "Desc" };

        _mockRepository.Setup(r => r.CreateTodo(It.IsAny<Todo>())).Returns(todo);

        var result = _todoController.CreateTodo(todo) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }
}
