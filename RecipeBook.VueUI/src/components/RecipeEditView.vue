<template>
	<div>
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
			</b-container>
		</div>
	</div>
</template>

<script>
	import { mapGetters } from 'vuex'
	import IngredientItem from '@/components/IngredientItem.vue'
	import DeleteRecipeModal from '@/components/DeleteRecipeModal.vue'
	import NewIngredientModal from '@/components/NewIngredientModal.vue'
	import toast from '@/mixins/toast.mixin'

	export default {
		name: 'RecipeEditView',
		components: { DeleteRecipeModal, IngredientItem, NewIngredientModal },
		props: {
			recipeID: Number,
			ingredientsToShow: Array,
		},
		mixins: [ toast ],
		data() {
			return {
				isLoading: false,	// prevent double clicking on buttons
			};
		},
		methods: {
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

				let self = this;
				let action = typeof this.recipe.recipeID === 'undefined' ? "recipe/createRecipe" : "recipe/updateRecipe"
				this.$store
					.dispatch(action)
					.then(() => this.$store.dispatch('recipe/createRecipeIngredients'))
					.then(() => {
						self.isLoading = false;
						// TODO: fix this
						//if (parseInt(this.$route.params.id) > 0) {
						//	return this.makeToast();
						//}
						//return this.redirect().then(this.makeToast).catch(this.error);
						var x = self.recipe.recipeID;
						if (parseInt(this.$route.params.id) === self.recipe.recipeID) {
							self.showToastSuccess('Updated');
						}
						else {
							this.$router.push({ name: 'Recipe', params: { id: x } }, () => {
								self.showToastSuccess('Updated');
							});
						}
					})
					.catch((error) => {
						this.showToastError({ex: error, message: 'Error saving stuff'})
					});
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
				// TODO: redirect to created page
				return new Promise((resolve) => {
					this.$router.push({ name: 'Recipes'/*, params: { id: this.recipeID }*/ });
					resolve();
				});
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

