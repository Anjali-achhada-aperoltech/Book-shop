$(document).ready(function () {

    function updateWishlistCount() {
        $.ajax({
            url: "/WishList/GetWishlistCount", // API to get count
            type: "GET",
            dataType: "json",
            success: function (response) {
                $("#wishlist-count").text(response.count); // Update count in header
            },
            error: function () {
                console.error("Error fetching wishlist count.");
            }
        });
    }
    updateWishlistCount();

    function updateCartCount() {
        $.ajax({
            url: "/Cart/GetData",
            type: "GET",
            dataType: "json",
            success: function (response) {
                if (response.count !== undefined) {
                    // Update the cart count in the header
                    $("#cartlist-count").text(response.count);
                }
            },
            error: function () {
                console.error("Error fetching cart count.");
            }
        });
    }

    // Call this function when the page loads to get the initial count
    updateCartCount();

    

    });

