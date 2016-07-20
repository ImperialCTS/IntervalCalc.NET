// Write your Javascript code.

// Initiating the interval sliders
$(function () {
    $(".interval-slider").each(function (i) {
        var slider = this;
        var minfield = '#' + $(this).data('slider-minfield');
        var maxfield = '#' + $(this).data('slider-maxfield');
        var isupdating = false;
        var getvalue = function () {
                if (isupdating) return;
                isupdating = true;
                $(minfield).val($(this).slider("values", 0));
                $(maxfield).val($(this).slider("values", 1));
                isupdating = false;
        };
        $(this).slider({
            range: true,
            min: $(this).data('slider-min'),
            max: $(this).data('slider-max'),
            step: $(this).data('slider-step'),
            values: [ $(minfield).val(), $(maxfield).val() ],
            slide: getvalue,
            change: getvalue
        });
        var setvalue = function() {
            if (isupdating) return;
            isupdating = true;
            $(slider).slider('values', [ $(minfield).val(), $(maxfield).val() ]);
            isupdating = false;
        };
        $(minfield).change(setvalue);
        $(maxfield).change(setvalue);
    });

    $(".interval-view-slider").each(function (i) {
        $(this).slider({
            range: true,
            disabled: true,
            min: $(this).data('slider-min'),
            max: $(this).data('slider-max'),
            values: [$(this).data('slider-a'), $(this).data('slider-b')]
        });
    });
});