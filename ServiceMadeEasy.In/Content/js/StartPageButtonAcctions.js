﻿setTimeout(function () {
    var goToStepNumer = $("#AllStepButton").find("a").eq(0).attr('questionCount');
    var questionId = $("#AllStepButton").find("a").eq(0).attr('id');
    ServerCalls("GoToStep", goToStepNumer, questionId)
}, 4000);


$("#btnSaveNext").click(function () {
    ServerCalls("SaveAndNext");
});

$("#btnSaveMarkForReview").click(function () {
    ServerCalls("SaveMarkForReview");
});
$("#btnNext").click(function () {
    ServerCalls("Next");
});
$("#btnBack").click(function () {
    ServerCalls("Back");
});

$(".my-go-ToStep").click(function () {
    var goToStepNumer = $(this).attr('questionCount');
    var questionId = $(this).attr('id');
    ServerCalls("GoToStep", goToStepNumer, questionId);
});

$("#btnClearResponse").click(function () {
    ServerCalls("ClearResponse");
});

$("#btnMarkForReviewAndNext").click(function () {
    ServerCalls("MarkForReviewAndNext");
});

function ServerCalls(_functionName, goToStepNumer, questionId) {

    var wizardDiv = $('DIV[title="Question"][style*="display: block"]')

    if (_functionName == "Next") {
        wizardDiv = wizardDiv.next();
    }
    if (_functionName == "Back") {
        wizardDiv = wizardDiv.prev();
    }

    var _testPaperId = $(wizardDiv).attr("testPaperId");

    var questionDiv = $(wizardDiv).eq(0).find("DIV[qId]");

    var _questionId = -1;

    if (_functionName == "GoToStep") {
        //_questionId = goToStepNumer;
        _questionId = questionId;
    } else {
        _questionId = $(questionDiv).attr("qId");
    }

    var checkboxlist = $(wizardDiv).find("input:checkbox");
    var _answerId = "-1";

    var ids = "";
    var count = 0;
    $.each(checkboxlist, function (i, checkbox) {

        if ($(this).is(":checked")) {
            if (count == 0) {
                ids = $(this).prop("id");
            } else {
                ids = ids + "," + $(this).prop("id");
            }
            count++;
        }
        _answerId = ids;
    });

    if (_functionName == "Next" || _functionName == "Back" || _functionName == "GoToStep" || _functionName == "MarkForReviewAndNext") {
        _answerId = "-2";
    }

    if (_functionName == "ClearResponse") {

        $.each(checkboxlist, function (i, checkbox) {
            if ($(this).is(":checked")) {
                $(this).removeAttr("checked");
            }
        });
    }

    if ((_answerId.length == 0 || _answerId == "-1") && _functionName != "ClearResponse") {
        $("#myModal").modal();//alert("Please choose a option!");
        return;
    }
    var URL = "/ApplicationUser/Test/StartPageButtonAcctions";

    var wizardContent = $('#wizard');

    $.ajax({

        type: 'POST',
        url: URL,
        dataType: 'json',
        data: { testPaperId: _testPaperId, questionId: parseInt(_questionId), answerIds: _answerId, functionName: _functionName },
        success: function (result) {
            if (result == "success") {
                // alert(result);
                if (_functionName == "Back") {
                    wizardContent.smartWizard("goBackward");
                } else if (_functionName == "GoToStep") {
                    wizardContent.smartWizard("goToStep", goToStepNumer);
                } else {
                    wizardContent.smartWizard("goForward");
                }
                $("#AllStepButton").find("#" + _questionId).eq(0).addClass("Visited");
                return _questionId;
            }
        },
        error: function (ex) {
            if (ex.responseText == "success") {
                //alert(ex.responseText);
                if (_functionName == "Back") {
                    wizardContent.smartWizard("goBackward");
                } else if (_functionName == "GoToStep" || _functionName == "ClearResponse") {
                    wizardContent.smartWizard("goToStep", goToStepNumer);
                } else {
                    wizardContent.smartWizard("goForward");
                }

                var questionButton = $("#AllStepButton").find("#" + _questionId).eq(0);

                if (_functionName == "Next" || _functionName == "Back" || _functionName == "GoToStep") {
                    if (!questionButton.hasClass("Visited")) {
                        questionButton.removeClass("btn-primary").addClass("Visited");
                    }
                }

                if (_functionName == "SaveAndNext") {

                    questionButton.removeClass("btn-primary").addClass("answered");

                    setTimeout(function () {
                        var _wizardDiv = $('DIV[title="Question"][style*="display: block"]')
                        var __testPaperId = $(_wizardDiv).attr("testPaperId");
                        var _questionDiv = $(_wizardDiv).eq(0).find("DIV[qId]");
                        var __questionId = $(_questionDiv).attr("qId");
                        var _questionButton = $("#AllStepButton").find("#" + __questionId).eq(0);
                        _questionButton.removeClass("btn-primary").addClass("Visited");

                        $.ajax({

                            type: 'POST',
                            url: URL,
                            dataType: 'json',
                            data: { testPaperId: __testPaperId, questionId: parseInt(__questionId), answerIds: "", functionName: "Next" },
                            success: function (result) {

                            }, error: function (ex) { }
                        });
                    }, 1000);
                }
                if (_functionName == "SaveMarkForReview") {

                    questionButton.removeClass("btn-primary").addClass("answeredMarkedForReview");

                    setTimeout(function () {
                        var _wizardDiv = $('DIV[title="Question"][style*="display: block"]')
                        var __testPaperId = $(_wizardDiv).attr("testPaperId");
                        var _questionDiv = $(_wizardDiv).eq(0).find("DIV[qId]");
                        var __questionId = $(_questionDiv).attr("qId");
                        var _questionButton = $("#AllStepButton").find("#" + __questionId).eq(0);
                        _questionButton.removeClass("btn-primary").addClass("Visited");

                        $.ajax({

                            type: 'POST',
                            url: URL,
                            dataType: 'json',
                            data: { testPaperId: __testPaperId, questionId: parseInt(__questionId), answerIds: "", functionName: "Next" },
                            success: function (result) {

                            }, error: function (ex) { }
                        });
                    }, 1000);
                }
                if (_functionName == "MarkForReviewAndNext") {

                    questionButton.removeClass("btn-primary").addClass("markedforReview");
                    setTimeout(function () {
                        var _wizardDiv = $('DIV[title="Question"][style*="display: block"]')
                        var __testPaperId = $(_wizardDiv).attr("testPaperId");
                        var _questionDiv = $(_wizardDiv).eq(0).find("DIV[qId]");
                        var __questionId = $(_questionDiv).attr("qId");
                        var _questionButton = $("#AllStepButton").find("#" + __questionId).eq(0);
                        _questionButton.removeClass("btn-primary").addClass("Visited");

                        $.ajax({

                            type: 'POST',
                            url: URL,
                            dataType: 'json',
                            data: { testPaperId: __testPaperId, questionId: parseInt(__questionId), answerIds: "", functionName: "Next" },
                            success: function (result) {

                            }, error: function (ex) { }
                        });
                    }, 1000);
                }

                if (_functionName == "ClearResponse") {

                    questionButton.removeClass("answered").removeClass("Visited").removeClass("answeredMarkedForReview").removeClass("markedforReview").addClass("btn-primary");
                }

                $('DIV[title="Question"][style*="display: block"]').focus();

                return _questionId;
            } else {
                alert('Failed to retrieve states.' + ex.result);
            }
        }
    });
}