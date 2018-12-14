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
const listviewItems = [
  {
    title:'MALE',
    subtitle:'',
    icon: "",
    active : false
  },
  {
    title: 'Casual Shoes',
    category:'Man',
    subCategory:'Casual',
    subtitle:'Man',
    icon: formal,
    active : true
  },
  {
    title: 'Sport Shoes',
    category:'Man',
    subCategory:'Sport',
    subtitle:'Man',
    icon: sport,
    active : true
  },
  {
    title: 'Snow Shoes',
    category:'Man',
    subCategory:'Winter',
    subtitle:'',
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
    category:'Woman',
    subCategory:'Casual',
    icon: womanformal,
    active : true
  },
  {
    title: 'Sport Shoes',
    subtitle:'Woman',
    category:'Woman',
    subCategory:'Sport',
    icon: sport,
    active : true
  },
  {
    title: 'Snow Shoes',
    subtitle:'Woman',
    category:'Woman',
    subCategory:'Winter',
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
    category:'Kid',
    subCategory:'Casual',
    icon: kid,
    active : true
  },
  {
    title: 'Sport Shoes',
    subtitle:'Kid',
    category:'Kid',
    subCategory:'Sport',
    icon: sport,
    active : true
  },
  {
    title: 'Snow Shoes',
    subtitle:'Kid',
    category:'Kid',
    subCategory:'Winter',
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
  //Bu tamamen test amacı ile oluşturulmuş bir şey
  GetFromDataBase(category)
  {
    console.log("items adding");
    var i=1000;
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Nike",
      Amount:50,
      Image:"https://images-na.ssl-images-amazon.com/images/I/61eTOL24pEL._UX395_.jpg",
      Desc:"Ayağınızda şık duracak ve rahat edeceksiniz.",
      Category:"Man",
      Price:300,
      SubCategory:"Sport",

    });
    //2
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Onemix",
      Amount:10,
      Image:"https://ae01.alicdn.com/kf/HTB1mlmEKXXXXXbSXFXXq6xXFXXXB/ONEMIX-Brand-Top-Quality-Women-Running-Shoes-with-Mesh-Cushion-Women-Sport-Shoes-Girls-Outdoor-Sneakers.jpg",
      Desc:"Sporcu bir kadın mısınız? Tam ayağınıza göre.",
      Category:"Woman",
      Price:60,
      SubCategory:"Sport",
      
    });
    //3
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"TWD Sports",
      Amount:150,
      Image:"https://rukminim1.flixcart.com/image/612/612/jezzukw0/shoe/x/a/t/wndr-13-8-asian-grey-green-original-imaenr72gssfxrbj.jpeg?q=70",
      Desc:"Erkek sporcuların vazgeçilmez tercihi!",
      Category:"Man",
      Price:90,
      SubCategory:"Sport",
    });
    //4
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Adza",
      Amount:5,
      Image:"https://rukminim1.flixcart.com/image/612/612/jfzpuvk0/shoe/c/y/a/ax-002-6-adza-grey-original-imaf4c5khcx8df5y.jpeg?q=70",
      Desc:"Adza ile sporun farkını hissedin...",
      Category:"Man",
      Price:200,
      SubCategory:"Sport",
    });
    //5
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Fashion",
      Amount:20,
      Image:"https://d1vs5fqeka2glf.cloudfront.net/07/e3/07cf246d7e8d1a444c8ccdd1c962b8e3.jpg",
      Desc:"Ayağınızda şık duracak ve rahat edeceksiniz.",
      Category:"Woman",
      Price:250,
      SubCategory:"Sport",
    });
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Fashion",
      Amount:7,
      Image:"https://images-na.ssl-images-amazon.com/images/I/91XnZi1tlXL._UY395_.jpg",
      Desc:"Çocuğunuz Ayakkabı dedi ve tutturdu mu? Bu ürünü görmek istersiniz",
      Category:"Kid",
      Price:100,
      SubCategory:"Sport",
    });
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Toursh Italian",
      Amount:17,
      Image:"https://ae01.alicdn.com/kf/HTB1_h5Gi3vD8KJjy0Flq6ygBFXa5/TOURSH-Shoes-Men-Casual-Leather-Shoes-Men-Flats-Leather-Casual-Mens-Luxury-Fashion-Handmad-Shoes-Italian.jpg_640x640.jpg",
      Desc:"İtalyan marka bir ayakkabınız yok mu? Neden?",
      Category:"Man",
      Price:100,
      SubCategory:"Casual",
    });
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Clarks",
      Amount:3,
      Image:"https://shop.r10s.jp/reload/cabinet/img38/clarks-918c-1-1.jpg",
      Desc:"Clarks ile hayata karşı sıkı durun!",
      Category:"Man",
      Price:700,
      SubCategory:"Casual",
    });
    
    firebase.database().ref("Products").push({
      ID:i++,
      Name:"Mesh Zapatilla",
      Amount:30,
      Image:"https://ae01.alicdn.com/kf/HTB1jrXoOXXXXXbyapXXq6xXFXXXm/2017-Designer-Summer-Women-Casual-Shoes-Female-Breathable-Mesh-Zapatillas-Shoes-for-Women-s-Soft-Canvas.jpg_640x640.jpg",
      Desc:"Clarks ile hayata karşı sıkı durun!",
      Category:"Woman",
      Price:100,
      SubCategory:"Casual",
    });
 firebase.database().ref("Products").push({
      ID:i++,
      Name:"Fashion",
      Amount:30,
      Image:"https://i.pinimg.com/originals/ee/83/3e/ee833e0e82cb4029b7ab4b91f6e0e694.jpg",
      Desc:"Topuklu ayakkabı sıradan mıdır?",
      Category:"Woman",
      Price:200,
      SubCategory:"Casual",
    });

 firebase.database().ref("Products").push({
      ID:i++,
      Name:"Koovan",
      Amount:90,
      Image:"https://www.dhresource.com/0x0s/f2-albu-g4-M00-ED-69-rBVaEFnHndOAb7QPAAL53z6Um4M010.jpg/casual-shoes-girls-boy-running-sneaker-kid.jpg",
      Desc:"Siz bu kadar şık olmak ister misiniz? ",
      Category:"Woman",
      Price:180,
      SubCategory:"Casual",
    });
