import React,{Component} from 'react';
import {StyleSheet,Text,View,ScrollView,TouchableOpacity} from 'react-native'
import Ionicons from '../node_modules/react-native-vector-icons/Ionicons'
import TextBox from '../components/TextBox'
import MyButton from '../components/MyButton'
import {Button,Input} from 'react-native-elements'

import firebase from '@firebase/app';
import '@firebase/database'

const userValues=[];
const userAccountTable="UserAccount/";
import {
    BallIndicator,
  } from 'react-native-indicators';
/*  BallIndicator,
  BarIndicator,
  MaterialIndicator,
  PacmanIndicator,
  PulseIndicator,
  SkypeIndicator,
  UIActivityIndicator,
  WaveIndicator, */
//login signup ve account aynı sayfada bulunacak ben bir state değişkeni ile giriş yapıp yapmadığını kontrol edeceğim giriş yapmışsa
//account yapmamışsa login görünecek... üzerine düşün
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
        email:"",
        name:"",
        role:"",
        surname:"",
        password:"",
        userLoaded:false,
        userSignedOut:false
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
    //   if(!firebase.auth().currentUser)
    //     this.props.navigation.navigate("SignIn");
    console.log("sign out");
    //this.props.onSignOutPressed();
    this.setState({userSignOut:true});

}

    findUserInfo(x)
    {
        var result=Object.values(x.val());
        infos=[];
        console.log("finduserinfo");
        result.forEach(element => {
        if(element.EMail === firebase.auth().currentUser.email)
        {
       
            infos.push(element.Name);
            infos.push(element.EMail);
            infos.push(element.Role);
            infos.push(element.Surname);

        } 
        });
        this.setState({name:infos[0],email:infos[1],role:infos[2],surname:infos[3],userLoaded:true});
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
    const{
        email,
        name,
        role,
        surname,
    
    }=this.state;
        return(
            !this.state.userLoaded?(
            <View>
                <BallIndicator color="tomato"/>
                <Text>Gathering Data...</Text>
                </View>

            ) :

    <ScrollView style={styles.container}>
        <View style={{alignContent:'center',alignItems:'center'}}>
            <Ionicons name="md-contact" color="tomato" size={100}/>
        </View>
        <Input  value={email}
                    keyboardAppearance="light"
                    autoCapitalize="none"
                    autoCorrect={false}
                    
                    returnKeyType={ 'next'}
                    blurOnSubmit={true}
                    containerStyle={{
                      marginTop: 16,
                      marginLeft:15,
                      borderBottomColor: 'rgba(0, 0, 0, 0.38)',
                    }}
                    inputStyle={{ marginLeft: 10 }}
                    placeholder={'Email'}
                    ref={input => (this.emailInput = input)}
                    // onSubmitEditing={() =>
                    //   isSignUpPage
                    //     ? this.confirmationInput.focus()
                    //     : this.login()
                    // }
                    onChangeText={email => this.setState({ email })}
                  />
                   <Input value={name}
                    keyboardAppearance="light"
                    autoCapitalize="none"
                    autoCorrect={false}
                    
                    returnKeyType={ 'next'}
                    blurOnSubmit={true}
                    containerStyle={{
                      marginTop: 16,
                      marginLeft:15,
                      borderBottomColor: 'rgba(0, 0, 0, 0.38)',
                    }}
                    inputStyle={{ marginLeft: 10 }}
                    placeholder={'Name'}
                    ref={input => (this.nameInput = input)}
                    // onSubmitEditing={() =>
                    //   isSignUpPage
                    //     ? this.confirmationInput.focus()
                    //     : this.login()
                    // }
                    onChangeText={name => this.setState({ name })}
                  />
                   <Input value={surname}
                    keyboardAppearance="light"
                    autoCapitalize="none"
                    autoCorrect={false}
                    
                    returnKeyType={ 'next'}
                    blurOnSubmit={true}
                    containerStyle={{
                      marginTop: 16,
                      marginLeft:15,
                      borderBottomColor: 'rgba(0, 0, 0, 0.38)',
                    }}
                    inputStyle={{ marginLeft: 10 }}
                    placeholder={'Surname'}
                    ref={input => (this.surnameInput = input)}
                    // onSubmitEditing={() =>
                    //   isSignUpPage
                    //     ? this.confirmationInput.focus()
                    //     : this.login()
                    // }
                    onChangeText={surname => this.setState({ surname })}
                  />
                    <Input value={role}
                    keyboardAppearance="light"
                    autoCapitalize="none"
                    autoCorrect={false}
                    
                    returnKeyType={ 'next'}
                    blurOnSubmit={true}
                    containerStyle={{
                      marginTop: 16,
                      marginLeft:15,
                      borderBottomColor: 'rgba(0, 0, 0, 0.38)',
                    }}
                    inputStyle={{ marginLeft: 10 }}
                    placeholder={'Role'}
                    ref={input => (this.roleInput = input)}
                    // onSubmitEditing={() =>
                    //   isSignUpPage
                    //     ? this.confirmationInput.focus()
                    //     : this.login()
                    // }
                    onChangeText={role => this.setState({ role })}
                  />
                  <Input keyboardAppearance="light"
                    autoCapitalize="none"
                    autoCorrect={false}
                    secureTextEntry={true}
                    returnKeyType={'next' }
                    blurOnSubmit={true}
                    containerStyle={{
                      marginTop: 16,
                      marginLeft:15,
                      borderBottomColor: 'rgba(0, 0, 0, 0.38)',
                    }}
                    inputStyle={{ marginLeft: 10 }}
                    placeholder={'Password'}
                    ref={input => (this.passwordInput = input)}
                  
                    onChangeText={password => this.setState({ password })}
                 
                  />
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
                onPress={()=>{this.signOut();this.props.onSignOutPressed();}}
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