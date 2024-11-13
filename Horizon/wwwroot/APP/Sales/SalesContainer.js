const Mapping = {
    'SaleDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new SaleDetails(options.data);
        }
    },
    SaleItemRawDetails: {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new SaleItemRawDetails(options.data);
        }
    }
};

const SaleDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.TotalAmount = ko.pureComputed(function () {
        return parseFloat(self.QTY()) * parseFloat(self.UnitPrice());
    });
    ValidateSaleDetails(self);

}

const SaleItemRawDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.TotalAmount = ko.pureComputed(function () {
        return parseFloat(self.QTY()) * parseFloat(self.UnitPrice());
    });
    ValidateSaleDetails(self);

}

const Sales = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.Messages = ko.observableArray([]);
    self.TotalQTY = ko.pureComputed(function () {
        return GetTotalFromArray(self.SaleDetails(), "QTY");
    });
    self.ItemRawTotalQTY = ko.pureComputed(function () {
        return GetTotalFromArray(self.SaleItemRawDetails(), "QTY");
    });


    self.TotalAmount = ko.pureComputed({
        read: function () {
            return GetTotalFromArray(self.SaleDetails(), "QTY");
        },
        write: function (value) {
            return value;
        },
        owner: self
    });

    self.ItemRawTotalAmount = ko.pureComputed({
        read: function () {
            return GetTotalFromArray(self.SaleItemRawDetails(), "QTY");
        },
        write: function (value) {
            return value;
        },
        owner: self
    });

    ValidateSales(self);


    self.AddItem = function () {
        let newLine = {
            Id: 0,
            StoreItemId: 0,
            QTY: 0,
            UnitPrice:0
        }
        self.SaleDetails.push(new SaleDetails(newLine));
    }
    self.RemoveItem = function (item) {
        if (item != null)
            self.SaleDetails.remove(item);
    }
    self.UpdateItem = function (item) {
        console.log(item)
        console.log(item.Id())
        let StoreItemInList =
            self.SaleDetails().filter(x => x.StoreItemId() === item.StoreItemId());
        console.log(StoreItemInList)
        console.log("out if")
        if (StoreItemInList?.length > 1) {
            console.log("in if")
            self.Messages.removeAll();
            self.Messages.push("هذا المنتج تم اضافته");
            $('#message').modal('show');
            self.SaleDetails.remove(item);
            return;

        }

    }


    self.AddItemRaw = function () {
        let newLine = {
            Id: 0,
            StoreItemId: 0,
            QTY: 0,
            UnitPrice: 0
        }
        self.SaleItemRawDetails.push(new SaleItemRawDetails(newLine));
    }
    self.RemoveItemRaw = function (item) {
        if (item != null)
            self.SaleItemRawDetails.remove(item);
    }
    self.UpdateItemRaw = function (item) {
        console.log(item)
        console.log(item.Id())
        let StoreItemInList =
            self.SaleItemRawDetails().filter(x => x.StoreItemId() === item.StoreItemId());
        console.log(StoreItemInList)
        console.log("out if")
        if (StoreItemInList?.length > 1) {
            console.log("in if")
            self.Messages.removeAll();
            self.Messages.push("هذا العنصر تم اضافته");
            $('#message').modal('show');
            self.SaleItemRawDetails.remove(item);
            return;

        }

    }
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
        SendAjaxCall(self, "/Sales/Home/SaveSales").then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};