import React, { Component } from 'react'
import { Text, View, StyleSheet, Image, ScrollView, ImageBackground, FlatList, TouchableOpacity } from 'react-native'
import { hero } from '../../assets/index'
import Icon1 from 'react-native-vector-icons/Entypo'
import DataHome from '../../data/DataHome'
import DataFreeCourse from '../../data/DataFreeCourse'
import DataCategory from '../../data/DataCategory'
import { createStackNavigator } from '@react-navigation/stack'
import { getCourseFromSever } from '../../networking/Server'
import axios from 'axios'
import { getCourse } from '../../data/DataCourse'
import { AirbnbRating } from 'react-native-elements';
import { AsyncStorage } from 'react-native';
class FlatListItemRow extends Component {
    render() {


        return (

            <>


                <Image style={styles.imgHome} source={{ uri: "data:image/png;base64," + this.props.item.thumbnailImage }} />
                <Text style={styles.imgTxt}>{this.props.item.courseName}</Text>
                <Text style={styles.instructorTxt}>{this.props.item.courseDuration}</Text>
                <Text style={styles.imgTxt}>{this.props.item.price} USD</Text>

            </>
        );
    }
}

class FlatListItemRowFree extends Component {
    render() {


        return (

            <>


                <Image style={styles.imgHome} source={{ uri: "data:image/png;base64," + this.props.item.course.thumbnailImage }} />
                <Text style={styles.imgTxt}>{this.props.item.course.courseName}</Text>
                <Text style={styles.instructorTxt}>{this.props.item.course.courseDuration}</Text>
                <Text style={styles.imgTxt}>{this.props.item.course.price} USD</Text>

            </>
        );
    }
}

