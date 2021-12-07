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
	import axios from 'axios'

	export default {
		name: 'Recipes',
		data() {
			return {
				recipes: [],
				url: 'https://localhost:5001',
			};
		},
		methods: {
		},
		computed: {
		},
		mounted() {
			axios.get(this.url + '/api/Recipes')
				.then(response => {
					console.log(response.data);
					this.recipes = response.data;
				})
		}
	}

</script>
