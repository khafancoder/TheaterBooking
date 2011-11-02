/// <reference path="jquery/jquery-1.4.4.js" />


$(function () {

    //UI Blocker
    //==========================================
    $.fn.extend({
        BlockUI: function (blockingText) {

            var blockingDIV = "<div id='BlockUI'>" + blockingText + "<br/><div class='BlockUIImage' /></div>";
            this.append(blockingDIV);
            return this;
        }
    });

    $.fn.extend({
        UnblockUI: function () {
            this.find("#BlockUI").remove();
            return this;
        }
    });
    //==========================================

    //Iconizer
    //==========================================
    $.fn.extend({
        Iconize: function (iconClass) {

            var itemIcon = $("<span/>");
            itemIcon.addClass("ui-icon");
            itemIcon.addClass(iconClass);
            itemIcon.css("display", "inline-block");
            itemIcon.css("position", "relative");
            itemIcon.css("top", "2px");
            itemIcon.css("margin-left", "3px");

            itemIcon.attr("title", this.first().text());

            this.prepend(itemIcon);
            this.attr("title", this.first().text());
            return this;

        }
    });
    //==========================================



    //Clear Form's User Inputs
    //==========================================
    $.fn.extend({
        ClearInputs: function () {

            var selector = "";
            selector += "input[type=text], ";
            selector += "input[type=password], ";
            selector += "input[type=checkbox], ";
            selector += "input[type=hidden][name!=__RequestVerificationToken], ";
            selector += "textarea";

            this.find(selector).not(".unclearable *").not(".unclearable").val("");

            return this;
        }
    });

    //==========================================



    //Ajax Uploader
    //==========================================
    $.fn.extend({
        AjaxUploader: function (options) {

            var uploadURL = options.uploadURL;
            var uploadParams = options.uploadParams;
            var allowedExtensions = options.allowedExtensions;
            var successEvent = options.success;
            var completeEvent = options.complete;
            var errorEvent = options.error;


            //Building iframe and the form on the fly
            var container = $(this);
            container.empty();
            var iframe = $("<iframe class='UploaderForm'/>");


            //iframe Load event handler
            iframe.load(function () {

                var uploadResult = $.parseJSON($(this).contents().find("body pre").text());
                if (uploadResult) {

                    iframe.parent().UnblockUI();
                    //uploaded successfully
                    if (uploadResult.ReturnCode == 0) {
                        //raising success event handler
                        if (successEvent)
                            successEvent(uploadResult);
                    }
                    else {
                        //raising complete event handler
                        if (errorEvent)
                            errorEvent(uploadResult);
                        else
                            alert("Error in uploading file : " + uploadResult.Message);
                    }

                    //raising complete event handler
                    if (completeEvent)
                        completeEvent(uploadResult);

                }


                var content = "";
                content += "<form enctype='multipart/form-data' method='post' action='" + uploadURL + "'>";
                content += "<div><input type='file' name='File' id='File'/></div>";
                content += "<input type='submit' value='Upload !' />";
                content += "</form>";

                var frameBody = iframe.contents().find("body");
                frameBody.empty();

                frameBody.append(content);
                var uploadForm = frameBody.find("form");

                //building hidden inputs for any passed uploadParam
                if (uploadParams) {
                    for (var i = 0; i < uploadParams.length; i++) {
                        uploadForm.append("<input type='hidden' name='" + uploadParams[i].key + "' id='" + uploadParams[i].key + "' value='" + uploadParams[i].value + "'/>");
                    }
                }


                //Form submit event handler
                frameBody.find("form").submit(function () {
                    var filename = frameBody.find("#File").val();

                    if (filename.length > 0) {
                        if (fileExtensionIsValid(filename, allowedExtensions)) {
                            iframe.parent().BlockUI("Uploading '" + filename + "', Please wait...");
                        }
                        else {
                            alert("Selected file is not allowed");
                            return false;
                        }
                    }
                    else {
                        alert("Please select your file !");
                        return false;
                    }
                });
                //      }

            });

            //adding iframe and filling it's contents
            container.append(iframe);

            return this;
        }
    });

    //==========================================


});



function fileExtensionIsValid(filename, extensions) {

    var result = false;
    if (extensions) {
        var ext = (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename) : undefined;
        ext = ext.toString().toLowerCase();

        for (var i = 0; i < extensions.length; i++) {

            if (extensions[i].toLowerCase() == ext)
                result = true;
        }
    }
    else
        result = true;

    return result;
}
