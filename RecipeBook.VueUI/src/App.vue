<template>
	<div id="app">
		<nav class="navbar navbar-expand-lg navbar-light" style="background-color: #ddeeff;">
			<div class="container fluid">
				<router-link to="/" class="navbar-brand">Recipe Book</router-link>
				<div class="collapse navbar-collapse" id="navbarCollapse">
					<div class="navbar-nav">
						<router-link to="/" class="nav-item nav-link">Home</router-link>
						<router-link to="/" class="nav-item nav-link">Tags</router-link>
						<router-link to="/recipes" class="nav-item nav-link">Recipes</router-link>
						<!--<router-link to="/recipe" class="nav-item nav-link">Ingredients</router-link>-->
						<router-link to="/about" class="nav-item nav-link">About</router-link>
					</div>
					<div class="navbar-nav ms-auto">
						<router-link to="/TestApi" class="nav-item nav-link">Test API</router-link>
					</div>
				</div>
			</div>
		</nav>
		<router-view />
	</div>
</template>

<script>

	import toast from '@/mixins/toast.mixin.js'
	import {
		GET_INGREDIENTS
	} from '@/store/actions.type'

	export default {
		mixins: [toast],
		mounted() {
			// Handle promise exceptions
			window.addEventListener('unhandledrejection', function (event) {
				// event.promise contains the promise object
				// event.reason contains the reason for the rejection
				if (event && event.reason && event.reason.message) {
					this.$toastError({ message: event.reason.message, title: 'Network Error', ex: event });
				}
			}.bind(this));

			// Handle general exceptions
			window.onerror = function (msg, url, line, col, error) {
				//code to handle or report error goes here
				console.error(`window.onerror: ${msg}`);
				console.error(error);
				this.$toastError(msg);
			}

			this.$store.dispatch(`ingredient/${GET_INGREDIENTS}`)
				.catch(error => {
					this.$toastError(error, 'Fetching ingredients');
				});
		}
	}
</script>

<style>
	#app {
		font-family: Avenir, Helvetica, Arial, sans-serif;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
		color: #2c3e50;
	}

	#nav {
		padding: 30px;
	}

	#nav a {
		font-weight: bold;
		color: #2c3e50;
	}

	#nav a.router-link-exact-active {
		color: #42b983;
	}
</style>
