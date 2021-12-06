<template>
    <div>
        <button class="form-control" data-bs-target="#myModal" data-bs-toggle="modal">New Ingredient</button>
        <div class="modal" id="myModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">New ingredient</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Name:</label>
                                <div class="col-sm-8">
                                    <input type="text" placeholder="Ingredient name..." class="form-control" />
                                </div>
                                <label class="col-sm-2 control-label">Notes:</label>
                                <div class="col-sm-8">
                                    <input type="text" placeholder="Notes..." class="form-control" />
                                </div>
                            </div>
                            <!-- TODO: validate Recipe name -->
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'NewIngredientModal',
        props: {
            ingredientList: Array,
            ingredientDetails: {
                itemID: Number,
                quantity: String,
                notes: String,
				selectedID: Number,
                error: Array,
            }
        },
        data() {
            return {
                //name: '',
                //notes: '',
                //error: ''
            };
        },
        methods: {
            _buildErrorMsg () {
				return this.ingredientDetails.error.join('; ');
			}
        },
        computed: {
            buildErrorMsg() {
                return this._buildErrorMsg();
            },
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
