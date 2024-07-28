const TypeListMapping = {
    'TypesList': {
        key: function (typesList) {
            return ko.utils.unwrapObservable(typesList.Id);
        },
        create: function (options) {
            return new TypeList(options.data);
        }
    }
};

const TypeList = function (data) {
    var self = this;
    ko.mapping.fromJS(data, TypeListMapping, self);

};

const SPAViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, TypeListMapping, self);

    self.validateNow = ko.observable(false);
    ValidateEntity(self);

    self.AddItem = function () {
        self.Messages.removeAll();
        ClearModel(self);
        
    };
    self.EditItem = function (editedItem) {
       
        for (var prop in self.Entity) {
            self.Entity[prop](editedItem[prop]());
        }
    };
    self.Cancel = function () {
       
        for (var prop in self.Entity) {
            self.Entity[prop](null);
        }
    }

    self.SaveEntity = function () {
      
        self.Messages.removeAll();
      
        let errs = ko.validation.group(self, { deep: true, live: true });
        self.validateNow(true);

        if (errs().length > 0) {
            self.Messages.removeAll();
            for (var i = 0; i < errs().length; i++) {
                self.Messages.push(errs()[i]);
            }
            errs.showAllMessages();
            return;
        }

        SendAjaxCall(self.Entity, self.SaveURL())
            .then((data) => { ShowListOrShowError(data, self) });

    }

};