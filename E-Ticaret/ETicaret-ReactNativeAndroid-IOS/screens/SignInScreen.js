import React,{Component} from 'react';
import {StyleSheet,Text,View,ScrollView,ToastAndroid} from 'react-native'
import TextBox from '../components/TextBox'
import Button from '../components/MyButton'
import Ionicons from '../node_modules/react-native-vector-icons/Ionicons'
import firebase from '@firebase/app';
import '@firebase/database'
import '@firebase/auth'
const userValues=[];
const userAccountTable="UserAccount/";
export class SignInScreen extends Component {
  constructor(props)
  {
    super(props);
    this.queryUser=this.queryUser.bind(this);
    this.UserSignedIn=this.UserSignedIn.bind(this);
    firebase.auth().onAuthStateChanged(this.UserSignedIn);
  }
  static navigationOptions = {
    title: 'Sign In',
    tabBarVisible:false,
    headerStyle: {
      backgroundColor: '#2B90FF',
    },
    headerTintColor: '#fff',
    headerTitleStyle: {
      fontWeight: 'bold',
    },
    
  };
  UserSignedIn()
  {
    console.log("user auth state changed");
    if(firebase.auth().currentUser)
    {
      console.log("user auth state changed inside if");
      ToastAndroid.show("Sign In Successfull!",ToastAndroid.SHORT);
      this.props.navigation.navigate('Account');
    }
    
  }
  signIn()
  {
    
    
    if(firebase.auth().currentUser)
    {
      console.log("UserSigIn Called");
      firebase.auth().signOut().catch((err)=>console.log(err.message));
    }
    firebase.auth().signInWithEmailAndPassword("sml.ozclk@gmail.com","softsam").catch((err)=>console.log(err.message));
    //
  }
 
  queryUser()
  {
    //validate values
    //userName=userValues[0];
   // pwd=userValues[1];
    //FOR TESTING
    userName="softsam";
    pwd="softsam";
 /*const nameToSearch = 'John';
firebase.ref('users').once('value') //get all content from your node ref, it will return a promise
.then(snapshot => { // then get the snapshot which contains an array of objects
  snapshot.val().filter(user => user.name === nameToSearch) // use ES6 filter method to return your array containing the values that match with the condition
})*/
    var loggedIn=false;
firebase.database().ref(userAccountTable).once('value',(x)=>{
  var result=Object.values(x.val());
  result.forEach(element => {
    if(element.userName === userName)
   {
    gotpwd=element.pwd;
    console.log(gotpwd);
    console.log("userFound");
    if(pwd===gotpwd)
    {
      console.log("Logged in!");
      //this.props.navigation.navigate('Account');
    }
   } 
  });
  
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

  }
  render() {
    //ios-log-in-outline
    return (
      <ScrollView style={styles.container}>
        <View style={{alignContent:'center',alignItems:'center'}}>
                <Ionicons name="md-log-in" color="#2B90FF" size={100}/>
            </View>
      <TextBox testID="0"  keyboardType="email-address" placeholder="Email" onChangeText={this.onTextChanged}></TextBox>
      <TextBox testID="1" secureTextEntry={true} placeholder="Password" onChangeText={this.onTextChanged}></TextBox>
      <View style={{alignContent:'center',alignItems:'center'}}>
      <Button text="SIGN IN" onPress={this.signIn}style={{width:250,margin:20,backgroundColor:'#2B90FF'}} textStyle={{fontSize:32}}></Button>
      <Text>You don't have an account? </Text>
      <Button text="SIGN UP" onPress={()=>this.props.navigation.navigate('SignUp')}style={{width:250,margin:20,backgroundColor:'#4bd963'}} textStyle={{fontSize:32}}></Button>
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