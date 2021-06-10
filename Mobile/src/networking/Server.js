import React, { Component } from 'react'
import { Text, View } from 'react-native'

const apiGetAllCourse ='https://localhost:44387/api/Courses'
async function getCourseFromSever() {
    try {
        let response = await fetch(apiGetAllCourse)
        let responseJson = await response.json();
        return responseJson.data;
        
    } catch (error) {
        console.error(`error is: ${error}`)
    }
}
export {getCourseFromSever};