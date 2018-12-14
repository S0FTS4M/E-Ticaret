import React,{Component} from 'react';
import {StyleSheet,Text,View,TouchableOpacity,ScrollView} from 'react-native'
import TextBox from '../components/TextBox'
import Ionicons from '../node_modules/react-native-vector-icons/Ionicons'
import Icon from "react-native-vector-icons/FontAwesome";
import MyButton from '../components/MyButton';
import {Button} from 'react-native-elements'
import firebase from '@firebase/app';
import '@firebase/database'
import '@firebase/auth'
const userValues=[];
const userAccountTable="UserAccount/";
export class SignUpScreen extends Component {
  
  static navigationOptions = {
    title: 'Sign Up',
    tabBarVisible:false,
    headerStyle: {
      backgroundColor: '#2B90FF',
    },
    headerTintColor: '#fff',
    headerTitleStyle: {
      fontWeight: 'bold',
    },
  };
  
  
 
  addUserToFireBase()
  {
    firebase.auth().createUserWithEmailAndPassword(userValues[3],userValues[1]).then((val)=>{console.log(val.user.email)}).catch(function(err){console.log(err.code);console.log(err.message); });
    //validate values
    userName=userValues[0];
    pwd=userValues[1];
    pwdConf=userValues[2];
    email=userValues[3];
    phone=userValues[4];
    var table=userAccountTable;

    firebase.database().ref(table).push({
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
      <ScrollView style={styles.container}>
        <View style={{alignContent:'center',alignItems:'center'}}>
              <Ionicons name="md-person-add" color="#2B90FF" size={100}/>
          </View>
        <TextBox testID="1" secureTextEntry={true} placeholder="Password" onChangeText={this.onTextChanged}></TextBox>
        <TextBox testID="2" secureTextEntry={true} placeholder="Password Confirm" onChangeText={this.onTextChanged}></TextBox>
        <TextBox testID="3" keyboardType="email-address" placeholder="e-Mail" onChangeText={this.onTextChanged}></TextBox>
        <TextBox testID="4" keyboardType="phone-pad" placeholder="Phone" onChangeText={this.onTextChanged}></TextBox>
        <View style={{alignContent:'center',alignItems:'center'}}>
        <Button
              title="Add to Cart"
              titleStyle={{ fontWeight: 'bold', fontSize: 18 }}
              linearGradientProps={{
                colors: ['#FF9800', '#F44336'],
                start: [1, 0],
                end: [0.2, 0],
              }}

              buttonStyle={{
                borderWidth: 0,
                borderColor: 'transparent',
                borderRadius: 20,
                
              }}
              containerStyle={{ marginVertical: 10, height: 40, width: 200 }}
              icon={<Icon name="arrow-right" size={15} color="white" />}
              iconRight
              iconContainerStyle={{ marginLeft: 5 }}
            />
        <MyButton text="SIGN UP" onPress={this.addUserToFireBase}style={{width:250,margin:20,backgroundColor:'#2B90FF'}} textStyle={{fontSize:32}}></MyButton>
        </View>
   </ScrollView>
    );
  }
  
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    
  },});