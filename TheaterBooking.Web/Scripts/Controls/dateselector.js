/// <reference path="../jquery/jquery-1.4.4.js" />
/// <reference path="../jquery/jquery-ui.js" />
$(function () {

    $("form").submit(function () {
        $(".ds_ptr_value").each(function () {

            var container = $(this).parents(".ds_ptr_container");

            var currentculture = container.attr("data-currentculture");
            var convertURL = container.attr("data-convertorurl");

            var year = container.find(".ds_ptr_year").val();
            var month = container.find(".ds_ptr_month").val();
            var day = container.find(".ds_ptr_day").val();

            $(this).val(getGregorianDate(year, month, day, currentculture, convertURL));
        });
    });


    $(".ds_ptr_year, .ds_ptr_month").change(function () {
        var container = $(this).parents(".ds_ptr_container");
        var year = container.find(".ds_ptr_year").val();
        var month = container.find(".ds_ptr_month").val();

        var daysInMonthURL = container.attr("data-daysinmonthurl");
        var currentculture = container.attr("data-currentculture");

        var loading = container.find(".ds_ptr_loading");
        loading.show();

        $.ajax({
            url: daysInMonthURL,
            data: "year=" + year + "&month=" + month + "&culture=" + currentculture,
            success: function (daysInMonth) {
                var daysSelect = container.find(".ds_ptr_day");
                daysSelect.empty();
                for (var i = 1; i <= daysInMonth; i++) {
                    daysSelect.append("<option>" + i + "</option>");
                }

                loading.hide();
            }
        });

    });
});


function getGregorianDate(year, month, day, currentCulture, convertURL) {
    var params = '{Year: "' + year + '", Month: "' + month + '", Day: "' + day + '",  Culture: "' + currentCulture + '" }';

    var result = $.ajax({
        url: convertURL,
        data: "paramsJSON=" + params,
        async: false
    }).responseText;

    return result;
}