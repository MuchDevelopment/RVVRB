(function () {
    'use strict';

    angular
        .module('rvvrb')
        .controller('SongListController', function($scope, audioPlaylistService, Songs, $rootScope, $timeout) {

            var filter = "?start=0&limit=20";

            Songs.promise(filter).success(function(data) {
                $scope.homeList = data.Data;
            });

            //refresh view on upload
            $rootScope.$on('newSongUploaded', function(event) {
                Songs.promise(filter).success(function(data) {
                    $scope.homeList = data.Data;
                });
            });

            //set up navigation area
            $scope.tabs = [
              { "title": 'Search', "template": '/App/Partials/search.html' },
              { "title": 'Upload', "template": '/App/Partials/upload.html' }
            ];

            //wire up search bars to tabbed area
            
            $rootScope.search = {};
            $scope.data = {};

           $scope.updateValueInScope = function () {
               
               $rootScope.search.searchTrackname = $scope.data.searchTrackname;
               $rootScope.search.searchArtist = $scope.data.searchArtist;
               $rootScope.search.searchDescription = $scope.data.searchDescription;
               $rootScope.search.searchTracknum = $scope.data.searchTracknum;
               $rootScope.search.searchUser = $scope.data.searchUser;
           }

        });


//.controller('SongAddController', SongAddController)
    //.controller('SongEditController', SongEditController)
    //.controller('SongDeleteController', SongDeleteController);


////inject Add Controller
    //SongAddController.$inject = ['$scope','$location','Song'];

    //function SongAddController($scope,$location,Song) {
    //    $scope.song = new Song();
    //    $scope.add = function () {
    //        $scope.song.$save(function () {
    //            $location.path('/');
    //        });
    //    };
    //}

    ////inject Edit Controller
    //SongEditController.$inject = ['$scope', '$routeParams', '$location', 'Song'];


    //function SongEditController($scope, $routeParams, $location,Song) {
    //    $scope.song = Song.get({ ID: $routeParams.ID });
    //    $scope.edit = function () {
    //        $scope.song.$save(function () {
    //            $location.path('/');
    //        });
    //    };
    //}

    ////inject Delete Controller
    //SongDeleteController.$inject = ['$scope', '$routeParams', '$location', 'Song'];

    //function SongDeleteController($scope, $routeParams, $location, Song) {
    //    $scope.song = Song.get({ ID: $routeParams.ID });
    //    $scope.remove = function () {
    //        $scope.song.$remove({ ID: $scope.song.ID }, function () {
    //            $location.path('/');
    //        });
    //    };
    //}

})();