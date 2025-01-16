
// Mở modal thêm mới
function CreateAction(link) {
    $.ajax({
        url: link,
        method: 'get',
        success: function (rs) {
            $('#MasterModal').html(rs)
            $('#MasterModal').modal('show');
        },
        error: function (rs) {
            console.log("Có lỗi xảy ra")
        }
    })
}

function EditAction(link, id) {
    $.ajax({
        url: link,
        method: 'get',
        data: { id: id },
        success: function (rs) {
            $('#MasterModal').html(rs)
            $('#MasterModal').modal('show');
        },
        error: function (rs) {
            console.log("Có lỗi xảy ra")
        }
    })
}

function DeleteAction(id) {
    let strHtml = `
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary">Save changes</button>
            </div>
        </div>
    </div>
    `
    $('#MasterModal').html(strHtml);
    $('#MasterModal').modal('show')

}

// Reload Table
function GetDataTable(link, tableId) {
    $.ajax({
        url: link,
        method: 'post',
        data: { PageIndex: 1, PageSize: 10 },
        success: function (rs) {
            $(`#${tableId}`).html(rs);
        },
        error: function (rs) {

        }
    })
}


// Lấy dữ liệu từ view form rồi gửi sang controller
function SaveData(formId, linkSave) {
    const formData = $(`#${formId}`).serialize();
    $.ajax({
        url: linkSave,
        method: 'post',
        data: formData,
        success: function (rs) {
            if (rs.status) {
                toastr.success(rs.message);
                $('#MasterModal').modal('hide');
            } else {
                toastr.error(rs.message);
            }
        },
        error: function (rs) {
            toastr.error(rs.message);
        }
    })
}


