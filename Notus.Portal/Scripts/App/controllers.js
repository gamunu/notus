var visualizeControllers = angular.module('VisualizeControllers', []);

visualizeControllers.controller('VisualizeHomeController', [
    '$scope', '$http', '$timeout', '$interval',
    function ($scope, $http, $timeout, $interval) {
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