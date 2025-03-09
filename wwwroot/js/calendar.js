var IsChanged = false;
$(document).ready(function () {
    initializeEventHandlers();
    console.log("Przeładowana");
});
function initializeEventHandlers() {
    $(document).on("click", ".choose-btn", handleChooseClick);
    $(document).on("click", ".edit-btn", handleEditClick);
    $(document).on("change", "#category", handleCategoryChange);
    $(document).on("submit", "#transactionForm", handleFormSubmit);
    $(document).on("click", ".delete-btn", handleDeleteClick);
    $(document).on("click", ".switch-modal-btn", handleModalSwitch);
    $(document).on("click", ".close-modal", handleListChangeClick);
}
//sets date in EditModal loading transactions to edit
function handleEditClick() {
    var selectedDate = $(this).data("day");
    $("#hiddenDate").val(selectedDate);

    let rawDate = selectedDate.split(' ')[0];
    let formattedDate = rawDate.split('.').reverse().join('-');
    let tbody = $("#editModal tbody");
    let dateHeader = $("#editModal h3")

    tbody.empty();
    dateHeader.empty();

    $.get("/Calendar/GetItemsByDate", { date: formattedDate }, function (data) {
        dateHeader.append(rawDate);
        if (data.length === 0) {
            tbody.append(`<tr><td colspan="4">Brak danych dla wybranej daty</td></tr>`);
        } else {
            console.log(data);
            data.forEach(item => {
                let AmountToDisplay = item.amount;
                let textclass = "text-success";
                if (item.category.type == 0) {
                    AmountToDisplay = -AmountToDisplay;
                    textclass = "text-danger";
                }
                tbody.append(`
                        <tr id="row-${item.id}">
                            <td>${item.category.name}</td>
                            <td class = "${textclass}">${AmountToDisplay}</td>
                            <td>
                                <button class="btn btn-danger delete-btn" data-id="${item.id}">Usuń</button>
                            </td>
                        </tr>
                    `);
            });
        }
    });
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
//Delete Transaction from db
function handleDeleteClick() {
    var itemId = $(this).data("id");

    $.post("/Calendar/Delete", { id: itemId }, function (response) {
        if (response.success) {
            $("#row-" + itemId).fadeOut();
            IsChanged = true;
        } else {
            alert(response.message);
        }
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
//Handles modal dependence
function handleModalSwitch() {
    var hideModal = $(this).data("hide");
    var showModal = $(this).data("show");

    $(hideModal).modal("hide"); 

    $(hideModal).on("hidden.bs.modal", function () {
        $(showModal).modal("show");
        $(hideModal).off("hidden.bs.modal");
    });
}
//reloads site when list of transaction was changed
function handleListChangeClick() {
    if (IsChanged) {
        location.reload();
    }
}