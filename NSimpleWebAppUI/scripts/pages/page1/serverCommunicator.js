define(['app'], function(app)
{
    app.service('serverCommunicator',
	[
		'$http', '$q',
		function($http, $q)
		{
			this.http = $http;
			var deferrer = $q;
			this.sendDataToServer = function (requestData)
			{
			    alert("sending request to server...");
			    return this.internalSendRequest("/IssueHH.GetIssue.req", requestData);
			};

		    this.internalSendRequest = function(uri, requestData)
		    {
		        var lock = deferrer.defer();
		        this.http.post(uri, requestData).success(function (responseData)
		        {
		            lock.resolve(responseData);
		        }).error(function(errorData)
		        {
		            //...
		        });
		        return lock.promise;
		    };
		}
	]);
});

