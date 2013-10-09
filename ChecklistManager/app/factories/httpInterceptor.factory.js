toastr.options = {
    "debug": false,
    "positionClass": "toast-bottom-full-width",
    "onclick": null,
    "fadeIn": 300,
    "fadeOut": 1000,
    "timeOut": 5000,
    "extendedTimeOut": 1000
};

function MyHttpInterceptor($q) {

    function getMessageFromError(error) {
        if (error.data.ExceptionMessage) {
            return {
                isHTML: false,
                status: error.status,
                message: error.data.ExceptionMessage
            };
        }

        return {
            isHtml: true,
            status: error.status,
            message: error.data
        };
    }
    return {
        // optional method
        'request': function (config) {
            // do something on success
            return config || $q.when(config);
        },

        // optional method
        'requestError': function (rejection) {
            //if (canRecover(rejection)) {
            //    return responseOrNewPromise
            //}
            var error  = getMessageFromError(rejection);
            toastr.error(error.message, 'Status=' + error.status);

            return $q.reject(rejection);
        },

        // optional method
        'response': function (response) {
            // do something on success
            return response || $q.when(response);
        },

        // optional method
        'responseError': function (rejection) {
            //if (canRecover(rejection)) {
            //    return responseOrNewPromise
            //}
            var error = getMessageFromError(rejection);
            toastr.error(error.message, 'Status=' + error.status);
            return $q.reject(rejection);
        }
    };
}