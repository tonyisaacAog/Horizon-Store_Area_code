const Mapping = {
    'InqueryDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemRawId);
        },
        create: function (options) {
            return new InqueryDetails(options.data);
        }
    },

};

const InqueryDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    //ValidateInqueryDetails(self);
    //self.TotalAmount = ko.pureComputed(function () {
    //    return parseFloat(self.Qty()) * parseFloat(self.UnitPrice());
    //});
}

const InqueryStoreItem = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    //ValidateInquery(self);
    self.Messages = ko.observableArray([]);
    //self.TotalQTY = ko.pureComputed(function () {
    //    return GetTotalFromArray(self.InqueryDetails(), "Qty");
    //});

    //self.PurchaseInfo.TotalAmount = ko.pureComputed({
    //    read: function () {
    //        return GetTotalFromArray(self.InqueryDetails(), "Qty");
    //    },
    //    write: function (value) {
    //        return value;
    //    },
    //    owner: self
    //});


    //self.UpdateTotalRaw = function () {
    //    self.InqueryDetails().forEach((element => {
    //        console.log(element.ConfigueQty())
    //        console.log(self.StoreItem.Quantity())
    //        element.Qty(element.ConfigueQty() * self.StoreItem.Quantity());
    //    }));
    //}

    //self.AddItem = function () {
     
    //}
    self.InqueryProduct = function ()
    {
        SendAjaxCall(self.StoreItem, "/Store/StoreItems/CalcInqueryForProduct/").then(
            (data) => {
                console.log(data);
                self.InqueryDetails(data.Details);
            });
    }

    //self.Save = function () {
    //    self.Messages.removeAll();
    //    let errs = ko.validation.group(self, { deep: true, live: true });
    //    if (errs().length > 0) {

    //        for (var i = 0; i < errs().length; i++) {
    //            self.Messages.push(errs()[i]);
    //        }

    //    }
    //    if (self.Messages().length > 0) {
    //        $('#message').modal('show');
    //        errs.showAllMessages();
    //        return;
    //    }
    //    SendAjaxCall(self, "/Purchases/Home/SavePurchaseForProduct").then((data) =>
    //    { RedirectOrShowError(data, self) })    
    //}   
};