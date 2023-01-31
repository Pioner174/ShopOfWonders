const buttonEdit = document.getElementById("editButton");
const buttonSave = document.getElementById("saveButton");
const pTag = document.getElementById("editablePTag");
const editableFields = document.querySelectorAll('p.editable');

buttonEdit.addEventListener("click", function () {
    editableFields.forEach(field => {
        field.setAttribute('contentEditable', true);
        field.classList.remove('text-muted');
        field.classList.add('editable', 'form-control', 'border-1');
    });
    document.getElementById("saveButton").classList.replace('disabled','active');
});

buttonSave.addEventListener("click", function () {
    editableFields.forEach(field => {
        var s = field.nextSibling;
        s.setAttribute('value', field.textContent)
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