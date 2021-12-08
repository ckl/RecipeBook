import axios from "axios";

const API_URL = process.env.VUE_APP_RecipeBookApi;

const ApiService = {
	init() {
		axios.defaults.baseURL = API_URL;
	},
	async get(resource, slug = '') {
		try {
			return axios.get(`${resource}/${slug}`);
		} catch (error) {
			throw new Error(`ApiService.get || Resource: ${resource} || Error: ${error}`);
		}
	},
	async post(resource, params) {
		try {
			return axios.post(resource, params);
		} catch (error) {
			throw new Error(`ApiService.post || Resource: ${resource} || Error: ${error}`);
		}
	},
	async put(resource, slug, params) {
		try {
			return axios.put(`${resource}/${slug}`, params);
		} catch (error) {
			throw new Error(`ApiService.put || Resource: ${resource} || Error: ${error}`);
		}
	}
};

export default ApiService;

export const IngredientService = {
	get(slug) {
		return ApiService.get('Ingredient', slug);
	}
};

export const RecipeService = {
	get(slug) {
		return ApiService.get('Recipes', slug);
	},
	getRecipeIngredients(slug) {
		return ApiService.get('RecipeIngredients', slug);
	},
	createRecipe(params) {
		return ApiService.post('Recipes', params);
	},
	updateRecipe(slug, params) {
		return ApiService.put('Recipes', slug, params)
	},
	createRecipeIngredients(params) {
		return ApiService.post('RecipeIngredients', params);
	}
};