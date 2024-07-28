const ValidateStoreItem = function (self) {


    self.OrderConfigure.extend({
        validation: {
            validator: function (val) {
                return val.length >= 1; // Replace 5 with your desired minimum length
            },
            message: "يجب وضع مسلتزمات للمنتج " // Replace with your desired error message
        }
    });

}
const ValidateConfigurationItem = function (self) {
    self.StoreItemId.extend({
        required: {
            params: true, message: "برجاء اختيار النوع"
        },
       
    })
    self.Qty.extend({
        required: {
            params: true, message: "برجاء اضافة الكمية"
        },
        number: true,
        min: 0.1
     
    })
}
  


    

