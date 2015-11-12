angular
    .module('rvvrb')
    .directive('addAudioButton', ['audioPlaylistService', '$rootScope', function (audioPlaylistService, $rootScope) {
        return {
            restrict: 'E',
            template: '<button class="AddButton">+</button>',
            scope: {item:'='},
            controllerAs: 'addAudioButtonCtrl',
            controller: function($scope) {},
            link: function (scope, element, attrs) {
                element.bind('click', function() {
                    audioPlaylistService.addToPlaylist(scope.item);
                });

            }
        }
    }]);