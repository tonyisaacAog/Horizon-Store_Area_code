const ValidateCreateUser = function (self) {

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

    self.Phone.extend({
      
         required: {
            params: true, message: "ادخل تليفون المستخدم "
        },
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
    })

    self.Password.extend({
        required: {
            params: true, message: "برجاء ادخال كلمة المرور "
        }
    })

    self.Mobile.extend({
        number: {
            params: true, message: "برجاء ادخال ارقام فقط "
        }
      
    })
}

