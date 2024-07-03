const RecordStatus = {
    Added: 0,
    Updated: 1,
    Deleted: 2,
    UnChanged: 3
}


const Mapping = {
    'OrderDetail': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new OrderDetail(options.data);
        }
    },
   
    'OrderConfigure': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new OrderConfigure(options.data);
        }
    },


};

const OrderConfigure = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
}


const OrderDetail = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    self.TotalAmount = ko.pureComputed(function () {
        return parseFloat(self.QTY()) * parseFloat(self.UnitPrice());
    });
    //self.OrderConfigure = ko.observableArray([]);
    ValidateOrderDetails(self)
    //self.AddConfigure = function (item) {
    //    let newLine = {
    //        Id: 0,
    //        StoreItemId:0,
    //        Qty:0,
    //        Descrpition:''
    //    }


    //    item.OrderConfigure.push(new OrderConfigure(newLine));

    //    console.log(ko.toJSON(item))
    //    console.log(newLine)
    //}

    //self.removeItemConfigure = function (item) {
    //    console.log(ko.toJSON(item))
    //    self.OrderConfigure.remove(item);

    //}
}

const OrderDetailsContainer = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    //ValidateOrder(self);
    self.Messages = ko.observableArray([]);
  
    ValidateOrder(self);

    self.TotalQTY = ko.pureComputed(function () {
        return GetTotalFromArray(self.OrderDetail(), "QTY");
    });

    self.Order.TotalAmount = ko.pureComputed({
        read: function () {
            return GetTotalFromArray(self.OrderDetail(), "QTY");
        },
        write: function (value) {
            return value;
        },
        owner: self
    });

    self.TotalAmount = ko.pureComputed(function (){
        return GetTotalFromArray(self.OrderDetail(), "TotalAmount");
    });


    ///////////////   function for parent   ////////////////////////

    self.SaveNotes = function () {
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
        SendAjaxCall(self.Order, "/Orders/Order/SaveNotesForOrder").then((data) => { RedirectOrShowError(data, self) })
    }

 
    self.AddItem = function () {
        let newLine = {
            Id: 0,
            ProductId: 0,
            QTY: 0,
            UnitPrice: 0
        }
        self.OrderDetail.push(new OrderDetail(newLine));
    }


    /* Start func for remove item from OrderList */
    self.RemoveItem = function (item) {
        if (item != null)
            self.OrderDetail.remove(item);
    }

    self.UpdateItem = function (item) {
        console.log(item)
        let RawItemInList =
            self.OrderDetail().filter(x => x.ProductId() ===
                item.ProductId());
        if (RawItemInList?.length <= 1) {
            if (item.RecordStatus() !== RecordStatus.Added)
                item.RecordStatus(RecordStatus.Updated);
        }
        else {
            self.OrderDetail.remove(item);
            self.Messages.removeAll();
            self.Messages.push("تم اختيار هذا المنتج من قبل");
            $('#message').modal('show');
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
        SendAjaxCall(self, "/Orders/Order/SaveOrder").then((data) => { RedirectOrShowError(data, self) })
    }
};

