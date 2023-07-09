// import { ref } from "vue";
// import axios from "axios";

// const API_URL = "https://localhost:7068";




// //處裡每個登入者的Todolsit

// export function useGetUserTodolist() {
//     const getUserTodolist = ref([]);

//     const handlegetUserTodolsit = () => {
//         const token = localStorage.getItem('jwtToken');
//         axios
//             .get(`${API_URL}/api/Todo/getAllUserTodolist`, getUserTodolist.value)
//             .then((response) => {
//                 getUserTodolist.value = response.data;
//                 console.log(response.data);
//             })
//             .catch((error) => {
//                 console.log(error);
//             });
//     };
// }