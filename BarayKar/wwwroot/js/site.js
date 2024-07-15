// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function RandomId() {
    var h = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'];
    var k = ['x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', '-', 'x', 'x', 'x', 'x', '-', '4', 'x', 'x', 'x', '-', 'y', 'x', 'x', 'x', '-', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x'];
    var id = '', i = 0, rb = Math.random() * 0xffffffff | 0;
    while (i++ < 36) {
        var c = k[i - 1], r = rb & 0xf, v = c == 'x' ? r : (r & 0x3 | 0x8);
        id += (c == '-' || c == '4') ? c : h[v]; rb = i % 8 == 0 ? Math.random() * 0xffffffff | 0 : rb >> 4
    }
    return id
}
 function SendEmploymentRequest(id) {
   
     rest.postAsync("/Employment/SendRequest", null, id, function (isSuccess, response) {
        if (response.isSuccess) {
            Swal.fire({
                title: "درخواست ارسال شد",
                text: response.message[0],
                icon: "success",
                confirmButtonText: "متوجه شدم"
            });
        } else {
      
            Swal.fire({
                title: "درخواست ارسال نشد",
                text: response.message[0],
                icon: "warning",
                confirmButtonText: "متوجه شدم"
            });
        }
    });
}

function ChangeRequestStatus(id) {
  
    // Create the main elements
    const modal = document.createElement('div');
    modal.classList.add('modal', 'show');
    modal.id = 'ChangeRequestModal';
    modal.tabIndex = '-1';
    modal.role = 'dialog';
    modal.setAttribute('aria-labelledby', 'exampleModalCenterTitle');
    modal.setAttribute('aria-hidden', 'true');
    modal.style.display = "block";

    const modalDialog = document.createElement('div');
    modalDialog.classList.add('modal-dialog', 'modal-dialog-centered');
    modalDialog.role = 'document';

    const modalContent = document.createElement('div');
    modalContent.classList.add('modal-content');
    modalContent.style.backgroundColor = "#8ab6ff";


    // Create the header
    const modalHeader = document.createElement('div');
    modalHeader.classList.add('modal-header');

    const modalTitle = document.createElement('h5');
    modalTitle.classList.add('modal-title');
    modalTitle.id = 'exampleModalLongTitle';
    modalTitle.textContent = 'تغییر وضعیت درخواست';

    
    modalHeader.appendChild(modalTitle);


    // Create the body
    const label1 = document.createElement('label');

    label1.className = "text-dark mt-2";
    label1.innerText = "وضعیت";
    const label2 = document.createElement('label');
    label2.innerText = "توضیحات";
    label2.className = "text-dark mt-2";

    const modalBody = document.createElement('div');
    modalBody.classList.add('modal-body');
    const textArea = document.createElement('textarea');
    textArea.setAttribute('rows', '4');
    textArea.classList.add('form-control');
  
    textArea.id = "comment";
    // Create the dropdown list
    const selectList = document.createElement('select');
    selectList.classList.add('form-control', 'mt-2');
    selectList.id = "status";

   

     const optionElement1 = document.createElement('option');
     optionElement1.value = "0";
     optionElement1.textContent = "پذیرش";
     selectList.appendChild(optionElement1);


    const optionElement2 = document.createElement('option');
    optionElement2.value = "1";
    optionElement2.textContent = "عدم پذیرش";
    selectList.appendChild(optionElement2);


    const optionElement3 = document.createElement('option');
    optionElement3.value = "2";
    optionElement3.textContent = "در انتظار";
    selectList.appendChild(optionElement3);






    modalBody.appendChild(label1);
    modalBody.appendChild(selectList);
    modalBody.appendChild(label2);
    modalBody.appendChild(textArea);
    // Create the footer
    const modalFooter = document.createElement('div');
    modalFooter.classList.add('modal-footer');

    const closeButtonModal = document.createElement('button');
    closeButtonModal.type = 'button';
    closeButtonModal.classList.add('btn', 'btn-secondary');
    closeButtonModal.setAttribute('data-dismiss', 'modal');
    closeButtonModal.textContent = 'بستن';
    closeButtonModal.onclick = function () {
        var modal = document.getElementById("ChangeRequestModal");
        modal.remove();
    };
    const saveButton = document.createElement('button');
    saveButton.type = 'button';
    saveButton.classList.add('btn', 'btn-primary');
    saveButton.textContent = 'ثبت';
    saveButton.onclick = function () {
        var comment = document.getElementById("comment");
        var status = document.getElementById("status");


        var body = {
            Status: status.value,
            Comment: comment.value,
            Id: id
        }
        rest.postAsync("/User/Dashboard/ChangeRequestStatus", null, body, function (isSuccess, response) {
            if (isSuccess) {
                let TdId = "Status_" + id;
                var statusTd = document.getElementById(TdId); statusTd.innerHTML = "";
                switch (status.value) {
                    case "0":
                        statusTd.innerHTML = '<span class="text-success">قبول شده</span>';
                        break;
                    case "1":
                        statusTd.innerHTML = '<span class="text-danger">رد شده</span>';
                        break;
                    case "2":
                        statusTd.innerHTML = ' <span class="text-warning">در انتظار</span>';
                        break;
                }
                var modal = document.getElementById("ChangeRequestModal");
                modal.remove();
            }
        });
        //rest.postAsync("/User/Dashboard/ChangeRequestStatus", null, body, function (isSuccess, response) {
        //    if (response.isSuccess) {
        //        let TdId = "Status_" + id;
        //        var statusTd = document.getElementById(TdId); statusTd.innerHTML = "";
        //        switch (status.value) {
        //            case "0":
        //                statusTd.innerHTML = '<span class="text-success">قبول شده</span>';
        //                break;
        //            case "1":
        //                statusTd.innerHTML = '<span class="text-danger">رد شده</span>';
        //                break;
        //            case "2":
        //                statusTd.innerHTML = ' <span class="text-warning">در انتظار</span>';
        //                break;
        //        }
        //        var modal = document.getElementById("ChangeRequestModal");
        //        modal.remove();
        //    }
        //}); 

    };
    modalFooter.appendChild(closeButtonModal);
    modalFooter.appendChild(saveButton);

    // Append all elements to build the modal
    modalContent.appendChild(modalHeader);
    modalContent.appendChild(modalBody);
    modalContent.appendChild(modalFooter);

    modalDialog.appendChild(modalContent);
    modal.appendChild(modalDialog);

    // Append the modal to the body
    document.body.appendChild(modal);
}

