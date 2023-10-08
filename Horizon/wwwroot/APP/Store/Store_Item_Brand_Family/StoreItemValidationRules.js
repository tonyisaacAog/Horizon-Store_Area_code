const ValidateVM = function (self) {

    self.ProductName.extend({
        required: {
            params: true, message: "ادخل اسم المنتج"
        }
    })

    self.FamilyId.extend({
        required: {
            params: true, message: "اختر نوع المنتج"
        }
    })

    self.StoreBrandId.extend({
        required: {
            params: true, message: "اختر ماركة المنتج"
        }
    })

    self.CurrentQty.extend({
        number: true,
        min: 0
    })
    self.DestroyedQty.extend({
        number: true,
        min: 0
    })
   
}