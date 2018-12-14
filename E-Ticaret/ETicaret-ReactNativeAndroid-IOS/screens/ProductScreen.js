import React,{Component} from 'react';
import {StyleSheet,Text,View,TouchableNativeFeedback,ScrollView,TouchableWithoutFeedbackComponent} from 'react-native'

import firebase from '@firebase/app';
import '@firebase/database'
import { ListItem } from 'react-native-elements'
import Ionicons from 'react-native-vector-icons/Ionicons';
import MatComIcons from 'react-native-vector-icons/MaterialCommunityIcons'

const userAccountTable="UserAccount/";
const snow="snowflake";
const formal="shoe-formal";
const womanformal="shoe-heel";
const sport="car-sports";
const kid="human-child"
const list = [
  {
    title:'MALE',
    subtitle:'',
    icon: "",
    active : false
  },
  {
    title: 'Casual Shoes',
    subtitle:'Man',
    icon: formal,
    active : true
  },
  {
    title: 'Sport Shoes',
    subtitle:'Man',
    icon: sport,
    active : true
  },
  {
    title: 'Snow Shoes',
    subtitle:'Man',
    icon: snow,
    active : true
  },
  {
    title:'FEMALE',
    subtitle:'',
    icon: "",
    active : false
  },
  {
    title: 'Casual Shoes',
    subtitle:'Woman',
    icon: womanformal,
    active : true
  },
  {
    title: 'Sport Shoes',
    subtitle:'Woman',
    icon: sport,
    active : true
  },
  {
    title: 'Snow Shoes',
    subtitle:'Woman',
    icon: snow,
    active : true
  },
  {
    title:'KIDS',
    subtitle:'',
    icon: "",
    active : false
  },
  {
    title: 'Casual Shoes',
    subtitle:'Kid',
    icon: kid,
    active : true
  },
  {
    title: 'Sport Shoes',
    subtitle:'Kid',
    icon: sport,
    active : true
  },
  {
    title: 'Snow Shoes',
    subtitle:'Kid',
    icon: snow,
    active : true
  },
  

]


export class ProductsScreen extends React.Component {
  constructor(props)
  {
    super(props);
    this.state={
      val:[],
    };
    this.getFromDB=this.getFromDB.bind(this);
  }
  static navigationOptions = {
    title: 'Products',
    headerStyle: {
      backgroundColor: '#f4511e',
    },
    headerTintColor: '#fff',
    headerTitleStyle: {
      fontWeight: 'bold',
    },
  };
   users=null;
  getFromDB()
  {
      if(firebase.auth().currentUser){
      console.log("getFromDB");
      firebase.database().ref(userAccountTable).once('value',(x)=>{
        var result=Object.values(x.val());
        this.setState({val:result});
        console.log(result);

        
      });
    }
    else
    {
      this.setState({val:""});
    }
  }

  render() {
    
    return (
      <ScrollView style={{ flex: 1}} >{
        
      list.map((item, i) => (
      <ListItem
      //badge={{ value: 3, textStyle: { color: 'white' }, containerStyle: { marginTop: -20 } }}
      component={TouchableNativeFeedback}
      key={i}
      topDivider={!item.active}
      bottomDivider={!item.active}
      containerStyle={{backgroundColor:'#eee'}}
      
      onPress={(e)=>{item.active?console.log(item.title + ":" + item.subtitle):console.log("bu aktif değil")}}
      title={item.title}
      titleStyle={!item.active?{fontSize:25}:{fontSize:16}}
      //subtitle={item.active?item.subtitle:null}
     leftIcon={item.active?<MatComIcons name={item.icon} size={35}/>:null }
      //leftAvatar={item.icon}
      rightIcon={item.active?<Ionicons name={"ios-arrow-dropright"} size={35} color="tomato" />:null}
      
      />
    ))
    }
      </ScrollView>
    );
  }
}