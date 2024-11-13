const ValidatePurchase = function (self) {

    self.PurchaseInfo.PurchasingDate.extend({
        required: {
            params: true, message: "برجاء تحديد التاريخ"
        }
    });
    self.PurchaseDetails.extend({
        validation: {
            validator: function (val) {
                return val.length >= 1; // Replace 5 with your desired minimum length
            },
            message: "فاتورة المشتريات يجب ان تحتوى على  مادة خام واحدة على الاقل." // Replace with your desired error message
        }
    });
}



const ValidatePurchaseDetails = function (self) {

    self.StoreItemId.extend({
        required: {
            params: true, message: "برجاء اختيار العنصر"
        }
    });
    self.Qty.extend({
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
  



const ValidatePurchaseStoreItemDetails = function (self) {

    self.StoreItemId.extend({
        required: {
            params: true, message: "برجاء اختيار المنتج"
        }
    });
    self.Qty.extend({
        required: {
            params: true, message: "برجاء اضافة الكمية"
        },
        number: true,
        min: 0.1
    })
    self.UnitPrice.extend({
        required: {
            params: true, message: "برجاء اضافة السعر"
        },
        number: true,
        min: 0.1
    })
}





