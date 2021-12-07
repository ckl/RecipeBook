<template>
	<div>
		<alert-dismissable :message="toast.message"
						   :variant="toast.variant"
						   :timer="toast.timer">
		</alert-dismissable>

		<div class="border p-2 my-3 bg-light rounded-3">
			<b-container fluid>
				<b-row class="my-1">
					<b-col>
						<span class="float-right">
							<delete-recipe-modal :recipe-id="recipe.recipeID"
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
								:key="ingredient.ingredientID"
								:item="ingredient"
								:index="index"
								:value="ingredient.name"
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
					</b-col>
				</b-row>
				<!-- TODO: add validation -->
			</b-container>
		</div>
	</div>
</template>

<script>
	import axios from 'axios'
	import showToastWarn from '@/mixins/toast.mixin'
	import { mapGetters } from 'vuex'
	import AlertDismissable from '@/components/Alert.Dismissable.vue'
	import IngredientItem from '@/components/IngredientItem.vue'
	import DeleteRecipeModal from '@/components/DeleteRecipeModal.vue'
	import NewIngredientModal from '@/components/NewIngredientModal.vue'

	export default {
		name: 'RecipeEditView',
		components: { AlertDismissable, DeleteRecipeModal, IngredientItem, NewIngredientModal },
		props: {
			recipeID: Number,
			ingredientsToShow: Array,
		},
		mixins: [showToastWarn],
		data() {
			return {
				isLoading: false,	// TODO: bring over here
				toast: {
					msg: '',
					variant: '',
					timer: 0
				},
				url: 'https://test.contoso.com:5001'
			};
		},
		methods: {
			//error(err) {
			//	this.isLoading = false;
			//	console.log(err);
			//	//this.toast = {
			//	//	message: "Error saving the recipe",
			//	//	variant: 'danger',
			//	//	timer: 10
			//	//};
			//},
			//resetToast() {
			//	this.toast = {
			//		message: '',
			//		variant: '',
			//		timer: 0
			//	};
			//},
			//clickDeleteRecipe() {

			//},
			clickSaveRecipe() {
				// TODO: move form validation somewhere else
				let errors = [];
				let ingredients = this.ingredientsToShow.filter(x => {
					return x.ingredientID && x.ingredientID > 0;
				});

				ingredients.forEach(x => {
					if (!x.quantity) {
						errors.push({ ingredientID: x.ingredientID, message: `Missing quantity for ${x.name}` });
					}
				});

				if (errors.length > 0) {
					let msg = errors.join('\r\n');
					this.showToastWarn(msg);
					return;
				}

				this.isLoading = true;
				this.statusMsg = 'Loading...';

				var self = this;
				self.upsertRecipe(this.recipe).then((resp) => {

					if (resp.data) {
						self.recipe.recipeID = resp.data.recipeID;
					}
					// get all the ingredients
					var data = self.ingredientsToShow.map(x => {
						return {
							RecipeID: this.recipe.recipeID,
							IngredientID: x.ingredientID,
							Quantity: x.quantity,
							Notes: x.notes
						};
					});

					// save
					return axios.post(self.url + '/api/RecipeIngredients', data);
				}).then(() => {
					//console.log(response);
					self.isLoading = false;
					//self.statusMsg = 'Success';
					//self.$emit('saved-recipe', self.recipe);
					// TODO: fix this
					if (!this.recipe.recipeID) {
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
						variant: 'success',
						autoHideDelay: 5000,
					});
					resolve();
				});
			},
			redirect() {
				return new Promise((resolve) => {
					this.$router.push({ name: 'Recipes'/*, params: { id: this.recipeID }*/ });
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
			...mapGetters({
				recipe: 'recipe/currentRecipe'
			}),
		},
		mounted() {
		}
	}

</script>

<style scoped>
	ul {
		list-style-type: none;
		padding: 0;
	}
</style>

