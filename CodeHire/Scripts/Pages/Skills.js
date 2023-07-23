
$(document).ready(function () {
    var vm = {
        skillIds: [],
        resumeId: $('#Id').val()
    };

    var skills = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/skills?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#skill').typeahead({
        minLength: 2,
        highlight: true
    }, {
        name: 'skills',
        display: 'name',
        source: skills
    }).on("typeahead:select", function (e, skill) {
        $("#skills").append("<li class='list-group-item'>" + skill.name + "</li>");

        $("#skills").typeahead("val", "");

        vm.skillIds.push(skill.id);
        $("#skill").typeahead("val", "");
    });

    $("#addSkills").on("submit", function (event) {
        event.preventDefault();
        $.ajax({
            url: "/api/resume/skills",
            method: "post",
            data: vm,
            success: function () {
                toastr.success("Skills successfully recorded.");

                $("#skill").typeahead("val", "'");
                $("#skills").empty();

                vm = { skillIds: [] };

                window.location.href = '/Resume/ResumeForm';
            },
            error: function () {
                toastr.error("Something unexpected happened.");
            }
        });
    });
});
