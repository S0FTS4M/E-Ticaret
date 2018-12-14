import React,{Component} from 'react';
import {StyleSheet,Text,View,ToastAndroid,ScrollView} from 'react-native'
import Button from '../components/MyButton'
import Ionicons from 'react-native-vector-icons/Ionicons'
import { SearchBar } from 'react-native-elements'

const dummySearchBarProps = {
  showLoading: false,
  onFocus: () => console.log('focus'),
  onBlur: () => console.log('blur'),
  onCancel: () => console.log('cancel'),
  onClearText: () => console.log('cleared'),
  onChangeText: text => console.log('text:', text),
};
 export class SearchScreen extends Component {
    static navigationOptions = {
      title: 'Search',
      header:
      <SearchBar
      placeholder="Search"
      platform="ios"
      
     
    />
      ,
      
      headerStyle: {
        backgroundColor: '#f4511e',
      },
      headerTintColor: '#fff',
      headerTitleStyle: {
        fontWeight: 'bold',
      },
    };
    message()
    {
        console.log("clikced");
        ToastAndroid.show("Hello From search",ToastAndroid.SHORT);
    }
    someMethod(){}
    render() {
      return (
        <ScrollView style={{ flex: 1}}>




        </ScrollView>
      );
    }
  }
/*

 */