window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Операция произошла успешно', { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, 'Операция не удалась', { timeOut: 5000 });
    }
}

window.ShowSweetAlert = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Уведомление об успехе!',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Уведомление об ошибке!',
            message,
            'error'
        )
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}