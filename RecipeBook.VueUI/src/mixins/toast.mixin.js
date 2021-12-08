const toast = {
    methods: {
        $toast: function (message, title, variant) {
            this.$bvToast.toast(title, {
                title: message,
                variant: variant,
                autoHideDelay: 10000,
            });
        },

        //showToastSuccess: function (message, title = 'Success') {
        //    this.$bvToast.toast(title, {
        //        title: message,
        //        variant: 'success',
        //        autoHideDelay: 10000,
        //    });
        //},
        //showToastWarn: function (message, title = 'Warning') {
        //    this.$bvToast.toast(title, {
        //        title: message,
        //        variant: 'warning',
        //        autoHideDelay: 10000,
        //    });
        //},
        // $toastError: function (params) {
        //     var message = params.message,
        //         title = params.title || 'Error',
        //         ex = params.ex;

        //     console.error(`Error: ${message}\nDetails: ${ex}`);
        //     this.$bvToast.toast(title, {
        //         title: message,
        //         variant: 'danger',
        //         autoHideDelay: 10000,
        //     });
        // }
		$toastError: function (response, message) {
            let status, statusText, error, title;
			
			if (response.response) {
				let resp = response.response;
				status = resp.status,
                statusText = resp.statusText,
				error = resp.data.errors.id[0],
				title = `${status} - ${statusText}`;
			}
			else {
				title = response.message;
				error = 'Error';
			}

			if (typeof message === 'string') {
				error += ` ${message}`;
			}

            console.error(`Error ${status}: ${statusText}: ${response}`);
			console.error(response);
            this.$bvToast.toast(error, {
                title: title,
                variant: 'danger',
                autoHideDelay: 10000,
            });
        }
    }
};

export default toast;