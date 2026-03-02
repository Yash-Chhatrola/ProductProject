//Multi Value selected Dropdown code

$(document).ready(function () {

    // Initialize Select2 for all (Multi)
    $('#CountryMulti, #StateMulti, #CityMulti').select2({
        matcher: function (params, data) {
            if ($.trim(params.term) === '') {
                return data;
            }

            var searchTerms = params.term.toUpperCase().split(' ');
            var dataText = data.text.toUpperCase();
            var match = true;

            searchTerms.forEach(function (term) {
                if (dataText.indexOf(term) === -1) {
                    match = false;
                }
            });

            if (match) {
                return data;
            }
            return null;
        }
    });

    $("#CountryMulti").select2({
        placeholder: "---Select Countries---",
        allowClear: true,
        theme: "classic",
        closeOnSelect: false,     // ✅ keep open to select many
        width: "100%",
        templateResult: countryTemplateResult,
        templateSelection: countryTemplateSelection,
        escapeMarkup: function (m) { return m; } // allow HTML
    });

    // ✅ When open, focus search box safely
    $('#CountryMulti').on('select2:open', function () {
        syncAllCheckboxes();
        const el = document.querySelector('.select2-container--open .select2-search__field');
        if (el) el.focus();
    });
    // ✅ When one item selected -> check that item checkbox
    $("#CountryMulti").on("select2:select", function (e) {
        setCheckbox(e.params.data, true);
    });

    // ✅ When one item unselected -> uncheck that item checkbox
    $("#CountryMulti").on("select2:unselect", function (e) {
        setCheckbox(e.params.data, false);
    });

    $('#StateMulti,#CityMulti').on('select2:open', function () {
        const el = document.querySelector('.select2-container--open .select2-search__field');
        if (el) el.focus();
    });

    $('#StateMulti').select2({
        placeholder: "---Select State---",
        allowClear: true,
        theme: "classic",
        selectOnClose: false
    });

    $('#CityMulti').select2({
        placeholder: "---Select City---",
        allowClear: true,
        theme: "classic",
        selectOnClose: false
    });

    getCountryMulti();
    $('#StateMulti,#CityMulti').prop('disabled', true);

    $("#CountryMulti").on("select2:close", showStateMulti); // ✅ load only once, after done selecting
    $("#StateMulti").on("change", showCityMulti);

    $("#SavebtnMulti").on("click", function (e) {
        e.preventDefault();
        SubmitDataMulti();
        resetDropdownMulti('#StateMulti', "State");
        resetDropdownMulti('#CityMulti', "City");
        getCountryMulti();
    });
});

function formatCountryOption(option) {
    if (!option.id) return option.text; // placeholder

    // check selected or not
    var selected = $("#CountryMulti").val() || [];
    var checked = selected.includes(option.id) ? "checked" : "";

    var $html = $(`
        <div style="display:flex;align-items:center;gap:8px;">
            <input type="checkbox" ${checked} />
            <span>${option.text}</span>
        </div>
    `);
    return $html;
}

// ✅ Show selected values in input (normal text)
function formatCountrySelection(option) {
    return option.text;
}

function getCountryMulti() {
    showLoader();
    $.getJSON('/Dropdown/CountryMulti', function (data) {
        populateDropdownMulti('#CountryMulti', data, "Country");
    });
}

function showStateMulti() {
    const countryId = $(this).val(); // ✅ now this is ARRAY because multiple

    resetDropdownMulti('#StateMulti', "State");
    resetDropdownMulti('#CityMulti', "City");

    // ✅ REQUIRED CHANGE: for multi, check length
    if (countryId && countryId.length > 0) {
        showLoader();
        $.ajax({
            url: '/Dropdown/StateMulti',
            type: 'GET',
            data: { id: countryId },   // ✅ send array
            traditional: true,         // ✅ REQUIRED CHANGE
            success: function (data) {
                populateDropdownMulti('#StateMulti', data, "State");
                $('#StateMulti').prop('disabled', false);
            }
        });
    }
}

function showCityMulti() {
    const stateId = $(this).val();
    resetDropdownMulti('#CityMulti', "City");

    if (stateId) {
        showLoader();
        $.getJSON('/Dropdown/CityMulti', { id: stateId }, function (data) {
            populateDropdownMulti('#CityMulti', data, "City");
            $('#CityMulti').prop('disabled', false);
        });
    }
}

// Helper Functions (Multi Duplicate)
function populateDropdownMulti(selector, data, type) {
    let items = `<option value="">---Select ${type}---</option>`;
    $.each(data, function (i, item) {
        items += `<option value="${item.id}">${item.name}</option>`;
    });
    hideLoader();
    $(selector).html(items).trigger('change.select2');
}

function resetDropdownMulti(selector, type) {
    $(selector).empty()
        .append(`<option value="">---Select ${type}---</option>`)
        .prop('disabled', true)
        .trigger('change.select2');
}

function SubmitDataMulti() {
    showLoader();

    var countryId = $('#CountryMulti').val() || [];// ✅ ARRAY
    var stateId = parseInt($('#StateMulti').val(), 10) || 0;
    var cityId = parseInt($('#CityMulti').val(), 10) || 0;

    if (countryId == null || countryId.length === 0) {
        alert("Country is Null");
        return;
    }

    var payload = {
        CountryId: countryId.map(x => parseInt(x, 10)), // ✅ ARRAY (you must handle in backend if saving)
        StateId: stateId ?? 0,
        CityId: cityId ?? 0
    };

    $.ajax({
        url: '/Dropdown/MultiSaveData',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(payload),
        success: function (response) {
            hideLoader();
            console.log(response);
            loadLatestMulti();
        },
        error: function () {
            hideLoader();
            alert("Failed to save");
        }
    });
}
function loadLatestMulti() {
    $.getJSON('/Dropdown/LatestMulti', function (data) {
        if (!data) {
            $('#MultiResultBox').html("<b>No record found</b>");
            return;
        }

        var html = `
            <div class="alert alert-info">
                <b>Saved (Multi)</b><br/>
                Countries: ${data.countryName}<br/>
                State: ${data.stateName}<br/>
                City: ${data.cityName}<br/>
                Date: ${data.dataCreated}
            </div>
        `;

        $('#MultiResultBox').html(html);
    });
}

//-------------------------After all code for show Checkbox-------------------------
// ===== Template: show checkbox in list =====
function countryTemplateResult(data) {
    if (!data.id) return data.text; // placeholder

    return `
        <div style="display:flex;align-items:center;gap:8px;">
            <input type="checkbox" />
            <span>${data.text}</span>
        </div>
    `;
}

// ===== Template: show selected text in input =====
function countryTemplateSelection(data) {
    return data.text;
}

// ===== Set checkbox checked/unchecked for one item =====
function setCheckbox(data, checked) {
    // result DOM element id is stored here
    if (data && data._resultId) {
        const $row = $("#" + data._resultId);
        $row.find("input[type='checkbox']").prop("checked", checked);
    }
}

// ===== Sync all checkboxes based on aria-selected =====
function syncAllCheckboxes() {
    $(".select2-results__option[role='option']").each(function () {
        const selected = $(this).attr("aria-selected") === "true";
        $(this).find("input[type='checkbox']").prop("checked", selected);
    });
}