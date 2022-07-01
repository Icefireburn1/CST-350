﻿$(function () {
    console.log("Page is ready");

    $(document).on("click", ".save-button", function (event) {
        event.preventDefault(); // Don't redirect to SaveGame URL

        $.ajax({
            method: "POST",
            url: "/saveload/savegame",
            success: function (data) {
                alert(data);
            }
        });
    });

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
            $(".game-board").html(data);
        }
    });
}