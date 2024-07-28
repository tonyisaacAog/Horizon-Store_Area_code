const GeneralKoViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
    self.Messages = ko.observableArray([]);
    ValidateVM(self);
    self.Save = function () {
        self.Messages.removeAll();
        let errs = ko.validation.group(self, { deep: true, live: true });
        if (errs().length > 0) {

            for (var i = 0; i < errs().length; i++) {
                self.Messages.push(errs()[i]);
            }

        }
        if (self.Messages().length > 0) {
            $('#message').modal('show');
            errs.showAllMessages();
            return;
        }
        SendAjaxCall(self, self.SaveUrl()).then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};