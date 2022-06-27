$(function () {
    console.log("Page is ready");

    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();

        if ($("h5").hasClass("victory") || $("h5").hasClass("defeat")) {
            return;
        }
        

        var cellNumber = $(this).val();
        console.log("Button number " + cellNumber + " was clicked");
        doButtonUpdate(cellNumber, false);
    });

    $(document).on("contextmenu", ".game-button", function (event) {
        event.preventDefault();

        if ($("h5").hasClass("victory") || $("h5").hasClass("defeat")) {
            return;
        }

        var cellNumber = $(this).val();
        doButtonUpdate(cellNumber, true);
    });
});

function doButtonUpdate(cellNumber, flag) {
    $.ajax({
        method: "POST",
        url: "/cell/showonecell",
        data: {
            "cellNumber": cellNumber,
            "flag": flag
        },
        success: function (data) {
            $(".button-zone").html(data);
        }
    });
}