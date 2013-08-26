function MyHttpInterceptor($q) {
    return {
        // optional method
        'request': function (config) {
            // do something on success
            return config || $q.when(config);
        },

        // optional method
        'requestError': function (rejection) {
            // do something on error
            toastr.error(rejection.data.ExceptionMessage, 'Status=' + rejection.status);
            //if (canRecover(rejection)) {
            //    return responseOrNewPromise
            //}
            return $q.reject(rejection);
        },

        // optional method
        'response': function (response) {
            // do something on success
            return response || $q.when(response);
        },

        // optional method
        'responseError': function (rejection) {
            // do something on error
            //if (canRecover(rejection)) {
            //    return responseOrNewPromise
            //}
            toastr.error(rejection.data.ExceptionMessage, 'Status=' + rejection.status);
            return $q.reject(rejection);
        }
    };
}