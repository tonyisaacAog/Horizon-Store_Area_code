const Mapping = {
    'UserRoles': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new UserRoles(options.data);
        }
    },

};

const UserRoles = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
}


const UpdateUser = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidateUpdateUser(self);
    self.Messages = ko.observableArray([]);
   
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
        console.log(self);
        SendAjaxCall(self, "/Settings/UserAdmin/SaveUpdateUser").then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};