$(document).ready(function() {
    $("#checkAnswer").click(function() {
        var responseModel = new ResponseModel();
        $.ajax({
            url: $(this).data("request-url"),
            dataType: "json",
            contentType: "application/json",
            type: "POST",
            data: JSON.stringify(responseModel),
            success: function (response) {               
                if (response.AnswerResult.toLowerCase() === "correct") {
                    $(".correctAnswer").fadeIn(1500);
                    $(".wrongAnswer").hide();
                    $(".noAnswerSelectedWarning").hide();
                } else if (response.AnswerResult.toLowerCase() === "wrong") {
                    $(".wrongAnswer").fadeIn(1500);
                    $(".correctAnswer").hide();
                    $(".noAnswerSelectedWarning").hide();
                } else {
                    $(".noAnswerSelectedWarning").fadeIn(1500);
                    $(".correctAnswer").hide();
                    $(".wrongAnswer").hide();
                }

                for (var i = 0; i < response.CorrectAnswersIds.length; i++) {
                    $("#" + response.CorrectAnswersIds[i] + ".quizOption").prop("checked", true);
                    $("#spanAnswerText" + response.CorrectAnswersIds[i]).addClass('green-answer');
                }               
            }
        });
    });

    $(":checkbox").change(function() {
        if ($(this).prop("checked")) {
            $("#hiddenAnswerId" + $(this).attr("id")).val($(this).attr("id"));
        } else {
            $("#hiddenAnswerId" + $(this).attr("id")).val(0);
        }
    });
   
    function ResponseModel() {
        var self = this;
        self.QuestionId = $("#QuestionId").val();
        var answerIds = [];
        $("input:checked").each(function() {
            answerIds.push($(this).attr("id"));
            self.AnswerIds = answerIds;
        });
    }  
});