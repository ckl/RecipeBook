<template>
	<div class="container">
		<div class="p-4 my-3 bg-light rounded-3 border">

			<recipe-edit-view :ingredients-to-show="ingredientsToShow"
							  v-on:add-ingredient="addIngredient"></recipe-edit-view>
		</div>
	</div>
</template>

<script>
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
			ingredientList() {
				return this.$store.getters.ingredientList;
			}
		},
		created() {
		},
		mounted() {
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

