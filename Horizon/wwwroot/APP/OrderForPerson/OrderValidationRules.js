const ValidateOrder = function (self) {

    //self.TotalAmount.extend({

    //});
    self.Order.ClientName.extend({
        required: {
            params: true, message: "برجاء ادخال اسم العميل "
        }
    });
    self.Order.ClientPhone.extend({
        required: {
            params: true, message: "برجاء رقم التليفون "
        }
    });
    self.Order.OrderDate.extend({
        required: {
            params: true, message: "برجاء تحديد تاريخ امر الشعل "
        }
    });

    self.OrderDetail.extend({
        validation: {
            validator: function (val) {
                return val.length >= 1; // Replace 5 with your desired minimum length
            },
            message: "امر الشغل يجب ان يحوى على الاقل منتج واحد." // Replace with your desired error message
        }
    });

};


const ValidateOrderDetails = function (self) {
  
    self.ProductId.extend({
        required: {
            params: true, message: "برجاء اختيار المنتج"
        }
    });
    self.QTY.extend({
        required: {
            params: true, message: "برجاء اضافة الكيمة"
        },
        number: true,
        min: 0.1
    });
    self.UnitPrice.extend({
        required: {
            params: true, message: "برجاء اضافة السعر"
        },
        number: true,
        min: 0.1
    });

};


//const ValidateOrderConfigure = function (self) {
//    self.StoreItemId.extend({
//        required: {
//            params: true, message: "برجاء اختيار المادة"
//        }
//    });
//    self.Qty.extend({
//        required: {
//            params: true, message: "برجاء اضافة الكيمة"
//        },
//        number: true,
//        min: 0.1
//    });
//    self.Descrpition.extend({
//        required: {
//            params: true, message: "برجاء ادخال ملاحظات اضافة المادة"
//        }
//    });
//};