GeneralKoWithUploadViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, [], self);


    self.Save = function () {
     
        var upload = $('#uploadfile').get(0).files[0];

        var formData = new FormData();
        var viewmodel = ko.toJSON(self);
        formData.append('uploadfile', upload);
        formData.append('vm', viewmodel);


        $.ajax({
            url: self.SaveUrl(),
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.newLocation != null) {
                    window.location = data.newLocation;
                }

                if (data.errors != null) {
                    self.IsWaitingAreaVisible(false);
                    self.IsDetailAreaVisible(true);
                    self.IsMessageAreaVisible(true);
                    self.Messages(data.errors);
                }
            }
        });

    }
    self.hideMessages = function () {

        self.IsMessageAreaVisible(false);
    };
};