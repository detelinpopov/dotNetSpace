$(document).ready(function() {   
    window.sr = ScrollReveal({ reset: true });
    sr.reveal(".scroll-reveal", { mobile: true });

    $(".start-quiz-button").click(function() {
        $(".load-indicator-container").fadeIn();
    });

    var showcase = $(".showcase");
    showcase.Cloud9Carousel({
        yPos: 42,
        yRadius: 48,
        mirrorOptions: {
            gap: 12,
            height: 0.2
        },
        buttonLeft: $(".nav > .left"),
        buttonRight: $(".nav > .right"),
        autoPlay: true,
        bringToFront: true,
        onRendered: showcaseUpdated,
        onLoaded: function () {
         
        }
    });

    function showcaseUpdated(showcase) {
        var title = $("#item-title").html(
            $(showcase.nearestItem()).attr("alt")
        );

        var c = Math.cos((showcase.floatIndex() % 1) * 2 * Math.PI);
        title.css("opacity", 0.5 + (0.5 * c));
    }   
});