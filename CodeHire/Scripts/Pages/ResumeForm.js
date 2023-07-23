$(document).ready(function () {
    var table = $("#workHistory").DataTable({
        ajax: {
            url: "/api/jobhistories",
            dataSrc: ""
        },
        columns: [
            {
                data: "jobTitle",
                render: function (data, type, jobHistory) {
                    return "<a href='/JobHistories/Edit/" + jobHistory.id + "'>" + data + "</a>";
                }
            },
            {
                data: "company"
            },
            {
                data: "location"
            },
            {
                data: "startDateString"
            },
            {
                data: "endDateString"
            },
            {
                data: "description"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button data-jobhistory-id=" + data + " class='btn-link js-delete'>Delete</button>";
                }
            }
        ]
    });

    $("#workHistory").on("click",
        ".js-delete",
        function () {
            var button = $(this);
            $.ajax({
                url: "/api/jobhistories/" + button.attr("data-jobhistory-id"),
                method: "DELETE",
                success: function () {
                    table.row(button.parents("tr")).remove().draw();
                }
            });
        });

    var table2 = $("#skills").DataTable({
        ajax: {
            url: "/api/resume/skills/" + $('#resumeId').val(),
            dataSrc: ""
        },
        columns: [
            {
                data: "name"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button data-skill-id=" + data + " class='btn-link js-delete'>Delete</button>";
                }
            }
        ]
    });

    $("#skills").on("click",
        ".js-delete",
        function () {
            var button = $(this);
            $.ajax({
                url: "/api/resume/skills/" + button.attr("data-skill-id") + "/" + $('#resumeId').val(),
                method: "DELETE",
                success: function () {
                    table2.row(button.parents("tr")).remove().draw();
                }
            });
        });

});