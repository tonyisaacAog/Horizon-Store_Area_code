const ValidateVM = function (self) {

    self.LocationName.extend({
        required: {
            params: true, message: "ادخل اسم المخزن "
        }
    })
}