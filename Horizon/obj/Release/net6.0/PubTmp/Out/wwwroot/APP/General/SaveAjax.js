const SendAjaxCall = (SentData, SaveURL) => {
    $(".se-pre-con").show();
    return new Promise((resolve, reject) => {
        $.ajax({
            url: SaveURL,
            type: "POST",
            data: ko.toJSON(SentData),
            contentType: "application/json",
            success: function (data) {
                $(".se-pre-con").fadeOut("slow");
                resolve(data);
            },
            error: function (jqXhr, textStatus, errorMessage) {
                $(".se-pre-con").fadeOut("slow");
                if (jqXhr.status === 401)
                    $("#JsError").text(" برجاء اعادة الدخول مرة أخري أو مراجعة الصلاحيات الخاصة بك");
                else
                    $("#JsError").text(jqXhr.status + " " + jqXhr.responseText.substring(0, 50) + ".....");
                $('#message').modal('show');
            }
        });

    });
}

const SendAjaxCallWithFiles = (SentData, SaveURL) => {
    $(".se-pre-con").show();
    return new Promise((resolve, reject) => {
        $.ajax({
            url: SaveURL,
            type: "POST",
            data: SentData,
            contentType: false,
            processData: false,
            success: function (data) {
                $(".se-pre-con").fadeOut("slow");
                resolve(data);
            }
        });

    })

}
 

const SendAjaxWithFetch = async (SentData, SaveURL) => {
    const response =await fetch(SaveURL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(SentData)
    });
    if (response.status === 200) {
        return response.json();
    }
    else {
        throw Error("Unable to get Data")
    }
}






