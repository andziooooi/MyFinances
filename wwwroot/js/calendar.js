$(document).ready(function () {
    $(document).on("click", ".edit-btn", function () {
        var selectedDate = $(this).data("day"); 
        $("#hiddenDate").val(selectedDate); 
    });
});
$(document).ready(function () {
    $(document).on("click", ".choose-btn", function () {
        var selectedtype = $(this).data("type"); 
        $("#transactionType").val(selectedtype); 
    });
});
$(document).ready(function () {
    $(document).on("click", ".choose-btn", function () {
        var selectedType = $(this).data("type");
        $("#transactionType").val(selectedType);

        var categorySelect = $("#category");
        categorySelect.empty();
        categorySelect.append('<option value="" disabled selected>Wybierz kategorię</option>');

        var categories = selectedType == 0 ? expenseCategories : incomeCategories;

        categories.forEach(function (cat) {
            categorySelect.append('<option value="' + cat.id + '">' + cat.name + '</option>');
        });
    });
});