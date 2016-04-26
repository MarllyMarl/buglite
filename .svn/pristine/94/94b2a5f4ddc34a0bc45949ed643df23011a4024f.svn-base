
$(function () {

    var urlHelper = new UrlHelper();
    var dialogNewContact = ($('#dialogNewContact').length > 0) ? $('#dialogNewContact') : $('<div id="dialogNewContact" style="display:hidden" title="New Contact"></div>').appendTo('body');
    var dialogContactSearch = ($('#dialogContactSearch').length > 0) ? $('#dialogContactSearch') : $('<div id="dialogContactSearch" style="display:hidden" title="Select Contact"></div>').appendTo('body');

    // Get the view from the contact controller - Contact Index
    $.get(urlHelper.ContactIndex(), {}, function (responseText, textStatus, XMLHttpRequest) {
        // Set the html content of the view to the dialog's html content
        dialogContactSearch.html(responseText);
        //dialogContactSearch.html(data);
        // Set up the dialog
        dialogContactSearch.dialog({
            autoOpen: false,
            bgiframe: true,
            modal: true,
            width: 580,
            height: 650,
            buttons: {
                /*// New Button click event
                New: function () {
                    dialogNewContact.dialog("open");
                    $(this).dialog("close");
                },*/
                // Continue Button click event
                Select: function () {
                    $.getJSON(urlHelper.ContactSearchById(), { id: $('#HiddenContactId').val() },
                        function (data) {
                            // Gets Contact's Id
                            $('#ContactId').val(data.ContactId);

                            // Gets Contact Display Fields
                            $('#TextContactName').val(data.ContactName);
                            $('#TextContactNumber').val(data.ContactNumber);
                            $('#TextContactEmail').val(data.EmailAddress);
                        }
                    );
                    $(this).dialog("close")
                },
                // Cancel Button click event
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            // Dialog close event
            close: function () {
                $(this).dialog("close");
            }
        });
    });

    // Get the view from the contact controller - Contact Create
    $.get(urlHelper.ContactCreate(), {}, function (responseText, textStatus, XMLHttpRequest) {
        // Set the html content of the view to the dialog's html content
        dialogNewContact.html(responseText);
        // Set up the dialog
        dialogNewContact.dialog({
            autoOpen: false,
            bgiframe: true,
            modal: true,
            width: 750,
            height: 850,
            buttons: {
                // Create Button click event
                Create: function () {
                    $.post(urlHelper.ContactCreate(), $("#ContactCreate").serialize(), function (data) {

                        if (data.indexOf("form") == -1) {

                            $('#HiddenContactId').val(data);

                            $.getJSON(urlHelper.ContactSearchById(), { id: $('#HiddenContactId').val() },
                                function (data) {
                                    // Update Issues's ContactId
                                    $('#ContactId').val(data.ContactId);

                                    // Update Contact Display Fields
                                    $('#TextContactName').val(data.ContactName);
                                    $('#TextContactNumber').val(data.ContactNumber);
                                    $('#TextContactEmail').val(data.EmailAddress);
                                }
                            );

                            dialogNewContact.dialog("close");
                        } else {
                            $("#dialogNewContact")[0].innerHTML = data;
                        }
                    });
                },
                // Cancel Button click event
                Cancel: function () {
                    dialogContactSearch.dialog("open");
                    $(this).dialog("close");
                }
            },
            // Dialog close event
            close: function () {
                $(this).dialog("close");
            }
        });
    });

    // New Contact Event
    $('#NewContact').click(function () {
        dialogNewContact.dialog("open");
        dialogContactSearch.dialog("close");
    });

    // Contact Search Event
    $('#ButtonContactSearch').click(function () {
        dialogContactSearch.dialog("open");
    });

});
