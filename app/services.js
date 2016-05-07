﻿'use strict';

app.factory('backendHubProxy', ['$rootScope', 'backendServerUrl', function ($rootScope, backendServerUrl) {
    function backendHubProxyFactory(serverUrl, hubName, startOptions) {
        var connection = $.hubConnection(backendServerUrl);
        var proxy = connection.createHubProxy(hubName);
        connection.start(startOptions).done(function () { });

        return {
            on: function (eventName, callback) {
                proxy.on(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            off: function (eventName, callback) {
                proxy.off(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            invoke: function (methodName, callback) {
                proxy.invoke(methodName)
                    .done(function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
            },
            connection: connection
        };
    };

    return backendHubProxyFactory;
}]);

app.service("bookingInfoService", [
    '$http', 'backendServerUrl', function ($http, backendServerUrl) {
        this.postLayout = function (layout) {
            var req = $http.post(backendServerUrl + 'api/Layout', {layout:layout});
            return req;
        };
        this.getLayout = function (layout) {
            var req = $http.get(backendServerUrl + 'api/Layout');
            return req;
        };
        this.postInfo = function(dateInfo) {
            var req = $http.post(backendServerUrl + 'api/BookingInformationAPI', dateInfo);
            return req;
        };
    }
]);
