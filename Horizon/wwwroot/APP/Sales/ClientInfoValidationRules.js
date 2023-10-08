const ValidateVM = function (self) {

    self.ClientName.extend({
        required: {
            params: true, message: "ادخل اسم العميل"
        }
    })
    self.ClientNameAr.extend({
        required: {
            params: true, message: "ادخل اسم العميل - عربي"
        }
    })

    self.Phone1.extend({
        required: {
            params: true, message: "ادخل تليفون العميل "
        },
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
    })

    self.Phone2.extend({
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
    })

    self.Phone3.extend({
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
    })

    self.Email.extend({
        email: {
            params: true, message: "برجاء ادخال بريد الكتروني صحيح "
        }
    })





  


    


}