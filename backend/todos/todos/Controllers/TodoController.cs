using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todos.Models;

namespace todos.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        //private static List<string> all = new List<string>()
        //{
        //    "Remodel Bathroom",
        //    "Finish my laser app",
        //    "Do things with kids"
        //};

        private IRepository<Todo> todoRepo;

        public TodoController(IRepository<Todo> todoRepo)
        {
            this.todoRepo = todoRepo;
        }

        // GET api/todos
        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            return todoRepo.GetAll();
        }

        // GET api/todos/5
        [HttpGet("{id}")]
        public Todo Get(int id)
        {
            return todoRepo.GetById(id);
        }

        // POST api/todos
        [HttpPost]
        public IEnumerable<Todo> Post([FromBody] Todo todo)
        {
            todoRepo.Create(todo);
            return todoRepo.GetAll();
        }

        // PUT api/todos/5
        [HttpPut("{id}")]
        public IEnumerable<Todo> Put([FromBody] Todo todo)
        {
            todoRepo.Update(todo);
            return todoRepo.GetAll();
        }

        // DELETE api/todos/5
        [HttpDelete("{id}")]
        public IEnumerable<Todo> Delete(int id)
        {
            var todo = todoRepo.GetById(id);
            todoRepo.Delete(todo);
            return todoRepo.GetAll();
        }
    }
}
