const ValidatePurchaseOrder = function (self) {


    self.DeliveryDate.extend({
        required: {
            params: true, message: "برجاء ادخال تاريخ التوريد "
        }
    });


    self.PurchaseOrderDate.extend({
        required: {
            params: true, message: "برجاء ادخال تاريخ امر الانتاج "
        }
    });


    self.TotalAmount.extend({
        required: {
            params: true, message: "برجاء ادخال اكبر من 1 "
        }
    });

    self.SupplierId.extend({
        required: {
            params: true, message: "برجاء اختيار المورد "
        }
    });

    self.PurchaseOrderDetails.extend({
        validation: {
            validator: function (val) {
                return val.length >= 1; // Replace 5 with your desired minimum length
            },
            message: "امر الانتاج يجب ان يحوى على الاقل منتج واحد." // Replace with your desired error message
        }
    });

};


const ValidatePurchaseOrderDetails = function (self) {
  
    self.StoreItemId.extend({
        required: {
            params: true, message: "برجاء اختيار المنتج"
        }
    });
    self.StoreItemAmount.extend({
        required: {
            params: true, message: "برجاء اضافة الكيمة"
        },
        number: true,
        min: 0.1
    });
   

};

