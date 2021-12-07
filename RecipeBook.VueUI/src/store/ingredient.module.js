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
		const { data } = await IngredientService.get(slug);
		context.commit("fetchEnd", data);
		return data;
	}
};

export const mutations = {
	fetchStart(state) {
		state.isLoading = true;
	},
	fetchEnd(state, data) {
		state.ingredients = data;
		state.isLoading = false;
	}
}

export default {
	state,
	getters,
	actions,
	mutations
};
