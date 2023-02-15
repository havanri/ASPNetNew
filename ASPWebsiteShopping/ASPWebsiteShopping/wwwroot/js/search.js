var input = document.querySelector(".js-search");
var products = document.querySelector(".js-all-product");
// Execute a function when the user presses a key on the keyboard
input.addEventListener("keypress", function (event) {
    // If the user presses the "Enter" key on the keyboard
    if (event.key === "Enter") {
        // Cancel the default action, if needed
        event.preventDefault();
        var key_word = input.value;
        console.log(key_word);
        var urlRequest = "https://localhost:7099/search?key=" + key_word;
       window.location.href = urlRequest;
       /* $.ajax({
            type: "GET",
            url: urlRequest,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, xhr) {
                console.log(xhr.status);
                if (xhr.status == 200) {
                    products.innerHTML="";
                    for (var i = 0; i < data.length; i++) {
                        console.log(data[i]);
                        let newItem = document.createElement("div");
                        newItem.classList.add("col-sm-6", "col-md-4", "col-lg-3", "p-b-35", "isotope-item", "women");
                        newItem.innerHTML = "<div class='block2'><div class='block2-pic hov-img0' ><img src='" + data[i].featureImagePath + "' alt='IMG-PRODUCT'><a href='#' class='block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1' style='min-width: 180px!important;' onclick='addToCart("+data[i].price+")'>THÊM VÀO GIỎ HÀNG </a></div><div class='block2-txt flex-w flex-t p-t- 14'><div class='block2-txt-child1 flex-col-l'><a asp-controller='pro' asp-action='"+data[i].slug+"' class='stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6' >"+data[i].name+"</a><b class='stext-105 cl3 js-money-format'>"+data[i].price+"</b> </div></div><div class='block2-txt-child2 flex-r p-t-3'><a href='#' class='btn-addwish-b2 dis-block pos-relative js-addwish-b2'><img class='icon-heart1 dis-block trans-04' src='lib/cozastore/images/icons/icon-heart-01.png' alt='ICON'><img class='icon-heart2 dis-block trans-04 ab-t-l' src='lib/cozastore/images/icons/icon-heart-02.png' alt='ICON'></a></div></div>";
                        products.appendChild(newItem);
                    }
                    console.log("thanh cong");
                }
            },
            error: function (data) {
                console.log("loi");
            },
        });*/
        // Trigger the button element with a click
        
    }
});
