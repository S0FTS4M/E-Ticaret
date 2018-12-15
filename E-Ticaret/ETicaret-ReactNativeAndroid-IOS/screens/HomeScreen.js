import React,{Component} from 'react';
import {StyleSheet,Text,View,ToastAndroid,ScrollView,TouchableOpacity,Image} from 'react-native'
//import Button from '../components/MyButton'
import firebase from '@firebase/app';
import '@firebase/database'
import {Card,Button, Icon,Header,PricingCard} from 'react-native-elements'
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
    this.cardItem=this.cardItem.bind(this);
    this.pricingCardItem=this.pricingCardItem.bind(this);
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
      if(p&&p.val()){
      var queryResult=Object.values(p.val());
      queryResult.forEach(element => {
        products.push({
          product:element
        });
      });
      
    }
    this.setState({productsLoaded:true});
    }
    getCategoriziedProducts(p)
    {
      products=[];
      if(p&&p.val()){
      var queryResult=Object.values(p.val());
      
      queryResult.forEach(element => {
        if(element.Category==this.state.category && element.SubCategory==this.state.subCategory){
        products.push({
          product:element
        });
      }
      });
      
    }
    this.setState({productsLoaded:true});
    }
    pricingCardItem(item)
    {
      
      titleDisc=item.Discount + " % Discount";
      priceWithDiscount=(item.Price - ((item.Price * item.Discount) / 100) ) + "$";
      return (
       
        <PricingCard key={item.ID||item.Id}
          color='#A72DE9'
          onButtonPress={()=>this.props.navigation.navigate('ProductShow',{productID:item.ID||item.Id})}
          title={titleDisc}
          price={priceWithDiscount}
          info={[item.Name, item.Desc, item.Category + " " + item.SubCategory]}
          button={{ title: 'VIEW NOW', icon: 'flight-takeoff' }}
/>

      );
    }
    
    cardItem(item)
    {
     
      return (<Card key={item.ID||item.Id}
              title={item.Name}

              image={{uri:item.Image}}
              imageProps={{resizeMode:"center",borderRadius:20}}
              >
              
              <Text style={{marginBottom: 10}}>{item.Desc}
              </Text>
              {item.Discount > 0 && <Text
                  style={{
                    flex: 1,
                    fontSize: 26,
                    color: 'green',
                    fontFamily: 'bold',
                    textAlign: 'right',
                  }}
                  
                >
                  {item.Discount + "%"}
                </Text>}
              <Button
                 icon={{name: 'search',color:'white'}}
                 iconRight={true}
                 onPress={()=>this.props.navigation.navigate('ProductShow',{productID:item.ID||item.Id})}
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
       barStyle="dark-content"

       backgroundColor="tomato"
       leftComponent={{ icon: 'home', color: '#fff' }}
        centerComponent={<Text style={{color:"white",fontSize:32}}>Home</Text>}
        rightComponent={<TouchableOpacity  onPress={()=>firebase.database().ref("Products").once('value',this.getProducts)}><Icon name="replay" size={26} color="white"/></TouchableOpacity>}
          />
          {
            
            products.map((item)=>item.product.Discount >= 45? this.pricingCardItem(item.product):this.cardItem(item.product))
          }
      
        </ScrollView>
      );
    }
  }
