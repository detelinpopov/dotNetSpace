$(document).ready(function() {
    var hdnUserMessageValue = $("[name=hdnUserMessage").val();
    if (hdnUserMessageValue.length > 0) {
        $("#messageDialog").show(2000);
    }

    $("#hideQuizDescriptionSections").click(function() {
        var hideSections = $(this);
        if (hideSections.is(":checked")) {
            $(".quiz-description").hide("slow");
            $(".quiz-description-category").hide("slow");
        } else {
            $(".quiz-description").show("slow");
            $(".quiz-description-category").show("slow");
        }
    });
});