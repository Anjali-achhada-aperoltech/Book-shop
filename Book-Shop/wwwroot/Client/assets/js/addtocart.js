    $(document).ready(function () {
        $(".add-to-cart").click(function (event) {
            event.preventDefault(); // Prevent default navigation

            var productId = $(this).data("id"); // Get Product ID
            console.log(productId);

            $.ajax({
                url: "/Cart/AddItemInCart",
                type: "POST",
                contentType: "application/json", // Ensure JSON format
                data: JSON.stringify({ Id: productId }),
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: "success",
                            title: "Success",
                            text: response.message,
                            showConfirmButton: false,
                            timer: 2000 // Auto close after 2 seconds
                        });
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: response.message
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText); // Debug in console
                    Swal.fire({
                        icon: "error",
                        title: "",
                        text: "Please Login Then to Item add in Cart"
                    });
                }
            });
        });
        });
