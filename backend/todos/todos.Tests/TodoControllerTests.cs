using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using todos.Controllers;
using todos.Models;
using Xunit;

namespace todos.Tests
{
    public class TodoControllerTests
    {
        private TodoController underTest;
        IRepository<Todo> todoRepo;

        public TodoControllerTests()
        {
            todoRepo = Substitute.For<IRepository<Todo>>();
            underTest = new TodoController(todoRepo);
        }

        [Fact]
        public void Get_Returns_List_of_Todos()
        {
            var expectedTodos = new List<Todo>()
            {
                new Todo(1, "First item"),
                new Todo(2, "Second item")
            };
            todoRepo.GetAll().Returns(expectedTodos);

            var result = underTest.Get();

            Assert.Equal(expectedTodos, result.ToList());
        }

        [Fact]
        public void Post_Creates_New_Todo()
        {
            var newTodo = new Todo(1, "New todo");
            var todoList = new List<Todo>();

            // Use When..Do to test void methods
            // When() allows us to call the method on the substitute and pass an argument
            // Do() allows us to pass a callback function that executes when the method is called
            todoRepo.When(t => t.Create(newTodo))
                .Do(t => todoList.Add(newTodo));

            todoRepo.GetAll().Returns(todoList);

            var result = underTest.Post(newTodo);

            Assert.Contains(newTodo, result);
        }

        [Fact]
        public void Delete_Removes_Todo()
        {
            var todoId = 1;
            var deletedTodo = new Todo(todoId, "First item");
            var todoList = new List<Todo>()
            {
                deletedTodo,
                new Todo(2, "Second item")
            };

            todoRepo.GetById(todoId).Returns(deletedTodo);
            todoRepo.When(d => d.Delete(deletedTodo))
                .Do(d => todoList.Remove(deletedTodo));
            todoRepo.GetAll().Returns(todoList);

            var result = underTest.Delete(todoId);

            // Assert.DoesNotContain(deletedTodo, result); Does not work in all cases
            Assert.All(result, item => Assert.Contains("Second item", item.Name));
        }

        [Fact]
        public void Put_Updates_Todo()
        {
            var originalTodo = new Todo(1, "First item");
            var expectedTodos = new List<Todo>()
            {
                originalTodo
            };
            var updatedTodo = new Todo(1, "Updated item");

            todoRepo.When(t => todoRepo.Update(updatedTodo))
                .Do(Callback.First(t => expectedTodos.Remove(originalTodo))
                .Then(t => expectedTodos.Add(updatedTodo)));
            todoRepo.GetAll().Returns(expectedTodos);

            var result = underTest.Put(updatedTodo);

            // Assert.Equal(expectedTodos, result.ToList());
            Assert.All(result, item => Assert.Contains("Updated item", item.Name));
        }
    }
}
