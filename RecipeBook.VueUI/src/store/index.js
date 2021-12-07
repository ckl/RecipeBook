import Vue from "vue";
import Vuex from "vuex";

import ingredient from "./ingredient.module";

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        ingredient,
    }
});