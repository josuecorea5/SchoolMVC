var dataTable;
$(document).ready(function () {
    dataTable = $('#example').DataTable({
        "ajax": {
            "url": 'studyPlan/getall',
            "dataSrc": "data"
        },
        columns: [
            { "data": "id", "width": "10%" },
            {
                "data": function (row) {
                    return `${row.grade.name} ${row.grade.description}`;
                },
                "width": "25%"
            },
            {
                "data": function (row) {
                    return {id: row.id, subjects: row.subjects}
                },
                "render": function (data) {
                    console.log(data)
                    return `
                       <div class="accordion accordion-flush" id="accordionExample">
                            <div class="accordion-item">
                                <h2 class="accordion-header" id="heading-${data.id}">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse-${data.id}" aria-expanded="true" 
                                    aria-controls="collapse-${data.id}">
                                        Subjects
                                    </button>
                                </h2>
                                <ul id="collapse-${data.id}" class="accordion-collapse collapse py-2 m-0" aria-labelledby="heading-${data.id}" data-bs-parent="#accordionExample">
                                    ${data.subjects.length ?  data.subjects.map(subject => `
                                            <li class="">
                                                ${subject.subject.name}
                                            </li>
                                    `).join("") : "<li>No subjects</li>"}
                                </ul>
                            </div>
                        </div>
                    `;
                },
                "width": "25%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="studyPlan/edit?id=${data}" class="btn btn-outline-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete("studyPlan/delete?id=${data}") class="btn btn-outline-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "23%"
            }
        ]
    });
});


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    Swal.fire({
                        title: "Done!",
                        text: data.message,
                        icon: "success"
                    });
                },
                error: function (data) {
                    Swal.fire({
                        title: "Error!",
                        text: data.message,
                        icon: "error"
                    });
                }
            })
        }
    });
}