const ValidateUpdateUser = function (self) {

    self.Name.extend({
        required: {
            params: true, message: "ادخل الاسم "
        }
    })
    self.UserName.extend({
        required: {
            params: true, message: "ادخل اسم المستخدم"
        }
    })

    self.Email.extend({
        email: {
            params: true, message: "برجاء ادخال بريد الكتروني صحيح "
        }
    })

    self.PhoneNumber.extend({
      
         required: {
            params: true, message: "ادخل تليفون المستخدم "
        },
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
    })

    self.Mobile.extend({
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
      
    })
}

