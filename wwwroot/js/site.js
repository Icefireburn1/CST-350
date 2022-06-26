$(function () {
    console.log("Page is ready");

    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();

        if ($("h5").hasClass("victory") || $("h5").hasClass("defeat")) {
            return;
        }
        

        var cellNumber = $(this).val();
        console.log("Button number " + cellNumber + " was clicked");
        doButtonUpdate(cellNumber);
    });
});

function doButtonUpdate(cellNumber) {
    $.ajax({
        method: "POST",
        url: "/cell/showonecell",
        data: {
            "cellNumber": cellNumber
        },
        success: function (data) {
            console.log(data);
            $(".button-zone").html(data);
        }
    });
}