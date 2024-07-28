const RecordStatus = {
    Added:0,
    Updated:1,
    Deleted:2,
    UnChanged:3
}

const Mapping = {
    'OrderConfigure': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.StoreItemId);
        },
        create: function (options) {
            return new OrderConfigure(options.data);
        }
    },

};

const OrderConfigure = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidateConfigurationItem(self);   
   
   
}

const ConfigureContainer = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidateStoreItem(self);
    self.Messages = ko.observableArray([]);



    self.RemoveItem = function (item) {
        if (item.RecordStatus() !== RecordStatus.Added)
            item.RecordStatus(RecordStatus.Deleted);
        else
            self.OrderConfigure.remove(item);
    }

    self.AddItem = function () {

        let newLine = {
            Id: 0,
            StoreItemId: 0,
            Qty: 0,
            OrderDetailsId: self.OrderDetailId(),
            RecordStatus: RecordStatus.Added,
            IsNew: true
        }


        self.OrderConfigure.push(new OrderConfigure(newLine));
    }

    self.UpdateItem = function (item) {
        console.log(item)
        let RawItemInList =
            self.OrderConfigure().filter(x => x.StoreItemId() ===
                item.StoreItemId());
        if (RawItemInList?.length<=1) {
            if (item.RecordStatus() !== RecordStatus.Added) {
                item.RecordStatus(RecordStatus.Updated);
                item.IsNew(true);
            }
            else {
                item.IsNew(true);
            }
        }
        else {
            self.OrderConfigure.remove(item);
            self.Messages.removeAll();
            self.Messages.push("تم اختيار هذا العنصر من قبل");
            $('#message').modal('show');
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
        SendAjaxCall(self, "/Orders/Order/SaveExtraConfiguration").then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};
