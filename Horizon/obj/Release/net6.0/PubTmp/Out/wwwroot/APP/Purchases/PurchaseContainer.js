const Mapping = {
    'PurchaseDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new PurchaseDetails(options.data);
        }
    },
    'PurchaseStoreItemDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new PurchaseStoreItemDetails(options.data);
        }
    }

};

const PurchaseDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidatePurchaseDetails(self);
    self.TotalAmount = ko.pureComputed(function () {
        return parseFloat(self.Qty()) * parseFloat(self.UnitPrice());
    });
}

const PurchaseStoreItemDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidatePurchaseStoreItemDetails(self);
    self.TotalAmount = ko.pureComputed(function () {
        return    parseFloat(self.UnitPrice());
    });
}


const Purchase = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidatePurchase(self);
    self.Messages = ko.observableArray([]);
    self.TotalQTY = ko.pureComputed(function () {
        return GetTotalFromArray(self.PurchaseDetails(), "Qty");
    });
    self.TotalAmount = ko.pureComputed(function () {
        return GetTotalFromArray(self.PurchaseDetails(), "TotalAmount");
    });

    self.StoreItemTotalQTY = ko.pureComputed(function () {
        return GetTotalFromArray(self.PurchaseStoreItemDetails(), "Qty");
    });
    self.StoreItemTotalAmount = ko.pureComputed(function () {
        return GetTotalFromArray(self.PurchaseStoreItemDetails(), "TotalAmount");
    });

    self.PurchaseInfo.TotalAmount = ko.pureComputed({
        read: function () {
            return GetTotalFromArray(self.PurchaseDetails(), "Qty");
        },
        write: function (value) {
            return value;
        },
        owner: self
    });

    self.RemoveItem = function (item) {
        if(item !=null)
            self.PurchaseDetails.remove(item);
    }
    self.AddItem = function () {
        let newLine = {
            Id: 0,
            StoreItemId: 0,
            Qty: 0,
            UnitPrice:0
        }
        self.PurchaseDetails.push(new PurchaseDetails(newLine));
    }
    self.UpdateItem = function (item) {
        console.log(item)
        console.log(item.Id())
        let RawItemInList =
            self.PurchaseDetails().filter(x => x.StoreItemId() === item.StoreItemId());
        console.log(RawItemInList)
        console.log("out if")
        if (RawItemInList?.length > 1) {
            console.log("in if")
            self.Messages.removeAll();
            self.Messages.push("هذا العنصر تم اضافته");
            $('#message').modal('show');
            self.PurchaseDetails.remove(item);
            return;

        } 

    }

    self.RemoveStoreItem = function (item) {
        if (item != null)
            self.PurchaseStoreItemDetails.remove(item);
    }
    self.AddStoreItem = function () {
        let newLine = {
            Id: 0,
            StoreItemId: 0,
            Qty: 0,
            UnitPrice: 0
        }
        self.PurchaseStoreItemDetails.push(new PurchaseStoreItemDetails(newLine));
    }
    self.UpdateStoreItem = function (item) {
        console.log(item)
        console.log(item.Id())
        let RawItemInList =
            self.PurchaseStoreItemDetails().filter(x => x.StoreItemId() === item.StoreItemId());
        console.log(RawItemInList)
        console.log("out if")
        if (RawItemInList?.length > 1) {
            console.log("in if")
            self.Messages.removeAll();
            self.Messages.push("هذا العنصر تم اضافته");
            $('#message').modal('show');
            self.PurchaseStoreItemDetails.remove(item);
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
        SendAjaxCall(self, "/Purchases/Home/SavePurchase").then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};

