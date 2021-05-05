// INITIALIZATION OF EDITABLE TABLE
// =======================================================
$('.js-editable-table tbody tr').editable({
    keyboard: true,
    dblclick: false,
    button: true,
    buttonSelector: '.js-edit',
    maintainWidth: true,
    edit: function (values) {
        $('.js-edit .js-edit-icon', this).removeClass('tio-edit').addClass('tio-save');
        $(this).find('td[data-field] input').addClass('form-control form-control-sm');

    },
    save: function (values) {
        $('.js-edit .js-edit-icon', this).removeClass('tio-save').addClass('tio-edit');

        $.post("Home/UpdateDepartment", {
            model: {
                'DepartmentId': values.index,
                'DepName': values.name,
                'DepLocation': values.location
            }
        }).done(function (data) {
            alert(data + ", Refresh now!");
            window.location = "/";
        }).fail(function () {
            $('#depModal').modal('hide')
            $.alert("Error");
        });
    },
    cancel: function (values) {
        $('.js-edit .js-edit-icon', this).removeClass('tio-save').addClass('tio-edit');
    }
});