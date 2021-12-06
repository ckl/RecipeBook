<template>
	<div>
		<b-alert v-model="showErrorAlert"
				 class="position-fixed fixed-top m-0 rounded-0"
				 style="z-index: 2000;"
				 variant="danger"
				 dismissible>
			{{ statusMsg }}
		</b-alert>

		<!--<b-alert :show="dismissCountDown"
				 dismissible
				 variant="warning"
				 @dismissed="dismissCountDown=0"
				 @dismiss-count-down="countDownChanged">
			<p>This alert will dismiss after {{ dismissCountDown }} seconds...</p>
			<b-progress variant="warning"
						:max="dismissSecs"
						:value="dismissCountDown"
						height="4px"></b-progress>
		</b-alert>-->

		<div class="border p-2 my-3 bg-light rounded-3">
			<b-container fluid>
				<b-row class="my-1">
					<b-col>
						<span class="float-right">
							<delete-recipe-modal :recipe-id="getRecipeID"
												 :recipe-name="recipe.name">
							</delete-recipe-modal>
						</span>
						<h4>Recipe Info</h4>
					</b-col>
				</b-row>
				<b-row class="my-1">
					<b-col sm="3">
						<label for="input-name1">Recipe Name:</label>
					</b-col>
					<b-col sm="7">
						<b-form-input v-model="recipe.name" required id="input-name1" placeholder="Recipe name..."></b-form-input>
					</b-col>
				</b-row>
				<b-row class="my-1">
					<b-col sm="3">
						<label for="input-name2">Description:</label>
					</b-col>
					<b-col sm="7">
						<b-form-input v-model="recipe.description" id="input-name2" placeholder="Description..."></b-form-input>
					</b-col>
				</b-row>
				<b-row class="my-1">
					<b-col sm="3">
						<label for="input-name3">Cook time (minutes):</label>
					</b-col>
					<b-col sm="7">
						<b-form-input v-model="recipe.cookTimeMinutes" id="input-name3" placeholder="Cook time in minutes..."></b-form-input>
					</b-col>
				</b-row>
				<b-row class="my-1">
					<b-col sm="3">
						<label for="input-name4">Notes:</label>
					</b-col>
					<b-col sm="7">
						<b-form-input v-model="recipe.notes" id="input-name4" placeholder="Notes..."></b-form-input>
					</b-col>
				</b-row>
			</b-container>
		</div>
		<div class="border p-2 my-3 bg-light rounded-3">
			<b-container fluid>
				<b-row class="my-1">
					<b-col>
						<h4>Ingredients</h4>
					</b-col>
				</b-row>
				<b-row class="my-1">
					<b-col>
						<!-- TODO: change to list group -->
						<!-- https://www.creative-tim.com/learning-lab/bootstrap-vue/list-group/argon-dashboard -->
						<ul>
							<li is="ingredient-item" v-for="(ingredient, index) in ingredientsToShow"
								:key="ingredient.selected.id"
								:item="ingredient.selected"
								:index="index"
								:value="ingredient.selected.text"
								:ingredient-list="ingredientList"
								:ingredient-details="ingredient"
								v-on:remove="ingredientsToShow.splice(index, 1)">
							</li>
						</ul>

					</b-col>
				</b-row>
				<b-row align-h="between" class="my-1">
					<b-col md="2">
						<button v-on:click="$emit('add-ingredient')" class="form-control">Add Ingredient</button>
					</b-col>
					<b-col md="2">
						<new-ingredient-modal></new-ingredient-modal>
					</b-col>
				</b-row>
			</b-container>
		</div>
		<div class="border p-2 my-3 bg-light rounded-3">
			<b-container>
				<b-row class="my-1">
					<b-col>
						<b-form-textarea v-model="recipe.directions" rows="10" placeholder="Directions"></b-form-textarea>
					</b-col>
				</b-row>
				<b-row class="my-1">
					<b-col>
						<b-button v-on:click="clickSaveRecipe" :disabled="isLoading" variant="success">Save Changes</b-button>
						{{ statusMsg }}
					</b-col>
				</b-row>
				<!-- TODO: add validation -->
			</b-container>
		</div>

		<!--<div class="p-4 my-3 bg-light rounded-3 form-control">
		<h5>Ingredients</h5>
		<ul>
			<li is="ingredient-item" v-for="(ingredient, index) in ingredientsToShow"
				:key="ingredient.selected.id"
				:item="ingredient.selected"
				:index="index"
				:value="ingredient.selected.text"
				:ingredient-list="ingredientList"
				:ingredient-details="ingredient"
				v-on:remove="ingredientsToShow.splice(index, 1)"
				class="form-control">
			</li>
		</ul>

		<button v-on:click="$emit('add-ingredient')" class="form-control">Add Ingredient</button>
		<new-ingredient-modal></new-ingredient-modal>

		<div class="p-4 my-3 bg-light rounded-3">
			<h5>Directions</h5>
			<textarea class="form-control" v-model="recipe.directions" rows="10" cols="80"></textarea>

			<div>
				<button v-on:click="clickSaveRecipe" :disabled="isLoading" class="btn btn-primary form-control">Save</button>
				<span v-if="statusMsg">{{ statusMsg }}</span>
			</div>
		</div>
	</div>-->
	</div>
