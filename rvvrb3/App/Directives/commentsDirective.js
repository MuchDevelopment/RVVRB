(function() {

    angular.module('rvvrb')
        .directive('commentList', function() {
            return {
                restrict: 'E',
                scope: false,
                templateUrl: '/App/Partials/comments.html',
                controller: 'CommentsController',
                controllerAs: 'comments'
            }
        })
        .service('CommentsService', function($http) {

            this.getComments = function(id) {
                return $http.get('/api/comments/' + id)
                    .success(function(data) {
                        angular.forEach(data.Data, function(comment) {
                            comment.time = new Date(comment.time);
                        });
                        //console.log('comment data parser running');
                    })
                    .error(function(data, status) {
                        console.log('Error: comments unavailable. That is annoying, well I hope an admin sees this soon.');
                        //console.log(data);
                    });
            };
                this.postComment = function(data) {
                    return $http.post('/api/comments/add', data)
                        .success(function() {
                            console.log('Comment posted successfully');
                        })
                        .error(function() {
                            console.log('There was an error posting your comment');
                        });
                };
        }).controller('CommentsController', [
            '$scope', 'CommentsService','$rootScope', function($scope, CommentsService,$rootScope) {

                CommentsService.getComments($scope.track.ID)
                    .then(function(response) {
                        $scope.comments = response.data.Data;
                    });

                var commentProp = 'NewComment:' + $scope.track.ID;

                $rootScope.$on(commentProp, function (event) {
                    CommentsService.getComments($scope.track.ID)
                        .then(function(response) {
                            $scope.comments = response.data.Data;
                            //$scope.$apply();
                        });
                });
            }


        ]).directive('commentForm', function() {
            return{
                restrict: 'E',
                templateUrl: '/App/Partials/commentForm.html',
                controller: 'CommentsFormController',
                controllerAs: 'CommentsFrmCtrl',
                scope:false
            }
        }).controller('CommentsFormController', function($scope, CommentsService,$rootScope) {
            $scope.c = {};

            $scope.submitComment = function (comments) {

                var data = {
                    comment: $scope.c.comment,
                    trackID: $scope.track.ID
                }

                CommentsService.postComment(data)
                    .then(function() {
                        $scope.commentsForm.$setPristine();
                        $scope.c = {};
                        $rootScope.$broadcast('NewComment:' + $scope.track.ID);
                    });
            };

        });
})();



