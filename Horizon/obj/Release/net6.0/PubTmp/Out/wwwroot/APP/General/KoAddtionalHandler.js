ko.bindingHandlers.numericText = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            precision = ko.utils.unwrapObservable(allBindingsAccessor().precision)
                || ko.bindingHandlers.numericText.defaultPrecision;
        let formattedValue = 0;
        if (value % 1 !== 0)
            formattedValue = $.number(value, 2);
        else
            formattedValue = $.number(value);
        ko.bindingHandlers.text.update(element, function () { return formattedValue; });
       
    },
  
    defaultPrecision: 2
};

ko.bindingHandlers.date = {
    // https://disqus.com/home/discussion/jasonmitchellcom/binding_and_formatting_dates_using_knockout_and_moment_js/#comment-1374417079
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        ko.utils.registerEventHandler(element, 'change', function () {
            var value = valueAccessor();
            if (element.value !== null && element.value !== undefined && element.value.length > 0) {
                value(element.value);
            }
            else {
                value('');
            }
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || 'LLL';

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && valueUnwrapped.length > 0) {
            output = moment(valueUnwrapped, "MM/DD/YYYY").format("DD/MM/YYYY");
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};

ko.bindingHandlers.longDate = {
    // https://disqus.com/home/discussion/jasonmitchellcom/binding_and_formatting_dates_using_knockout_and_moment_js/#comment-1374417079
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        ko.utils.registerEventHandler(element, 'change', function () {
            var value = valueAccessor();
            if (element.value !== null && element.value !== undefined && element.value.length > 0) {
                value(element.value);
            }
            else {
                value('');
            }
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || 'LLL';

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && valueUnwrapped.length > 0) {
            output = moment(valueUnwrapped, "DD, MM d, yy").format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};