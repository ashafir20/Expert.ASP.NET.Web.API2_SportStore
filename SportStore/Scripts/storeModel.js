var model = {
    products: ko.observableArray([]), //This property is an observable array that will be used to store the product objects obtained from the server.
    orders: ko.observableArray([]), //This property is an observable array that will be used to store the order objects obtained from the server.
    authenticated: ko.observable(false), //This property will be set to true when a successful authentication request has been performed and will be false otherwise.
    username: ko.observable(null), //This property will be set to the username entered by the user
    password: ko.observable(null), //This property will be set to the password entered by the user.
    error: ko.observable(""), //This property is set to the error string that will be displayed to the user when an Ajax request fails.
    gotError: ko.observable(false) //This property is set to true when a request fails and false when a request succeeds. I will use this property to decide when to display error messages to the user.
};

$(document).ready(function () {
    ko.applyBindings();
    setDefaultCallbacks(function (data) {
        if (data) {
            console.log("---Begin Success---");
            console.log(JSON.stringify(data));
            console.log("---End Success---");
        } else {
            console.log("Success (no data)");
        }
        model.gotError(false);
    },
    function (statusCode, statusText) {
        console.log("Error: " + statusCode + " (" + statusText + ")");
        model.error(statusCode + " (" + statusText + ")");
        model.gotError(true);
    });
});