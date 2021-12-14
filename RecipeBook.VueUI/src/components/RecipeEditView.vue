<template>
	<div>
		<alert-dismissable
			:timer="errAlert.timer"
			:messages="errAlert.messages"
			:variant="errAlert.variant"
			@dismissed="errAlert.timer=0"></alert-dismissable>
		<div class="border p-2 my-3 bg-light rounded-3">
			<b-container fluid>
				<b-row class="my-1">
					<b-col>
						<span class="float-right" v-if="recipe.recipeId">
							<delete-recipe-modal :recipe-id="recipe.recipeId"
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
						<ul>
							<li is="ingredient-item" v-for="(ingredient, index) in ingredientsToShow"
								:key="ingredient.ingredientId"
								:item="ingredient"
								:index="index"
								:value="ingredient.name"
								:ingredient-details="ingredient"
								:class="{'error-border': ingredient.hasErrors}"
								v-on:remove="ingredientsToShow.splice(index, 1)">
							</li>
						</ul>

					</b-col>
				</b-row>
				<b-row align-h="between" class="my-1">
					<b-col md="2">
						<button v-on:click="ingredientsToShow.push({})" class="form-control">Add Ingredient</button>
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
						<b-button v-on:click="clickSaveRecipe" 
							:disabled="isLoading" 
							variant="success">Save Changes
							<b-spinner v-if="isLoading" small></b-spinner>	
						</b-button>
					</b-col>
				</b-row>
			</b-container>
		</div>
	</div>
</template>

<script>
	import { mapActions, mapGetters } from 'vuex'
	import IngredientItem from '@/components/IngredientItem.vue'
	import DeleteRecipeModal from '@/components/DeleteRecipeModal.vue'
	import NewIngredientModal from '@/components/NewIngredientModal.vue'
	import toast from '@/mixins/toast.mixin'
	import {
		RECIPE_CREATE,
		RECIPE_UPDATE,
		RECIPE_INGREDIENTS_CREATE
	} from '@/store/actions.type'
import AlertDismissable from './Alert.Dismissable.vue'

	export default {
		name: 'RecipeEditView',
		components: { DeleteRecipeModal, IngredientItem, NewIngredientModal, AlertDismissable },
		mixins: [ toast ],
		props: {
		},
		data() {
			return {
				errAlert: {
					timer: 0,
					messages: [],
					variant: ''
				}
			};
		},
		computed: {
			...mapGetters({
				recipe: 'recipe/currentRecipe',
				isLoading: 'recipe/isLoading',
				ingredientsToShow: 'recipe/currentRecipeIngredients'
			}),
		},
		methods: {
			...mapActions({
				createRecipeIngredients: `recipe/${RECIPE_INGREDIENTS_CREATE}`
			}),
			clickSaveRecipe() {
				// TODO: move form validation somewhere else
				this.errAlert.messages = []
				let ingredients = this.ingredientsToShow.filter(x => {
					return x.ingredientId && x.ingredientId > 0;
				});

				if (ingredients.length === 0) {
					this.errAlert.messages.push('Recipe has no ingredients')
				}

				if (!this.recipe.name || this.recipe.name.trim() === '') {
					this.errAlert.messages.push('Missing Recipe Name')
				}

				if (!this.recipe.cookTimeMinutes) {
					this.errAlert.messages.push('Missing Cook time')
				}

				let missingIngredient = false
				ingredients.forEach((el, i, arr) => {
					arr[i].hasErrors = false
					if (!el.quantity) {
						arr[i].hasErrors = true
						missingIngredient = true
					}
				});
				// TODO: ingredient names not being populated in el.name above. fix it
				if (missingIngredient) {
					this.errAlert.messages.push('Missing ingredient information')
				}

				if (!this.recipe.directions) {
					this.errAlert.messages.push('Missing cooking directions')
				}

				if (this.errAlert.messages.length > 0) {
					this.errAlert.timer = 10
					this.errAlert.variant = "danger"
					return;
				}

				let self = this;
				let action = typeof this.recipe.recipeId === 'undefined' ?
					`recipe/${RECIPE_CREATE}` :
					`recipe/${RECIPE_UPDATE}`

				this.$store
					.dispatch(action)
					.then(() => this.createRecipeIngredients())
					.then(() => {
						if (parseInt(this.$route.params.id) === self.recipe.recipeId) {
							self.$toast('Updated', 'Success', 'success')
						}
						else {
							// TODO: fix this - need to redirect to edit-view & show toaster on save
							// Look into this.$nextTick( () => { })
							this.$router.push({ name: 'Recipe', params: { id: self.recipe.recipeId }, hash: '#edit-view' }, () => {
								this.$nextTick(() => {
									self.$toast('Updated', 'Success', 'success')
								});
							});
						}
					})
					.catch((error) => {
						this.errAlert.timer = 10
						this.errAlert.variant = "danger"
						this.$toastError(error, 'Error saving stuff')
					})
			},
			validateForm() {

			}
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

	.error-border {
		border: 1px solid red;
	}
</style>

