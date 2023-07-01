<script setup>
    const showModel = ref(false)
    import axios from "axios";
    




</script>

<template>
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
</template>

<style scoped>
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
