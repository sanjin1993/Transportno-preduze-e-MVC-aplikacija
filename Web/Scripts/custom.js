$(document).ready(function () {
    $("#datumOd").datepicker();
    $("#datumDo").datepicker();

    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data:$("form").serialize(),
            type: "get"


        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.paged-list").attr("data-otf-target");
            $(target).replaceWith(data);
            
        });

        return false;
    };


  

    $(".row").on("click", ".paged-list a", getPage);

    //Dispozicija
  
    $(".instradacija-input input[type='text']").each(function () {

        $(this).val("");
    });


    function validateInstradacijaInput() {
        var isValid = true;
        $(".instradacija-input input[type='text']").each(function () {
            if (isNan($(this).val())) {
                console.log("proslo");
            }

        });

        return isValid;
    }


});



function updateModel(data) {
    if (data.Url != null) {
        window.location.href = data.Url;
    }

    else{
    $("span[data-valmsg-for]").text("");
    for (var i = 0; i < data.Errors.length;i++){
        console.log(data.Errors[i]);
        $('span[data-valmsg-for="' + data.Errors[i].Name + '"]').text(data.Errors[i].Message);
        
    }
        }
}



