import React,{Component} from 'react';
import {StyleSheet,Text,View,ToastAndroid,ScrollView,TouchableOpacity} from 'react-native'
//import Button from '../components/MyButton'
import firebase from '@firebase/app';
import '@firebase/database'
import {Card,Button, Icon,Header} from 'react-native-elements'
import {
  DotIndicator,
} from 'react-native-indicators';




var products=[]
 export class HomeScreen extends Component {

  
  constructor(props)
  {

    super(props);
    this.state={
      productsLoaded:false,
      category:"",
      subCategory:"",
    }
    this.getProducts=this.getProducts.bind(this);
    this.getCategoriziedProducts=this.getCategoriziedProducts.bind(this);
    this.componentDidMount=this.componentDidMount.bind(this);
   firebase.database().ref("Products").once('value',this.getProducts);
  }
  componentDidMount() {
    this.props.navigation.addListener('willFocus', (playload)=>{
      if(playload&&playload.action.params&&playload.action.params.category){
     this.setState({category:playload.action.params.category,subCategory:playload.action.params.subCategory,productsLoaded:false});
    //call filler function here  
    firebase.database().ref("Products").once('value',this.getCategoriziedProducts);
    }
    });
  }
  
    static navigationOptions = {
      title: 'Home',
      header:null,
      headerStyle: {
        backgroundColor: '#f4511e',
      },
      headerTintColor: '#fff',
      headerTitleStyle: {
        fontWeight: 'bold',
      },
    };
    getProducts(p)
    {
      products=[];
      var queryResult=Object.values(p.val());
      queryResult.forEach(element => {
        products.push({
          product:element
        });
      });
      this.setState({productsLoaded:true});
    }
    getCategoriziedProducts(p)
    {
      
      var queryResult=Object.values(p.val());
      products=[];
      queryResult.forEach(element => {
        if(element.Category==this.state.category && element.SubCategory==this.state.subCategory){
        products.push({
          product:element
        });
      }
      });
      this.setState({productsLoaded:true});
    }
    cardItem(item)
    {
     
      return (<Card key={item.ID}
              title={item.name}
              image={{uri:item.Image}}>
              <Text style={{marginBottom: 10}}>{item.Desc}
              </Text>
              <Button
                 icon={{name: 'search',color:'white'}}
                 iconRight={true}
                buttonStyle={{borderRadius: 0, marginLeft: 0, marginRight: 0, marginBottom: 0}}
                title='VIEW NOW' /></Card>);
    }
    message()
    {
        console.log("clikced");
        ToastAndroid.show("Hello",ToastAndroid.SHORT);
    }
    render() {
      const category=this.state.category;
      const subCategory=this.state.subCategory;
      if(category&&subCategory){
      console.log(category);
      console.log(subCategory);
      }
      
      return (

        !this.state.productsLoaded?
          (
            <View style={{flex:1,justifyContent:"center",alignItems:"center"}}>
             
               <DotIndicator count={6} color="tomato"/>
             
            </View>
          )
          :
        <ScrollView style={{ flex: 1}}>
       <Header
       backgroundColor="tomato"
        centerComponent={<Text style={{color:"white",fontSize:32}}>Home</Text>}
        rightComponent={<TouchableOpacity  onPress={()=>firebase.database().ref("Products").once('value',this.getProducts)}><Icon name="replay" size={26} color="white"/></TouchableOpacity>}
          />
          {
            
            products.map((item)=>this.cardItem(item.product))
          }
      
        </ScrollView>
      );
    }
  }
