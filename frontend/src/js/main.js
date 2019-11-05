import Header from "./components/Header";
import Footer from "./components/Footer";
import Todos from "./components/Todos";
import Todo from "./components/Todo";
import apiActions from "./api/apiActions";
import Home from "./components/Home";

export default () => {
  pageBuild();
};

function pageBuild() {
  header();
  footer();
  navHome();
  navTodos();
}

function header() {
  const header = document.getElementById("header");
  header.innerHTML = Header();
}

function footer() {
  const footer = document.getElementById("footer");
  footer.innerHTML = Footer();
}

function navHome() {
  const homeButton = document.querySelector(".nav__home");
  homeButton.addEventListener("click", function() {
    document.querySelector("#app").innerHTML = Home();
  });
}

function navTodos() {
  const toDosButton = document.querySelector(".nav__todos");
  const app = document.querySelector("#app");

  toDosButton.addEventListener("click", function() {
    apiActions.getRequest("https://localhost:44327/api/todos", toDos => {
      document.querySelector("#app").innerHTML = Todos(toDos);
    });
  });

  app.addEventListener("click", function() {
    if (event.target.classList.contains("add-toDo__submit")) {
      const toDo = event.target.parentElement.querySelector(
        ".add-toDo__toDoName"
      ).value;

      console.log(toDo);
      apiActions.postRequest(
        "https://localhost:44327/api/todos",
        {
          name: toDo
        },
        toDos => {
          console.log(toDos);
          document.querySelector("#app").innerHTML = Todos(toDos);
        }
      );
    }
  });

  app.addEventListener("click", function() {
    if (event.target.classList.contains("delete-toDo__submit")) {
      const toDoId = event.target.parentElement.querySelector(".toDo__id")
        .value;
      console.log("delete " + toDoId);
      apiActions.deleteRequest(
        `https://localhost:44327/api/todos/${toDoId}`,
        toDos => {
          app.innerHTML = Todos(toDos);
        }
      );
    }
  });

  app.addEventListener("click", function() {
    if (event.target.classList.contains("edit-toDo__submit")) {
      const toDoId = event.target.parentElement.querySelector(".toDo__id")
        .value;
      console.log("edit " + toDoId);
      apiActions.getRequest(
        `https://localhost:44327/api/todos/${toDoId}`,
        toDo => {
          app.innerHTML = Todo(toDo);
        }
      );
    }
  });

  app.addEventListener("click", function() {
    if (event.target.classList.contains("update-toDo__submit")) {
      const toDoId = event.target.parentElement.querySelector(
        ".update-toDo__id"
      ).value;
      const toDoName = event.target.parentElement.querySelector(
        ".update-toDo__name"
      ).value;

      const toDoData = {
        id: toDoId,
        name: toDoName
      };

      apiActions.putRequest(
        `https://localhost:44327/api/todos/${toDoId}`,
        toDoData,
        todos => {
          document.querySelector("#app").innerHTML = Todos(todos);
        }
      );
    }
  });
}
