<template>
    <div class="container">
        <p style="text-align: left; margin-left: 4vw;"><a href="/" class="link-secondary link-offset-2 link-underline-opacity-25 link-underline-opacity-100-hover">Back to Restaurant List</a></p>
        <h2>Add Restaurant</h2>
        <form @submit.prevent="handleSubmit">
            <div class="mb-3">
                <label for="restaurantName" class="form-label">Nazwa</label>
                <input 
                    type="text" 
                    class="form-control" 
                    id="restaurantName" 
                    v-model="restaurantName" 
                    :class="{ 'is-invalid': nameError }" 
                    required
                >
                <div v-if="nameError" class="invalid-feedback">{{ nameError }}</div>
            </div>
            <div class="mb-3">
                <label for="restaurantType" class="form-label">Typ</label>
                <select 
                    class="form-select" 
                    id="restaurantType" 
                    v-model="selectedType" 
                    :class="{ 'is-invalid': typeError }" 
                    required
                >
                    <option value="">Select Type</option>
                    <option v-for="type in Types" :key="type" :value="type">{{ type }}</option>
                </select>
                <div v-if="typeError" class="invalid-feedback">{{ typeError }}</div>
            </div>
            <button type="submit" class="btn btn-primary">Add</button>
            <div v-if="apiError" class="alert alert-danger mt-3">{{ apiError }}</div>
        </form>
    </div>
</template>



<script>
import { getData, postData } from "../ApiCommunication/Restaurants.js"

export default {
    name: 'AddNewRestaurant',
    data() {
        return {
            restaurantName: '',
            selectedType: '',
            Types: [],
            nameError: '',
            typeError: '',
            apiError: ''
        };
    },
    methods: {
        async getTypes() {
            this.Types = await getData("/RestaurantTypes");
        },
        async handleSubmit() {
            this.nameError = '';
            this.typeError = '';
            this.apiError = '';

            const newRestaurant = {
                name: this.restaurantName,
                restaurantType: this.selectedType,
            };

            try {
                const response = await postData("/AddRestaurant", newRestaurant);

                this.$router.push({ name: 'RestaurantDish', query: { restaurantId: response } });

            } catch (error) {
                if (error.response && error.response.data.errors) {
                    const errors = error.response.data.errors;
                    if (errors.Name) {
                        this.nameError = errors.Name[0]; 
                    }
                    if (errors.Type) {
                        this.typeError = errors.Type[0]; 
                    }
                } else {
                    this.apiError = "Error";
                }
            }
        }
    },
    mounted() {
        this.getTypes(); 
    }
};
</script>


<style>
.addRestaurant {
    margin-bottom: 5vh;
    margin-left: 70vw;
}
</style>
