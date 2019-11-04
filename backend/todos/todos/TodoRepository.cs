﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todos.Models;

namespace todos
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoContext context) : base(context)
        {

        }
    }
}
