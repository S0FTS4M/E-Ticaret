import React,{Component} from 'react';
import {StyleSheet,Text,View,ToastAndroid,ScrollView} from 'react-native'
//import Button from '../components/MyButton'
import {Card,Button} from 'react-native-elements'

const products=[]
 export class HomeScreen extends Component {

  constructor(props)
  {
    super(props);
    products.push({
      name:"Nike",
      uri:"https://images-na.ssl-images-amazon.com/images/I/61eTOL24pEL._UX395_.jpg",
      detail:"Erkek spor ayakkabısı"
    });
    
    products.push({
      name:"ONEMIX",
      uri:"https://ae01.alicdn.com/kf/HTB1mlmEKXXXXXbSXFXXq6xXFXXXB/ONEMIX-Brand-Top-Quality-Women-Running-Shoes-with-Mesh-Cushion-Women-Sport-Shoes-Girls-Outdoor-Sneakers.jpg",
      detail:"Kadın spor ayakkabısı"
    });

    products.push({
      name:"TWD Sports",
      uri:"https://rukminim1.flixcart.com/image/612/612/jezzukw0/shoe/x/a/t/wndr-13-8-asian-grey-green-original-imaenr72gssfxrbj.jpeg?q=70",
      detail:"Erkek spor ayakkabısı"
    });
    
    products.push({
      name:"Ayakkabı",
      uri:"https://images-na.ssl-images-amazon.com/images/I/61eTOL24pEL._UX395_.jpg",
      detail:"Erkek spor ayakkabısı"
    });
    
    products.push({
      name:"ADZA",
      uri:"https://rukminim1.flixcart.com/image/612/612/jfzpuvk0/shoe/c/y/a/ax-002-6-adza-grey-original-imaf4c5khcx8df5y.jpeg?q=70",
      detail:"Erkek spor ayakkabısı"
    });
    
    
    products.push({
      name:"FASHION",
      uri:"https://d1vs5fqeka2glf.cloudfront.net/07/e3/07cf246d7e8d1a444c8ccdd1c962b8e3.jpg",
      detail:"kadın spor ayakkabısı"
    });
    
    products.push({
      name:"FASHION",
      uri:"https://images-na.ssl-images-amazon.com/images/I/91XnZi1tlXL._UY395_.jpg",
      detail:"Çocuk spor ayakkabısı"
    });
  }
    static navigationOptions = {
      title: 'Home',
      
      headerStyle: {
        backgroundColor: '#f4511e',
      },
      headerTintColor: '#fff',
      headerTitleStyle: {
        fontWeight: 'bold',
      },
    };
    cardItem(name ,imageURL,detail)
    {
     return (<Card key={name}
              title={name}
              image={{uri:imageURL}}><Text style={{marginBottom: 10}}>{detail}</Text><Button
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
      return (
        <ScrollView style={{ flex: 1}}>
          {
            products.map((item)=>this.cardItem(item.name,item.uri,item.detail))
          }
         
        </ScrollView>
      );
    }
  }
