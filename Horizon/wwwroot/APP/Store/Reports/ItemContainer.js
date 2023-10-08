const Mapping = {
    'TransactionRawContainer.StoreItemRawTransactions': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new StoreItemRawTransactions(options.data);
        }
    }
};

const StoreItemRawTransactions = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
}



const CardReport = function (data) {
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

        self.TransactionRawContainer.Search.StoreItemId(self.StoreItemRaw.Id);
     
        self.TransactionRawContainer.StoreItemRawTransactions.removeAll();
        SendAjaxCall(self.TransactionRawContainer, "/Store/Reports/LoadReportItemRaw").then((data) => {
            DestroyDataTable();
            self.TransactionRawContainer.StoreItemRawTransactions(data.Result.StoreItemRawTransactions);
            self.TransactionRawContainer.Search.StartBalance(data.Result.Search.StartBalance);
            self.TransactionRawContainer.Search.EndBalance(data.Result.Search.EndBalance);
            DataTableWithFilter();
        })
    }
};
