const RecordStatus = {
    Added:0,
    Updated:1,
    Deleted:2,
    UnChanged:3
}

const Mapping = {
    'ItemConfigurationVMs': {
        key: function (ro) {
            return ko.utils.unwrapObservable(ro.Id);
        },
        create: function (options) {
            return new ItemConfigurationVMs(options.data);
        }
    },

};

const ItemConfigurationVMs = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidateConfigurationItem(self);   
    self.RemoveItem = function () {
        self.RecordStatus(RecordStatus.Deleted);
    }
   
}

const ConfigureContainer = function (data) {
    var self = this;
    ko.mapping.fromJS(data, Mapping, self);
    ValidateStoreItem(self);
    self.Messages = ko.observableArray([]);

    self.AddItem = function () {

        let newLine = {
            Id: 0,
            StoreItemId: self.StoreItemVM.Id(),
            StoreItemRawId: 0,
            MinimumAmount: 0,
            RecordStatus: RecordStatus.Added
        }


        self.ItemConfigurationVMs.push(new ItemConfigurationVMs(newLine));
    }

    self.UpdateItem = function (item) {

        let RawItemInList =
            self.ItemConfigurationVMs().filter(x => x.StoreItemRawId() ===
                item.StoreItemRawId());
        if (RawItemInList?.length<=1) {
            if (item.RecordStatus() !== RecordStatus.Added)
                        self.RecordStatus(RecordStatus.Updated);
        }
        else {
            self.ItemConfigurationVMs.remove(item);
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
        SendAjaxCall(self, "/Store/ItemConfiguration/SaveConfiguration").then((data) =>
        { RedirectOrShowError(data, self) })    
    }   
};
