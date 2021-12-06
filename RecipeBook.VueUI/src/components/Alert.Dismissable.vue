<template>
    <div class="container">
        <b-alert :show="timer"
             dismissible
             :variant="variant"
             @dismissed="dismissCountDown=0"
             @dismiss-count-down="countDownChanged"
             class="position-fixed fixed-top m-0 rounded-0"
			 style="z-index: 2000;">
        <p>{{ message }}</p>
        <b-progress :variant="variant"
                    :max="timer"
                    :value="dismissCountDown"
                    height="4px"></b-progress>
    </b-alert>
    </div>
</template>

<script>
    export default {
        name: 'AlertDismissable',
        props: {
            message: String,
            variant: String,
			timer: Number,
        },
        data() {
            return {
                dismissCountDown: 0
            };
        },
        methods: {
            countDownChanged(dismissCountDown) {
                this.dismissCountDown = dismissCountDown;
			},
			//showAlert() {
			//	this.dismissCountDown = this.timer
			//}
        },
        computed: {
            buildErrorMsg() {
                return this._buildErrorMsg();
            },
        },
        mounted() {
		}
    }
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
