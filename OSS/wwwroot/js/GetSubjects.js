$('#LecturerId').focusout(function () {
    var selectedLecturerId = $("#LecturerId").val();
    var subjectSelect = $('#SubjectId');
    subjectSelect.empty();
    if (selectedLecturerId != null && selectedLecturerId != '') {
        $.getJSON('Home/GetSubjects', { lecturerId: selectedLecturerId }, function (subjects) {
            if (subjects != null && !jQuery.isEmptyObject(subjects)) {
                subjectSelect.append($('<option/>', {
                    value: null,
                    text: '--- select subject ---'
                }));

                $.each(subjects, function (index, subject) {
                    subjectSelect.append($('<option/>', {
                        value: subject.value
                    }).text(subject.text));
                });
            };
        });
    }
});