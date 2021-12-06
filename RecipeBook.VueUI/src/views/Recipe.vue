<template>
	<div class="container">
		<!--<div class="p-4 my-3 bg-light rounded-3">
			<h2>Mary mother of Joseph</h2>
			<p>What is going on in today's world? Nobody knows. Except the one who does. But maybe he doesn't even know</p>
		</div>-->

		<b-alert
				 v-model="showErrorAlert"
				 class="position-fixed fixed-top m-0 rounded-0"
				 style="z-index: 2000;"
				 variant="danger"
				 dismissible>
				  {{ statusMsg }}
		</b-alert>


		<div class="row border p-4 my-3 bg-light rounded-3">

			<h3>{{ recipeName }}</h3>
			<b-tabs class="w-100">
				<b-tab title="Text View">
					<recipe-text-view :recipe-name="recipeName"
									  :description="description"
									  :cook-time-minutes="cookTimeMinutes"
									  :notes="notes"
									  :ingredients-text="ingredientsText"
									  :directions-text="directionsText"></recipe-text-view>
				</b-tab>

				<b-tab title="Edit View">
					<recipe-edit-view :recipe-id="recipeID"
									  :ingredient-list="ingredientList"
									  :ingredients-to-show="ingredientsToShow"
									  v-on:add-ingredient="addIngredient"
									  v-on:saved-recipe="syncObjs"></recipe-edit-view>
				</b-tab>
			</b-tabs>

		</div>
	</div>
</template>

<script>
	import axios from 'axios'
	import RecipeEditView from '@/components/RecipeEditView.vue'
	import RecipeTextView from '@/components/RecipeTextView.vue'

	export default {
		name: 'Recipe',
		components: {
			RecipeEditView, RecipeTextView },
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
				showErrorAlert: false,
				statusMsg: ''
			};
		},
		methods: {
			error(err) {
				console.log(err);
				this.showErrorAlert = true;
				this.statusMsg = 'Error - check the console';
			},
			addIngredient() {
				this.ingredientsToShow.push({
					ingredientList: this.ingredientList,
					selected: {},
					error: []
				});
			},
			syncObjs(recipe) {
				this.recipeID = recipe.recipeID;
				this.recipeName = recipe.name;
				this.description = recipe.description;
				this.cookTimeMinutes = recipe.cookTimeMinutes;
				this.notes = recipe.notes;
				this.directionsText = recipe.directions;
			}
		},
		computed: {
			getIngredientsText() {
				var ingredients = this.ingredientsToShow.map(x => {
					var text = x.selected.text + ' - ' + x.quantity;
					if (x.notes) {
						text += ' (' + x.notes + ')';
					}
					return text;
				})
				return ingredients.join(' \n')
			}
		},
		created() {
			this.ingredientsText = this.getIngredientsText
		},
		mounted() {
			if (this.$route.params.id > 0) {
				axios.get(this.url + '/api/Recipes/' + this.$route.params.id)
					.then(response => {
						this.recipeID = response.data.recipeID;
						this.recipeName = response.data.name;
						this.description = response.data.description;
						this.cookTimeMinutes = response.data.cookTimeMinutes;
						this.notes = response.data.notes;
						this.directionsText = response.data.directions;
						console.log(response)
					})
					.catch(this.error);
			}

			var self = this;
			axios.get(this.url + '/api/Ingredient')
				.then(response => {
					console.log(response.data);
					self.ingredientList = response.data.map(x => {
						return { id: x.ingredientID, text: x.name };
					});
					self.ingredientList.unshift({ id: -1, text: 'Select an option...' })

					return axios.get(this.url + '/api/RecipeIngredients/' + this.$route.params.id);
				})
				.then(response => {
					console.log(response)
					if (response.data && Array.isArray(response.data)) {
						response.data.forEach((ingredient) => {
							self.ingredientsToShow.push({
								selected: self.ingredientList.find(x => x.id == ingredient.ingredientID),
								ingredientList: self.ingredientList,
								quantity: ingredient.quantity,
								notes: ingredient.notes,
								error: []
							})
						});

						this.ingredientsText = this.getIngredientsText

						self.isLoading = false
					}
				})
				.catch(this.error);
		}
	}

</script>

<style scoped>
/*	li {
		display: inline-block;
		margin: 0 10px;
	}

	.text-view {
		white-space: pre-line;
	}
*/</style>

