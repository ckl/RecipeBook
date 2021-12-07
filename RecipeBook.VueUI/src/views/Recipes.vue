<template>
	<div class="container text-start">
		<div class="p-4 my-3 bg-light rounded-3">
			<h2>Mary mother of Joseph</h2>
			<p>What is going on in today's world? Nobody knows. Except the one who does. But maybe he doesn't even know</p>
		</div>

		<div class="p-4 my-3 bg-light rounded-3">
			<span class="h3">Recipes</span>
			<button @click="$router.push('/new-recipe')" class="btn btn-primary float-right">New</button>
			<div class="list-group mt-3">
				<div class="list-group-item" v-for="recipe in recipes" :key="recipe.recipeID">
					<router-link :to="{ name: 'Recipe', params: { id: recipe.recipeID }}">
						{{ recipe.name }}
					</router-link>
					- {{ recipe.description }}
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import { mapGetters } from 'vuex'
	import toast from '@/mixins/toast.mixin'

	export default {
		name: 'Recipes',
		mixins: [toast],
		data() {
			return {
			};
		},
		methods: {
		},
		computed: {
			...mapGetters({
				recipes: 'recipe/recipes'
			})
		},
		mounted() {
			this.$store.dispatch('recipe/fetchRecipes')
				.catch(error => {
					this.showToastError({ message: 'Failed to get recipe list', ex: error });
				})
		}
	}

</script>
<style scoped>
a {
	color: #42b983;
}
</style>
