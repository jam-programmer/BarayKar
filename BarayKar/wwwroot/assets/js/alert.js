function RemoveModal(title, text, controller, action, id) {
    const modal = document.createElement('div');
    modal.classList.add('modal', 'fade', 'show');
    modal.setAttribute('tabindex', '-1');
    modal.setAttribute('id', 'modalDelete');
    modal.setAttribute('aria-modal', 'true');
    modal.setAttribute('role', 'dialog');
    modal.style.display = 'block';
    modal.id = "RemoveModal";

    const modalDialog = document.createElement('div');
    modalDialog.classList.add('modal-dialog');
    modalDialog.setAttribute('role', 'document');

    const modalContent = document.createElement('div');
    modalContent.classList.add('modal-content');

    const closeButton = document.createElement('a');
    closeButton.setAttribute('href', '#');
    closeButton.classList.add('close');
    closeButton.setAttribute('data-bs-dismiss', 'modal');
    const closeIcon = document.createElement('em');
    closeIcon.classList.add('icon', 'ni', 'ni-cross');
    closeButton.onclick = function () {
        var modalRmove = document.getElementById("RemoveModal");
        var modalbackground = document.getElementById("modalBack");

        if (modalRmove && modalbackground) {
            modalRmove.remove();
            modalbackground.remove();
        }
    };
    closeButton.appendChild(closeIcon);

    const modalBody = document.createElement('div');
    modalBody.classList.add('modal-body', 'modal-body-lg', 'text-center');

    const nkModal = document.createElement('div');
    nkModal.classList.add('nk-modal', 'py-4');

    const modalIcon = document.createElement('em');
    modalIcon.classList.add('nk-modal-icon', 'icon', 'icon-circle', 'icon-circle-xxl', 'ni', 'ni-cross', 'bg-danger');

    const modalTitle = document.createElement('h4');
    modalTitle.classList.add('nk-modal-title');
    modalTitle.textContent = title;

    const modalText = document.createElement('div');
    modalText.classList.add('nk-modal-text', 'mt-n2');
    const textParagraph = document.createElement('p');
    textParagraph.classList.add('text-soft');
    textParagraph.textContent = text;
    modalText.appendChild(textParagraph);

    const buttonList = document.createElement('ul');
    buttonList.classList.add('d-flex', 'justify-content-center', 'gx-4', 'mt-4');

    const deleteButton = document.createElement('button');
    deleteButton.setAttribute('id', 'deleteEvent');
    deleteButton.classList.add('btn', 'btn-success', 'm-1');
    deleteButton.textContent = 'بله، حذف شود';
    deleteButton.onclick = function () {
        var request = {
            id: id
        };
        var url = '/Admin/' + controller + '/' + action;

        rest.postAsync(url, null, request, function (isSuccess, response) {
            if (isSuccess) {




                if (response.isSuccess) {
                    var itemId = 'Item_' + id;
                    var item = document.getElementById(itemId);
                    item.remove();
                } else {
                    Swal.fire({
                        position: "center",
                        icon: "warning",
                        title: response.message[0],
                        showConfirmButton: false,
                        timer: 3500
                    });
                }
            } else {
                Swal.fire({
                    position: "center",
                    icon: "warning",
                    title: "خطای رخ داده است.",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
            var modalRmove = document.getElementById("RemoveModal");
            var modalbackground = document.getElementById("modalBack");
            modalRmove.remove();
            modalbackground.remove();
        });

    };

    const cancelButton = document.createElement('button');
    cancelButton.setAttribute('data-bs-dismiss', 'modal');
    cancelButton.setAttribute('data-bs-toggle', 'modal');
    cancelButton.setAttribute('data-bs-target', '#editEventPopup');
    cancelButton.classList.add('btn', 'btn-danger', 'btn-dim', 'm-1');
    cancelButton.textContent = 'لغو';
    cancelButton.onclick = function () {
        var modalRmove = document.getElementById("RemoveModal");
        var modalbackground = document.getElementById("modalBack");
        modalRmove.remove();
        modalbackground.remove();
    };

    // Add elements to the modal
    modal.appendChild(modalDialog);
    modalDialog.appendChild(modalContent);
    modalContent.appendChild(closeButton);
    modalContent.appendChild(modalBody);
    modalBody.appendChild(nkModal);
    nkModal.appendChild(modalIcon);
    nkModal.appendChild(modalTitle);
    nkModal.appendChild(modalText);
    nkModal.appendChild(buttonList);
    buttonList.appendChild(deleteButton);
    buttonList.appendChild(cancelButton);

    // Create and append modal-backdrop element
    const modalBackdrop = document.createElement('div');
    modalBackdrop.id = "modalBack";
    modalBackdrop.classList.add('modal-backdrop', 'fade', 'show');

    // Append modal-backdrop to the body
    document.body.appendChild(modalBackdrop);
    // Append modal to the body
    document.body.appendChild(modal);
}