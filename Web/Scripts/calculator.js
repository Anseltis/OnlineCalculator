/**
 * Action after server response
 * @param {string} text - original expression
 * @param {string} result - result or error message
 * @param {element} processing - calculating control
 */
function writeResult(text, result, processing) {

    processing.remove();
    $("#calculated")
        .append($("<p></p>").html("Expression: " + text))
        .append($("<p></p>").html("Result: " + result));
}

/**
 * Send text expression
  */
function calculate() {

    var text = $("#expression").val();
    $("#expression").val("");

    var processing = $("<p></p>").html("> " + text);
    $("#calculating").append(processing);

    $.ajax({
        dataType: "json",
        url: "/Home/Calculate",
        data: {text: text },
        success: function(data) {
            writeResult(text, data.result, processing);
        },
        error: function () {
            writeResult(text, "Server connection error.", processing);
        }
    });
}

/**
 * Initialize button press and enter key press
 */
window.onload = function () {
    $("#calculate").click(calculate);
    $("#expression").keypress(function (e) {
        if (e.which == 13) {
            calculate();
        }
    });
};