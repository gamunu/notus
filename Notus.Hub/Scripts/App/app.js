'use strict';

var visualizeApp = angular.module('VisualizeApp', ['ui.router', 'VisualizeControllers', 'ngAnimate', 'ui.slider', 'ngSanitize', 'ui.select']);


visualizeApp.config([
    '$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
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
    'uiSelectConfig', function (uiSelectConfig) {
        uiSelectConfig.theme = 'bootstrap';
        uiSelectConfig.resetSearchInput = true;
        uiSelectConfig.appendToBody = true;
    }
]);
/**
 * World Map directive
 */
visualizeApp.directive('worldMap', function () {
    return {
        restrict: 'EA',
        template: '<div class="map"></div>',
        link: function ($scope, $elem) {
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

            window.setInterval(function () {
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

            d3.select(window).on('resize', function () {
                basicChoropleth.resize();
            });
        }
    };
});

/**
 * 
 */
visualizeApp.directive('treeMap', function () {
    return {
        restrict: 'EA',
        template: '<div class="gdb-compare-treemap"></div>',
        link: function ($scope, $elem, $attr) {
            var margin = { top: 40, right: 10, bottom: 10, left: 10 },
                width = 960 - margin.left - margin.right,
                height = 500 - margin.top - margin.bottom;

            var color = d3.scale.category20c();

            var treemap = d3.layout.treemap()
                .size([width, height])
                .sticky(true)
                .value(function (d) { return d.size; });

            var div = d3.select('.gdb-compare-treemap')
                .style('position', 'relative')
                .style('width', (width + margin.left + margin.right) + 'px')
                .style('height', (height + margin.top + margin.bottom) + 'px')
                .style('left', margin.left + 'px')
                .style('top', margin.top + 'px');

            d3.json('/Content/json/treemap.json', function (error, root) {
                var node = div.datum(root).selectAll('.node')
                    .data(treemap.nodes)
                    .enter().append('div')
                    .attr('class', 'node')
                    .attr('data-toggle', 'tooltip')
                    .attr('title', function (d) { return d.children ? null : d.name; })
                    .call(position)
                    .style('background', function (d) { return d.children ? color(d.name) : null; })
                    .text(function (d) { return d.children ? null : d.name; });

                $('[data-toggle="tooltip"]').tooltip({ 'container': 'body' });
                d3.selectAll('input').on('change', function () {
                    var value = this.value === 'count'
                        ? function () { return 1; }
                        : function (d) { return d.size; };

                    node.data(treemap.value(value).nodes)
                        .transition()
                        .duration(1500)
                        .call(position);
                });
            });

            function position() {
                this.style('left', function (d) { return d.x + 'px'; })
                    .style('top', function (d) { return d.y + 'px'; })
                    .style('width', function (d) { return Math.max(0, d.dx - 1) + 'px'; })
                    .style('height', function (d) { return Math.max(0, d.dy - 1) + 'px'; });
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
visualizeApp.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
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


var accountModule = angular.module('AccountModule', ['ui.bootstrap', 'ngSanitize', 'ui.router', 'AccountControllers', 'ngAnimate', 'ui.select','angularMoment']);
/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
 * performs a AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform a OR.
 */
accountModule.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
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

accountModule.config([
    '$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        // For any unmatched url, redirect to default
        $urlRouterProvider.otherwise('/');

        var home = {
            name: 'home',
            url: '/',
            controller: 'AccountHomeController'
        };

        $stateProvider
            .state(home);
    }
]);

accountModule.directive('donutChart', function () {
    return {
        restrict: 'EA',
        scope: {
            model: '=',
            header: '=',
            subheader: '='
        },
        link: function ($scope, $elem) {
            var div = $elem[0];

            var width = 250,
                height = 250,
                radius = Math.min(width, height) / 2,
                twoPi = 2 * Math.PI;

            var color = d3.scale.category20();

            var pie = d3.layout.pie()
                .padAngle(.02)
                .value(function (d) { return d.Total; })
                .sort(null);

            //Arc with start angle 0 for background
            var backgroudArc = d3.svg.arc()
                .innerRadius(radius * 0.8)
                .outerRadius(radius)
                .startAngle(0)
               .endAngle(twoPi);

            var arc = d3.svg.arc()
                .innerRadius(radius * 0.8)
                .outerRadius(radius)
            .cornerRadius(20);;

            var svg = d3.select(div).append('svg')
                .attr('width', '100%')
                .attr('height', '100%')
                .attr('viewBox', '0 0 ' + Math.min(width, height) + ' ' + Math.min(width, height))
                .attr('preserveAspectRatio', 'xMinYMin')
                .append('g')
                .attr('transform', 'translate(' + Math.min(width, height) / 2 + ',' + Math.min(width, height) / 2 + ')');

            var text = svg.append('svg:text')
                .attr('class', 'Today')
                .style('font-size', '20px')
                .attr('transform', 'translate(-90,0)');
            var text2 = svg.append('svg:text')
               .attr('class', 'Today')
               .style('font-size', '10px')
               .attr('transform', 'translate(-70,20)');


            // Add the background arc, from 0 to 100% (twoPi).
            svg.append('g').append('path').style('fill', '#ddd')
                .attr('d', backgroudArc);

            $scope.$watch('model', function () {
                if ($scope.model) {
                    var data = $scope.model;
                    svg.append('g').datum(data.Groups).selectAll('path')
                        .data(pie)
                        .enter().append('path')
                        .style('fill', function (d, i) {
                            return color(i);
                        }).attr('d', arc.endAngle((data.Total / data.Goal) * twoPi));

                    text2.text($scope.subheader);
                    text.text($scope.header);
                }
            });
        }
    }
});


accountModule.directive('barChart', function () {
    return {
        restrict: 'EA',
        scope: {
            model: '=',
            measurment: '='
        },
        link: function ($scope, $element, $attr) {

            var element = $element[0];


            var width = 700;                        //width
            var height = 450;                        //height
            var padding = { top: 40, right: 40, bottom: 40, left: 40 };
            var dataset;
            //Set up stack method
            var stack = d3.layout.stack().x(function (d) {
                return new Date(d.StartTime).getHours();
            });


            var color_hash = {
                0: ['Invite', '#1f77b4'],
                1: ['Accept', '#2ca02c'],
                2: ['Decline', '#ff7f0e']

            };

            //Set up scales
            var xScale = d3.time.scale();
            var yScale = d3.scale.linear();

            var xAxis = d3.svg.axis()
                            .scale(xScale)
                            .orient('bottom');

            var yAxis = d3.svg.axis()
                           .scale(yScale)
                           .orient('left')
                           .ticks(10);



            //Easy colors accessible via a 10-step ordinal scale
            var color = d3.scale.category10();

            //Create SVG element
            var svg = d3.select(element)
                .append('svg')
                .attr('width', '100%')
                .attr('height', '100%')
                .attr('viewBox', '0 0 ' + width + ' ' +height)
                .attr('preserveAspectRatio', 'xMinYMin');

            // Add a group for each row of data
            var groups = svg.selectAll('g');
            //  .data(dataset)
            //  .enter()
            //  .append('g')
            //  .attr('class', 'rgroups')
            //  .attr('transform', 'translate(' + padding.left + ',' + (h - padding.bottom) + ')');
            /*  .style('fill', function (d) {
                  return color_hash[dataset.indexOf(d)][1];
              });*/

            // Add a rect for each data value
            var rects = groups.selectAll('rect')
                .data(function (d) { return d; })
                .enter()
                .append('rect')
                .attr('width', 2)
                .style('fill-opacity', 1e-6);


            rects.transition()
                 .duration(function (d, i) {
                     return 500 * i;
                 })
                 .ease('linear')
                .attr('x', function (d) {
                    return xScale(new Date(d.time));
                })
                .attr('y', function (d) {
                    return -(-yScale(0) - yScale(d[$scope.measurment]) + (height - padding.top - padding.bottom) * 2);
                })
                .attr('height', function (d) {
                    return -yScale(d[$scope.measurment]) + (height - padding.top - padding.bottom);
                })
                .attr('width', 15)
                .style('fill-opacity', 1);

            svg.append('g')
                .attr('class', 'x axis')
                .attr('transform', 'translate(40,' + (height - padding.bottom) + ')')
                .call(xAxis);


            svg.append('g')
                .attr('class', 'y axis')
                .attr('transform', 'translate(' + padding.left + ',' + padding.top + ')')
                .call(yAxis);

            // adding legend

            var legend = svg.append('g')
                            .attr('class', 'legend')
                            .attr('x', width - padding.right - 65)
                            .attr('y', 25)
                            .attr('height', 100)
                            .attr('width', 100);

            /*legend.selectAll('g').data(dataset)
                  .enter()
                  .append('g')
                  .each(function (d, i) {
                      var g = d3.select(this);
                      g.append('rect')
                          .attr('x', w - padding.right - 65)
                          .attr('y', i * 25 + 10)
                          .attr('width', 10)
                          .attr('height', 10)
                          .style('fill', color_hash[String(i)][1]);

                      g.append('text')
                       .attr('x', w - padding.right - 50)
                       .attr('y', i * 25 + 20)
                       .attr('height', 30)
                       .attr('width', 100)
                       .style('fill', color_hash[String(i)][1])
                       .text(color_hash[String(i)][0]);
                  });*/

            svg.append('text')
            .attr('transform', 'rotate(-90)')
            .attr('y', 0 - 5)
            .attr('x', 0 - (height / 2))
            .attr('dy', '1em')
            .text('Active time');

            
            $scope.$watch('model', function () {

                if (!$scope.model)
                    return;

                var model = $scope.model;
                dataset = model.Data;

                stack.y(function(d) {
                    return d[$scope.measurment];
                });

                stack(dataset);


                xScale.domain([new Date(model.Total.MinTime), new Date(model.Total.MaxTime)])
                .rangeRound([0, width - padding.left - padding.right]);

                yScale.domain([0,
                                d3.max(dataset, function (d) {
                                    return d3.max(d, function (d) {
                                        return d[$scope.measurment];
                                    });
                                })
                ]).range([height - padding.bottom - padding.top, 0]);

                xAxis.scale(xScale)
                     .ticks(d3.time.hour, 5)
                     .tickFormat(d3.time.format('%I:%M %p'));

                yAxis.scale(yScale)
                     .orient('left')
                     .ticks(5);

                groups = svg.selectAll('.rgroups')
                   .data(dataset);

                groups.enter().append('g')
                .attr('class', 'rgroups')
                .attr('transform', 'translate(' + padding.left + ',' + (height - padding.bottom) + ')')
                .style('fill', function (d, i) {
                    return color(i);
                });


                var rect = groups.selectAll('rect')
                    .data(function (d) { return d; });

                rect.enter()
                  .append('rect')
                  .attr('x', width)
                  .attr('width', 1)
                  .style('fill-opacity', 1e-6);

                rect.transition()
                    .duration(1000)
                    .ease('linear')
                    .attr('x', function (d) {
                        return xScale(new Date(d.StartTime));
                    })
                    .attr('y', function (d) {
                        return -(-yScale(0) - yScale(d[$scope.measurment]) + (height - padding.top - padding.bottom) * 2);
                    })
                    .attr('height', function (d) {
                        return -yScale(d[$scope.measurment]) + (height - padding.top - padding.bottom);
                    })
                    .attr('width', 30)
                    .style('fill-opacity', 1);

                rect.exit()
                   .transition()
                   .duration(1000)
                   .ease('circle')
                   .attr('x', width)
                   .remove();

                groups.exit()
                   .transition()
                   .duration(1000)
                   .ease('circle')
                   .attr('x', width)
                   .remove();

                svg.select('.x.axis')
                   .transition()
                   .duration(1000)
                   .ease('circle')
                   .call(xAxis);

                svg.select('.y.axis')
                   .transition()
                   .duration(1000)
                   .ease('circle')
                   .call(yAxis);

                svg.select('.xtext')
                   .text('Hours');

                //  svg.select('.title')
                //  .text('Number of messages per hour on ' + date + '.');
            });
        }
    }
});