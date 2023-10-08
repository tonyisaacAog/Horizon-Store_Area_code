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


const CreateUser = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidateCreateUser(self);
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
        SendAjaxCall(self, "/Settings/UserAdmin/SaveNewUser").then((data) =>
        {
            console.log(data);
            RedirectOrShowError(data, self)
        })
    }        
};