</template>

<script>
	import axios from 'axios'
	import IngredientItem from '@/components/IngredientItem.vue'
	import DeleteRecipeModal from '@/components/DeleteRecipeModal.vue'
	import NewIngredientModal from '@/components/NewIngredientModal.vue'

	export default {
		name: 'RecipeEditView',
		components: { DeleteRecipeModal, IngredientItem, NewIngredientModal },
		props: {
			recipeID: Number,
			ingredientList: Array,
			ingredientsToShow: Array,
		},
		data() {
			return {
				isLoading: false,	// TODO: bring over here
				recipName: this.recipeName,
				recipe: {
					recipeID: this.recipeID,
					name: '',
					description: '',
					directions: '',
					cookTimeMinutes: 0,
					notes: ''
				},
				statusMsg: '',		// TODO: same
				showErrorAlert: false,
				url: 'https://test.contoso.com:5001'
			};
		},
		methods: {
			error(err) {
				this.isLoading = false;
				console.log(err);
				this.showErrorAlert = true;
				this.statusMsg = 'Error - check the console';
			},
			clickDeleteRecipe() {

			},
			clickSaveRecipe() {
				var hasErrors = false;
				for (var i = 0; i < this.ingredientsToShow.length; ++i) {
					this.ingredientsToShow[i].error = []
					if (!this.ingredientsToShow[i].selected || !this.ingredientsToShow[i].selected.id || this.ingredientsToShow[i].selected.id <= 0) {
						hasErrors = true;
						this.ingredientsToShow[i].error.push('Missing ingredient');
					}
					if (!this.ingredientsToShow[i].quantity) {
						hasErrors = true;
						this.ingredientsToShow[i].error.push('Missing quantity');
					}
				}

				if (hasErrors) {
					return;
				}

				this.isLoading = true;
				this.statusMsg = 'Loading...';

				var self = this;
				this.recipe.recipeID = this.getRecipeID;
				self.upsertRecipe(this.recipe).then((resp) => {

					if (resp.data) {
						self.recipe.recipeID = resp.data.recipeID;
					}
					// get all the ingredients
					var data = self.ingredientsToShow.map(x => {
						return {
							RecipeID: this.recipe.recipeID,
							IngredientID: x.selected.id,
							Quantity: x.quantity,
							Notes: x.notes
						};
					});

					// save
					return axios.post(self.url + '/api/RecipeIngredients', data);
				}).then(() => {
					//console.log(response);
					self.isLoading = false;
					self.statusMsg = 'Success';
					self.$emit('saved-recipe', self.recipe);
					// TODO: fix this
					if (!this.$route.params.id) {
						return this.redirect().then(this.makeToast).catch(this.error);
					}
					return this.makeToast();
				})
				.catch(this.error);
			},
			makeToast() {
				return new Promise((resolve) => {
					this.$bvToast.toast("Updated", {
						title: 'Success',
						autoHideDelay: 5000,
					});
					resolve();
				});
			},
			redirect() {
				return new Promise((resolve) => {
					this.$router.push({ name: 'Recipe', params: { id: this.recipeID } });
					resolve();
				});
			},
			upsertRecipe(recipe) {
				if (recipe.recipeID > 0) {
					return axios.put(this.url + '/api/Recipes/' + recipe.recipeID, recipe);
				}
				else if (this.$route.params.id > 0) {
					recipe.recipeID = this.$route.params.id;
					return axios.put(this.url + '/api/Recipes/' + recipe.recipeID, recipe);
				}
				else {
					return axios.post(this.url + '/api/Recipes', recipe);
				}
			},
		},
		computed: {
			getRecipeID() {
				if (this.recipeID > 0) {
					return this.recipeID;
				}
				else if (this.$route.params.id > 0) {
					return this.$route.params.id;
				}

				return 0;
			},
		},
		mounted() {
			if (this.$route.params.id > 0) {
				axios.get(this.url + '/api/Recipes/' + this.$route.params.id)
					.then(response => {
						this.recipe.name = response.data.name;
						this.recipe.description = response.data.description;
						this.recipe.cookTimeMinutes = response.data.cookTimeMinutes;
						this.recipe.notes = response.data.notes;
						this.recipe.directions = response.data.directions;
						//console.log(response)
					})
					.catch(this.error);
			}

		}
	}

</script>

<style scoped>
	ul {
		list-style-type: none;
		padding: 0;
	}
</style>

