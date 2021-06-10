import React, { Component } from 'react'
import { Text, View } from 'react-native'
import axios from 'axios'

async function getCourse() {
    axios({
        method:'GET',
        url:'http://10.0.2.2:5001/api/Courses'
    })
    .then(res =>{
        console.log(res.data[0].courseId)
        return res.data;
    })
    .catch (error =>console.log(error))
}
export {getCourse};