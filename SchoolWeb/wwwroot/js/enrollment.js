$(document).ready(function () {
    $('#example').DataTable({
        "ajax": {
            "url": 'enrollment/getall',
            "dataSrc": "data"
        },
        columns: [
            { "data": "id", "width": "10%" },
            {
                "data": function (row) {
                    return `${row.student.name} ${row.student.lastName}`;
                },
                "width": "25%"
            },
            {
                "data": function (row) {
                    return `${row.grade.name}`
                },
                "width": "17%"
            },
            {
                "data": function (row) {
                    let enumValue = {
                        0: "CURRENTLY_ENROLLED",
                        1: "FAILED",
                        2: "PASSED"
                    }
                    return `${enumValue[row.enrollmentStatus]}`
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="enrollment/edit?id=${data}" class="btn btn-outline-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="enrollment/delete?id=${data}" class="btn btn-outline-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "23%"
            }
        ]
    });
});