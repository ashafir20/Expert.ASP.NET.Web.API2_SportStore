/*
This Customer Controller is used to define the functions that will
support the views that present functionality to the customer and operation on the application models—both the
common model and the one that is specific to the customer client. Listing 7-14 shows the functions that I defined.
This is a lengthy file, and much of the code is responsible for sorting and filtering the common model objects that
represent the SportsStore products so they can be presented to the user.
*/

var setCategory = function (category) {
    customerModel.selectedCategory(category);
    filterProductsByCategory();
}

var setView = function (view) {
    customerModel.currentView(view);
}

var removeFromCart = function (productSelection) {
    customerModel.cart.remove(productSelection);
}

var addToCart = function (product) {
    var found = false;
    var cart = customerModel.cart();
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].product.Id == product.Id) {
            found = true;
            var count = cart[i].count + 1;
            customerModel.cart.splice(i, 1);
            customerModel.cart.push({
                count: count,
                product: product
            });
            break;
        }
    }

    if (!found) {
        customerModel.cart.push({ count: 1, product: product });
    }

    setView("cart");
}

var placeOrder = function () {
    var order = {
        Customer: model.username(),
        Lines: customerModel.cart().map(function (item) {
            return {
                Count: item.count,
                ProductId: item.product.Id
            }
        })
    };

    saveOrder(order, function () {
        setView("thankyou");
    });
}

model.products.subscribe(function(newProducts) {
    filterProductsByCategory();
    customerModel.productCategories.removeAll();
    customerModel.productCategories.push.apply(customerModel.productCategories,
        model.products().map(function(p) {
            return p.Category;
        })
        .filter(function(value, index, self) {
            return self.indexOf(value) === index; //get an array with unique values only
        }).sort());
});

customerModel.cart.subscribe(function (newCart) {
    customerModel.cartTotal(newCart.reduce(
    function (prev, item) {
        return prev + (item.count * item.product.Price);
    }, 0));
    customerModel.cartCount(newCart.reduce(
    function (prev, item) {
        return prev + item.count;
    }, 0));
});

var filterProductsByCategory = function () {
    var category = customerModel.selectedCategory();
    customerModel.filteredProducts.removeAll();
    customerModel.filteredProducts.push.apply(customerModel.filteredProducts,
    model.products().filter(function (p) {
        return category == null || p.Category == category;
    }));
}

$(document).ready(function() {
    getProducts();
});