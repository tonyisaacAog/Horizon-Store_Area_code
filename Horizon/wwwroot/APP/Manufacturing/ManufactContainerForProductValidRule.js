const ManufacturingContainerValidate = function (self) {
   
    self.ManufacturingInfoVM.BatchDate.extend({
        required: {
            params: true, message: "برجاء اختيار التاريخ"
        }
    })
   
    self.StoreLocationId.extend({
        required: {
            params: true, message: "برجاء اختيار المخزن"
        } 
    })
    self.ProductConfigurations.ItemConfigurationVM.extend({
        validation: {
            validator: function (val) {
                return val.length >= 1; // Replace 5 with your desired minimum length
            },
            message: "يجب ان يكون هناك مواد خام للمنتج للتصنيع" // Replace with your desired error message
        }
    })
    self.ProductConfigurations.StoreItemVM.Quantity.extend({
        number: true,
        min: 1
    })
}

const ItemConfigurationValidation = function (self) {
    self.StoreItemRawId.extend({
        required: {
            params: true, message: "برجاء اختيار النوع"
        },

    })
    self.MinimumAmount.extend({
        required: {
            params: true, message: "برجاء اضافة الكمية"
        },
        number: true,
        min: 0.1
    })

    self.UsedQTY.extend({
       number: true,
        min: 0.1,
        validation: {
            validator: function (val, otherval) {
                return parseFloat(val) > parseFloat(otherval)
            },
            message: "لا يمكن أن تكون أكبر من الكمية المتاحة " + self.CurrentQty(),
            params: self.CurrentQty()
        },
       
    })
    

}