$(document).ready(function () {
    $('#example').DataTable({
        "ajax": {
            "url": 'student/getall',
            "dataSrc": "data"
        },
        columns: [
            {
                "data": function (row) {
                    return { id: row.id, code: row.code }
                },
                render: function (data) {
                    return `<a href="student/detail?id=${data.id}">${data.code}</a>`
                },
                "width": "25%"
            },
            {
                "data": function (row) {
                    return `${row.name} ${row.lastName}`;
                },
                "width": "10%"
            },
            { "data": "age", "width": "5%" },
            {
                "data": function (row) {
                    return `${row.dateOfBirth.split('T')[0]}`
                },

                "width": "10%"
            },
            { "data": "email", "width": "10%" },
            { "data": "phone", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="student/edit?id=${data}" class="btn btn-outline-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="student/delete?id=${data}" class="btn btn-outline-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "23%"
            }
        ]
    });
});
