const RedirectOrShowError = function (data, self) {
    if (data.newLocation !== null && data.newLocation !== undefined) {
        window.location = data.newLocation;

    }
    else {
        ShowError(self, data);
    }
    
}

const ShowTransDetail = function (data, self) {
    self.Trans.TransId(data.feed.TransId);
    self.Trans.UserName(data.feed.UserName);
    self.Trans.TransDate(data.feed.TransDate);
    self.Trans.ActionDate(data.feed.ActionDate);
    self.Trans.TransDesc(data.feed.TransDesc);
    self.Trans.TotalDebit(data.feed.TotalDebit);
    self.Trans.TotalCredit(data.feed.TotalCredit);
    self.Trans.TranactionDetails.removeAll();
    data.feed.TranactionDetails.forEach(x => {
        self.Trans.TranactionDetails.push(x);
    });
    $('#journalTrans').modal('show');
}

const ShowError = function (self, data) {
    if (data.errors !== null && data.errors !== undefined) {

        self.Messages(data.errors);
        $('#message').modal('show');
    }
}

const ClearModel = function (self) {
    for (var prop in self.Entity) {

        if (self.Entity[prop] === self.Entity["Id"]) {
            self.Entity[prop]("0");
        }
        else if (typeof (self.Entity[prop]) === 'object') {
            for (var p in self.Entity[prop]) {
                HandelProppertyValue(self.Entity[prop][p]);
            }
        }
        else {

            HandelProppertyValue(self.Entity[prop]);

        }
    }
}

const HandelProppertyValue = function (prop) {
    if (typeof (prop()) === "boolean") {
        prop(false);
    }
    else {
        prop(null);
    }
}

const ShowListOrShowErrorWithMapping = function (data, self, TypeList) {
    if ((data.OK !== null || data.OK !== undefined) && data.OK === false) {

        self.Messages(data.Messages);
        self.IsMessageAreaVisible(true);
    }

    else {
        DestroyDataTable();
        self.TypesList.removeAll();
        BuildArray(data.EntityList, self.TypesList, TypeList)
        DataTableWithFilter();

        for (var prop in self.Entity) {
            self.Entity[prop](null);
        }

        var myModal = document.getElementById('DetailsArea');
        const modal = bootstrap.Modal.getInstance(myModal);
        modal.hide();
        self.validateNow(false);
    }

}

const ShowListOrShowError = function (data, self) {
    if ((data.Ok !== null || data.Ok !== undefined) && data.Ok===false) {
        self.Messages(data.Message);
    }
    else if (data.errors !== undefined && data.errors.length > 0)
    {
        self.Messages(data.errors);
    }
    else {
        DestroyDataTable();
        self.TypesList.removeAll();
        ko.mapping.fromJS(data.EntityList, TypeListMapping, self.TypesList);
        DataTableWithFilter();
        ClearModel(self);
         
        const modal = bootstrap.Modal.getInstance(document.getElementById('DetailsArea'));
        modal.hide();
       // self.validateNow(false);
    }

}

const BuildArray = function (data, ary, objectType) {
    if (data !== null || data !== undefined) {
        for (var i = 0; i < data.length; i++) {
            if (objectType !== undefined && objectType !== null)
                ary.push(new objectType(data[i]));
            else
                ary.push(data[i]);
        }
    }
}


const GetTotalFromArray = function (ary, prop) {
    var total = 0;
    for (var i = 0; i < ary.length; i++) {
        total += parseFloat(ary[i][prop]());
    }
    return total;
}

const BuildDropDown = (response, dropdown) => {
    dropdown.empty();
    $("<option />", {
        val: "",
        text: "Select ---"
    }).appendTo(dropdown);
    $.each(response, function (index, item) {
        $("<option />", {
            val: item.Value,
            text: item.Text
        }).appendTo(dropdown);
    });
}
