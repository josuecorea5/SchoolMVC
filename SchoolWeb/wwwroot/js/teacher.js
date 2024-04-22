$(document).ready(function () {
    $('#example').DataTable({
        "ajax": {
            "url": 'teacher/getall',
            "dataSrc": "data"
        },
        columns: [
            {
                "data": "id",
                "width": "10%"
            },
            {
                "data": function (row) {
                    return { name: row.name, lastName: row.lastName, id: row.id };
                },
                render: function (data) {
                    return `<a href="teacher/detail?id=${data.id}">${data.name} ${data.lastName}</a>`
                },
                "width": "20%"
            },
            { "data": "title", "width": "20%" },
            { "data": "email", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="teacher/edit?id=${data}" class="btn btn-outline-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="teacher/delete?id=${data}" class="btn btn-outline-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
});
