import Vue from "vue";
import { RecipeService } from "@/services/api.service"

const initialState = {
	isLoading: false,
	currentRecipe: {},
	currentRecipeIngredients: [],
	recipes: []
};

export const state = { ...initialState };

const getters = {
	recipes(state) {
		return state.recipes;
	},
	currentRecipe(state) {
		return state.currentRecipe;
	},
	currentRecipeIngredients(state) {
		return state.currentRecipeIngredients;
	},
	isLoading(state) {
		return state.isLoading;
	}
};

export const actions = {
	async fetchRecipes(context) {
		context.commit("fetchRecipesStart");
		const { data } = await RecipeService.get();
		context.commit("fetchRecipesEnd", data);
		return data;
	},
	async fetchRecipe(context, slug) {
		const { data } = await RecipeService.get(slug);
		context.commit("fetchRecipeEnd", data);
		return data;
	},
	async fetchRecipeIngredients(context, slug) {
		if (!slug) {
			return;
		}
		const { data } = await RecipeService.getRecipeIngredients(slug);
		context.commit("fetchRecipeIngredientsEnd", data);
		return data;
	},
	async createRecipe({ state }) {
		return RecipeService.createRecipe(state.currentRecipe);
	},
	async updateRecipe({ state }) {
		return RecipeService.updateRecipe(state.currentRecipe.recipeID, state.currentRecipe);
	},
	async createRecipeIngredients({ state }) {
		state.currentRecipeIngredients.forEach((el, i, arr) => {
			arr[i].recipeID = state.currentRecipe.recipeID;
		});
		return RecipeService.createRecipeIngredients(state.currentRecipeIngredients);
	},
	async recipeResetState({ commit }) {
		commit("recipeResetStateCalled");
	}
};

export const mutations = {
	fetchRecipesStart(state) {
		state.isLoading = true;
	},
	fetchRecipesEnd(state, data) {
		state.recipes = data;
		state.isLoading = false;
	},
	fetchRecipeEnd(state, data) {
		state.currentRecipe = data;
	},
	fetchRecipeIngredientsEnd(state, data) {
		//state.currentRecipe = state.currentRecipe || {};
		state.currentRecipeIngredients = data;
	},
	createRecipeEnd(state, data) {
		state.currentRecipe = data;
	},
	recipeResetStateCalled(state) {
		for (let s in state) {
			Vue.set(state, s, initialState[s]);
		}
	}
}

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
