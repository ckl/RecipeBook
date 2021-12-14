<template>
	<div class="container">
		<div class="row border p-4 my-3 bg-light rounded-3">
			<h3>{{ currentRecipe.name }}</h3>


			<b-card class="w-100">
				<b-tabs v-model="tabIndex" class="w-100">
					<b-tab title="Text View" href="text-view">
						<recipe-text-view :recipe-name="currentRecipe.name"
										:description="currentRecipe.description"
										:cook-time-minutes="currentRecipe.cookTimeMinutes"
										:notes="currentRecipe.notes"
										:ingredients-text="ingredientsText"
										:directions-text="currentRecipe.directions"></recipe-text-view>
					</b-tab>

					<b-tab title="Edit View" href="edit-view">
							<recipe-edit-view :recipe-id="currentRecipe.recipeId"></recipe-edit-view>
					</b-tab>
				</b-tabs>
			</b-card>
		</div>
	</div>
</template>

<script>
	import { mapActions, mapGetters } from 'vuex'
	import toast from '@/mixins/toast.mixin'
	import RecipeEditView from '@/components/RecipeEditView.vue'
	import RecipeTextView from '@/components/RecipeTextView.vue'
	import {
		GET_RECIPE,
		GET_RECIPE_INGREDIENTS
	} from '@/store/actions.type'

	export default {
		name: 'Recipe',
		components: {
			RecipeEditView, RecipeTextView
		},
		mixins: [toast],
		data() {
			return {
				tabIndex: 0,
				tabs: ['#text-view', '#edit-view']
			};
		},
		methods: {
			...mapActions({
				getRecipe: `recipe/${GET_RECIPE}`,
				getRecipeIngredients: `recipe/${GET_RECIPE_INGREDIENTS}`
			}),
			tabCheck() {
				this.$nextTick(() => {
					this.tabIndex = this.tabs.findIndex(tab => tab === this.$route.hash)
				})
			}
		},
		computed: {
			...mapGetters({
				currentRecipe: 'recipe/currentRecipe',
				ingredientsToShow: 'recipe/currentRecipeIngredients',
			}),
			ingredientsText() {
				if (! Array.isArray(this.ingredientsToShow)) {
					return;
				}
				let ingredients = this.ingredientsToShow.map(x => {
					let text = `${x.name} - ${x.quantity}`;
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
			let id = this.$route.params.id
			this.tabCheck()
			this.getRecipe(id)
				.then(() => this.getRecipeIngredients(id))
				.catch(error => this.$toastError(error, `Getting RecipeId ${id}`))
		},
	}

</script>

<style scoped>
</style>

