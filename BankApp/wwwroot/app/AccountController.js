
app.controller("myCtrl", function ($scope, $http, apiCustomerUrl, apiAccountUrl) {

    $scope.customers = [];

    $http.get(apiCustomerUrl)
        .then(function (response) {
            $scope.customers = response.data;
        }, function (error) {
            alert('Error getting user info: ' + error.data);
        });

    $scope.selectedCustomer = null;
    $scope.showTransactions = false;
    $scope.showTransactionSection = false;

    $scope.selectCustomer = function (customer) {
        $scope.selectedCustomer = customer;
        $scope.showTransactionSection = false;

        $http.get(apiAccountUrl + customer.id)
            .then(function (response) {
                $scope.selectedCustomer.account = response.data;
                $scope.showTransactionSection = response.data.showTransaction;
            }, function (error) {
                alert('Error getting user info: ' + error.data);
            });

    };

    $scope.addAmount = function (customer) {
        var amount = parseFloat(prompt("Enter amount:"));
        if (isNaN(amount) || amount == 0) {
            alert("Invalid amount!");
            return;
        }

        $http.post(apiAccountUrl, {
            CustomerId: customer.id,
            InitialCredit: amount
        })
            .then(function (response) {
                customer.account = {
                    id: response.data.id,
                    balance: response.data.balance,
                    transactions: response.data.transactions,
                };
                customer.hasAccount = true;
                $scope.showTransactionSection = response.data.showTransaction;
                alert('transaction created successfully!');
            }, function (error) {
                alert('Error adding amount: ' + error.data);
            });
    };

    $scope.addAccount = function (customer) {
        var confirmCreate = confirm("Are you sure you want to create an account for " + customer.customerName + "?");
        if (confirmCreate) {
            $http.post(apiAccountUrl, {
                CustomerId: customer.id,
                InitialCredit: 0
            })
                .then(function (response) {
                    customer.account = {
                        id: response.data.id,
                        balance: 0,
                        transactions: [],
                    };
                    customer.hasAccount = true;
                    $scope.showTransactionSection = false;
                    alert('Account created successfully!');
                }, function (error) {
                    alert('Error creating account: ' + error.data);
                });
        }
    };
}); 