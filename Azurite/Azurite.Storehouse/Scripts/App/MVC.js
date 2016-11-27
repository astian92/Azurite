var MVC = {};

var MagicStrings = (function () {
    this.CreateController = function (controllerName) {
        var controller = new Controller(controllerName);
        MVC[controllerName] = controller;

        return controller;
    }

    var users = CreateController("Users");
    users.CreateAction("GetUsers");
    users.CreateAction("Edit");
    users.CreateAction("Delete");

    var customers = CreateController("Customers");
    customers.CreateAction("GetCustomers");
    customers.CreateAction("Details");

    var categories = CreateController("Categories");
    categories.CreateAction("GetCategories");
    categories.CreateAction("Edit");
    categories.CreateAction("Delete");

    var products = CreateController("Products");
    products.CreateAction("GetProducts");
    products.CreateAction("Edit");
    products.CreateAction("Delete");
    products.CreateAction("GetCategoryAttributes");

    var orders = CreateController("Orders");
    orders.CreateAction("GetOrders");
    orders.CreateAction("Details");

    var dashboard = CreateController("Dashboard");
    dashboard.CreateAction("YearIncome");
    dashboard.CreateAction("MonthIncome");
    dashboard.CreateAction("WeeklyIncome");
    dashboard.CreateAction("SoldItems");
    
  
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
