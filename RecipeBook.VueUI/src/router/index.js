import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
	{
		path: '/',
		name: 'Home',
		component: Home
	},
	{
		path: '/about',
		name: 'About',
		// route level code-splitting
		// this generates a separate chunk (about.[hash].js) for this route
		// which is lazy-loaded when the route is visited.
		component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
	},
	{
		path: '/recipes',
		name: 'Recipes',
		component: () => import('../views/Recipes.vue')
	},
	{
		path: '/recipe/:id',
		name: 'Recipe',
		props: true,
		component: () => import('../views/Recipe.vue')
	},
	{
		path: '/new-recipe',
		name: 'NewRecipe',
		props: true,
		component: () => import('../views/NewRecipe.vue')
	},
	{
		path: '/TestApi',
		name: 'TestApi',
		component: () => import('../views/TestApi.vue')
	},
	{
		path: '/*',
		component: () => import('../views/NotFound.vue')
	}
]

const router = new VueRouter({
	routes
})

export default router
