$(document).ready(function () {
    $("#importLecturer").click(function (event) {

        var formData = new FormData();
        formData.append('uploadedFile', $('#loadedFile')[0].files[0]);

        $.ajax({
            url: 'FileModels/AddFile',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                console.log('File Submitted!');
            },
            error: function () {
                console.log("Error in file submission!");
            }
        });

        return false;
    });
});