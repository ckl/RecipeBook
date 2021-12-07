<template>
    <div class="container">
        <b-row class="my-1">
            <b-input-group>
                <template #prepend>
                    <b-form-select v-model="ingredientDetails.selected.ingredientID"
                                   :options="ingredientList"
                                   value-field="ingredientID"
                                   text-field="name"></b-form-select>
                </template>

				<b-form-input v-model="ingredientDetails.quantity" placeholder="Quantity"></b-form-input>
				<b-form-input v-model="ingredientDetails.notes" placeholder="Notes"></b-form-input>

                <b-button v-on:click="$emit('remove')">Remove</b-button>
                <span v-if="ingredientDetails.error.length">
                    {{ buildErrorMsg }}
                </span>

            </b-input-group>
        </b-row>
    </div>
</template>

<script>
    export default {
        name: 'IngredientItem',
        props: {
            ingredientDetails: {
                quantity: String,
                notes: String,
				selected: Object,
                error: Array,
            }
        },
        data() {
            return {
            };
        },
        methods: {
            _buildErrorMsg: function () {
				return this.ingredientDetails.error.join('; ');
			}
        },
        computed: {
            buildErrorMsg() {
                return this._buildErrorMsg();
            },
            ingredientList() {
                return this.$store.getters.ingredients;
			}
        },
        mounted() {
		}
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