class HomeScreen extends Component {
    constructor(props) {
        super(props);
        this.state = ({
            courses: []
        })
    }
    componentDidMount() {
        axios({
            method: 'GET',
            url: 'http://10.0.2.2:5001/api/GetCourseList'
        })
            .then(res => {
                //console.log(res.data[0].courseId)
                this.setState({
                    courses: res.data
                })
            })
            .catch(error => console.log(error))

        axios({
            method: 'GET',
            url: 'http://10.0.2.2:5001/api/TopCourses'
        })
            .then(resTop => {
                //console.log(resTop.data[0].courseId)
                this.setState({
                    coursesTop: resTop.data
                })
            })
            .catch(error => console.log(error))

        axios({
            method: 'GET',
            url: 'http://10.0.2.2:5001/api/GetTop6CourseDesktop?option=1'
        })
            .then(resFree => {
                //console.log(resFree.data)
                this.setState({
                    coursesFree: resFree.data
                })
            })
            .catch(error => console.log(error))
    }
    render() {
        return (
            <ScrollView>
                <View style={styles.container}>
                    <ImageBackground style={styles.backgroundTop} source={hero}>
                        <Text style={styles.imgTopTxt}>Study worldwide</Text>
                        <Text style={styles.imgTopTxt}>Find your future</Text>
                    </ImageBackground>
                    <Text style={styles.txt}> Featured</Text>
                    <View style={styles.container}>
                        <FlatList
                            horizontal={true}
                            data={this.state.coursesTop}
                            keyExtractor={item => item.courseId}
                            renderItem={({ item, index }) => {
                                return (
                                    <TouchableOpacity style={styles.boxImage} onPress={() => this.props.navigation.navigate('detail',
                                        {
                                            courseName: item.courseName,
                                            courseImg: item.thumbnailImage,
                                            courseDes: item.description,
                                            courseNameInstructor: item.courseDuration,
                                            courseCreate: item.startDate,
                                            priceCourse: item.price,
                                            courseRating:item.rating,
                                            courseDetailId: item.courseId,
                                            instructorId:item.accountId
                                        }
                                    )}>
                                        <FlatListItemRow item={item} index={index} />
                                    </TouchableOpacity>
                                );
                            }}
                        />
                    </View>
                    <Text style={styles.txt}> Free Course</Text>
                    <View style={styles.container}>
                        <FlatList
                            horizontal={true}
                            data={this.state.coursesFree}
                            keyExtractor={item => item.course.courseId}
                            // data = {this.state.courseFromSever}
                            renderItem={({ item, index }) => {
                                return (
                                    <TouchableOpacity style={styles.boxImage} onPress={() =>
                                        this.props.navigation.navigate('detail', {
                                            courseName: item.course.courseName,
                                            courseImg: item.course.thumbnailImage,
                                            courseDes: item.course.description,
                                            courseNameInstructor: item.course.courseDuration,
                                            courseCreate: item.course.startDate,
                                            priceCourse: item.course.price,
                                            courseRating:item.course.rating,
                                            courseDetailId: item.course.courseId,
                                            instructorId:item.course.accountId
                                        })}>
                                        <FlatListItemRowFree item={item} index={index} />
                                    </TouchableOpacity>
                                );
                            }}
                        />
                    </View>
                    <Text style={styles.txt}> Top Search</Text>
                    <View style={styles.container}>
                        <FlatList
                            horizontal={true}
                            data={this.state.courses}
                            keyExtractor={item => item.courseId}
                            renderItem={({ item, index }) => {
                                return (
                                    <TouchableOpacity style={styles.boxImage}
                                        onPress={() =>
                                            this.props.navigation.navigate('detail', {
                                                courseName: item.courseName,
                                                courseImg: item.thumbnailImage,
                                                courseDes: item.description,
                                                courseNameInstructor: item.courseDuration,
                                                courseCreate: item.startDate,
                                                priceCourse: item.price,
                                                courseRating:item.rating,
                                                courseDetailId: item.courseId,
                                                instructorId:item.accountId
                                            })}>
                                        <FlatListItemRow item={item} index={index} />
                                    </TouchableOpacity>
                                );
                            }}
                        />
                    </View>
                </View>
            </ScrollView>
        )
    }
}
class CourseDetail extends Component {
    async buyCourse(){
        try {
            const value = await AsyncStorage.getItem('accountId');
            console.log(value);
            if (value !== null) {
                axios.post(
                    'http://10.0.2.2:5001/api/AddAccountInCourse',
                    {
                      'accountId': value,
                      'courseId': this.props.route.params.courseDetailId,
                      'isBought': true,
                      'getPayment': false,
                      'invoiceCode': "2RQbzk",
                    }
                  )
                    .then(res => {
                        console.log(value + this.props.route.params.courseDetailId);
                        console.log(res.data);
                        axios.post(
                            'http://10.0.2.2:5001/api/AddAccountInCourse',
                            {
                              'accountId':  this.props.route.params.instructorId,
                              'courseId': this.props.route.params.courseDetailId,
                              'isBought': false,
                              'getPayment': true,
                              'invoiceCode': "2RQbzk",
                            }
                          )
                            .then(res => {
                                this.props.navigation.goBack();
                                alert('Buy success');
                                console.log(res.data);
                            })
                            .catch(error => console.log(error))
                    })
                    .catch(error => console.log(error))
            }
        } catch (error) {
        }
    }
    render() {

        return (
            <View style={{ flex: 1, padding: 5 }}>
                <View style={{ alignItems: 'center' }}>
                    <Text style={styles.detailCourseName}>{this.props.route.params.courseName}</Text>
                    <Image style={styles.imgHome} source={{ uri: "data:image/png;base64," + this.props.route.params.courseImg }} />
                </View>
                <Text style={styles.detailCoursDes}>{this.props.route.params.courseDes}</Text>
                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                    <Text style={{ fontSize: 25, marginRight: 5,color:'orange' }}>({this.props.route.params.courseRating}.0)</Text>
                    <AirbnbRating
                        count={5}
                        size={25}
                        showRating={false}
                        isDisabled={true}
                        defaultRating={this.props.route.params.courseRating}
                    />
                </View>
                <Text>{this.props.route.params.courseNameInstructor}</Text>
                <Text>{this.props.route.params.courseCreate}</Text>
                <Text style={styles.detailPrice}>Total Cost: {this.props.route.params.priceCourse}</Text>
                <TouchableOpacity style={styles.btnBuyNow} onPress={() => this.buyCourse()}>
                    <Text style={styles.txtBuyNow}>Buy Now</Text>
                </TouchableOpacity>
            </View>

        )
    }
}
const Stack = createStackNavigator();
export default class StackHome extends Component {

    render() {
        return (
            <Stack.Navigator >
                <Stack.Screen name="HomeScreen" component={HomeScreen} />
                <Stack.Screen name="detail" component={CourseDetail} />
            </Stack.Navigator>
        )
    }
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingBottom: 20
    },
    backgroundTop: {
        height: 200,
        justifyContent: 'center',
        paddingLeft: 30
    },
    imgTopTxt: {
        fontSize: 20,
        color: '#fff',
        fontWeight: 'bold',

    },
    imgHome: {
        height: 150,
        width: 230,
        borderRadius: 20
    },
    boxImage: {
        width: 250,
        flexWrap: 'nowrap',
        paddingLeft: 20
    },
    imgTxt: {
        fontSize: 15,
        fontWeight: 'bold'
    },
    instructorTxt: {
        color: '#33334d'
    },
    txt: {
        paddingLeft: 20,
        fontSize: 25,
        fontWeight: "bold"
    },
    detailCourseName: {
        fontSize: 35,
        fontWeight: 'bold',
    },
    detailCoursDes: {
        fontSize: 20,
        fontWeight: '600',
        opacity: 0.7
    },
    detailPrice: {
        fontSize: 20,
        fontWeight: 'bold'
    },
    btnBuyNow: {
        backgroundColor: 'blue',
        justifyContent: 'center',
        alignItems: 'center'
    },
    txtBuyNow: {
        padding: 10,
        fontSize: 20,
        fontWeight: 'bold',
        color: '#fff'
    }
})