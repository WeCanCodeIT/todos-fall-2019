export default function Todo(toDo) {
  return `
    <section class='main-content__toDo'>
        <h3>${toDo.name}</h3>
    </section>
  
    <section class='update-toDo'>
        <input class='update-toDo__name' type='text' value="${toDo.name}">
        <button class='update-toDo__submit'>Save Changes</button>
        <input class='update-toDo__id' type='hidden' value="${toDo.id}">
    </section>
`;
}
