//function createModalRequestEmployment() {
//    // Create modal div
//    var modalDiv = document.createElement('div');
//    modalDiv.classList.add('modal', 'fade');
//    modalDiv.id = 'exampleModalCenter';
//    modalDiv.tabIndex = '-1';
//    modalDiv.role = 'dialog';
//    modalDiv.setAttribute('aria-labelledby', 'exampleModalCenterTitle');
//    modalDiv.setAttribute('aria-hidden', 'true');

//    // Create modal dialog div
//    var modalDialogDiv = document.createElement('div');
//    modalDialogDiv.classList.add('modal-dialog', 'modal-dialog-centered');
//    modalDialogDiv.role = 'document';

//    // Create modal content div
//    var modalContentDiv = document.createElement('div');
//    modalContentDiv.classList.add('modal-content');

//    // Create modal header
//    var modalHeaderDiv = document.createElement('div');
//    modalHeaderDiv.classList.add('modal-header');

//    var modalTitle = document.createElement('h5');
//    modalTitle.classList.add('modal-title');
//    modalTitle.id = 'exampleModalLongTitle';
//    modalTitle.textContent = 'ارسال درخواست همکاری';

//    var closeButton = document.createElement('button');
//    closeButton.type = 'button';
//    closeButton.classList.add('close');
//    closeButton.setAttribute('data-dismiss', 'modal');
//    closeButton.setAttribute('aria-label', 'Close');

//    var closeSpan = document.createElement('span');
//    closeSpan.setAttribute('aria-hidden', 'true');
//    closeSpan.textContent = '×';

//    closeButton.appendChild(closeSpan);
//    modalHeaderDiv.appendChild(modalTitle);
//    modalHeaderDiv.appendChild(closeButton);

//    // Create modal body
//    var modalBodyDiv = document.createElement('div');
//    modalBodyDiv.classList.add('modal-body');

//    var textArea = document.createElement("textarea");
//    textArea.className = "form-control";
//    textArea.id = "description";
//    modalBodyDiv.appendChild(textArea);
//    // Create modal footer
//    var modalFooterDiv = document.createElement('div');
//    modalFooterDiv.classList.add('modal-footer');

//    var closeButtonFooter = document.createElement('button');
//    closeButtonFooter.type = 'button';
//    closeButtonFooter.classList.add('btn', 'btn-secondary');
//    closeButtonFooter.setAttribute('data-dismiss', 'modal');
//    closeButtonFooter.textContent = 'Close';

//    var saveChangesButton = document.createElement('button');
//    saveChangesButton.type = 'button';
//    saveChangesButton.classList.add('btn', 'btn-primary');
//    saveChangesButton.textContent = 'Save changes';

//    modalFooterDiv.appendChild(closeButtonFooter);
//    modalFooterDiv.appendChild(saveChangesButton);

//    modalContentDiv.appendChild(modalHeaderDiv);
//    modalContentDiv.appendChild(modalBodyDiv);
//    modalContentDiv.appendChild(modalFooterDiv);

//    modalDialogDiv.appendChild(modalContentDiv);

//    modalDiv.appendChild(modalDialogDiv);

//    // Append modal to the body
//    document.body.appendChild(modalDiv);
//}
