$(function () {
    $(document).on("click", ".action_delete", actionDelete);
    // $(document).on('click','.edit-information-order',editOrder);
    $('.js-attribute-select').on('change', updateInformationSpeciesOfAttribute);
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
   
};
function updateInformationSpeciesOfAttribute(e) {
    var valueSelected = this.value;
    
    if (valueSelected != 0) {
        /*var optionSelected = $('.js-attribute-select').find(":selected");*/
        var optionSelected = this.options[this.selectedIndex];
        console.log(this.options[0]);

        optionSelected.setAttribute("disabled", "");
        nameSelected = this.options[this.selectedIndex].innerHTML;
        /*alert(valueSelected);*/

        //ajax get data species[] of attribute
        var urlRequest = "https://localhost:7099/Product/OptionSpecies/" + this.value;
        $.ajax({
            type: "GET",
            url: urlRequest,
            success: function (data, textStatus, xhr) {
                console.log(xhr.status);
                if (xhr.status == 200) {
                    console.log(data);
                    console.log("thanh cong delete");
                    //add element
                    let itemReplace = document.querySelector(".attribute-species");
                    let newItem = document.createElement("div");
                    newItem.classList.add("species");
                    newItem.innerHTML = '<label>Tên:' + nameSelected + '</label><br/><label> Giá trị(s):</label ><div class="select2-purple" ><select name="attribute_values[]" class="js-species-select" multiple="multiple" data-placeholder="Chọn tên chủng loại" data-dropdown-css-class="select2-purple" style="width: 100%;">' + data + '</select></div> ';
                    itemReplace.appendChild(newItem);


                    reload_js('/js/customize-form.js');
                }
            },
            error: function (data) {
                console.log("loi");
            },
        });
    }
}
function reload_js(src) {
    $('script[src="'+src+'"]').remove();
    $('<script>').attr('src', src).appendTo('head');
}


