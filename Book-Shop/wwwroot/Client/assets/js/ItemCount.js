$(document).ready(function () {

    function updateWishlistCount() {
        $.ajax({
            url: "/WishList/GetWishlistCount", // API to get wishlist count
            type: "GET",
            dataType: "json",
            success: function (response) {
                $("#wishlist-count").text(response.count || 0); // Ensure 0 if undefined
            },
            error: function () {
                console.error("Error fetching wishlist count.");
            }
        });
    }

    function updateCartCount() {
        $.ajax({
            url: "/Cart/GetData", // API to get cart count
            type: "GET",
            dataType: "json",
            success: function (response) {
                $("#cartlist-count").text(response.count || 0); // Ensure 0 if undefined
            },
            error: function () {
                console.error("Error fetching cart count.");
            }
        });
    }

    // Call both functions once the page loads
    updateWishlistCount();
    updateCartCount();
});
