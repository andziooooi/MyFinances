$(document).ready(function () {
    initializeEventHandlers();
});
function initializeEventHandlers() {
    $(document).on("click", ".choose-btn", handleChooseClick);
    $(document).on("click", ".edit-btn", handleEditClick);
    $(document).on("change", "#category", handleCategoryChange);
    $(document).on("submit", "#transactionForm", handleFormSubmit);
}
//sets date in EditModal
function handleEditClick() {
    var selectedDate = $(this).data("day");
    $("#hiddenDate").val(selectedDate);
}
//choosing new transaction type
function handleChooseClick() {
    var selectedType = $(this).data("type");
    loadTransactionModal(selectedType);
}
//Handles the category change event
function handleCategoryChange() {
    var selectedCategory = $(this).val();
    var specialCategoryId = "4"; //id of salary category

    //clear inputs
    $("#amount").val('');
    $("#workedHours").val('');
    $("#payPerHour").val('');
    $(".error-label").hide();

    if (selectedCategory === specialCategoryId) {
        $("#amount").hide().removeAttr("required");
        $("#workedHours, #payPerHour").attr("type", "text").attr("required", true).show();
        $("label[for='amount']").hide();
        $("label[for='workedHours'], label[for='payPerHour']").show();
    } else {
        $("#amount").show().attr("required", true);
        $("#workedHours, #payPerHour").attr("type", "hidden").removeAttr("required");
        $("label[for='amount']").show();
        $("label[for='workedHours'], label[for='payPerHour']").hide();
    }
}
// Loads the transaction modal with the selected date and type
function loadTransactionModal(type) {
    let selectedDate = $("#hiddenDate").val();
    let selectedType = type
    let rawDate = selectedDate.split(' ')[0];
    let formattedDate = rawDate.split('.').reverse().join('-');
    console.log("wysłana data:", formattedDate)
    $.get("/Calendar/LoadTransactionModal", { date: formattedDate, type: selectedType }, function (data) {
        $("#modalContainer").html(data);
        $("#addTransactionModal").modal("show");
    });
}
// Handles form submission via AJAX
function handleFormSubmit(e) {
    e.preventDefault();

    $(".error-label").hide();

    $.post("/Calendar/AddEntry", $(this).serialize(), function (response) {
        if (response.success) {
            $("#addTransactionModal").modal("hide");
            location.reload();
        } else if(response.errors){
                for (let field in response.errors) {
                    let errorMessages = response.errors[field];
                    $(`#${field}Error`).text(errorMessages).show();
                }
        }
    }).fail(function (xhr) {
        console.error("Błąd podczas zapisu:", xhr);
    });
}