import Vue from 'vue'
import App from './App.vue'
import router from './router'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

Vue.config.productionTip = false

Vue.config.errorHandler = function (err, vm, info) {
	// handle error
	// `info` is a Vue-specific error info, e.g. which lifecycle hook
	// the error was found in. Only available in 2.2.0+
	console.log(err);
	this.$bvToast.toast("Vue Error", {
		title: info,
		variant: 'danger',
		autoHideDelay: 10000,
	});
}

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
