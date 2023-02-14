document.querySelector("body").onload = function () { cartLoad() };
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
                    newItem.innerHTML = "<div class='header-cart-item-img'><img src='/" + productItem.featureImagePath + "' alt='IMG'></div><div class='header-cart-item-txt p-t-8'><a href='#' class='header-cart-item-name m-b-18 hov-cl1 trans-04'>" + productItem.name + "</a><span class='header-cart-item-info'>" + carts[i].quan + " x " +new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(productItem.price) + "</span></div>";
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
function editDataProductInCart(quantity) {
    
}
//Add product from cart
/*function addProductFromCart(id) {
    //
    let carts = JSON.parse(localStorage.getItem("carts"));
    //product first in cart
    if (carts == null) {
        carts = [
            {
                id: id,
                quan: 1,
            },
        ];
        localStorage.setItem("carts", JSON.stringify(carts));
    } //exist cart
    else {
        let index = null;
        //find product by id
        for (let i = 0; i < carts.length; i++) {
            if (carts[i].id == id) {
                index = i;
            }
        }
        //console.log(index);
        //product first
        if (index == null) {
            let newP = {
                id: id,
                quan: 1,
            };
            carts.push(newP);
        }
        //exist product
        else {
            carts[index].id = id;
            var quantity = (carts[index].quan += 1);
            changeValueQuantity(quantity);
        }

        localStorage.setItem("carts", JSON.stringify(carts));
    }
    //pass to cookie
    let myItem = localStorage.getItem("carts");
    redirectCart(myItem);
    console.log(carts);
}
function changeValueQuantity(quantity) {
    *//*display*//*
    //quantity element
    var doc = document.activeElement.parentElement;
    //tr
    var trParent = doc.parentElement.parentElement;
    //console.log(doc.getAttribute("class"));
    var notes = null;
    var priceElement = null;
    var totalElement = null;
    var price = 0;
    var total = 0;
    for (var i = 0; i < doc.childNodes.length; i++) {
        if (doc.childNodes[i].className == "cart_quantity_input") {
            notes = doc.childNodes[i];
            break;
        }
    }
    for (var i = 0; i < trParent.childNodes.length; i++) {
        //get price product
        if (trParent.childNodes[i].className == "cart_price") {
            priceElement = trParent.childNodes[i];
            console.log("priceElement" + priceElement.nodeName);
            //price
            priceT = priceElement.firstChild.textContent.split(" ")[0];
            price = priceT.replace(',', '');
            //var convert = priceT.replace(',', '');
            // console.log('convert'+convert);
            //console.log(new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(convert));
            //console.log("price" + price);
        }
        if (trParent.childNodes[i].className == "cart_total") {
            totalElement = trParent.childNodes[i];
            total = price * quantity;
            var text = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(total);
            //console.log("total"+totalElement.firstChild.innerHTML);
            totalElement.firstChild.innerHTML = text.split('.').join(',');
            //console.log(new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(total));
            //totalElement.replaceChild(newE,totalElement.firstChild);
            break;
        }
    }
    console.log("quantity" + quantity);
    notes.value = quantity; //get value input quantity
    subTotal();
}
function setCookieProductChooseDelete(id) {
    localStorage.setItem("idPCD", id);
}
//Remove product from cart
function removeProductFromCart(id) {
    let btnMinusProduct = document.querySelector(".cart_quantity_down");
    console.log(btnMinusProduct.nodeName);
    let carts = JSON.parse(localStorage.getItem("carts"));
    for (let i = 0; i < carts.length; i++) {
        if (carts[i].id == id && carts[i].quan > 1) {
            var quantity = (carts[i].quan -= 1);
            changeValueQuantity(quantity);
        }
    }
    localStorage.setItem("carts", JSON.stringify(carts));
    //pass to cookie
    let myItem = localStorage.getItem("carts");
    redirectCart(myItem);
}
function deleteProductInCart() {
    var inputProductsInCart = document.querySelectorAll(".cart_product_id");
    var idChoose = localStorage.getItem("idPCD");
    for (var i = 0; i < inputProductsInCart.length; i++) {
        if (inputProductsInCart[i].value == idChoose) {
            // console.log(inputProductsInCart[i].value);
            inputProductsInCart[i].parentElement.remove();
            var index = -1;
            var carts = JSON.parse(localStorage.getItem("carts"));
            for (var i = 0; i < carts.length; i++) {
                if (carts[i].id == idChoose) {
                    index = i;
                }
            }
            if (index != -1) {
                carts.splice(index, 1);
                localStorage.setItem("carts", JSON.stringify(carts));
                //pass to cookie
                let myItem = localStorage.getItem("carts");
                redirectCart(myItem);
            }
            subTotal();
            break;
        }
    }
}*/
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
