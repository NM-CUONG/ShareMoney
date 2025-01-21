
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

// Mở modal cập nhật
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

// Mở modal xóa
function DeleteAction(link) {
    let strHtml = `
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">XÁC NHẬN XÓA!</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <h4>Bạn có chắc chắn muốn xóa?</h4>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" onclick="HandleDelete('${link}')">Xóa</button>
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Đóng</button>
            </div>
        </div>
    </div>
    `
    $('#MasterModal').html(strHtml);
    $('#MasterModal').modal('show')

}

// Hàm xóa
function HandleDelete(link) {
    $.ajax({
        url: link,
        method: 'get',
        success: function (rs) {
            if (rs.status) {
                toastr.success(rs.message);
                $('#MasterModal').modal('hide');
                Refresh(link, "viewData");
            } else {
                toastr.error(rs.message);
            }
        },
        error: function (rs) {
            toastr.error(rs.message);
        }
    })
}


// Làm mới danh sách
function Refresh(link, tableId) {
    let linkRefresh = link?.replace(/Create|Edit|Delete/, "Refresh");
    $.ajax({
        url: linkRefresh,
        method: 'get',
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
                Refresh(linkSave, "viewData");
            } else {
                toastr.error(rs.message);
            }
        },
        error: function (rs) {
            toastr.error(rs.message);
        }
    })
}


