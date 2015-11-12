
/*
**  TODO:
      - Refactor to override <audio> element and add another directive for a playlist="" attrib
      <audio playlist="myRadPlaylist">

*/
'use strict';

angular.module('com.philmerrell.audioPlaylist', [])
    .directive('audio', [
        'audioPlaylistService', 'appPlaylistService', function (audioPlaylistService, appPlaylistService) {
            return {
                templateUrl: '/App/Partials/playlist.html',
                restrict: 'E',
                replace: true,
                transclude: 'element',
                scope: {
                    //playlist: '='
                },
                controller: 'audioPlaylistController',
                link: function postLink(scope, element, attrs, audioCtrl) {

                    var slider = element.find('input[type="range"]')[0];
                    //console.log(slider);

                    var audio = element.find('audio')[0];
                    //var audio = document.createElement('audio');
                    //element.appendChild(audio);
                    console.log(audio);
                    scope.audio = audio;

                    if (scope.playlist.length) {
                        
                        audio.src = scope.currentlyPlaying.src;
                    }

                    var button = element.find('button');

                    button.bind('click', function() {

                        audioCtrl.toggleAudio();

                    });

                    /* AUDIO LISTENERS */

                    audio.addEventListener('timeupdate', function() {

                        var ct = this.currentTime;
                        var d = this.duration;

                        scope.$apply(function() {
                            scope.currentTime = audioCtrl.timeElapsed(ct);
                            scope.remainingTime = audioCtrl.timeRemaining(d, ct);
                            scope.percentElapsed = audioCtrl.percentElapsed(d, ct);
                        });

                    });

                    audio.addEventListener('playing', function() {

                        scope.$apply(function() {
                            scope.player.buttonIcon = audioCtrl.playerControls.pauseIcon;
                        });

                    });

                    audio.addEventListener('pause', function() {

                        scope.$apply(function() {
                            scope.player.buttonIcon = audioCtrl.playerControls.playIcon;
                        });
                    });

                    audio.addEventListener('ended', function() {

                        audioCtrl.nextTrack();

                    });

                    audio.addEventListener('progress', function() {
                        var d = this.duration;

                        if (this.buffered !== null) {
                            var b = this.buffered.end(0);

                            scope.$apply(function() {
                                scope.percentLoaded = audioCtrl.percentLoaded(d, b);
                            });
                        }


                    });

                    audio.addEventListener('waiting', function() {
                        scope.$apply(function() {
                            scope.player.buttonIcon = audioCtrl.playerControls.loadingIcon;
                        });
                    });

                    var setSliderProgress = function() {
                        var newVal = slider.value / (100 / audio.duration);

                        audio.currentTime = newVal;
                        //console.log('changed');
                    };

                    slider.addEventListener('change', setSliderProgress, false);

                    slider.addEventListener('input', function() {

                        slider.removeEventListener('change', setSliderProgress, false);
                        var newValue = slider.value / (100 / audio.duration);
                        audio.currentTime = newValue;

                    });
                    
                    audio.playlist = audioPlaylistService.getPlaylist();
                }
            };
        }
    ])
    .controller('audioPlaylistController', [
        '$scope', '$attrs', 'audioPlaylistService','appPlaylistService', '$rootScope', function ($scope, $attrs, audioPlaylistService,appPlaylistService, $rootScope) {

            $scope.playlist = [];
            /* Init Controller Values */
            $scope.percentElapsed = 0;
            $scope.currentlyPlaying = null;
            
            $rootScope.$on('audioPlaylistService.update', function (event) {

                $scope.playlist = audioPlaylistService.getPlaylist();     
                
                if (typeof $scope.currentlyplaying === 'undefined' && $scope.playlist.length===1) {
                    var track = $scope.playlist[0];
                
                    $scope.audio.src = track.src;
                    $scope.currentlyPlaying = track;
                    $scope.audio.load();
                }


                $scope.$apply();
            });

            $scope.currentlyPlaying = audioPlaylistService.getPlaylist()[0];

            //console.log('playing:' + $scope.currentlyPlaying);

            this.playerControls = {
                playIcon: 'Play',
                pauseIcon: 'Pause',
                loadingIcon: 'Loading'
            };

            $scope.player = {
                buttonIcon: this.playerControls.playIcon
            };

            // END INIT CONTROLLER

            
            this.loadPlaylist = function() {
                //console.log('loading playlist');
                $scope.playlist = audioPlaylistService.getPlaylist();
                $scope.audio.src = $scope.playlist[0].src;
                $scope.currentlyPlaying = $scope.playlist[0];
                $scope.audio.load();
                $scope.audio.play();
            };

            this.timeElapsed = function(currentTime) {

                var t = currentTime;

                var seconds = Math.floor(t % 60),
                    elapsedSeconds = seconds < 10 ? '0' + seconds : seconds,
                    minutes = Math.floor((t / 60) % 60),
                    elapsedMinutes = minutes < 10 ? '0' + minutes : minutes,
                    elapsedHours = Math.floor(((t / 60) / 60) % 60);

                if (elapsedHours === 0) {
                    return elapsedMinutes + ':' + elapsedSeconds;
                } else {
                    return elapsedHours + ':' + elapsedMinutes + ':' + elapsedSeconds;
                }
            };

            this.timeRemaining = function(duration, currentTime) {

                var d = duration,
                    t = currentTime;

                var timeLeft = d - t,
                    seconds = Math.floor(timeLeft % 60) || 0,
                    remainingSeconds = seconds < 10 ? '0' + seconds : seconds,
                    minutes = Math.floor((timeLeft / 60) % 60) || 0,
                    remainingMinutes = minutes < 10 ? '0' + minutes : minutes,
                    hours = Math.floor(((timeLeft / 60) / 60) % 60) || 0;

                if (hours === 0) {
                    return '-' + remainingMinutes + ':' + remainingSeconds;
                } else {
                    return '-' + hours + ':' + remainingMinutes + ':' + remainingSeconds;
                }

            };

            this.percentLoaded = function(duration, progressBuffer) {

                return parseInt(((progressBuffer / duration) * 100), 10);

            };

            this.percentElapsed = function(duration, currentTime) {

                return (Math.floor((100 / duration) * currentTime)) || 0;

            };

            this.toggleAudio = function() {
                if ($scope.audio.paused) {
                    $scope.audio.play();
                } else {
                    $scope.audio.pause();
                }
            };

            this.nextTrack = function() {

                var nextTrack = $scope.playlist.indexOf($scope.currentlyPlaying) + 1;

                if ($scope.playlist.length > nextTrack) {


                    console.log($scope.playlist[nextTrack]);

                    $scope.currentlyPlaying = $scope.playlist[nextTrack];
                    $scope.audio.src = $scope.playlist[nextTrack].src;
                    $scope.audio.load();
                    $scope.audio.play();

                } else {

                    //window.alert('all done');
                    $scope.currentlyPlaying = $scope.playlist[0];
                    $scope.audio.src = $scope.playlist[0].src;
                    $scope.audio.currentTime = 0;
                    $scope.audio.pause();

                }
            };

            $scope.previousTrack = function() {

                var previousTrack = $scope.playlist.indexOf($scope.currentlyPlaying) - 1;

                if (previousTrack < 0) {
                    $scope.currentlyPlaying = $scope.playlist[0];
                } else {

                    $scope.audio.src = $scope.playlist[previousTrack].src;
                    $scope.currentlyPlaying = $scope.playlist[previousTrack];
                    $scope.audio.load();
                    $scope.audio.play();
                }
            };

            $scope.playTrack = function(track) {
                $scope.audio.src = track.src;
                $scope.currentlyPlaying = track;
                $scope.audio.load();
                $scope.audio.play();
            };

            $scope.removeTrack = function(track) {
                audioPlaylistService.removeFromPlaylist(track);
            };

        }
    ])
    .factory('audioPlaylistService', ['$rootScope', function ($rootScope) {

        var me = this;
        this.playlist = [];

        return {
            setPlaylist: function (newPlaylist) {
                if (newPlaylist != null) {
                    me.playlist = newPlaylist;
                } else {
                    console.log('null in newplaylist');
                }
            },
            getPlaylist: function() {
                if (me.playlist !== null) {
                    return me.playlist;
                } else {
                    return;
                }
            },
            addToPlaylist: function (track) {
                if (me.playlist !== null) {
                    if (me.playlist.indexOf(track) === -1) {
                        me.playlist.push(track);
                    }
                } else {
                    me.playlist = track;
                }
                $rootScope.$broadcast('audioPlaylistService.update');
            },
            removeFromPlaylist: function(track) {
                me.playlist.splice(me.playlist.indexOf(track),1);
            }
        };
    }]);


