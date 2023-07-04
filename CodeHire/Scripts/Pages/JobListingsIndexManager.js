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
                    return "<a href='/JobListings/Edit/" + jobListing.id + "'>" + data + "</a>";
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
                    return "<a href='/JobListings/AppliedUsers/" + data + "' class='btn btn-primary'>View Applications</a>";
                }
            },
            {
                data: "id",
                render: function(data) {
                    return "<button data-joblisting-id=" + data + " class='btn-link js-delete'>Delete</button>";
                }
            }
        ]
    });

    $("#jobListings").on("click",
        ".js-delete",
        function() {
            var button = $(this);
            bootbox.confirm("Are you sure you want to delete this customer?",
                function(result) {
                    if (result) {
                        $.ajax({
                            url: "api/joblistings/" + button.attr("data-joblisting-id"),
                            method: "DELETE",
                            success: function() {
                                table.row(button.parents("tr")).remove().draw();
                            }
                        });
                    }
                });
        });
});