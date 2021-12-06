const toast = {
    methods: {
        showToastSuccess: function (message, title = 'Success') {
            this.$bvToast.toast(title, {
                title: message,
                variant: 'success',
                autoHideDelay: 10000,
            });
        },
        showToastError: function (message, title = 'Error') {
            this.$bvToast.toast(title, {
                title: message,
                variant: 'danger',
                autoHideDelay: 10000,
            });
        }
    }
};

export default toast;