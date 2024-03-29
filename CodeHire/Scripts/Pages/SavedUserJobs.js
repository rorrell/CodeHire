﻿

$(document).ready(function () {
    var table = $("#jobListings").DataTable({
        ajax: {
            url: "/api/savedjobs",
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
                    return "<a href='/JobListings/ApplyToSaved/" + data + "' class='btn btn-primary'> Apply</a>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<a data-joblisting-id=" + data + " class='btn btn-primary js-delete'>Delete</a>";
                }
            }
        ]
    });

    $("#jobListings").on("click",
        ".js-delete",
        function () {
            var button = $(this);
            $.ajax({
                url: "/api/savedjobs/" + button.attr("data-joblisting-id"),
                method: "DELETE",
                success: function () {
                    table.row(button.parents("tr")).remove().draw();
                }
            });
        });
});