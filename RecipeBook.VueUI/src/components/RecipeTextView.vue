<template>
	<div class="border p-4 my-3 bg-light rounded-3">
		<div class="form-horizontal">
			<h5>Recipe Info:</h5>

			<p>
				<span class="fw-bold">Name:</span> {{ recipeName }}<br />
				<span class="fw-bold">Description:</span> {{ description }}<br />
				<span class="fw-bold">Cook Time (minutes):</span> {{ cookTimeMinutes }}<br />
				<span class="fw-bold">Notes:</span> {{ notes }}<br />
			</p>
			<!-- TODO: validate Recipe name -->

			<h5>Ingredients:</h5>
			<p class="text-view">
				{{ ingredientsText }}
			</p>

			<h5>Directions:</h5>
			<p class="text-view">
				{{ directionsText }}
			</p>
		</div>
	</div>
</template>

<script>
	export default {
		name: 'RecipeTextView',
		props: {
			recipeName: String,
			description: String,
			cookTimeMinutes: Number,
			notes: String,
			ingredientsText: String,
			directionsText: String,
		},
		data() {
			return { };
		},
		methods: {
		},
		computed: {
		},
		mounted() {
		}
	}

</script>

<style scoped>
	.text-view {
		white-space: pre-line;
	}

</style>

