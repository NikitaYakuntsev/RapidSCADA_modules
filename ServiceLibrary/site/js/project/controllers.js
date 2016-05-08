function DeviceListCtrl($scope) {}

function ChooseCtrl ($scope, $http) {
    //getSCADAList(SCADAListCallback);


    $scope.errFlag = false;
    $scope.errorMsg = "test";
    $scope.arr = [1,2,3];

    function SCADAListCallback(value) {
        if (typeof value == "string")
        {
            $scope.errFlag = true;
            $scope.errorMsg = "errFlage=true";//value;
            return;
        }
        $scope.errorMsg = "errFlag=false";
        $scope.errFlag = false;
        $scope.value = value;
        $scope.arr = [1,2,3];
    }
}



function DeviceCtrl($scope, $routeParams) {}