$(document).ready(function () {
    var table = $("#jobListings").DataTable({
        ajax: {
            url: "/api/joblistings",
            dataSrc: ""
        },
        columns: [
            {
                data: "title",
                render: function (data, type, jobListing) {
                    return "<a href='/JobListings/Details/" + jobListing.id + "'>" + data + "</a>";
                }
            },
            {
                data: "company"
            },
            {
                data: "location"
            },
            {
                data: "languages",
                render: function (data, type, full) {
                    return $.map(data, function (d, i) {
                        return d.name;
                    }).join(', ');
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<a href='#' class='btn btn-primary'>Apply</a>";
                }
            }
        ]
    });

});