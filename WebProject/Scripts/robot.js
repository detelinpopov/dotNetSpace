$(document).ready(function () {
    $('#editor').hide();
    setTimeout(function() {
            toggleSpeechBubbleDisplay("But I want to learn how to code.");
            setTimeout(function() {
                    toggleSpeechBubbleDisplay("Actually I know some JavaScript and jQuery.");
                    setTimeout(function() {
                        toggleSpeechBubbleDisplay("Please help me to update my site!");
                        setTimeout(function () {                              
                            $('#editor').show();
                                var editor = ace.edit("editor");
                                editor.setTheme("ace/theme/monokai");
                                editor.getSession().setMode("ace/mode/javascript");
                            },
                                5000);
                        },
                        5000);
                },
                5000);
        },
        5000);
});

function toggleSpeechBubbleDisplay(text) {
    $(".speech-bubble").hide();
    $(".speech-bubble").text(text);
    $(".speech-bubble").fadeIn(2000);
}