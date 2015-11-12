(function() {
    
    angular.module('rvvrb')
    .controller('UploadCtrl', function ($scope, $http, $timeout, $upload,$rootScope) {
        $scope.upload = [];

        $scope.percent = 0;
        $scope.uploadMessage = '';
        $scope.styling = '';

        $scope.fileUploadObj = {
            songname: $scope.trackname,
            songtracknum: $scope.tracknum,
            songartist: $scope.artist,
            songdescription: $scope.description
        };
 
        $scope.onFileSelect = function ($files) {
            $scope.files = $files;
        }

        $scope.uploadFile = function () {
            console.log($scope.fileUploadObj);
            $scope.fileUploadObj = {
                songname: $scope.trackname,
                songtracknum: $scope.tracknum,
                songartist: $scope.artist,
                songdescription: $scope.description
            };

            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $scope.files.length; i++) {
                var $file = $scope.files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "./api/files/upload", // webapi url
                        method: "POST",
                        data: { fileUploadObj: $scope.fileUploadObj },
                        file: $file
                    }).progress(function (evt) {
                        $scope.percent = parseInt(100.0 * evt.loaded / evt.total);
                        $scope.uploadMessage = $scope.percent + '%';
                        if ($scope.percent === 100)$scope.percent = 0;
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.files = '';
                        $scope.fileUploadObj = '';
                        $scope.trackname = '';
                        $scope.tracknum = '';
                        $scope.artist = '';
                        $scope.description = '';
                        $scope.fileInput = null;
                        $scope.uploadMessage = 'Upload Complete';
                        $scope.percent = 0;
                        angular.forEach(
                            angular.element("input[type='file']"),
                            function (inputElem) {
                                angular.element(inputElem).val(null);
                            });
                        $rootScope.$broadcast('newSongUploaded');

                    }).error(function (data, status, headers, config) {
                        $scope.uploadMessage = 'Upload Failed';
                    });
                })(i);
            }
        }


 
        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        }
});


})();