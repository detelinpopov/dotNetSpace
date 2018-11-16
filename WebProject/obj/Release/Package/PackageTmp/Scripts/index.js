$(document).ready(function() {   
    window.sr = ScrollReveal({ reset: true });
    sr.reveal(".scroll-reveal", { mobile: true });

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

    $("#searchQuizText").on("change", findQuizByKeyword);

    $("#searchQuizText").on("click", findQuizByKeyword);

    function findQuizByKeyword() {
        var keywordInput = $("#searchQuizText");
        var element = $(".quiz-container[name*='" + keywordInput.val().trim().toLowerCase() + "']");
        if (element == null || element.length === 0) {
            element = $(".quiz-container-last[name*='" + keywordInput.val().trim().toLowerCase() + "']");
        }
        if (element != null && element.length > 0) {
            $("#searchByKeywordResult").hide("slow");
            $("html, body").animate({ scrollTop: element.offset().top - ($(window).height() / 2) }, 1000);
            $(this).val("");
        } else if (keywordInput.val() !== "" &&
            keywordInput.val() !== null &&
            keywordInput.val().trim().toLowerCase() !== "c#") {
            $("#searchByKeywordResult").show("slow");
        } else {
            $("#searchByKeywordResult").hide("slow");
        }
    }

    $(".footer").click(function() {
        $("html, body").animate({ scrollTop: 0 }, 2000);
    });

    $(function() {
        var availableQuizzes = [
            "ASP.Net Mvc",
            "JavaScript",
            "jQuery",
            "Unit Testing",
            "Sql Server",
            "SharePoint",
            "Data Structures",
            "Entity Framework",
            "Scrum"
        ];
        $("#searchQuizText").autocomplete({
            open: function (event, ui) {
                $('.ui-autocomplete').off("menufocus hover mouseover mouseenter");
            },
            source: availableQuizzes
        });
    });

    $("label").click(function () {
        var el = $(this).children('span:first-child');
        el.addClass("circle");
        var newone = el.clone(true);
        el.before(newone);
        $("." + el.attr("class") + ":last").remove();
    });

    $(".start-quiz-button").click(function() {
        $(".load-indicator-container").fadeIn();
    });
});