<template>
	<div class="container">
		<!--<div class="p-4 my-3 bg-light rounded-3">
			<h2>Mary mother of Joseph</h2>
			<p>What is going on in today's world? Nobody knows. Except the one who does. But maybe he doesn't even know</p>
		</div>-->

		<div class="p-4 my-3 bg-light rounded-3 border">

			<recipe-edit-view :ingredient-list="ingredientList"
								:ingredients-to-show="ingredientsToShow"
								v-on:add-ingredient="addIngredient"></recipe-edit-view>
		</div>
	</div>
</template>

<script>
	import axios from 'axios'
	import RecipeEditView from '@/components/RecipeEditView.vue'

	export default {
		name: 'Recipe',
		components: { RecipeEditView },
		data() {
			return {
				isLoading: true,
				recipeID: -1,
				recipeName: '',
				description: '',
				cookTimeMinutes: '',
				notes: '',
				directionsText: '',
				ingredientsText: '',
				ingredientList: [],
				ingredientsToShow: [],
				url: 'https://test.contoso.com:5001',
				statusMsg: ''
			};
		},
		methods: {
			addIngredient() {
				this.ingredientsToShow.push({
					ingredientList: this.ingredientList,
					selected: {},
					error: []
				});
			},
		},
		computed: {
		},
		created() {
		},
		mounted() {
			// TODO: refactor this into a service (along with it in Recipe.vue)
			var self = this;
			axios.get(this.url + '/api/Ingredient')
				.then(response => {
					console.log(response.data);
					self.ingredientList = response.data.map(x => {
						return { id: x.ingredientID, text: x.name };
					});
					self.ingredientList.unshift({ id: -1, text: 'Select an option...' })
					self.isLoading = false
				})
				.catch(error => {
					console.log(error)
					self.isLoading = false
				});
		}
	}

</script>

<style scoped>
	li {
		display: inline-block;
		margin: 0 10px;
	}

	.text-view {
		white-space: pre-line;
	}
</style>

