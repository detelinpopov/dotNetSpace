$(document).ready(function() {
    var hdnUserMessageValue = $("[name=hdnUserMessage").val();
    if (hdnUserMessageValue.length > 0) {
        $("#messageDialog").show(2000);
    }

    window.sr = ScrollReveal({ reset: true });
    sr.reveal(".quiz-container", { duration: 800, mobile: true, rotate: { x: 5, y: 5, z: 5 } });
   
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

    $("#searchQuizText").on('change', function () {        
        var element = $(".quiz-container[name*='" + $(this).val().toLowerCase() + "']");
        if (element != null && element.length > 0) {
            $("#searchByKeywordResult").hide("slow");
            $("html, body").animate({ scrollTop: element.offset().top - ($(window).height() / 2) }, 1000);
            $(this).val("");
        } else {
            $("#searchByKeywordResult").show("slow");
        }
    });

    $(".footer").click(function() {
        $("html, body").animate({ scrollTop: 0 }, 2000);
    });
});