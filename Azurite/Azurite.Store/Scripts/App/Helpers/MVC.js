var MVC = {};

var MagicStrings = (function () {
    this.CreateController = function (controllerName) {
        var controller = new Controller(controllerName);
        MVC[controllerName] = controller;

        return controller;
    }

    var categories = CreateController("Categories");
    categories.CreateAction("GetBaseCategories");
    categories.CreateAction("GetSubCategories");
    categories.CreateAction("GetCategoryAttr");

    var products = CreateController("Products");
    products.CreateAction("GetCategoryProducts");

    var shoppingCart = CreateController("ShoppingCart");
    shoppingCart.CreateAction("Products");
    shoppingCart.CreateAction("AddProduct");

    return {
        CreateController: CreateController,
    }
}());

function Controller(name) {
    this.Name = name;

    this.CreateAction = function (actionName) {
        this[actionName] = actionName;
        this[actionName + "Full"] = "/" + this.Name + "/" + actionName;

        return this[actionName];
    }
}
