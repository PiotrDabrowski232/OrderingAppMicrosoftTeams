import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://example.com/api', 
  timeout: 5000,  
  headers: {
    'Content-Type': 'application/json',  
  }
});

export const getData = async (endpoint) => {
  try {
    const response = await apiClient.get(endpoint);
    return response.data;
  } catch (error) {
    console.error('Error fetching data:', error);
    throw error;  
  }
};

export const postData = async (endpoint, data) => {
  try {
    const response = await apiClient.post(endpoint, data);
    return response.data;
  } catch (error) {
    console.error('Error posting data:', error);
    throw error; 
  }
};
