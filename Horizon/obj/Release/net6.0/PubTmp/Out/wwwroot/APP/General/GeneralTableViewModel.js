const itemvm = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
}
const GeneralTable = function (data) {
    var self = this;
    //ko.mapping.fromJS(data, {}, self);
   self.items = ko.observableArray([]);
   
    for (var i = 0; i < data.length; i++) {
        self.items.push(new itemvm(data[i]));
    }

};