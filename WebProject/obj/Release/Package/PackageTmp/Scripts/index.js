$(document).ready(function() {
    var hdnUserMessageValue = $("[name=hdnUserMessage").val();
    if (hdnUserMessageValue.length > 0) {
        $("#messageDialog").show(2000);       
    }
});