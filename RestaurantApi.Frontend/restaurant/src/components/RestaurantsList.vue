<template>
  <div class="container">
    <button type="button" class="btn btn-primary addRestaurant" @click="addRestaurant">Add</button>
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Type</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="restaurant in Restaurants" :key="restaurant.id">
          <td>{{ restaurant.name }}</td>
          <td>{{ restaurant.restaurantType }}</td>
          <td><button type="button" class="btn" @click="showDishes(restaurant.id)"><i class="bi bi-receipt"></i></button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import {getData} from "../ApiCommunication/Restaurants.js"

export default {
  name: 'RestaurantsList',
  data() {
    return {
      Restaurants: {
      }
    };
  },
  methods: {
    async fetchRestaurants() {
      this.Restaurants = await getData("/Restaurants");
    },
    addRestaurant() {
      this.$router.push({ name: 'AddNewRestaurant'});
    },
    showDishes(id) {
      this.$router.push({ name: 'RestaurantDish', query: { restaurantId: id } });
    }
  },
  mounted() {
    this.fetchRestaurants();
  }
};
</script>

<style>
.addRestaurant{
  margin-bottom: 5vh;
  margin-left: 70vw;
}
</style>
