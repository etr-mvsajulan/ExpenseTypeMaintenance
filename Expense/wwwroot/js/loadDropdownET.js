

$(document).ready(function () {
    // Make an AJAX request to get the list of ExpenseTypes
    $.get("/ExpenseDetails/LoadExpensetypes", function (data) {
        // Iterate through the data and populate the dropdown
        var dropdown = $("#expenseTypeDropdown");
        dropdown.empty(); // Clear existing options

        $.each(data, function (key, entry) {
            dropdown.append($('<option></option>').attr('value', entry.Code).text(entry.Description));
        });
        // Set the selected option based on the model's ExpenseTypeID
        dropdown.val(expenseTypeID);
    });
});

$('#expenseTypeDropdown').change(function () {
    var selectedValue = $(this).val();
    // Set the selected value to the hidden input (ExpenseTypeID)
    $('#ExpenseTypeID').val(selectedValue);
});