$(document).ready(function () {
    $.ajax({
        url: "/home/enrollmentsInformation",
        type: 'GET',
        success: function (response) {
            GenerateGraphic(response.data);
        }
    })
})

function GenerateGraphic(data) {
    Highcharts.chart('enrollmentDasboard', {
        colors: ['#2c3e50'],
        chart: {
            type: 'column'
        },
        title: {
            text: 'Total Enrollments by grade',
            style: {
                "color": "#0a4b3e"
            },
            align: 'center'
        },
        xAxis: {
            categories: data.map(enrollment => enrollment.gradeName),
            crosshair: true,
            accessibility: {
                description: 'Grades'
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Students by grade'
            }
        },
        tooltip: {
            valueSuffix: ' (Total)'
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [
            {
                name: 'Students',
                data: data.map(enrollment => enrollment.totalEnrollment),
            }
        ]
    });
}