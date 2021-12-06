<template>
    <div>
        <b-button @click="showConfirmDelete" variant="danger">Delete</b-button>
        <b-modal v-model="showModal">
            Are you sure you want to delete <strong>{{ recipeName }}</strong>?
        </b-modal>
    </div>
</template>

<script>
    import axios from 'axios'

    export default {
        name: 'DeleteRecipeModal',
        props: {
            recipeID: Number,
            recipeName: String,
			errorList: Array,
        },
        data() {
            return {
                showModal: false,
                statusMsg: ''
            };
        },
        methods: {
    //        _buildErrorMsg () {
				//return this.ingredientDetails.error.join('; ');
    //        },
			error(err) {
				console.log(err);
				//this.showErrorAlert = true;
				this.statusMsg = 'Error - check the console';
			},
			showConfirmDelete() {
				this.boxTwo = ''
				this.$bvModal.msgBoxConfirm('Please confirm that you want to delete "' + this.recipeName + '".', {
					title: 'Please Confirm',
					okVariant: 'danger',
					okTitle: 'YES',
					cancelTitle: 'NO',
					footerClass: 'p-2',
					hideHeaderClose: false,
					centered: true
				})
					.then((resp) => {
                        if (resp === true) {
							this.deleteRecipe().then(this.redirect).then(this.makeToast).catch(this.error);
						}
					})
					.catch(this.error)
            },
            makeToast() {
                return new Promise((resolve) => {
                    this.$bvToast.toast("Successfully deleted", {
                        title: 'Success',
                        autoHideDelay: 5000,
                    });
                    resolve();
                });
            },
            redirect() {
                return new Promise((resolve) => {
					this.$router.push({ name: 'Recipes' });
					resolve();
                });
			},
            deleteRecipe () {
                console.log(process.env);

                // TODO: using params is a hack. figure out why this.recipeID is undefined
                this.statusMsg = 'Deleting...';
                return axios.delete(process.env.VUE_APP_RecipeBookApi + 'Recipes/' + this.$route.params.id)
                    .then(() => {
                        this.statusMsg = 'Done';
                        this.$router.push('Recipes')
                    });
			}
        },
        computed: {
            //buildErrorMsg() {
            //    return this._buildErrorMsg();
            //},
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
