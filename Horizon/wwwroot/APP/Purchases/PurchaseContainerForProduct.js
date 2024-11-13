const Mapping = {
    'PurchaseDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemId);
        },
        create: function (options) {
            return new PurchaseDetails(options.data);
        }
    },

};

const PurchaseDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidatePurchaseDetails(self);
    self.TotalAmount = ko.pureComputed(function () {
        return parseFloat(self.Qty()) * parseFloat(self.UnitPrice());
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

    self.PurchaseInfo.TotalAmount = ko.pureComputed({
        read: function () {
            return GetTotalFromArray(self.PurchaseDetails(), "Qty");
        },
        write: function (value) {
            return value;
        },
        owner: self
    });


    self.UpdateTotalRaw = function () {
        self.PurchaseDetails().forEach((element => {
            console.log(element.ConfigueQty())
            console.log(self.StoreItem.Quantity())
            element.Qty(element.ConfigueQty() * self.StoreItem.Quantity());
        }));
    }
    self.UpdateNumberOfRack = function () {

            $.ajax({
                url: "/Purchases/PurchaseOrder/GetPurchaseOrder/" + self.PurchaseOrderId(),
                type: "GET",
                success: function (data) {
                    console.log(data);
                    var item = data.data.filter(function (obj) {
                        return obj.StoreItemId == self.StoreItem.Id();
                    });
                    console.log(item);
                    if (item.length > 0) {
                        var quantity = item[0].StoreItemAmount;
                        console.log(quantity);
                        self.StoreItem.Quantity(quantity);
                        self.UpdateTotalRaw();
                    } else {
                        console.log("Item not found");
                        $("#PurchaseOrderId").val(null).trigger('change');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error occurred:", status, error);
                    $("#PurchaseOrderId").val(null).trigger('change');
                }
            });
        
 
    }

    self.AddItem = function () {
     
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
        SendAjaxCall(self, "/Purchases/Home/SavePurchaseForProduct").then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};