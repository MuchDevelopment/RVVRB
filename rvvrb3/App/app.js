(function () {

    angular.module('rvvrb', [
           'ngResource',
           'ngSanitize',
           'ui.bootstrap',
           'ui.router',
           'com.philmerrell.audioPlaylist',
           'angularFileUpload'
    ])
        .config(['$stateProvider', function ($stateProvider) {

            $stateProvider
                .state('Home', {
                    url: "",
                    views:{
                        "Main": {
                            templateUrl: '/App/Partials/list.html',
                            controller: 'SongListController'
                            }  
                        },
                        "SoundPlayer": {
                        }
                });

            //$routeProvider
            //    .when('/', {
            //        templateUrl: '/App/Partials/list.html',
            //        controller: 'SongListController',
            //        resolve: {
            //            'SongsData': function(Songs) {
            //            return Songs.promise;
            //            }
            //        }
            //    }).when('/songs/add', {
            //        templateUrl: '/App/Partials/add.html',
            //        controller: 'SongAddController'
            //    }).when('/profile/:id', {
            //        templateUrl: '/App/Partials/add.html',
            //        controller: 'ProfileViewController'
            //    }).when('/songs/edit/:id', {
            //        templateUrl: '/App/Partials/edit.html',
            //        controller: 'SongEditController'
            //    }).when('/songs/delete/:id', {
            //        templateUrl: '/App/Partials/delete.html',
            //        controller: 'SongDeleteController'
            //    });


           //$locationProvider.html5Mode({
           //    enabled: true,
           //    requireBase: false
           //});
        }]);

})();