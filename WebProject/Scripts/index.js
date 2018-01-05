$(document).ready(function() {
    var hdnUserMessageValue = $("[name=hdnUserMessage").val();
    if (hdnUserMessageValue.length > 0) {
        $("#messageDialog").show(2000);
    }

    window.sr = ScrollReveal({ reset: true });
    sr.reveal(".quiz-container", { duration: 1000, distance: 1000, mobile: true });
   
    $("#showQuizDescriptionSections").click(function() {
        var hideSections = $(this);
        if (hideSections.is(":checked")) {
            $(".quiz-description").show("slow");
            $(".quiz-description-topic").show("slow");
        } else {
            $(".quiz-description").hide("slow");
            $(".quiz-description-topic").hide("slow");
        }
    });
});