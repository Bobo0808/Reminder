<script setup>
import { inject,ref } from "vue";
import axios from "axios";
const API_URL = "https://localhost:7068";
const showModel = ref(false);

const token = inject('$token');
const router = useRouter();
const state = reactive({
  token, 
});

const logout = () => {
  // 清除本地的 Token
  localStorage.removeItem('token');
  state.token = null; 
  console.log('Token:', state.token);
  router.push('/login');
};



const Add = ref({
  category:"",
  remark:"",
  creatDate:new Date(),
  scaheduleDate:new Date(),
  isComplete:false
});
const Edit = ref({
  category:"",
  remark:"",
  createDate:new Date(),
  scaheduleDate:new Date(),
  isComplete:false
});

const AddTask = (event) => {
  event.preventDefault();
  axios
    .post(`${API_URL}/api/Todo/Create`, Add.value)
    .then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

const handleEdit = (event) => {
  event.preventDefault();
  axios
    .post(`${API_URL}/api/Todo/Create`, Add.value)
    .then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

const handleDelete = (event) => {
  event.preventDefault();
  axios
    .post(`${API_URL}/api/Todo/Create`, Add.value)
    .then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};





</script>




<template>
<body>
  <header>
      <h2 class="logo">Todolist</h2>
      <nav class="navigation">
        <!-- <a href="#" @click="changeView(3)">驗證</a> -->
        <!-- <a href="#">TEST</a>
            <a href="#">TEST</a>
            <a href="#">TEST</a>
            <a href="#">TEST</a> -->
        <button @click="logout" class="btnLogin-popup">Logout</button>
      </nav>
    </header>
  <!-- <h1 class="title">
  To Do List
</h1> -->
<div v-show="showModel" class="overlay">
    <div class="modal">
      <h3>主題</h3>
      <input type="text" class="enter-list" placeholder="在此輸入主題" v-model="category">
      <h4>內容</h4>
      <textarea name="note" id="note" cols="30" rows="10" v-model="remark"></textarea>
      <div class="iscomplete">
            <input type="radio" name="iscomplete" id="dot-1" :value=true v-model="Add.isComplete"/>
            <input type="radio" name="iscomplete" id="dot-2" :value=false v-model="Add.isComplete"/>
            <span class="iscomplete-title">是否完成</span>
            <div class="category">
              <label for="dot-1">
                <span class="dot one"></span>
                <span class="iscomplete">是</span>
              </label>
              <label for="dot-2">
                <span class="dot two"></span>
                <span class="iscomplete">否</span>
              </label>
            </div>
          </div>
      <button @click="AddTask">送出</button>
      <button class="close" @click="showModel=false">Close</button>
    </div>
  </div>
  
<div class="todo-list">
  <div class="list-head">
    <input type="text" class="enter-list" placeholder="在此輸入主題" v-model="category">
    <button class="add-list" @click="showModel=true">新增</button>
    <button class="add-list-2">檢視已完成</button>
    
  </div>
  <div class="tasks">
    <div class="item">
      <p>new item here</p>
      <div class="item-btn">
        <font-awesome-icon icon="fa-solid fa-pen-to-square" @click="handleEdit"/>
        <font-awesome-icon icon="fa-solid fa-xmark" @click="handleDelete"/>
      </div>
    </div>
  </div>
</div>
</body>

</template>

<style scoped>
header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  padding: 20px 100px;
  background-color: rgb(39, 37, 36);
  display: flex;
  justify-content: space-between;
  align-items: center;
  z-index: 99;
}

.logo {
  font-size: 2em;
  color: #fff;
}

.navigation a {
  position: relative;
  font-size: 1.1em;
  color: #fff;
  text-decoration: none;
  font-weight: 500;
  margin-left: 40px;
}

.navigation a::after {
  content: "";
  position: absolute;
  left: 0;
  bottom: -6px;
  width: 100%;
  height: 3px;
  background: #fff;
  border-radius: 5px;
  transform-origin: right;
  transform: scaleX(0);
  transition: transform 0.5s;
}

.navigation a:hover::after {
  transform: scaleX(1);
}

.navigation .btnLogin-popup {
  width: 130px;
  height: 50px;
  background: transparent;
  border: 2px solid #fff;
  outline: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1.1em;
  color: #fff;
  font-weight: 500;
  margin-left: 40px;
  transition: 0.5s;
}

.navigation .btnLogin-popup:hover {
  background: #fff;
  color: #162938;
}




body{
  background-color: #f5f5f5;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  height: 100vh;
  width: 100vw;
}
.title{
  font-size: 30px;
  font-weight: 600;
  color:rgb(8, 6, 133);
  letter-spacing: 1px;
  margin: 30px 0;
}
.todo-list{
  width: 450px;
  padding: 20px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
  border-radius: 5px;
  background-color: #fff;
}
.list-head{
  display:grid;
  grid-template-columns: 4fr 1fr;
  gap: 20px;

}
.enter-list{
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ccc;
  font-size: 16px;
  outline: none;
}
.add-list{
  border: none;
  border-radius: 5px;
  background-color: rgb(8, 6, 133);
  outline: none;
  font-size: 16px;
  color: #fff;
  cursor: pointer;
}
.add-list-2{
  border: none;
  border-radius: 5px;
  background-color: rgb(6, 133, 97);
  outline: none;
  font-size: 16px;
  color: #fff;
  cursor: pointer;
}
.tasks{
  width:100%;
  margin-top: 30px;
}
.item{
  padding: 10px;
  border-radius: 5px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
  display: flex;
  justify-content: space-between;
}
.item p {
  font-size: 16px;
  font-weight: 500;
  color:#111;
  letter-spacing: 1px;
}
.item.complete p{
  text-decoration: line-through;
  color: #11111180;

}
.item-btn{
  display: flex;
  align-items: center;
  gap: 10px;
}
.item-btn svg{
  color: rgb(8, 6, 133);
  font-size: 18px;
  cursor: pointer;
}
/* 彈窗 */
.overlay{
    position: absolute;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.77);
    z-index: 10;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .modal{
    width: 750px;
    background-color: white;
    border-radius: 10px;
    padding: 30px;
    position: relative;
    display: flex;
    flex-direction: column;
  }
  .modal button{
    padding:10px 20px ;
    font-size: 20px;
    width: 100%;
    background-color: rgb(33, 140, 227);
    border:none;
    color:white;
    cursor: pointer;
    margin-top: 15px;
  }
.modal .close{
  background-color: rgb(225, 15, 102) ;
  margin-top: 7px;
}

.iscomplete .iscomplete-title {
  color: black;
  font-size: 25px;
  font-weight: 400;
}

.category {
  color: black;
  display: flex;
  width: 80%;
  margin: 14px 0;
  justify-content: space-between;
}

.category label {
  display: flex;
  align-items: center;
  cursor: pointer;
}

.category label .dot {
  height: 18px;
  width: 18px;
  border-radius: 50%;
  margin-right: 10px;
  background: #d9d9d9;
  border: 5px solid transparent;
  transition: all 0.3s ease;
}

#dot-1:checked ~ .category label .one,
#dot-2:checked ~ .category label .two,
#dot-3:checked ~ .category label .three {
  background: #9b59b6;
  border-color: #d9d9d9;
}

input[type="radio"] {
  display: none;
}


</style>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: "Poppins" sans-serif;
}
</style>

<script>
  export default {
    name: 'App'
  }
</script>