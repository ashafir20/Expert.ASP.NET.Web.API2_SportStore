var customerModel = {
    productCategories: ko.observableArray([]), //This property is an array of the product category names, which I use to allow the customer to filter products so that only those in a given category are shown.
    filteredProducts: ko.observableArray([]), //This property contains the set of products that belong to the currently selected category.
    selectedCategory: ko.observable(null), //This property specifies the currently selected category and is used to filter the products shown to the customer through the filteredProducts property.
    cart: ko.observableArray([]), //This property represents the customer’s shopping cart and contains details of the products they have selected and the quantity of each.
    cartTotal: ko.observable(0), //This property specifies the total value of the products in the cart.
    cartCount: ko.observable(0), //This property specifies the number of products in the cart.
    currentView: ko.observable("list") //This property specifies which view the custom should be shown.
}