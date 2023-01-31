const button = document.getElementById("editButton");
const pTag = document.getElementById("editablePTag");
const editableFields = document.querySelectorAll('p.editable');

button.addEventListener("click", function () {
    editableFields.forEach(field => {
        field.setAttribute('contentEditable', true);
        field.classList.remove('text-muted');
        field.classList.add('editable', 'form-control', 'border-3');
    });
});



//// Add a click event listener to the button
//document.querySelector('button').addEventListener('click', function () {
//    // Make all fields editable
//    editableFields.forEach(field => {
//        field.setAttribute('contentEditable', true);
//        field.classList.add('border', 'border-primary');
//    });
//});