<template>
	<div class="container">
		<!--<div class="p-4 my-3 bg-light rounded-3">
			<h2>Mary mother of Joseph</h2>
			<p>What is going on in today's world? Nobody knows. Except the one who does. But maybe he doesn't even know</p>
		</div>-->

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
	import toast from '@/mixins/toast-mixin'
	import RecipeEditView from '@/components/RecipeEditView.vue'
	import RecipeTextView from '@/components/RecipeTextView.vue'

	export default {
		name: 'Recipe',
		components: {
			RecipeEditView, RecipeTextView
		},
		mixins: [toast],
		data() {
			return {
				isLoading: true,
				recipeID: -1,
				recipeName: '',
				description: '',
				cookTimeMinutes: 0,
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
			},
			getIngredients() {
				return new Promise((resolve, reject) => {
					axios.get(this.url + '/api/Ingredient')
						.then(response => {
							this.ingredientList = response.data.map(x => {
								return { id: x.ingredientID, text: x.name };
							});
							this.ingredientList.unshift({ id: -1, text: 'Select an option...' })
							resolve();
						})
						.catch((error) => {
							reject({ msg: 'Error loading ingredient list', ex: error });
						});
				});
			},
			getRecipe(id) {
				return new Promise((resolve, reject) => {
					if (parseInt(id) <= 0) {
						return resolve();
					}

					axios.get(this.url + '/api/Recipes/' + id)
						.then(response => {
							this.recipeID = response.data.recipeID;
							this.recipeName = response.data.name;
							this.description = response.data.description;
							this.cookTimeMinutes = response.data.cookTimeMinutes;
							this.notes = response.data.notes;
							this.directionsText = response.data.directions;
							resolve();
						})
						.catch((error) => {
							reject({ msg: `Error retrieving recipe with ID: ${id}`, ex: error });
						});
				});
			},
			getRecipeIngredients(id) {
				return new Promise((resolve, reject) => {
					axios.get(this.url + '/api/RecipeIngredients/' + id)
						.then(response => {
							if (response.data && Array.isArray(response.data)) {
								response.data.forEach((ingredient) => {
									this.ingredientsToShow.push({
										selected: this.ingredientList.find(x => x.id == ingredient.ingredientID),
										ingredientList: this.ingredientList,
										quantity: ingredient.quantity,
										notes: ingredient.notes,
										error: []
									})
								});

								this.ingredientsText = this.getIngredientsText;	// TODO: is this needed?
							}
							resolve();
						})
						.catch((error) => {
							reject({ msg: `Error retrieving recipe ingredients for recipeID: ${id}`, ex: error });
						});
				});
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
			var self = this;
			this.recipeID = this.$route.params.id;
			this.getIngredients()
				.then(() => this.getRecipe(this.recipeID))
				.then(() => this.getRecipeIngredients(this.recipeID))
				.then(() => {
					self.isLoading = false;
				})
				.catch((error) => {
					this.showToastError({ message: error.msg, ex: error.ex });
				});
		},
	}

</script>

<style scoped>
</style>