firebase.database().ref("Products").push({
      ID:i++,
      Name:"Adidas",
      Amount:30,
      Image:"https://shop.r10s.jp/reload/cabinet/img13/adi-winterfun-boy-2.jpg",
      Desc:"Kış gelince çocuklar üşümesin!!!",
      Category:"Kid",
      Price:200,
      SubCategory:"Winter",
    });

 

firebase.database().ref("Products").push({
      ID:i++,
      Name:"Zipper",
      Amount:90,
      Image:"https://cdn.shopify.com/s/files/1/1521/5010/products/Women-Boots-Zipper-Shoes-Women-Winter-Boots-Female-Warm-Winter-Shoes-Ankle-Boots-for-Women-Cotton_2c86eef6-9079-4ca6-bf43-2a7be26ffcb2_1024x1024.jpg?v=1512745887",
      Desc:"Soğuk için şıklığınızdan vaz geçecek değilsiniz!",
      Category:"Woman",
      Price:600,
      SubCategory:"Winter",
    });
firebase.database().ref("Products").push({
      ID:i++,
      Name:"EOGC",
      Amount:90,
      Image:"https://ae01.alicdn.com/kf/HTB15SU4dH_I8KJjy1Xaq6zsxpXaF/Vintage-Style-Men-Boots-Natural-Leather-Autumn-And-Winter-Shoes-Water-Proof-Work-Safety-Shoes-Men.jpg_640x640.jpg",
      Desc:"Soğuk için şıklığınızdan vaz geçecek değilsiniz!",
      Category:"Man",
      Price:780,
      SubCategory:"Winter",
    });
  }
  
  render() {
    
    return (
      <ScrollView style={{ flex: 1}} >{
        
      listviewItems.map((item, i) => (
      <ListItem
      //badge={{ value: 3, textStyle: { color: 'white' }, containerStyle: { marginTop: -20 } }}
      component={TouchableNativeFeedback}
      key={i}
      topDivider={!item.active}
      bottomDivider={!item.active}
      containerStyle={{backgroundColor:'#eee'}}
      
      onPress={(e)=>{item.active?this.props.navigation.navigate('Home',{category:item.category,subCategory:item.subCategory}):console.log("bu aktif değil")}}
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