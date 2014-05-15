define([], function () {
	return {
		defaultRoutePaths: '/',
		routes: {
			'/p1': {
				templateUrl: '/views/page1/page1.html',
				dependencies: [
                    'pages/page1/page1Controller'
				]
			},
			'/p2': {
				templateUrl: '/views/page2/page2.html',
				dependencies: [
                    'pages/page2/page2Controller'
				]
			}
		}
	};
});