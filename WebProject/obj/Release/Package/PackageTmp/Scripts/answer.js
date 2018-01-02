﻿$(document).ready(function() {

    var $loading = $("#ajaxLoadIndicator").hide();
    $(document)
        .ajaxStart(function() {            
            $loading.show();
        })
        .ajaxStop(function() {
            $loading.hide();
        });

    $("#checkAnswer").click(function () {
        $("#finishTestLink").hide();
        var responseModel = new ResponseModel();
        $.ajax({
            url: $(this).data("request-url"),
            dataType: "json",
            contentType: "application/json",
            type: "POST",
            data: JSON.stringify(responseModel),          
            success: function (response) {               
                if (response.AnswerResult.toLowerCase() === "correct") {
                    $("#divResult").text("Your answer is correct");
                    $("#divResult").addClass("div-result-correct");
                    $(".quiz-option").prop("disabled", true);
                } else if (response.AnswerResult.toLowerCase() === "wrong") {
                    $("#divResult").text("Your answer is wrong");
                    $("#divResult").addClass("div-result-wrong");
                    $(".quiz-option").prop("disabled", true);
                }
               
                $("#checkAnswer").hide();
                $("#divResult").fadeIn(1000);
                for (var i = 0; i < response.CorrectAnswersIds.length; i++) {
                    $("#" + response.CorrectAnswersIds[i] + ".quiz-option").prop("checked", true);
                    $("#spanAnswerText" + response.CorrectAnswersIds[i]).addClass("green-answer");
                }

                $("#nextQuestion").fadeIn(1000);
                $("#finishTestLink").fadeIn(1000);
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

    $(function () {
        $("#finishTestLink").click(function (event) {
            event.preventDefault();
            $('<div title="Confirm"></div>').dialog({
                open: function (event, ui) {
                    $(this).html("Are you sure you want to finish the test?");
                },
                close: function () {
                    $(this).remove();
                },
                resizable: false,                
                modal: true,
                buttons: {
                    'Yes': function () {
                        $(this).dialog('close');
                        window.location.href = "/Quiz/QuizCompleted";

                    },
                    'No': function () {
                        $(this).dialog('close');                      
                    }
                }
            });
        });
    });    
});