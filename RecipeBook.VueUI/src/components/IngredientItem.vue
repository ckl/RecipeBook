<template>
    <div class="container">
        <!--<div class="row">
            <div class="col-sm-4">
                <select v-model="ingredientDetails.selected">
                    <option v-for="(ingredient, index) in ingredientList"
                            :key="index"
                            class="form-control"
                            :value="ingredient">-->
                            <!--:value="ingredient.ingredientID"-->
                        <!--{{ ingredient.text }}
                    </option>
                </select>
            </div>
            <div class="col-sm-2">
                <input type="text" class="form-control" placeholder="Quantity" v-model="ingredientDetails.quantity" />
            </div>
            <div class="col-sm-2">
                <input type="text" class="form-control" placeholder="Notes" v-model="ingredientDetails.notes" />
            </div>
            <div class="col-sm-4">
                <button v-on:click="$emit('remove')">Remove</button>
                <span v-if="ingredientDetails.error.length">
                    {{ buildErrorMsg }}
                </span>
                </div>
            </div>-->
        <b-row class="my-1">
            <b-input-group>
                <template #prepend>
                    <b-form-select v-model="ingredientDetails.selected.id"
                                   :options="ingredientList"
                                   value-field="id"
                                   text-field="text"></b-form-select>
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
            ingredientList: Array,
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
