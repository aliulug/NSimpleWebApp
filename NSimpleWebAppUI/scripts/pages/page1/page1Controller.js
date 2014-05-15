//load is configured in routes.js
define(['app', 'pages/page1/serverCommunicator'], function (app)
{
	app.controller('page1Controller',
    [
		'$scope', 'serverCommunicator',
		function ($scope, serverCommunicator)
		{
		    $scope.query = "";

            //below function is not used. just an example to use an angular service for server (http) communication
		    $scope.getIssue = function()
		    {
		        var getIssueInput = { "IssueId": parseInt($scope.query) };
		        serverCommunicator.sendDataToServer(getIssueInput).then(function (resultFromServer)
		        {
		            alert("server responded (" + JSON.stringify(resultFromServer) + ")");
		        });
		    };
		}
    ]);
});

