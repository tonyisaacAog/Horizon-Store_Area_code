const ValidateVM = function (self) {

    self.ItemName.extend({
        required: {
            params: true, message: "ادخل اسم المادة الخام"
        }
    })

    self.ItemNameAr.extend({
        required: {
            params: true, message: "ادخل اسم المادة الخام عربى"
        }
    })


    self.RawItemTypeId.extend({
        required: {
            params: true, message: "اختر نوع المادة"
        }
    })

    
    self.WarningLimit.extend({
        number: true,
        min: 0
    })
   
}