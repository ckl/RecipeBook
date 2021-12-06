<template>
  <div class="about">
    <h1>This is a testy test</h1>
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
