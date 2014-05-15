require.config({
	baseUrl: 'scripts',
	paths: {
		'angular': '/lib/angular/angular',
		'angular-route': '/lib/angular/angular-route/angular-route',
		'angular-resource': '/lib/angular/angular-resource/angular-resource',
		'angular-ui': '/lib/angular/ui-bootstrap-tpls-0.10.0',
		'angular-ui-utils': '/lib/angular/angular-ui/ui-utils',
        'jquery':'/lib/jquery/1.11.1/jquery',
        'bootstrap': '/lib/bootstrap/3.1.1/js/bootstrap',
        'applicationWideScriptExample': '/scripts/applicationWideScriptExample'
	},
	shim: {
		'app': {
		    deps: ['angular', 'angular-route', 'angular-resource', 'angular-ui', 'bootstrap', 'jquery', 'applicationWideScriptExample']
		},
		'angular-route': {
			deps: ['angular']
		},
		'angular-resource': {
			deps: ['angular']
		},
		'angular-ui': {
			deps: ['angular']
		},
		'ada-angular-modules': {
			deps: ['angular', 'angular-ui-utils']
		},
		'angular-ui-utils': {
			deps: ['angular']
		},
		'bootstrap': {
		    deps: ['jquery']
		}
	}
});

require
(
    [
        'app'
    ],
    function (app)
    {
    	angular.bootstrap(document, ['app']);
    }
);