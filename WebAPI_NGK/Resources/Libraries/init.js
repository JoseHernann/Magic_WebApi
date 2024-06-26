
(function ($) {


	var dominio = 'https://8dsuewv5csufmdjbmdy8srdtkqkrepimqporlpdx.azurewebsites.net';
	var subdominio = '';
	var urlWebAPI = dominio + (subdominio == '' ? '' : '/' + subdominio) + '/api';
	
	$.fn.SetAndGetWebAPI_Get = function (Controller) {

		//debugger;
		var def = jQuery.Deferred();
		var resultData = {
			defStatus: 'empty',
			data: {}
		};

		$.ajax({
			url: `${urlWebAPI}/${Controller}`,
			type: 'GET',
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function (data) {
				resultData.defStatus = 'success';
				resultData.data = data;
				def.resolve(resultData);
			},
			error: function (data) {
				resultData.defStatus = 'error';
				resultData.data = data;
				def.reject(resultData);
			}
		});

		return def.promise();
	};

	$.fn.SetAndGetWebAPI_Post = function (Controller, JSONParameters) {

		var def = jQuery.Deferred();
		var resultData = {
			defStatus: 'empty',
			data: {}
		};

		$.ajax({
			url: `${urlWebAPI}/${Controller}`,
			type: 'POST',
			data: JSON.stringify(JSONParameters),
			contentType: 'application/json; charset=utf-8',
			dataType: 'json',
			success: function (data) {
				resultData.defStatus = 'success';
				resultData.data = data;
				def.resolve(resultData);
			},
			error: function (data) {
				resultData.defStatus = 'error';
				resultData.data = data;
				def.reject(resultData);
			}
		});

		return def.promise();
	};

})(jQuery);

$(document).ready(function () {

	//PENDING TO CODE

});
