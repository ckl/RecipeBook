import { IngredientService } from "@/services/api.service"

const initialState = {
	isLoading: false,
	ingredients: []
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
	async fetchIngredients(context, slug) {
		context.commit("fetchIngredientsStart")
		const { data } = await IngredientService.get(slug);
		context.commit("fetchIngredientsEnd", data);
		return data;
	}
};

export const mutations = {
	fetchIngredientsStart(state) {
		state.isLoading = true;
	},
	fetchIngredientsEnd(state, data) {
		state.ingredients = data;
		state.isLoading = false;
	}
}

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
