const RecordStatus = {
    Added: 0,
    Updated: 1,
    Deleted: 2,
    UnChanged: 3
}


const Mapping = {
    'PurchaseOrderDetails': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new PurchaseOrderDetails(options.data);
        }
    },
};




const PurchaseOrderDetails = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    
    ValidatePurchaseOrderDetails(self)
}


const PurchaseOrderContainer = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    //ValidateOrder(self);
    self.Messages = ko.observableArray([]);
  
    ValidatePurchaseOrder(self);
    self.PurchaseOrderDetails.extend({
        validator: function (arr, params) {
            if (!arr || typeof arr !== "object" || !(arr instanceof Array)) {
                throw "[validArray] Parameter must be an array";
            }
            if (params.required !== undefined) {
                return params.required ? arr.length >= params.length : params.required;
            } else {
                return arr.length >= params;
            }
        }
    })

    //self.TotalStoreItemAmount = ko.pureComputed(function () {
    //    //return GetTotalFromArray(self.PurchaseOrderDetails(), "StoreItemAmount");
    //});

  

    self.TotalAmount = ko.pureComputed(function () {

        return GetTotalFromArray(self.PurchaseOrderDetails().filter(obj => obj.RecordStatus() != RecordStatus.Deleted), "StoreItemAmount");
    });


    ///////////////   function for parent   ////////////////////////


 
    self.AddItem = function () {
        let newLine = {
            Id: 0,
            StoreItemId: 0,
            StoreItemAmount: 0,
            Notes:null,
            RecordStatus: RecordStatus.Added
        }

        self.PurchaseOrderDetails.push(new PurchaseOrderDetails(newLine));
    }


    /* Start func for remove item from OrderList */
    self.RemoveItem = function (item) {
        if (item != null && item.RecordStatus() == RecordStatus.Added)
            self.PurchaseOrderDetails.remove(item);
        if (item != null && item.RecordStatus() != RecordStatus.Added)
            item.RecordStatus(RecordStatus.Deleted);

    }

    self.UpdateItem = function (item) {
        let RawItemInList =
            self.PurchaseOrderDetails().filter(x => x.StoreItemId() ===
                item.StoreItemId());
        if (RawItemInList?.length <= 1 && item.RecordStatus() != RecordStatus.Deleted) {
            if (item.RecordStatus() !== RecordStatus.Added)
                item.RecordStatus(RecordStatus.Updated);
        }
        else if (RawItemInList?.length > 1 && item.RecordStatus() == RecordStatus.Added && RawItemInList[0].RecordStatus() == RecordStatus.Deleted) {

            
            console.log(ko.toJS(self.PurchaseOrderDetails()));

            self.PurchaseOrderDetails.remove(item);
            self.PurchaseOrderDetails().map(x => {
                if (x.StoreItemId() === item.StoreItemId()) {
                    x.RecordStatus(RecordStatus.Updated);
                }
            });
            console.log(ko.toJS(self.PurchaseOrderDetails()));

        }
        else {
            self.PurchaseOrderDetails.remove(item);
            self.Messages.removeAll();
            self.Messages.push("تم اختيار هذا المنتج من قبل");
            $('#message').modal('show');
        }

    }
    self.ChangeItem = function (item) {
        console.log(item)
        let RawItemInList =
            self.PurchaseOrderDetails().filter(x => x.StoreItemId() ===
                item.StoreItemId());
        if (RawItemInList?.length <= 1) {
            if (item.RecordStatus() !== RecordStatus.Added)
                item.RecordStatus(RecordStatus.Updated);
        }
        

    }

    /* end func */

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
        SendAjaxCall(self, "/Purchases/PurchaseOrder/SavePurchaseOrder").then((data) => { RedirectOrShowError(data, self) })
    }
};

