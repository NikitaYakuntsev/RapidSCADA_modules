angular.module("scada",['ngRoute']).
config(function($routeProvider) {
    $routeProvider.
    when('/', {controller:ChooseCtrl, templateUrl:'html/choose.html'}).
    when('/deviceList', {controller:DeviceListCtrl, templateUrl:'html/deviceList.html'}).
    when('/device/:deviceId', {controller:DeviceCtrl, templateUrl:'html/device.html'}).
    otherwise({redirectTo:'/'});
});

