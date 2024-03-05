import axios from 'axios';

const apiUrl = "http://localhost:5107"

axios.defaults.baseURL = apiUrl;




export default {
  getTasks: async () => {
    try{
      const result = await axios.get(`${apiUrl}/`)  
      console.log(result.data) 
      return result.data; ;
    }
    catch (error) {
      console.error(error);
      // Handle the error if needed
      return null;
    }
  },
  
  addTask: async(name)=>{
    try{
      console.log('addTask', name)
      await axios.post(`${apiUrl}/${name}`) 
     //TODO
     return {};
    }
    catch (error) {
      console.error(error);
      // Handle the error if needed
      return null;
    }
  },

  setCompleted: async(id, IsComplete)=>{
    try{
      const result=await axios.put(`${apiUrl}/${id}/${IsComplete}`)
      //TODO
      console.log('setCompleted', {id, IsComplete})
      return {}
    }
    catch (error) {
      console.error(error);
      // Handle the error if needed
      return null;
    }
  },

  deleteTask:async(id)=>{
    try{
      console.log('deleteTask')
      const result=await axios.delete(`${apiUrl}/${id}`)
      return {};
    }
    catch (error) {
      console.error(error);
      // Handle the error if needed
      return null;
    }
  }
};
