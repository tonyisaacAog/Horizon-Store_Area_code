const Mapping = {
    'Purchasings': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new Purchasings(options.data);
        }
    },

};

const Purchasings = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
}

const PurchasingReport = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.Messages = ko.observableArray([]);
    self.LoadReport = function () {
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
        SendAjaxCall(self.SearchDate, "/Purchases/Reports/LoadReport").then(
            (data) =>
            {
                DestroyDataTable();
                data.Purchasings.forEach(element => {
                    self.Purchasings.push(element)
                });
                DataTableWithFilter();
            })
    }

};