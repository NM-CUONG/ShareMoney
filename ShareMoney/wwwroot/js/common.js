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

