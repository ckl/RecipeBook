<template>
  <div class="container">
    <div class="p-4 my-3 bg-light rounded-3">
		<h2>Testing page</h2>
		<p>Can test some stuff here</p>
	</div>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'Upcoming',
  props: {
    msg: String
  },
  mounted () {
    axios.get('https://localhost:44331/api/Ingredient')
      .then(response => {
        console.log(response.data)
      })
      .catch(error => {
        console.log(error)
      })
  }
}
</script>
