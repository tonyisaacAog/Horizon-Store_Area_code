const ValidateSales = function (self) {

    self.SaleInfo.SalesDate.extend({
        required: {
            params: true, message: "برجاء تحديد التاريخ"
        }
    })

    self.SaleInfo.StoreLocationId.extend({
        required: {
            params: true, message: "برجاء تحديد مخزن الصرف"
        }
    })


    self.SaleDetails.extend({
        validation: {
            validator: function (val) {
                return val.length >= 1; // Replace 5 with your desired minimum length
            },
            message: "يجب وضع المنتجات التى تريد بيعها " // Replace with your desired error message
        }
    });
}



const ValidateSaleDetails = function (self) {

    self.StoreItemId.extend({
        required: {
            params: true, message: "برجاء اختيار المنتج"
        }
    });
    self.QTY.extend({
        required: {
            params: true, message: "برجاء اضافة الكيمة"
        },
        number: true,
        min:0.1
    })
    self.UnitPrice.extend({
        required: {
            params: true, message: "برجاء اضافة السعر"
        },
        number: true,
        min: 0.1
    })
}
  


    

