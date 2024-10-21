<template>
        <p style="text-align: left; margin-left: 4vw;"><a href="/" class="link-secondary link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover">Back to Restaurant List</a></p>
    <div class="container" v-if="Dishes.length !== 0">
        <button type="button" class="btn btn-primary addRestaurant" @click="addDish">Add</button>
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">Name</th>
                    <th scope="col">Price</th>
                    <th scope="col">Calories</th>
                    <th scope="col">Extras</th>
                    <th scope="col">Blocked</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="dish in Dishes" :key="dish.id">
                    <td>{{ dish.name }}</td>
                    <td>{{ dish.price }} zł</td>
                    <td>{{ dish.calories }} cal</td>
                    <td>{{ dish.isExtras ? "Yes" : "No" }}</td>
                    <td>{{ dish.blocked ? "Yes" : "No" }}</td>
                    <td>
                        <button v-if="!dish.blocked" type="button" class="btn btn-danger" @click="BlockDish(dish.id)">Block</button>
                        <button v-if="dish.blocked" type="button" class="btn btn-success" @click="UnBlockDish(dish.id)">Unblock</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="container" v-if="Dishes.length === 0">
        <button type="button" class="btn btn-primary addRestaurant" @click="addDish">Add</button>
        <h4>this restaurant does not have any dishes defined yet add a new dish to have access to the action</h4>
    </div>
</template>

<script>
import { getData, updateData } from "../ApiCommunication/Restaurants.js"

export default {
    name: 'RestaurantDish',
    props: {
        restaurantId: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            Dishes: [],  
        };
    },
    methods: {
        async fetchDishes() {
            this.Dishes = await getData(`/Dishes?restaurantId=${this.restaurantId}`); 
        },
        addDish() {
            this.$router.push({ name: 'AddNewDish', query: { restaurantId: this.restaurantId} });
        },
        async BlockDish(id){
            var result = await updateData(`/Block?dishId=${id}`)
            if(result)
                await this.fetchDishes()
        },
        async UnBlockDish(id){
            var result = await updateData(`/UnBlock?dishId=${id}`)
            if(result)
                await this.fetchDishes()
        },
    },
    mounted() {
        this.fetchDishes(); 
    }
};
</script>

<style>
.addRestaurant {
    margin-bottom: 5vh;
    margin-left: 70vw;
}
</style>
