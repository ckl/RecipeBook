<template>
    <div class="container">
        <b-row class="my-1">
            <b-input-group>
                <template #prepend>
                    <b-form-select v-model="ingredientDetails.ingredientId"
                                   :options="ingredientList"
                                   value-field="ingredientId"
                                   text-field="name"></b-form-select>
                </template>

				<b-form-input v-model="ingredientDetails.quantity" placeholder="Quantity"></b-form-input>
				<b-form-input v-model="ingredientDetails.notes" placeholder="Notes"></b-form-input>

                <b-button v-on:click="$emit('remove')">Remove</b-button>
            </b-input-group>
        </b-row>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'

    export default {
        name: 'IngredientItem',
        props: {
            ingredientDetails: Object
        },
        data() {
            return {
            };
        },
        methods: {
        },
        computed: {
            ...mapGetters({
                ingredientList: 'ingredient/ingredients'
			})
        },
        mounted() {
		}
    }
</script>

<style scoped>
</style>
