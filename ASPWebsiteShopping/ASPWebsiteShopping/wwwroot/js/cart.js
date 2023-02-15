document.querySelector("body").onload = function () { cartLoad() };
var money = document.querySelectorAll(".js-money-format");
for (var i = 0; i <= money.length; i++) {
    var price = money[i].textContent;
    console.log(price);
    money[i].innerHTML = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(parseInt(price));
}
function cartLoad() {
    let carts = JSON.parse(localStorage.getItem("carts"));
    var urlRequest = "https://localhost:7099/home/infCart";
    var cartStorage = document.querySelector(".js-cartStorage");
    cartStorage.replaceChildren();
    $.ajax({
        type: "GET",
        url: urlRequest,
        success: function (data, textStatus, xhr) {
            console.log(xhr.status);
            if (xhr.status == 200) {
                console.log(data);
                var total = 0;
                var quantityIconCart = 0;
                for (var i = 0; i < carts.length; i++) {
                    quantityIconCart += carts[i].quan;

                    console.log('quantity', quantityIconCart);
                    productItem = data.find(x => x.id === carts[i].id);
                    total += carts[i].quan * productItem.price;
                    let newItem = document.createElement("li");
                    newItem.classList.add("header-cart-item","flex-w", "flex-t","m-b-12");
                    newItem.innerHTML = "<div class='header-cart-item-img'><img src='/" + productItem.featureImagePath + "' alt='IMG' ></div><div class='header-cart-item-txt p-t-8'><a href='#' class='header-cart-item-name m-b-18 hov-cl1 trans-04'>" + productItem.name + "</a><span class='header-cart-item-info'>" + carts[i].quan + " x " +new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(productItem.price) + "</span></div>";
                    cartStorage.appendChild(newItem);
                }
                console.log('quantity', quantityIconCart);
                var iconCartHtml = document.querySelector(".js-show-cart");
                iconCartHtml.setAttribute("data-notify", quantityIconCart);
                console.log("thanh cong delete");
                //total modal

                var totalModalHtml = document.querySelector(".js-modal-cart-total");
                totalModalHtml.innerHTML = "Total:  " + new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(total);
            }
        },
        error: function (data) {
            console.log("loi");
        },
    });
}
function addToCart(id) {
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
            editDataProductInCart(quantity);
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
    //show panel
    var panelCart = document.querySelector('.js-panel-cart');
    panelCart.classList.add('show-header-cart');
}
//
function removeOneTimes(id) {
    let carts = JSON.parse(localStorage.getItem("carts"));
    console.log(carts);
    for (let i = 0; i < carts.length; i++) {
        var index;
        if (carts[i].id == id) {        
            index = i;
        }
    }
    carts.splice(index, 1);
    localStorage.setItem("carts", JSON.stringify(carts));
    //pass to cookie
    let myItem = localStorage.getItem("carts");
    redirectCart(myItem);
    //reload
    cartLoad();
    checkoutLoad();
}
function subTotal() {
    var subTotal = 0;
    var listProductInCart = document.getElementsByClassName("single-product-cart");
    // console.log("length"+listProductInCart.length);
    for (var i = 0; i < listProductInCart.length; i++) {
        var price = 0;
        var quantity = 0;
        for (var f = 0; f < listProductInCart[i].childNodes.length; f++) {
            //listProductInCart[i]=<tr>
            if (listProductInCart[i].childNodes[f].className == "cart_price") {
                //console.log("price root "+listProductInCart[i].childNodes[f].childNodes[0].textContent);
                //console.log("tim ra"+listProductInCart[i].childNodes[f].className);
                priceT = listProductInCart[i].childNodes[f].firstChild.textContent.split(" ")[0];
                price = priceT.replace(',', '');
                //price = parseFloat(listProductInCart[i].childNodes[f].firstChild.textContent.split(" ")[0]);
                console.log("gia" + price);
            }
            // console.log("e: "+listProductInCart[i].childNodes[f].className);
            if (
                listProductInCart[i].childNodes[f].className == "cart_quantity"
            ) {
                quantity =
                    listProductInCart[i].childNodes[f].children[0].children[1].value;
            }

            // if(listProductInCart[i].children[f].className == "cart_price"){
            //     console.log("true"+i+f);
            //     priceE=listProductInCart[i].children[f];
            //     price = parseFloat(priceE.firstChild.textContent.split(" ")[0]);
            //     console.log("price"+price);
            // }
        }
        subTotal += price * quantity;
    }
    var subToTalRoot = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(subTotal);
    var btnSubTotal = document.getElementById("cart-sub-total");
    btnSubTotal.innerHTML = subToTalRoot.split('.').join(',');
    var btnTotal = document.getElementById("cart-total");
    btnTotal.innerHTML = subToTalRoot.split('.').join(',');
}
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
function redirectCart(myItem) {
    setCookie("productsOfCart", myItem, 1);
}
