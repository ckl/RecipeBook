<template>
	<div class="container">
		<div class="p-4 my-3 bg-light rounded-3 border">

			<recipe-edit-view :ingredients-to-show="ingredientsToShow"
							  v-on:add-ingredient="ingredientsToShow.push({})"></recipe-edit-view>
		</div>
	</div>
</template>

<script>
	import RecipeEditView from '@/components/RecipeEditView.vue'
	import { RECIPE_RESET_STATE } from '@/store/actions.type'

	export default {
		name: 'Recipe',
		components: { RecipeEditView },
		data() {
			return {
				ingredientsToShow: [],
			};
		},
		async beforeRouteUpdate(to, from, next) {
			await this.$store.dispatch(`recipe/${RECIPE_RESET_STATE}`);
			next();
		},
		methods: {
		},
		computed: {
		},
		created() {
		},
		mounted() {
			this.$store.dispatch(`recipe/${RECIPE_RESET_STATE}`)
				.then(() => { console.log('done')})
				.catch((err) => { console.log(err)})
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

