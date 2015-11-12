(function() {
    'use strict';
    angular.module('rvvrb')
    .factory('Songs', function($http) {

            var myData = null;

            return {
                promise: function (filter) {
                    //pagination filter ?start=x&limit=x
                    return $http.get('/api/homepage/'+filter);
                },
                setQuery: function(query) {
                    myQuery = query;
                },
                setData: function (data) {
                    myData = data;
                },
                getData: function(data) {
                    return myData;
                }
            }

    });


})();

//(function() {
//    'use strict';
//    angular.module('rvvrb')
//    .factory('Songs', function ($http, $q) {

//(function () {
//    'use strict';
//    angular.module('rvvrb')
//    .factory('Songs', function ($http, $q) {

//        var myData = null;
//        var myQuery = null;

//        var deferred = $q.defer();

//        var promise = $http.get('/api/songs/').success(function (data) {
//            myData = data;
//        });

//        return {
//            promise: promise,
//            setQuery: function (query) {
//                myQuery = query;
//            },
//            setData: function (data) {
//                myData = data;
//            },
//            getData: function (data) {
//                return myData;
//            }
//        }

//    });


//})();




//    });
//})();
        //.module('SongFactory', ['$http'])
//, {}, {query: { method: 'GET', params: {}, isArray: true }


