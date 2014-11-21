var productUrl = "/api/products/";


/*
Page 138
The code consists of three functions, getProducts, deleteProduct, and saveProduct, each of which sends an
Ajax call to the corresponding call to the server-side Products controller. Note that these functions exist solely to map
server-side data to and from the client-side model.
*/

var getProducts = function () {
    sendRequest(productUrl, "GET", null, function(data) {
        model.products.removeAll();
        model.products.push.apply(model.products, data);
    });
};

var deleteProduct = function(id) {
    sendRequest(productUrl + id, "DELETE", null, function() {
        model.products.remove(function(item) {
            return item.Id == id;
        });
    });
};

var saveProduct = function (product, successCallback) {
    sendRequest(productUrl, "POST", product, function () {
        getProducts();
        if (successCallback) {
            successCallback();
        }
    });
}

