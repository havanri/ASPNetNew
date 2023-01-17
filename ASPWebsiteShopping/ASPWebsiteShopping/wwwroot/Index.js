var data = [];
$("#Tags").on("change", (e) => {
    data = [];
    console.log("vasl", $("#Tags").val())
    data.push($("#Tags").val())
    console.log("data", data)
});
$("#FormCreateProduct").submit(function(e) { 
    e.preventDefault();

    let myform = document.getElementById("#FormCreateProduct");
    var Tags = JSON.stringify(data);
  //  var req = new FormData(myform);
    var other_data = $('form').serialize();


   // req.append("Tags", other_data)
   console.log("Tags", other_data)
    $.ajax({
        type: "Post",
        url: "/Product/Create",
        data: other_data,
        urlSrc: "",
        success: function (data) {
            
        },
        error: function (data) {
            console.log("loi");
        },
    });
    
});