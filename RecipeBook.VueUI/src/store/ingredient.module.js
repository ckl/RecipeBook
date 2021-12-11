import { IngredientService } from "@/services/api.service"
import {
	GET_INGREDIENTS,
	INGREDIENT_CREATE,
	INGREDIENT_RESET_STATE
} from './actions.type'
import {
	GET_INGREDIENTS_START,
	GET_INGREDIENTS_END,
	INGREDIENT_CREATE_END,
	INGREDIENT_RESET_STATE_CALLED
} from './mutations.type';

const initialState = () => {
	return {
		isLoading: false,
		ingredients: [],
		currentIngredient: {}
	}
}

export const state = initialState()

const getters = {
	ingredients(state) {
		return state.ingredients;
	},
	currentIngredient(state) {
		return state.currentIngredient
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
	async [INGREDIENT_CREATE](contex) {
		const { data } = await IngredientService.createIngredient(state.currentIngredient);
		contex.commit(INGREDIENT_CREATE_END, data);
		return data;
	},
	async [INGREDIENT_RESET_STATE]({ commit }) {
		commit(INGREDIENT_RESET_STATE_CALLED);
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
	},
	[INGREDIENT_RESET_STATE_CALLED](state) {
		Object.assign(state, initialState())
	}
}

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
