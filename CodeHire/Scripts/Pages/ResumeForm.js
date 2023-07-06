$(document).ready(function () {
    var table = $("#workHistory").DataTable({
        ajax: {
            url: "/api/joblistings",
            dataSrc: ""
        },
        columns: [
            {
                data: "jobTitle"
            },
            {
                data: "company"
            },
            {
                data: "location"
            },
            { data: "endDate", visible: false, targets: [3] },
            {
                data: "startDate",
                render: (data, type, row) => data + ' - ' + row[3],
                targets: 0
            }
        ]
    });

});