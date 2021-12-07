<template>
	<div class="container">
		<div class="row border p-4 my-3 bg-light rounded-3">

			<h3>{{ currentRecipe.name }}</h3>
			<b-tabs class="w-100">
				<b-tab title="Text View">
					<recipe-text-view :recipe-name="currentRecipe.name"
									  :description="currentRecipe.description"
									  :cook-time-minutes="currentRecipe.cookTimeMinutes"
									  :notes="currentRecipe.notes"
									  :ingredients-text="ingredientsText"
									  :directions-text="currentRecipe.directions"></recipe-text-view>
				</b-tab>

				<b-tab title="Edit View">
					<recipe-edit-view :recipe-id="currentRecipe.recipeID"
									  :ingredients-to-show="ingredientsToShow"
									  v-on:add-ingredient="addIngredient"></recipe-edit-view>
				</b-tab>
			</b-tabs>
		</div>
	</div>
</template>

<script>
	import { mapGetters } from 'vuex'
	import toast from '@/mixins/toast.mixin'
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
			};
		},
		methods: {
			addIngredient() {
				this.ingredientsToShow.push({
					ingredientList: this.ingredientList,
				});
			},
		},
		computed: {
			...mapGetters({
				currentRecipe: 'recipe/currentRecipe',
				ingredientsToShow: 'recipe/currentRecipeIngredients',
				ingredientList: 'recipe/ingredients'
			}),
			//ingredientList() {
			//	return this.$store.getters.ingredients;
			//},
			ingredientsText() {
				if (! Array.isArray(this.ingredientsToShow)) {
					return;
				}
				var ingredients = this.ingredientsToShow.map(x => {
					var text = x.name + ' - ' + x.quantity;
					if (x.notes) {
						text += ' (' + x.notes + ')';
					}
					return text;
				})
				return ingredients.join(' \n')
			}
		},
		created() {
		},
		mounted() {
			this.$store.dispatch('recipe/fetchRecipe', this.$route.params.id)
				.then(this.$store.dispatch('recipe/fetchRecipeIngredients', this.$route.params.id))
				.catch(error => {
					this.showToastError(error)
				});
		},
	}

</script>

<style scoped>
</style>

