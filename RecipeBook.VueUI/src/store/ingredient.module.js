import { IngredientService } from "@/services/api.service"
import {
	GET_INGREDIENTS,
	INGREDIENT_CREATE
} from './actions.type'
import {
	GET_INGREDIENTS_START,
	GET_INGREDIENTS_END,
	INGREDIENT_CREATE_END
} from './mutations.type';
const initialState = {
	isLoading: false,
	ingredients: [],
	currentIngredient: {}
};

export const state = { ...initialState };

const getters = {
	ingredients(state) {
		return state.ingredients;
	},
	isLoading(state) {
		return state.isLoading;
	}
};

export const actions = {
	async [GET_INGREDIENTS](context, slug) {
		context.commit(GET_INGREDIENTS_START)
		const { data } = await IngredientService.get(slug);
		context.commit(GET_INGREDIENTS_END, data);
		return data;
	},
	async [INGREDIENT_CREATE](contex, slug) {
		const { data } = await IngredientService.post(slug);
			contex.commit(INGREDIENT_CREATE_END);
		return data;
	}
};

export const mutations = {
	[GET_INGREDIENTS_START](state) {
		state.isLoading = true;
	},
	[GET_INGREDIENTS_END](state, data) {
		state.ingredients = data;
		state.isLoading = false;
	},
	[INGREDIENT_CREATE_END](state, data) {
		state.isLoading = false;
		state.currentIngredient = data;
	}
}

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
