<template>
	<div class="container text-start">
		<div class="p-4 my-3 bg-light rounded-3">
			<h2>Mary mother of Joseph</h2>
			<p>
				What is going on in today's world? Nobody knows. Except the one
				who does. But maybe he doesn't even know
			</p>
		</div>

		<div class="p-4 my-3 bg-light rounded-3">
			<span class="d-flex justify-content-between">
				<b-form-input
					v-model="filterRecipe"
					placeholder="Search..."
					class="float-center col-5"></b-form-input>
				<button
					@click="$router.push('/new-recipe')"
					class="btn btn-primary col-1">
					New
				</button>
			</span>
			<div class="list-group mt-3">
				<b-table
					striped
					hover
					:items="recipes"
					:fields="fields"
					:filter="filterRecipe"
					:filter-included-fields="['name', 'description']">
					<template #cell(name)="data">
						<router-link
							:to="{
								name: 'Recipe',
								params: { id: data.item.recipeId },
							}">
							{{ data.item.name }}
						</router-link>
					</template>
				</b-table>
			</div>
		</div>
	</div>
</template>

<script>
import { mapActions, mapGetters } from "vuex"
import toast from "@/mixins/toast.mixin"
import { GET_RECIPES } from "@/store/actions.type"

export default {
	name: "Recipes",
	mixins: [toast],
	data() {
		return {
			filterRecipe: "",
			fields: [
				{ key: "recipeId", sortable: true },
				{ key: "name", sortable: true },
				{ key: "description" },
				{
					key: "cookTimeMinutes",
					label: "Cook Time (minutes)",
					sortable: true,
				},
			],
		}
	},
	methods: {
		...mapActions({
			getRecipes: `recipe/${GET_RECIPES}`,
		}),
	},
	computed: {
		...mapGetters({
			recipes: "recipe/recipes",
		}),
	},
	mounted() {
		this.getRecipes().catch((error) =>
			this.$toastError(error, "Getting recipe list")
		)
	},
}
</script>
<style scoped>
a {
	color: #42b983;
}
</style>
