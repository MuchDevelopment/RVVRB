angular
    .module('rvvrb')
    .service('appPlaylistService', ['$rootScope', function ($rootScope) {
            this.appPlaylist = [];
        this.addSong = function(song) {
            this.appPlaylist.push(song);
            $rootScope.$broadcast('appPlaylist.update');     
        };
        this.getPlaylist = function () {
            return this.appPlaylist;
        }
    }]);
