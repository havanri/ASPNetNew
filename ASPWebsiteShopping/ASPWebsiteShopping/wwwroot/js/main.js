$(function () {
    $(document).on("click", ".action_delete", actionDelete);
    // $(document).on('click','.edit-information-order',editOrder);
});
//delete
function actionDelete(event) {
    event.preventDefault();
    let urlRequest = $(this).attr('href');
    console.log(urlRequest);
    let that = $(this);
    Swal.fire({
        title: "Bạn có chắc không?",
        text: "Dữ liệu không thể hoàn tác!!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "GET",
                url: urlRequest,
                timeout: 600000,
                success: function (data, textStatus, xhr) {
                    console.log(xhr.status);
                    if (xhr.status == 200) {
                        console.log("thanh cong delete");
                        console.log(that.parent().parent().parent());
                        that.parent().parent().parent().remove();
                        Swal.fire(
                            "Deleted!",
                            "Product Deleted Success",
                            "successfully"
                        );
                    }
                },
                error: function (data) {
                    console.log("loi");
                },
            });
        }
    });
}
function loadSidebar() {
   
}