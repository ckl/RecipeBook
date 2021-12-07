import axios from "axios";

const API_URL = process.env.VUE_APP_RecipeBookApi;

const ApiService = {
	init() {
		axios.defaults.baseURL = API_URL;
	},
	get(resource, slug = '') {
		return axios.get(`${resource}/${slug}`).catch(error => {
			throw new Error(`ApiService.get: ${error}`);
		})
	}
};

export default ApiService;

export const IngredientService = {
	get(slug) {
		return ApiService.get('Ingredient', slug);
	}
};