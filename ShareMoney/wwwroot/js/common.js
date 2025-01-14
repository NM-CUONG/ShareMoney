
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
function SaveData(formId, linkSave, tableId, linkRefresh) {
    const formData = $(`#${formId}`).serialize();
    $.ajax({
        url: linkSave,
        method: 'post',
        data: formData,
        success: function (rs) {
            if (rs.status) {
                toastr.success(rs.message);
                // refresh table sau khi lưu thành công
                GetDataTable(linkRefresh, tableId);
            } else {
                toastr.error(rs.message);
            }
        },
        error: function (rs) {
                toastr.error(rs.message);
        }
    })
}


