import { RecipeService } from "@/services/api.service"
import {
	GET_RECIPE,
	GET_RECIPES,
	GET_RECIPE_INGREDIENTS,
	RECIPE_CREATE,
	RECIPE_UPDATE,
	RECIPE_INGREDIENTS_CREATE,
	RECIPE_RESET_STATE
} from "./actions.type";
import {
	GET_RECIPES_START,
	GET_RECIPES_END,
	GET_RECIPE_END,
	GET_RECIPE_INGREDIENTS_END,
	RECIPE_CREATE_END,
	RECIPE_RESET_STATE_CALLED,
	RECIPE_INGREDIENTS_CREATE_END
} from "./mutations.type";

const initialState = () => {
	return {
		isLoading: false,
		currentRecipe: {},
		currentRecipeIngredients: [],
		recipes: []
	}
};

export const state = initialState();

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
	async [GET_RECIPE](context, slug) {
		const { data } = await RecipeService.get(slug);
		context.commit(GET_RECIPE_END, data);
		return data;
	},
	async [GET_RECIPES](context) {
		context.commit(GET_RECIPES_START);
		const { data } = await RecipeService.get();
		context.commit(GET_RECIPES_END, data);
		return data;
	},
	async [GET_RECIPE_INGREDIENTS](context, slug) {
		if (!slug) {
			return;
		}
		const { data } = await RecipeService.getRecipeIngredients(slug);
		context.commit(GET_RECIPE_INGREDIENTS_END, data);
		return data;
	},
	async [RECIPE_CREATE](context) {
		const { data } = await RecipeService.createRecipe(state.currentRecipe);
		context.commit(RECIPE_CREATE_END, data);
		return data;
	},
	async [RECIPE_UPDATE]({ state }) {
		return RecipeService.updateRecipe(state.currentRecipe.recipeId, state.currentRecipe);
	},
	async [RECIPE_INGREDIENTS_CREATE](context) {
		state.currentRecipeIngredients.forEach((el, i, arr) => {
			arr[i].recipeId = state.currentRecipe.recipeId;
		});
		const { data } = await RecipeService.createRecipeIngredients(state.currentRecipeIngredients)
		context.commit(RECIPE_INGREDIENTS_CREATE_END, data)
	},
	async [RECIPE_RESET_STATE]({ commit }) {
		commit(RECIPE_RESET_STATE_CALLED);
	}
};

export const mutations = {
	[GET_RECIPES_START](state) {
		state.isLoading = true;
	},
	[GET_RECIPES_END](state, data) {
		state.recipes = data;
		state.isLoading = false;
	},
	[GET_RECIPE_END](state, data) {
		state.currentRecipe = data;
	},
	[GET_RECIPE_INGREDIENTS_END](state, data) {
		state.currentRecipeIngredients = data;
	},
	[RECIPE_CREATE_END](state, data) {
		state.currentRecipe = data;
	},
	[RECIPE_INGREDIENTS_CREATE_END](state, data) {
		state.currentRecipeIngredients = data
	},
	[RECIPE_RESET_STATE_CALLED](state) {
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
