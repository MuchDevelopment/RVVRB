(function () {
    'use strict';

    angular.module('rvvrb')
            .filter('startFrom', function () {
                return function (input, start) {
                    //console.log("start:"+start);
                    start = +start; //parse to int
                    return input.slice(start);
                };
            })
    .controller('PlayerController', function ($scope, $http, $rootScope) {
           // console.log("MainController loaded");
            $scope.currentTrack = 0;
            $scope.pageSize = 5;
            $scope.data = [];

            var updateTrack = function () {
                $rootScope.$broadcast('audio.set', $scope.data[$scope.currentTrack].preview_url, $scope.data[$scope.currentTrack],$scope.currentTrack, $scope.data.length, $scope.data);
                console.log($scope.data.length);
            };

            $rootScope.$on('audio.next', function () {
                $scope.currentTrack++;
                if ($scope.currentTrack < $scope.data.length) {
                    updateTrack();
                } else {
                    $scope.currentTrack = $scope.data.length - 1;
                }
            });

            $rootScope.$on('audio.prev', function () {
                $scope.currentTrack--;
                if ($scope.currentTrack >= 0) {
                    updateTrack();
                } else {
                    $scope.currentTrack = 0;
                }
            });

        $http.get('/api/playlist/albumtracks/4Lp4NxL8v03Mxg3hbAgW1Z')
                .success(function (response) {
                    $scope.data = response.items;
                //console.log("ran httpget");
                    updateTrack();
                });
    });
})();