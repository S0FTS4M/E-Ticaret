import React from "react";
import { View, Text,StyleSheet,TouchableOpacity } from "react-native";
import { createStackNavigator, createAppContainer,createBottomTabNavigator } from "react-navigation";
import Ionicons from 'react-native-vector-icons/Ionicons';
import firebase from '@firebase/app';
import '@firebase/database';
import {HomeScreen} from './screens/HomeScreen';
import {ProductsScreen} from './screens/ProductScreen';
import SignInUPScreen from './screens/LoginSignUpScreen' ;
import {SearchScreen} from './screens/SearchScreen';
import {ProductShowScreen} from './screens/ProductShowScreen'
import { CartScreen } from "./screens/CartScreen";
import { YellowBox } from 'react-native';
import _ from 'lodash';

YellowBox.ignoreWarnings(['Setting a timer']);
const _console = _.clone(console);
console.warn = message => {
  if (message.indexOf('Setting a timer') <= -1) {
    _console.warn(message);
  }
};


const HomeStack = createStackNavigator({
  Home: HomeScreen,
  Cart:CartScreen,
  ProductShow:{screen:ProductShowScreen,
    navigationOptions: ({ navigation }) => ({
      tabBarVisible: true,
     
    }),

}});
const SearchStack = createStackNavigator({
  Search: SearchScreen,
 
});
  
const ProductsStack = createStackNavigator({
  Products:ProductsScreen,
  
});

const AccountsStack = createStackNavigator({
  
  SignIn:SignInUPScreen,
 // Account:AccountScreen,


  
});
//
const BottomTabContainer = createBottomTabNavigator({Home:HomeStack,Products:ProductsStack,Account:AccountsStack,Search:SearchStack},
  {
    defaultNavigationOptions: ({ navigation }) => ({
      tabBarIcon: ({ focused, horizontal, tintColor }) => {
        const { routeName } = navigation.state;
        let iconName;
        if (routeName === 'Home') {
          iconName = 'md-home';
        } else if (routeName === 'Products') {
          iconName = 'ios-apps';
        }else if (routeName === 'Account') {
          iconName = 'ios-contact';
        }
        else if (routeName === 'Search') {
          iconName = 'ios-search';
        }
        // You can return any component that you like here! We usually use an
        // icon component from react-native-vector-icons
        return <Ionicons name={iconName}  size={horizontal ? 20 : 25} color={tintColor} />;
      },
    }),
    tabBarOptions: {
      activeTintColor: 'tomato',
      inactiveTintColor: 'gray',
      
    },

  });

const AppContainer =createAppContainer(BottomTabContainer);

export default class App extends React.Component {
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


  }
  
  render() {
    return  <AppContainer onNavigationStateChange={(prevState, currentState) => {
      const currentScreen = getCurrentRouteName(currentState);
      const prevScreen = getCurrentRouteName(prevState);
     /// console.log(currentScreen);
     
    }}/>;
    
  }
}
function getCurrentRouteName(navigationState) {
  if (!navigationState) {
    return null;
  }
  const route = navigationState.routes[navigationState.index];
  // dive into nested navigators
  if (route.routes) {
    return getCurrentRouteName(route);
  }
  return route.routeName;
}