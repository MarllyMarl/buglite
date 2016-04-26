$(function () {

    var dialogComment = ($('#dialogComment').length > 0) ? $('#dialogComment') : $('<div id="dialogComment" style="display:hidden" title="Reason for Change"><textarea id="EditComment" name="EditComment" maxlength="1000" cols="20" rows="2" style="width:100%;height:90%" data-val="true" data-val-required="Reason for change required."></textarea><span id="charRemain"></span></div>').appendTo('body');

    dialogComment.dialog({
        autoOpen: false,
        bgiframe: true,
        modal: true,
        width: 600,
        height: 450,
        buttons: {
            Continue: function () {
                if ($("#EditComment").val().length > 1000) {
                    alert("You type way too much!")
                    $("#EditComment").focus();
                } else if ($("#EditComment").val() != "") {
                    $("#HiddenComment").val($("#EditComment").val());
                    document.forms["EditIssueFormId"].submit();
                } else {
                    alert("A comment is required.");
                    $("#EditComment").addClass(".input-validation-error");
                    $("#EditComment").focus();
                }
            }
        },
        close: function () {
            $(this).dialog("close");
        }
    });

    $(document.forms[0]).submit(function () {
        var form = document.forms[0];
        if ($(form).valid()) {
            // Make sure the comment text area is blank.
            $("#EditComment").val("");
            $("#EditComment").trigger("keydown");

            // Open the comment dialog.
            dialogComment.dialog("open");

            // Stops page from submittings.
            return false;
        }
    });

    $("#EditComment").keydown(charCounter);

    $("#EditComment").keyup(charCounter);

    function charCounter() {
        var limit = parseInt($("#EditComment").attr('maxlength'));
        var text = $("#EditComment").val();
        var count = $("#EditComment").val().length;

        if (count > limit) {
            var new_text = text.substr(0, limit);
            $("#EditComment").val(new_text);
            count = $("#EditComment").val().length;
        }

        $("#charRemain").text(count + " of 1000")
    }
});