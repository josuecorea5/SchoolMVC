$(document).ready(function () {
    $('#example').DataTable({
        "ajax": {
            "url": 'subject/getall',
            "dataSrc": "data"
        },
        columns: [
            {
                "data": "id",
                "width": "10%"
            },
            {
                "data": function (row) {
                    return { code: row.code, id: row.id };
                },
                render: function (data) {
                    return `<a href="subject/detail?id=${data.id}">${data.code}</a>`
                },
                "width": "20%"
            },
            { "data": "name", "width": "20%" },
            {
                "data": function (row) {
                    return `${row.teacher.name} ${row.teacher.lastName}`;
                },
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="subject/edit?id=${data}" class="btn btn-outline-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="subject/delete?id=${data}" class="btn btn-outline-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
});
