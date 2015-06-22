var visualizeControllers = angular.module('VisualizeControllers', []);

visualizeControllers.controller('VisualizeHomeController', [
    '$scope', '$http',
    function ($scope, $http) {
        console.log('VisualizeHome');
        $http.get('/api/WorldMap').success(function (data) {
            $scope.mapdata = data;
        });

        $http.get('/api/Causes').success(function (data) {
            $scope.causes = data;
        });

        $scope.metics = [
            { Name: 'Deaths' },
            { Name: 'YLLLs (Years of Life Lost)' },
            { Name: 'YLDs (Years Lived With Diablility)' },
            { Name: 'DALYs (Disabiliy-Adjusted Life Years)' }
        ];
        $scope.ages = [
         { Name: 'All ages' },
         { Name: 'Under 5 years' },
         { Name: '5-14 years' },
         { Name: '15-49 years' },
         { Name: '50-69 years' },
         { Name: '70+ years' },
         { Name: 'Age-standardized' },
        { Name: 'Age-specific' }
        ];

        $scope.colors = [
         { Name: 'Rate of Change' },
         { Name: 'Risk Factor Attribution' },
         { Name: 'Uncertainty' }
        ];
        $scope.GraphControls = { slider: 0, select: {} };


        $scope.someGroupFn = function (item) {
            if (item.ParentCause != null) {
                return $scope.causes[item.ParentCause - 1].Name;
            }
        };


        // Slider options with event handlers
        $scope.slider = {
            'options': {
                start: function (event, ui) { console.log('Event: Slider start - set with slider options', event); },
                stop: function (event, ui) { console.log('Event: Slider stop - set with slider options', event); }
            }
        };


    }
]);


var visualizeControllers = angular.module('AccountControllers', []);

/**
 * AccoutHomecontroller
 */
visualizeControllers.controller('AccountHomeController',
    function ($scope, $modal, $log, $http) {
        $scope.fitnessData = {};

        $scope.tabs = [
   { title: 'Dynamic Title 1', content: 'Dynamic content 1' },
   { title: 'Dynamic Title 2', content: 'Dynamic content 2', disabled: true }
        ];

        $http.get('/api/fit/activities/types').success(function (data) {
            $scope.causes = data;
        });

        $scope.measurments = [
            'ActiveTime',
            'Steps',
            'Distance',
            'Energy'
        ];

        $scope.measurment = $scope.measurments[0];

        $scope.onMeasurment = function ($item, $model) {
            $scope.measurment = $item;
            $http.get('/api/fit/activities/only/' + $item).success(function (data) {
                $scope.histroydata = data;
            });
        }

        $scope.animationsEnabled = true;

        $scope.addActivity = function () {

            var modalInstance = $modal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'addActivityModel.html',
                controller: 'ModalInstanceCtrl',
                size: 'sm',
                resolve: {
                    $causes: function () {
                        return $scope.causes;
                    }
                }
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };

        $scope.logWeight = function () {
            var modelInstance = $modal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'logWeightModel.html',
                controller: 'LogWeightCtrl',
                size: 'sm',
                resolve: {
                    $causes: function () {
                        return $scope.causes;
                    }
                }
            });

            modelInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        /* Get data for Active time,Calories burned,Steps */
        $http.get('/api/fit/activities/total/minutes').success(function (data) {
            $scope.timedata = data;

            if (data.Total >= data.Goal) {
                $scope.timeheader = 'Gloal';
            } else {
                $scope.timeheader = (data.Goal - data.Total) + ' min to goal';
            }
            $scope.timesubheader = data.Total + ' min today';
        });
        $http.get('/api/fit/activities/total/energy').success(function (data) {
            $scope.caloriesdata = data;



            $scope.caloriesheader = '1,453 calories burned';
            $scope.caloriessubheader = 'Your avg is 2,399 calories';

        });
        $http.get('/api/fit/activities/total/steps').success(function (data) {
            $scope.stepssdata = data;
            $scope.stepsheader = '0 steps today';
            $scope.stepssubheader = 'Your average is 6,000 steps';
        });

        $http.get('/api/fit/activities/only/activetime').success(function (data) {
            $scope.histroydata = data;
        });

        $http.get('/api/fit/activities/total/group/date').success(function(data) {
            $scope.timelinedata = data;
        });
    });


visualizeControllers.controller('ModalInstanceCtrl', function ($scope, $modalInstance, $causes) {

    $scope.causes = $causes;

    $scope.ok = function () {
        console.log($scope.activity.$valid);
        if ($scope.activity.$valid) {
            $modalInstance.close();
        }
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    /* Add activity model elements */

    //set max selectable date
    $scope.maxDate = new Date();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    /* Time picker */
    $scope.time = new Date();
    $scope.duration = 0;
    $scope.steps = 0;
    $scope.calories = 0;
    $scope.distance = 0;
    $scope.date = new Date();
    $scope.type = null;

});


visualizeControllers.controller('LogWeightCtrl', function ($scope, $modalInstance, $causes) {
    $scope.causes = $causes;
    $scope.ok = function () {
        console.log($scope.logweight.$valid);
        if ($scope.logweight.$valid) {
            $modalInstance.close();
        }
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    /* Add activity model elements */

    //set max selectable date
    $scope.maxDate = new Date();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.weight = 1;

    /* Time picker */
    $scope.time = new Date();
    $scope.duration = 0;
    $scope.steps = 0;
    $scope.calories = 0;
    $scope.distance = 0;
    $scope.date = new Date();
    $scope.type = null;
});

visualizeControllers.controller('InfoController',
    function ($scope, $modal, $log, $http) {

        $http.get('/Content/json/blood.json').success(function (data) {
            $scope.blooddata = data;
        });
    });