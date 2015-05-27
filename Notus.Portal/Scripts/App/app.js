var visualizeApp = angular.module('VisualizeApp', ['ui.router', 'VisualizeControllers', 'ngAnimate', 'ui.slider', 'ngSanitize', 'ui.select']);


visualizeApp.config([
    '$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
        // For any unmatched url, redirect to default
        $urlRouterProvider.otherwise('/');

        var home = {
            name: 'home',
            url: '/',
            controller: 'VisualizeHomeController',
            views: {
                'graph': { templateUrl: '/visualize/home' }
            }
        };

        var compare = {
            name: 'compare',
            url: '/compare',
            controller: 'VisualizeHomeController',
            views: {
                'graph': { templateUrl: '/visualize/treemap' }
            }
        };

        $stateProvider
            .state(home)
            .state(compare);
    }
], [
    'uiSelectConfig', function(uiSelectConfig) {
        uiSelectConfig.theme = 'bootstrap';
        uiSelectConfig.resetSearchInput = true;
        uiSelectConfig.appendToBody = true;
    }
]);
/**
 * World Map directive
 */
visualizeApp.directive('worldMap', function($window) {
    return {
        restrict: 'EA',
        template: '<div class="map"></div>',
        link: function($scope, $elem, $attrs) {
            var mapDiv = $elem.find('div')[0];
            var basicChoropleth = new Datamap({
                element: mapDiv,
                projection: 'mercator',
                fills: {
                    defaultFill: '#ABDDA4',
                    authorHasTraveledTo: '#fa0fa0'
                },
                data: $scope.mapdata,
                responsive: true
            });

            var colors = d3.scale.category10();

            window.setInterval(function() {
                basicChoropleth.updateChoropleth({
                    USA: colors(Math.random() * 10),
                    RUS: colors(Math.random() * 100),
                    AUS: { fillKey: 'authorHasTraveledTo' },
                    BRA: colors(Math.random() * 50),
                    CAN: colors(Math.random() * 50),
                    ZAF: colors(Math.random() * 50),
                    IND: colors(Math.random() * 50)
                });
            }, 2000);

            d3.select(window).on('resize', function() {
                basicChoropleth.resize();
            });
        }
    };
});

/**
 * 
 */
visualizeApp.directive('treeMap', function($window) {
    return {
        restrict: 'EA',
        template: '<div class="gdb-compare-treemap"></div>',
        link: function($scope, $elem, $attr) {
            var margin = { top: 40, right: 10, bottom: 10, left: 10 },
                width = 960 - margin.left - margin.right,
                height = 500 - margin.top - margin.bottom;

            var color = d3.scale.category20c();

            var treemap = d3.layout.treemap()
                .size([width, height])
                .sticky(true)
                .value(function(d) { return d.size; });

            var div = d3.select('.gdb-compare-treemap')
                .style('position', 'relative')
                .style('width', (width + margin.left + margin.right) + 'px')
                .style('height', (height + margin.top + margin.bottom) + 'px')
                .style('left', margin.left + 'px')
                .style('top', margin.top + 'px');

            d3.json('/Content/json/treemap.json', function(error, root) {
                var node = div.datum(root).selectAll('.node')
                    .data(treemap.nodes)
                    .enter().append('div')
                    .attr('class', 'node')
                    .attr('data-toggle', 'tooltip')
                    .attr('title', function(d) { return d.children ? null : d.name; })
                    .call(position)
                    .style('background', function(d) { return d.children ? color(d.name) : null; })
                    .text(function(d) { return d.children ? null : d.name; });

                $('[data-toggle="tooltip"]').tooltip({ 'container': 'body' });
                d3.selectAll('input').on('change', function() {
                    var value = this.value === 'count'
                        ? function() { return 1; }
                        : function(d) { return d.size; };

                    node.data(treemap.value(value).nodes)
                        .transition()
                        .duration(1500)
                        .call(position);
                });
            });

            function position() {
                this.style('left', function(d) { return d.x + 'px'; })
                    .style('top', function(d) { return d.y + 'px'; })
                    .style('width', function(d) { return Math.max(0, d.dx - 1) + 'px'; })
                    .style('height', function(d) { return Math.max(0, d.dy - 1) + 'px'; });
            }
        }
    };
});

/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
 * performs a AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform a OR.
 */
visualizeApp.filter('propsFilter', function() {
    return function(items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function(item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});