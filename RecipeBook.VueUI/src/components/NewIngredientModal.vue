<template>
    <div>
        <b-button v-b-modal.new-ingredient-modal variant="outline-primary">New Ingredient</b-button>
		<b-modal
			id="new-ingredient-modal"
			title="New Ingredient"
			@show="resetModal"
			@hidden="resetModal"
			@ok="handleOk"
		>
			<b-form ref="form" @submit.stop.prevent="handleSubmit">
				<b-form-group
					label="Ingredient-name"
					label-for="quantity-input"
					invalid-feedback="Quantity is required"
					:state="nameState" >

					<b-form-input
						id="name-input"
						v-model="ingredient.name"
						:state="nameState"
						type="text"
						placeholder="Ingredient name"
						required
					></b-form-input>
				</b-form-group>

				<b-form-group
					label="Notes"
					label-for="notes-input">
					<b-form-input
						id="notes-input"
						v-model="ingredient.notes"
						type="text"
						placeholder="Notes">
					</b-form-input>
				</b-form-group>
			</b-form>
		</b-modal>
    </div>
</template>

<script>
	import toast from '@/mixins/toast.mixin'
	import { mapActions, mapGetters } from 'vuex'
	import { 
		INGREDIENT_CREATE,
		INGREDIENT_RESET_STATE,
	} from '@/store/actions.type.js'

    export default {
        name: 'NewIngredientModal',
		mixins: [ toast ],
        data() {
            return {
				nameState: null
            };
        },
		computed: {
			...mapGetters({
				ingredient: 'ingredient/currentIngredient'
			})
		},
		methods: {
			checkFormValidity() {
				const valid = this.$refs.form.checkValidity()
				this.nameState = valid
				return valid
			},
			resetModal() {
				this.$store.dispatch(`ingredient/${INGREDIENT_RESET_STATE}`);
			},
			handleOk(bvModalEvt) {
				// prevent modal from closing
				bvModalEvt.preventDefault()
				this.handleSubmit()
			},
			handleSubmit() {
				if (!this.checkFormValidity()) {
					return
				}

				this.createIngredient()
				.then(() => {
					this.$toast('Success', 'hello', 'success')
					this.$nextTick(() => this.$bvModal.hide('new-ingredient-modal'))
				})
				.catch(error => this.$toastError(error, 'Creating ingredient'))
				
			},
			...mapActions({
				createIngredient: `ingredient/${INGREDIENT_CREATE}`
			})
        },
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
/* h3 {
  margin: 40px 0 0;
} */
/* ul {
  list-style-type: none;
  padding: 0;
} */
/* li {
  display: inline-block;
  margin: 0 10px;
} */
a {
  color: #42b983;
}
</style>
