import React, { Component } from 'react'
import { Text, View,StyleSheet,Image,TouchableOpacity } from 'react-native'
import {people} from '../../assets/index'
import Icon from 'react-native-vector-icons/MaterialCommunityIcons'
import { createStackNavigator } from '@react-navigation/stack';
import StackLogin from './StackLogin'



function AccountScreen({ navigation }) {
    
        return (
            <View style={styles.container}> 
                <View style={styles.accountInfo}>
                <Image style={styles.imageAccount} source={people}/>
                <Text style={styles.textInfo}>Tran Ngoc Hung</Text>
                <View style={styles.boxEmail}>
                <Icon name="email" size={25} color="red" style={{paddingTop:8,paddingRight:5}}></Icon>
                <Text style={styles.emailInfo}>tranngochung2109@gmail.com</Text>
                </View>
                <TouchableOpacity  >
                    <View style={styles.btnLogOut} >
                        <Text style={styles.btnTxt}>Sign Out</Text>
                    </View>
                </TouchableOpacity>
                </View>
            </View>
        )
        
    }


const Stack = createStackNavigator();
    export default class StackAccount extends Component {
        render() {
            return (
                
                    <Stack.Navigator>
                        <Stack.Screen name="AccountScreen" component={AccountScreen} />
                        <Stack.Screen name="detail" component={View} />
                    </Stack.Navigator>
                
            )   
        }
    }

    
    
const styles = StyleSheet.create({
    container:{
        flex:1,
        paddingHorizontal:20,
        paddingVertical:20
    },
    accountInfo:{
        alignItems:'center',
        justifyContent:'center',
        marginTop:10
    },
    imageAccount:{
        width:150,
        height:150,
        borderRadius:80,
        borderWidth:2,
        borderColor:'#fff'
    },
    textInfo:{
        fontWeight:'bold',
        fontSize:25
    },
    emailInfo:{
        marginVertical:10,
        opacity:0.8,
        fontSize:15,
    },
    boxEmail:{
        flexDirection:'row',
        paddingVertical:10
    },
    btnLogOut:{
        width:200,
        height:50,
        backgroundColor:'#2596be',
        justifyContent:'center',
        alignItems:'center'
    },
    btnTxt:{
        fontSize:20,
        color:'white',
        fontWeight:'bold'
    }
})

