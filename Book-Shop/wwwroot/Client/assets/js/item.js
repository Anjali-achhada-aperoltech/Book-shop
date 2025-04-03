$(document).ready(function () {

    // ✅ Reusable AJAX function
    function sendAjaxRequest(url, data, resultDiv, successMessage, errorMessage) {
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            dataType: "json",
            beforeSend: function () {
                Swal.fire({
                    title: "Processing...",
                    text: "Please wait...",
                    allowOutsideClick: false,
                    didOpen: () => Swal.showLoading()
                });
            },
            success: function (response) {
                Swal.fire({
                    title: response.success ? "Success!" : "Failed!",
                    text: response.message,
                    icon: response.success ? "success" : "error",
                    confirmButtonText: "OK"
                });

                if (response.success) {
                    $(resultDiv).text(successMessage).fadeIn().delay(2000).fadeOut();
                }
            },
            error: function () {
                Swal.fire({
                    title: "Error!",
                    text: errorMessage,
                    icon: "error",
                    confirmButtonText: "OK"
                });
            }
        });
    }

    // ✅ Add to Cart - Form Submission
    $("#add-to-cart-form").on("submit", function (event) {
        event.preventDefault();
        var formData = $(this).serialize();
        sendAjaxRequest("/Cart/AddCart", formData, "#cart-result-message", "Item added to cart!", "Failed to add item to cart.");
    });

    // ✅ Fix: Wishlist should trigger on button click, NOT form submit
    $(".add-to-wishlist-btn").click(function () {
        var BookitemId = $(this).data("bookitemid"); // Get Book ID from button

        if (!BookitemId) {
            Swal.fire({
                title: "Error!",
                text: "Invalid product.",
                icon: "error",
                confirmButtonText: "OK"
            });
            return;
        }

        // Fix here: Add missing error message argument
        sendAjaxRequest("/WishList/AddWishList", { BookitemId: BookitemId }, "#wishlist-result-message", "Item added to wishlist!", "Failed to add item to wishlist.");
    });

});
