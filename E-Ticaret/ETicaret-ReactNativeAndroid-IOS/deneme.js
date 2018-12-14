/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React, {Component} from 'react';
import { StyleSheet, Text, View,TouchableOpacity} from 'react-native';
import {
  createStackNavigator,
  createAppContainer
} from 'react-navigation';

import firebase from '@firebase/app';
import '@firebase/database'

import TextBox from './components/TextBox'

type Props = {};
const userValues=[];
const App=createAppContainer()
export default  class App extends Component<Props> {

  componentWillMount()
  {
    var config = {
      apiKey: "AIzaSyBffXQCMpQYkqD1P6WKymTUd2LkfccU2TU",
      authDomain: "eticaretreact.firebaseapp.com",
      databaseURL: "https://eticaretreact.firebaseio.com",
      projectId: "eticaretreact",
      storageBucket: "eticaretreact.appspot.com",
      messagingSenderId: "905886557078"
    };
    
    firebase.initializeApp(config);
    fbdb=firebase.database();

    //console.warn("deneme");


   
  }
 
  addUserToFireBase()
  {
    //validate values
    userName=userValues[0];
    pwd=userValues[1];
    pwdConf=userValues[2];
    email=userValues[3];
    phone=userValues[4];
    var table="UserAccount/"+userName;
    firebase.database().ref(table).set({
      pwd,
      email,
      phone
  }).then((data)=>{
      //success callback
      console.log('data ' , data)
  }).catch((error)=>{
      //error callback
      console.log('error ' , error)
  });
  }
  onTextChanged(val)
  {
    if(this.testID==="0"){
      userValues[0]=val;
    }
    if(this.testID==="1"){
      userValues[1]=val;
    }
    if(this.testID==="2"){
      userValues[2]=val;
    }
    if(this.testID==="3"){
      userValues[3]=val;
    }
    if(this.testID==="4"){
      userValues[4]=val;
    }
  }
  render() {
    return (
      <View style={styles.container}>
      <TextBox testID="0" placeholder="User Name" onChangeText={this.onTextChanged}></TextBox>
      <TextBox testID="1" secureTextEntry={true} placeholder="Password" onChangeText={this.onTextChanged}></TextBox>
      <TextBox testID="2" secureTextEntry={true} placeholder="Password Confirm" onChangeText={this.onTextChanged}></TextBox>
      <TextBox testID="3" keyboardType="email-address" placeholder="e-Mail" onChangeText={this.onTextChanged}></TextBox>
      <TextBox testID="4" keyboardType="phone-pad" placeholder="Phone" onChangeText={this.onTextChanged}></TextBox>
      <TouchableOpacity onPress={this.addUserToFireBase}><Text>Click Me</Text></TouchableOpacity>
   </View>
    );
  }
}


const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5FCFF',
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5,
  },

});
