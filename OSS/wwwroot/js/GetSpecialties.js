$('#FacultyId').change(function () {
    var selectedFacultyId = $("#FacultyId").val();
    var specialtySelect = $('#SpecialtyId');
    specialtySelect.empty();
    if (selectedFacultyId != null && selectedFacultyId != '') {
        $.getJSON('Home/GetSpecialties', { facultyId: selectedFacultyId }, function (specialties) {
            if (specialties != null && !jQuery.isEmptyObject(specialties)) {
                specialtySelect.append($('<option/>', {
                    value: null,
                    text: '--- select specialty ---'
                }));
                
                $.each(specialties, function (index, specialty) {
                    specialtySelect.append($('<option/>', {
                        value: specialty.value
                    }).text(specialty.text));
                });
            };
        });
    }
});