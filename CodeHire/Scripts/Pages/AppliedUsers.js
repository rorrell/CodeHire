

$(document).ready(function () {
    var table = $("#jobListings").DataTable({
        ajax: {
            url: "/api/appliedusersbyjob/" + window.location.href.split('/').pop(),
            dataSrc: ""
        },
        columns: [
            {
                data: "email",
                render: function (data) {
                    return "<a href='mailto:" + data + "'>" + data + "</a>";
                }
            }
        ]
    });
});