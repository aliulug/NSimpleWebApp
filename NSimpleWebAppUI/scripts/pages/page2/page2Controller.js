//load is configured in routes.js
define(['app'], function (app)
{
	app.controller('page2Controller',
    [
		'$scope', 
		function ($scope)
		{
			$scope.varInScope = "value of the variable in scope";
		}
    ]);
});
