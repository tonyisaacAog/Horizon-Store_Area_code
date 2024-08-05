const Mapping = {
    'ProductConfigurations.ItemConfigurationVM': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new ItemConfigurationVM(options.data);
        }
    }
};

const ItemConfigurationVM = function (data) {
    const self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ItemConfigurationValidation(self);

};



const ManufacturingContainer = function (data) {
    const self = this;
    ko.mapping.fromJS(data, Mapping, self);

    self.SelectedProduct = ko.observable(0);


    self.Messages = ko.observableArray([]);

    ManufacturingContainerValidate(self);
    self.UpdateTotalRaw = function () {
        self.ProductConfigurations.ItemConfigurationVM().forEach((element => {
            element.UsedQTY(element.MinimumAmount() * self.ProductConfigurations.StoreItemVM.Quantity());
        }));
    }

    self.RemoveItem = function (data) {

        self.ProductConfigurations.ItemConfigurationVM.remove(data);
    }

    function update() {
        self.ProductConfigurations.ItemConfigurationVM().forEach((element => {
            element.UsedQTY(element.MinimumAmount() * self.ProductConfigurations.StoreItemVM.Quantity());
        }));
    }
    update()

    self.AddItem = function () {

        let newLine = {
            Id: 0,
            StoreItemId: self.ProductConfigurations.Id(),
            StoreItemRawId: 0,
            MinimumAmount: 1,
            UsedQTY: 0,
            Notest:null,
            CurrentQty:0
        }

        self.ProductConfigurations.ItemConfigurationVM.push(new ItemConfigurationVM(newLine));
    }
    self.UpdateItem = function (item) {
        let RawItemInList =
            self.ProductConfigurations.ItemConfigurationVM().filter(x => x.StoreItemRawId() === item.StoreItemRawId());
        if (RawItemInList?.length > 1) {
            ErrorManager()
            self.ProductConfigurations.ItemConfigurationVM.remove(item);
        } else {
          
            SendAjaxCall(item, "/Manufacturing/Home/GetDataItemRaw/" + item.StoreItemRawId()).then(
                (data) =>
                {
                    item.CurrentQty(data.QTY);
                });
        }
    }
    self.Save = function () {
        let message = ErrorManager();
        if (message == 1) {
            return;
        }
        SendAjaxCall(self, "/Manufacturing/Home/SaveManufacturing").then((data) => { RedirectOrShowError(data, self); });
    };


    self.LoadProductWithConfig = function () {
        let RawItemInList =
            self.ProductConfigurations().filter(x => x.Id() == self.SelectedProduct());
        if (RawItemInList?.length < 1) {
            let message = ErrorManager();
            if (message == 1) {
                return;
            }
            SendAjaxCall(self, "/Manufacturing/Home/GetConfigurationProduct/" + self.SelectedProduct())
                .then(data => {
                    self.ProductConfigurations.push(new ProductConfigurations(data));
                })
        }
        else {
            self.Messages.removeAll();
            self.Messages.push("تم اختيار هذا العنصر من قبل");
            $('#message').modal('show');
            self.ProductConfigurations.remove(data);
        }
    }

    function ErrorManager() {
        self.Messages.removeAll();
        const errs = ko.validation.group(self, { deep: true, live: true });
        if (errs().length > 0) {
            for (let i = 0; i < errs().length; i++) {
                self.Messages.push(errs()[i]);
            }
        }
        if (self.Messages().length > 0) {
            $('#message').modal('show');
            errs.showAllMessages();
            return 1;
        }
    }

};

/*
const Mapping = {
    'ProductConfigurations': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new ProductConfigurations(options.data);
        }
    },
    'ItemConfigurationVM': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new ItemConfigurationVM(options.data);
        }
    }
};


const ItemConfigurationVM = function (data) {
    const self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ItemConfigurationValidation(self);
    self.ShowEdit = ko.observable(true);
    self.EnableEdit = function () {
        self.ShowEdit() == true ? false : true;
    }
};

const ProductConfigurations = function (data) {
    const self = this;
    ko.mapping.fromJS(data, Mapping.ItemConfigurationVM, self);
    self.Messages = ko.observableArray([]);

    self.RemoveItem = function (data) {
       
        self.ItemConfigurationVM.remove(data);
    }

    self.EnableEdit = function (item) {
        item.ShowEdit() == true ? false : true;
    }
    self.UpdateItem = function (item) {

        let RawItemInList =
            self.ItemConfigurationVM().filter(x => x.StoreItemRawId()===item.StoreItemRawId());
        if (RawItemInList?.length > 1) {
            ErrorManager()
            self.ItemConfigurationVM.remove(item);
        }
        
    }

    self.AddItem = function () {

        let newLine = {
            Id: 0,
            StoreItemId: self.StoreItemVM.Id(),
            StoreItemRawId: 0,
            MinimumAmount: 1,
            UsedUsedQTY : 1,
           
        }
        console.log(self.ItemConfigurationVM());

        self.ItemConfigurationVM.push(new ItemConfigurationVM(newLine));
    }


    self.UpdateTotalRaw = function () {
       
        self.ItemConfigurationVM().forEach((element => {
            element.UsedQTY(element.MinimumAmount() * self.StoreItemVM.Quantity());
        }));
    }
    
};

const ManufacturingContainer = function (data) {
    const self = this;
    ko.mapping.fromJS(data, Mapping.ProductConfigurations, self);

    self.SelectedProduct = ko.observable(0);


    self.Messages = ko.observableArray([]);
    ManufacturingContainerValidate(self);

    self.Save = function () {
        ErrorManager();
        SendAjaxCall(self, "/Manufacturing/Manufacturing/GetProductConfiguration").then((data) => { RedirectOrShowError(data, self); });
    };


    self.LoadProductWithConfig = function () {
        let RawItemInList =
            self.ProductConfigurations().filter(x => x.Id() == self.SelectedProduct());
        if (RawItemInList?.length < 1) {
            ErrorManager();
            SendAjaxCall(self, "/Manufacturing/Manufacturing/GetConfigurationProduct/" + self.SelectedProduct())
                .then(data => {
                    self.ProductConfigurations.push(new ProductConfigurations(data));
                })
        }
        else {
            self.ProductConfigurations.remove(data);
            self.Messages.removeAll();
            self.Messages.push("تم اختيار هذا العنصر من قبل");
            $('#message').modal('show');
            self.ProductConfigurations.remove(data);
        }
    }

    function ErrorManager() {
        self.Messages.removeAll();
        const errs = ko.validation.group(self, { deep: true, live: true });
        if (errs().length > 0) {
            for (let i = 0; i < errs().length; i++) {
                self.Messages.push(errs()[i]);
            }
        }
        if (self.Messages().length > 0) {
            $('#message').modal('show');
            errs.showAllMessages();
            return;
        }
    }


};


*/