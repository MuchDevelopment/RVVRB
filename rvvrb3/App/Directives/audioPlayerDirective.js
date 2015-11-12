
(function () {

    angular.module('rvvrb', [])
        .directive('audioPlayer', function($rootScope) {
            return {
                restrict: 'E',
                scope: {},
                controller: function($scope, $element) {

                    $scope.audio = new Audio();
                    $scope.currentNum = 0;

                    //broadcast next prev for global interaction
                    $scope.next = function() { $rootScope.$broadcast('audio.next'); };
                    $scope.prev = function() { $rootScope.$broadcast('audio.prev'); };

                    //tell player to pause / play from play button
                    $scope.playpause = function() { var a = $scope.audio.paused ? $scope.audio.play() : $scope.audio.pause(); };

                    // listen for audio-element events, and broadcast stuff
                    $scope.audio.addEventListener('play', function() { $rootScope.$broadcast('audio.play', this); });
                    $scope.audio.addEventListener('pause', function() { $rootScope.$broadcast('audio.pause', this); });
                    $scope.audio.addEventListener('timeupdate', function() { $rootScope.$broadcast('audio.time', this); });
                    $scope.audio.addEventListener('ended', function() {
                        $rootScope.$broadcast('audio.ended', this);
                        $scope.next();
                    });
                   // $rootScope.$broadcast('audio.set', $scope.data[$scope.currentTrack].preview_url, $scope.data[$scope.currentTrack], $scope.currentTrack, $scope.data.length, $scope.data);
                    // set track & play it
                    $rootScope.$on('audio.set', function(r, file, info, currentNum, totalNum,data) {
                        var playing = !$scope.audio.paused;
                        $scope.audio.src = file;
                        var a = playing ? $scope.audio.play() : $scope.audio.pause();
                        $scope.info = info;
                        $scope.currentNum = currentNum;
                        $scope.totalNum = totalNum;
                        $scope.playlist = data;
                        $scope.remainingSongCount = totalNum - currentNum;
                        //console.log(data);
                        //console.log("total:"+(totalNum - currentNum));
                    });

                    // update display of things - makes time-scrub work
                    //TO DO RE-ENABLE WHEN READY
                    //setInterval(function() { $scope.$apply(); }, 500);
                },
                templateUrl: '/App/Partials/player.html'
            }

        });

})();