import React,{Component} from 'react';
import {StyleSheet,Text,View,ScrollView,TouchableOpacity} from 'react-native'
import Ionicons from '../node_modules/react-native-vector-icons/Ionicons'
import TextBox from '../components/TextBox'
import MyButton from '../components/MyButton'
import {Button} from 'react-native-elements'

import firebase from '@firebase/app';
import '@firebase/database'
import { FirebaseError } from '@firebase/util';
const userValues=[];
const userAccountTable="UserAccount/";

export class AccountScreen extends Component{
    static navigationOptions = {
        title: 'Account',
        headerStyle: {
          backgroundColor: '#f4511e',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold',
        },
      };
constructor(props){
    super(props);
 
    this.state={
        username:"",
        email:"",
        phone:"",
    }
 
    this.getUserInfo=this.getUserInfo.bind(this);
    this.findUserInfo=this.findUserInfo.bind(this);
    this.UserSignedOut=this.UserSignedOut.bind(this);
   // this.update=this.update.bind(this);
    firebase.auth().onAuthStateChanged(this.UserSignedOut);
    this.getUserInfo();
}
componentDidMount()
{
    // this.update();
    // this.props.navigation.addListener('willFocus',this.update);

}
UserSignedOut()
{
      //if user logged out we need to go to sign in page
      if(!firebase.auth().currentUser)
        this.props.navigation.navigate("SignIn");

}

    findUserInfo(x)
    {
        var result=Object.values(x.val());
        infos=[];
        console.log("finduserinfo");
        result.forEach(element => {
        if(element.email === firebase.auth().currentUser.email)
        {
            console.log("set state for:");
            console.log(element.userName);
            infos.push(element.userName);
            infos.push(element.email);
            infos.push(element.phone);

        } 
        });
        this.setState({username:infos[0],email:infos[1],phone:infos[2]});
    }


    signOut()
    {
        firebase.auth().signOut().catch((err)=>console.info(err.message));
    }
    updateUserInfo()
    {

    }
    // update()
    // {
    //     console.log("componentDidMount");
    //       //if user logged out we need to go to sign in page
    //       if(!firebase.auth().currentUser)
    //       {
    //           this.props.navigation.navigate("SignIn");
    //       }
    // }
getUserInfo()
{
    console.log("getUserInfo");
    if(firebase.auth().currentUser)
    {
        console.log(firebase.auth().currentUser.email);
        firebase.database().ref(userAccountTable).once('value',this.findUserInfo);
    }
    else{
        this.signOut();
    }
}
    
      

    render(){
    
        return(
    <ScrollView style={styles.container}>
        <View style={{alignContent:'center',alignItems:'center'}}>
            <Ionicons name="md-contact" color="tomato" size={100}/>
        </View>
        <TextBox testID="0" text={this.state.username} placeholder="User Name" ></TextBox>
        <TextBox testID="1"  secureTextEntry={true} placeholder="Password" ></TextBox>
        <TextBox testID="2" secureTextEntry={true} placeholder="Password Confirm" ></TextBox>
        <TextBox testID="3" text={this.state.email} keyboardType="email-address" placeholder="e-Mail" ></TextBox>
        <TextBox testID="4" text={this.state.phone} keyboardType="phone-pad" placeholder="Phone"></TextBox>
        <View style={{alignContent:'center',alignItems:'center'}}>
        
            
            <Button
                containerStyle={{ marginVertical: 20 }}
                style={{
                  flex: 1,
                  justifyContent: 'center',
                  alignItems: 'center',
                  
                }}
                buttonStyle={{
                  height: 55,
                  width: 250,
                  borderRadius: 30,
                  justifyContent: 'center',
                  alignItems: 'center',
                  backgroundColor:'#FF9800',//#FF9800
                }}
              
                title="UPDATE"
                titleStyle={{
                  fontFamily: 'regular',
                  fontSize: 20,
                  color: 'white',
                  textAlign: 'center',
                }}
                onPress={this.updateUserInfo}
                activeOpacity={0.5}
              />
              <Button
                containerStyle={{ marginVertical: 20 }}
                style={{
                  flex: 1,
                  justifyContent: 'center',
                  alignItems: 'center',
                 
                }}
                buttonStyle={{
                  height: 55,
                  width: 250,
                  borderRadius: 30,
                  justifyContent: 'center',
                  alignItems: 'center',
                  backgroundColor:'#F44336',
                }}
              
                title="Sign Out"
                titleStyle={{
                  fontFamily: 'regular',
                  fontSize: 20,
                  color: 'white',
                  textAlign: 'center',
                }}
                onPress={this.signOut}
                activeOpacity={0.5}
              />
        </View>
   </ScrollView>

        );
    }
}
//rgba(214,116,112,1)   
//rgba(233,174,87,1)
const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#fff',
      
    },});