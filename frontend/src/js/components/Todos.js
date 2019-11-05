export default function Todos(toDos) {
  return `
  <ul> 
  ${toDos
    .map(toDo => {
      return `
          <li>
              <h3>${toDo.name}</h3>
              <input class="toDo__id" type="hidden" value="${toDo.id}">
              <button class="edit-toDo__submit">Edit</button>
              <button class="delete-toDo__submit">Delete</button>
          </li>
      `;
    })
    .join("")}
</ul>

<section class='add-toDo'>
  <input class='add-toDo__toDoName' type='text' placeholder='Add a toDo!'>
  <button class='add-toDo__submit'>Submit</button>
</section>
    
    
    `;
}
