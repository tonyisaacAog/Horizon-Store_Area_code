const ValidateVM = function (self) {

    self.MeasureUnitName.extend({
        required: {
            params: true, message: "ادخل اسم وحدة القياس"
        }
    })
    self.ECode.extend({
        pattern: {
            params: /^.{5}$/, message: "يجب ان تدخل 5 احرف فقط"
        }
    })
}