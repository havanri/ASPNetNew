checkoutLoad();
function checkoutLoad() {
    let carts = JSON.parse(localStorage.getItem("carts"));
    var urlRequest = "https://localhost:7099/home/infCart";
    var checkoutStorage = document.querySelector(".table-shopping-cart");
    checkoutStorage.replaceChildren();
    //add header for table
    let newItemHeader = document.createElement("tr");
    newItemHeader.classList.add("table_head");
    newItemHeader.innerHTML = "<th class='column-1'>Ảnh</th><th class='column-2'>Sản Phẩm</th><th class='column-3'>Giá</th><th class='column-4'>Số Lượng</th><th class='column-5'>Thành Tiền</th>";
    checkoutStorage.appendChild(newItemHeader);
    $.ajax({
        type: "GET",
        url: urlRequest,
        success: function (data, textStatus, xhr) {
            console.log(xhr.status);
            if (xhr.status == 200) {
                console.log(data);
                var subTotal = 0;
                for (var i = 0; i < carts.length; i++) {
                    productItem = data.find(x => x.id === carts[i].id);
                    var priceRoot = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(productItem.price);
                    subTotal += productItem.price * carts[i].quan;
                    let newItem = document.createElement("tr");
                    newItem.classList.add("table_row");
                    newItem.innerHTML = "<td class='column-1'><div class='how-itemcart1' onclick='removeOneTimes(" + productItem.id +")' ><img src='" + productItem.featureImagePath + "' alt='IMG'></div></td><td class='column-2'>" + productItem.name + "</td><td class='column-3'>" + priceRoot + "</td><td class='column-4'><div class='wrap-num-product flex-w m-l-auto m-r-0'><div class='btn-num-product-down cl8 hov-btn3 trans-04 flex-c-m' onclick='removeToCartFromCheckout(" + productItem.id + ")'><i class='fs-16 zmdi zmdi-minus'></i></div><input class='mtext-104 cl3 txt-center num-product' type='number' name='num-product1' value='" + carts[i].quan + "'><div class='btn-num-product-up cl8 hov-btn3 trans-04 flex-c-m' onclick='addToCartFromCheckout(" + productItem.id + ")'><i class='fs-16 zmdi zmdi-plus'></i></div></div></td><td class='column-5'>" + new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format((productItem.price * carts[i].quan)) + "</td>";
                    checkoutStorage.appendChild(newItem);
                }
                //subtotal and total
                var subToTalRoot = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(subTotal);
                console.log(subTotal);
                var subTotalHtml = document.querySelector(".js-sub-total");
                subTotalHtml.innerHTML = subToTalRoot;

                var totalHtml = document.querySelector(".js-total");
                totalHtml.innerHTML = subToTalRoot;
                console.log("thanh cong");
            }
        },
        error: function (data) {
            console.log("loi");
        },
    });
}
function addToCartFromCheckout(id) {
    let carts = JSON.parse(localStorage.getItem("carts"));
    if (carts == null) {
        carts = [
            {
                id: id,
                quan: 1,
            },
        ];
        localStorage.setItem("carts", JSON.stringify(carts));
    } else {
        let index = null;
        for (let i = 0; i < carts.length; i++) {
            if (carts[i].id == id) {
                index = i;
            }
        }
        console.log(index);
        if (index == null) {
            let newP = {
                id: id,
                quan: 1,
            };
            carts.push(newP);
        } else {
            carts[index].id = id;
            var quantity = (carts[index].quan += 1);
        }
        localStorage.setItem("carts", JSON.stringify(carts));
        console.log()
    }
    //pass to cookie
    let myItem = localStorage.getItem("carts");
    //
    console.log(carts);
    redirectCart(myItem);
    //reload cart
    cartLoad();
    checkoutLoad();
    //show panel
}
function removeToCartFromCheckout(id) {
    let carts = JSON.parse(localStorage.getItem("carts"));
    for (let i = 0; i < carts.length; i++) {
        if (carts[i].id == id && carts[i].quan > 1) {
            var quantity = (carts[i].quan -= 1);
        }
    }
    localStorage.setItem("carts", JSON.stringify(carts));
    //pass to cookie
    let myItem = localStorage.getItem("carts");
    redirectCart(myItem);
    //reload
    cartLoad();
    checkoutLoad();
}
$(function () {
    $(document).on("click", "#js-submit-checkout", alertSuccessfulCheckout);
});
function alertSuccessfulCheckout() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Thanh toán thành công !!!',
        showConfirmButton: false,
        timer: 1500
    });
}