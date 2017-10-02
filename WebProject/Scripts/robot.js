$(document).ready(function() {
    $("#editor").hide();
    setTimeout(function() {
            toggleSpeechBubbleDisplay("But I want to learn how to code.");
            setTimeout(function() {
                    toggleSpeechBubbleDisplay("Actually I know some JavaScript and jQuery.");
                    setTimeout(function() {
                            toggleSpeechBubbleDisplay("Please help me to update my site!");
                            setTimeout(function() {
                                    $("#editor").show();
                                    var editor = ace.edit("editor");
                                    editor.setTheme("ace/theme/monokai");
                                    editor.getSession().setMode("ace/mode/javascript");
                                    var code = editor.getValue();                                  
                                    eval(code);
                                },
                                1000);
                        },
                        1000);
                },
                1000);
        },
        1000);

    $('#btnExecute').click(function () {
        var editor = ace.edit("editor");
        var code = editor.getValue();       
        eval(code);
    });
});

function toggleSpeechBubbleDisplay(text) {
    $(".speech-bubble").hide();
    $(".speech-bubble").text(text);
    $(".speech-bubble").fadeIn(2000);
}