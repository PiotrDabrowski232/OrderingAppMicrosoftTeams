<template>
    <div>
        <p style="text-align: left; margin-left: 4vw;"><a :href="'/Dishes?restaurantId='+this.restaurantId" class="link-secondary link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover">Back to Menu</a></p>
        <h2>Add Dish</h2>
        <form @submit.prevent="handleSubmit" style="width: 25vw; margin-left: 37vw;
        margin-top: 9vh;">
            <div class="mb-3">
                <label for="dishName" class="form-label">Name</label>
                <input type="text" class="form-control" id="dishName" v-model="name"
                    :class="{ 'is-invalid': nameError }" required>
                <div v-if="nameError" class="invalid-feedback">{{ nameError }}</div>
            </div>
            <div class="mb-3">
                <label for="dishDescription" class="form-label">Description</label>
                <textarea class="form-control" id="dishDescription" rows="3" v-model="description"
                    :class="{ 'is-invalid': descriptionError }" required></textarea>
                <div v-if="descriptionError" class="invalid-feedback">{{ descriptionError }}</div>
            </div>
            <div class="mb-3">
                <label for="dishPrice" class="form-label">Price</label>
                <input type="number" class="form-control" id="dishprice" v-model="price" step="0.01"
                    :class="{ 'is-invalid': priceError }" required>
                <div v-if="priceError" class="invalid-feedback">{{ priceError }}</div>
            </div>
            <div class="mb-3">
                <label for="dishCalories" class="form-label">Calories</label>
                <input type="number" class="form-control" id="dishCalories" v-model="calories"
                    :class="{ 'is-invalid': caloriesError }" required>
                <div v-if="caloriesError" class="invalid-feedback">{{ caloriesError }}</div>
            </div>
            <div class="mb-3">
                <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckDefault" v-model="isExtras">
                <label class="form-check-label" for="flexSwitchCheckDefault">Is Extras</label>
            </div>
            <button style="margin-left: 25vw;" type="submit" class="btn btn-primary">Add</button>
            <div v-if="apiError" class="alert alert-danger mt-3">{{ apiError }}</div>
        </form>
    </div>
</template>

<script>
import { postData } from "../ApiCommunication/Restaurants.js"

export default {
    name: 'AddNewDish',
    props: {
        restaurantId: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            name: "",
            description: "",
            price: 0,
            calories: 0,
            isExtras: false,
            nameError: "",
            descriptionError: "",
            priceError: "",
            caloriesError: "",
        };
    },
    methods: {
        async handleSubmit() {
            this.nameError = '';
            this.descriptionError = '';
            this.priceError = '';
            this.caloriesError = '';

            const newDish = {
                name: this.name,
                description: this.description,
                price: this.price,
                calories: this.calories,
                isExtras: this.isExtras
            }
            try {
                const response = await postData(`/AddDish?restaurantId=${this.restaurantId}`, newDish);

                if(response !== null || response !== undefined)
                    this.$router.push({ name: 'RestaurantDish', query: { restaurantId: this.restaurantId } });

            } catch (error) {
                if (error.response && error.response.data.errors) {
                    const errors = error.response.data.errors;
                    if (errors.Name) {
                        this.nameError = errors.Name[0];
                    }
                    if (errors.Description) {
                        this.descriptionError = errors.Description[0];
                    }
                    if (errors.Price) {
                        this.priceError = errors.Price[0];
                    }
                    if (errors.Calories) {
                        this.caloriesError = errors.Calories[0];
                    }
                } else {
                    this.apiError = "Error";
                }
            }
        },
    },
    mounted() {
    }
};
</script>

<style>
.addRestaurant {
    margin-bottom: 5vh;
    margin-left: 70vw;
}
</style>
