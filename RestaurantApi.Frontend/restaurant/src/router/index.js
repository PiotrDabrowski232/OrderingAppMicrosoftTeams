import { createRouter, createWebHistory } from 'vue-router'
import RestaurantsList from '@/components/RestaurantsList.vue'
import RestaurantDish from '@/components/RestaurantDish.vue'
import AddNewRestaurant from '@/components/AddNewRestaurant.vue'

const routes = [
  {
    path: '/',
    name: 'RestaurantsList',
    component: RestaurantsList
  },
  {
    path: '/Dishes',
    name: 'RestaurantDish',
    component: RestaurantDish,
    props: (route) => ({ restaurantId: route.query.restaurantId }) 
  },
  {
    path: '/NewRestaurant',
    name: 'AddNewRestaurant',
    component: AddNewRestaurant
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
