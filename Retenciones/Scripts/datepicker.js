jQuery.validator.methods["date"] = function (value, element) { return true; } 

//DATEPICKER       
$(document).ready(function () {
    $('.datepicker').datepicker(
        {
            dateFormat: 'dd/mm/yy'
        });
});

//DATETIMEPICKER       
$(document).ready(function () {
    $('.datetimepicker').datetimepicker(
        {
            dateFormat: 'dd/mm/yy',
            timeFormat: 'HH:mm:ss',
        });
});

//TIMEPICKER       
$(document).ready(function () {
    $('.timepicker').timepicker(
        {
            timeFormat: 'HH:mm:ss',
        });
});