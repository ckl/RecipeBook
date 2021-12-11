<template>
    <div class="container">
		<b-alert :show="timer"
             dismissible
             :variant="variant"
			 :messages="messages"
             @dismissed="$emit('dismissed')"
             @dismiss-count-down="countDownChanged"
             class="position-fixed fixed-top m-0 rounded-0"
			 style="z-index: 2000;">
			 <ul>
				 <li v-for="message in messages" :key="message">
					{{ message }}
				</li>
			</ul>
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
            messages: Array,
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
        },
        computed: {
        },
        mounted() {
		}
    }
</script>

<style scoped>
a {
  color: #42b983;
}
</style>
