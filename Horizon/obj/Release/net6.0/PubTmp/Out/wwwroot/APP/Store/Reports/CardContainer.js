const Mapping = {
    'StoreLocationBalances': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new StoreLocationBalances(options.data);
        }
    },
    'TransactionDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new TransactionDetails(options.data);
        }
    }
};

const TransactionDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
}

const StoreLocationBalances = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
}


const CardReport = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.Messages = ko.observableArray([]);

    self.QtyAfter = ko.computed(function () {
        return self.StoreItem.CurrentQty() - self.StoreItem.DestroyedQty();
    })

    self.TransactionContainer.Search.EndBalance(0)
    self.TransactionContainer.Search.StartBalance(0)



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

        self.TransactionContainer.Search.StoreItemId(self.StoreItem.Id);
     
        self.TransactionContainer.TransactionDetails.removeAll();
        SendAjaxCall(self.TransactionContainer, "/Store/Reports/LoadReport").then((data) => {

            console.log(data.Result.Search.StartBalance);
            console.log(data.Result.Search.EndBalance);
            DestroyDataTable();
            self.TransactionContainer.Search.EndBalance(data.Result.Search.EndBalance)
            self.TransactionContainer.Search.StartBalance(data.Result.Search.StartBalance)
            self.TransactionContainer.TransactionDetails(data.Result.TransactionDetails);
            DataTableWithFilter();
        })
    }
};
