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

};



const ManufacturingContainer = function (data) {
    const self = this;
    ko.mapping.fromJS(data, Mapping, self);
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

}