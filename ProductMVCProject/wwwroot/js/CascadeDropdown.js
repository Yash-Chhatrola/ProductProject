//$(document).ready(function () {
//    $('#State').prop('disabled', true);
//    $('#City').prop('disabled', true);
//    GetCountry();
//});

//function GetCountry() {
//    $.ajax({
//        url: '/Dropdown/Country',
//        success: function (result) {
//            $('#Country').empty();// Clear existing options
//            $('#Country').append('<option value="0">--Select Country--</option>');
//            if (result && result.length > 0) {
//                $.each(result, function (i, data) {
//                    $('#Country').append('<option value=' + data.id + '>' + data.name + '</option > ');
//                });
//            }
//            else {
//                $('#Country').append('<option value="0">No Countries available</option>');
//            }
//        }
//    });
//}


//$('#Country').change(function () {
//    var id = $(this).val();

//    // Check if the Country dropdown has a valid value
//    if (!id || id === "0") {
//        $('#State').empty(); // Clear State options
//        $('#State').append('<option value="0">--Select State--</option>');
//        $('#State').prop('disabled', true); // Disable State dropdown
//        $('#City').empty(); // Clear City options
//        $('#City').append('<option value="0">--Select City--</option>');
//        $('#City').prop('disabled', true); // Disable City dropdown
//    } else {
//        $('#State').empty();
//        $('#State').append('<option value="0">--Select State--</option>');

//        $.ajax({
//            url: '/Dropdown/State?id=' + id,
//            success: function (result) {
//                $('#State').prop('disabled', false); // Enable State dropdown
//                if (result && result.length > 0) {
//                    $.each(result, function (i, data) {
//                        $('#State').append('<option value="' + data.id + '">' + data.name + '</option>');
//                    });
//                }
//                else {
//                    $('#State').append('<option value="0">No States available</option>');
//                }
//            }
//        });
//    }
//});

//$('#State').change(function () {
//    var id = $(this).val();
//    // Check if the State dropdown has a valid value
//    if (!id || id === "0") {
//        $('#City').empty(); // Clear City options
//        $('#City').append('<option value="0">--Select City--</option>');
//        $('#City').prop('disabled', true); // Disable City dropdown
//    } else {
//        $('#City').empty();
//        $('#City').append('<option value="0">--Select City--</option>');

//        $.ajax({
//            url: '/Dropdown/City?id=' + id,
//            success: function (result) {
//                $('#City').prop('disabled', false); // Enable City dropdown
//                if (result && result.length > 0) {
//                    $.each(result, function (i, data) {
//                        $('#City').append('<option value="' + data.id + '">' + data.name + '</option>');
//                    });
//                }
//                else {
//                    $('#City').append('<option value="0">No Cities available</option>');
//                }
//            }
//        });
//    }
//});


$(document).ready(function () {
    $('#State, #City').prop('disabled', true);
    GetCountry();
});

function GetCountry() {
    $.ajax({
        url: '/Dropdown/Country',
        method: 'GET',
        success: function (result) {
            renderOptions('#Country', result, '--Select Country--');
        }
    });
}

$('#Country').change(function () {
    var id = $(this).val();

    // Always reset child dropdowns immediately
    resetDropdown('#State', '--Select State--');
    resetDropdown('#City', '--Select City--');

    if (id && id !== "0") {
        $.ajax({
            url: '/Dropdown/State',
            data: { id: id },
            success: function (result) {
                renderOptions('#State', result, '--Select State--');
            }
        });
    }
});

$('#State').change(function () {
    var id = $(this).val();
    resetDropdown('#City', '--Select City--');

    if (id && id !== "0") {
        $.ajax({
            url: '/Dropdown/City',
            data: { id: id },
            success: function (result) {
                renderOptions('#City', result, '--Select City--');
            }
        });
    }
});

/**
 * Optimized DOM manipulation: 
 * Builds a long string first, then injects it into the DOM in ONE operation.
 */
function renderOptions(selector, data, defaultText) {
    var $el = $(selector);
    var html = '<option value="0">' + defaultText + '</option>';

    if (data && data.length > 0) {
        // Build the string in a loop (fast)
        for (var i = 0; i < data.length; i++) {
            html += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
        }
        $el.prop('disabled', false);
    } else {
        html = '<option value="0">No data available</option>';
        $el.prop('disabled', true);
    }

    // Single DOM update (Performance winner)
    $el.html(html);
}

function resetDropdown(selector, defaultText) {
    $(selector).html('<option value="0">' + defaultText + '</option>').prop('disabled', true);
}