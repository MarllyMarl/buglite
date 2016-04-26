<script type="text/javascript">

        function onUploadError() {
            alert("error during upload");
        }

        function addTableRow(filename, response) 
        {
            $('<tr id="fileItem_' + response + '">' + 
                '<td>' + 
                    '<table class="formatTable" cellpadding="0" cellspacing="0" style="width: 100%; border: solid 1px #B3CBDF;">' + 
                        '<tbody>' + 
                        '<tr>' + 
                            '<td rowspan="2" class="PendingFileAttachmentItemContainer"><img alt="Pending File" src="@(Url.Content(string.Format("~/Content/images/pendingFlag.png")))" /></td>' + 
                            '<th> <label> File: </label> </th>' + 
                            '<td><input id="newFileId_' + response + '" name="newFileId_' + response + '" type="hidden" value="' + response + '" />' + 
                                '<input id="newFileName_' + response + '" name="newFileName_' + response + '" type="text" style="width: 325px;" value="' + filename + '" />' + 
                            '</td>' + 
                            '<td rowspan="2" style="text-align:right;">' + 
                                '<a  class="noLink" href="" onclick="removeAttachment(' + response + ');return false;">' + 
                                    '<img src="@(Url.Content("~/Content/images/cancel.png"))" alt="remove" />' + 
                                '</a>' + 
                            '</td>' + 
                        '</tr>' + 
                        '<tr>' + 
                            '<th> <label> Description: </label> </th>' + 
                            '<td> <input id="newFileDescription_' + response + '" name="newFileDescription_' + response + '" type="text" style="width:325px;" /> (optional)</td>' + 
                        '</tr>' + 
                        '</tbody>' + 
                    '</table>' + 
                '</td>' + 
             '</tr>').appendTo('#files_list');

            $('.PendingFileAttachmentItemContainer').fadeIn('normal');

            return true;
        }

        function removeAttachment(fileId) {
            var p = $('#fileItem_' + fileId);
            p.fadeOut('normal', function () { p.remove(); });
            return false;
        }

        $(document).ready(function () {
            var button = $('#fileUploader'), interval;

            new AjaxUpload(button, {
                //action: '@(Url.Content("~/Uploader/AddAttachment/"))',
                action: '@(Url.Content("~/Issues/AddAttachment/"))',
                name: 'myfile',
                responseType: 'json',
                onSubmit: function (file, ext) {
                    button.text('Uploading');
                    $("#progress").show();
                    this.disable();

                    interval = window.setInterval(function () {
                        var text = button.text();
                        if (text.length < 13) {
                            button.text(text + '.');
                        } else {
                            button.text('Uploading');
                        }
                    }, 200);
                },
                onComplete: function (file, response) {

                    button.text('Upload');
                    $("#progress").hide();
                    window.clearInterval(interval);

                    this.enable();

                    onUploadComplete(file, response.id);
                }
            });
        });
        
    </script>
