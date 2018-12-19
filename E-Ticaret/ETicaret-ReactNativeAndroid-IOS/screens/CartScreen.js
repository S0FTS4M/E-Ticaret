import React,{ Component } from "react";
import { View ,Image,Text,ToastAndroid,ScrollView} from "react-native";
import { Card,ListItem } from "react-native-elements";
import  firebase  from "@firebase/app";
import '@firebase/auth'
import '@firebase/database'
import Swipeout from "react-native-swipeout";



export class CartScreen extends Component{
    
    static navigationOptions = {
        title: 'My Cart',
        headerStyle: {
          backgroundColor: '#f4511e',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold',
        },
      };
    constructor(props)
    {
        super(props);
        this.state={
            productsID:[],
            productArr:[],
            activeItem:undefined,

        };
      this.gatherCartListItems=this.gatherCartListItems.bind(this);
      this.removeItemFromList=this.removeItemFromList.bind(this);
      this.gatherCartListItems();
      ToastAndroid.show("Swipe elements for managing!",ToastAndroid.LONG);
    }
    gatherCartListItems()
    {
        if(firebase.auth().currentUser)
        {
            firebase.database().ref("Cart").once("value",(snap)=>{
                var vals=Object.values(snap.toJSON());
                var keys=Object.keys(snap.toJSON());
                i=0;
                             
                vals.forEach(element => {
                    currentKey=keys[i];
                    if(element.EMail == firebase.auth().currentUser.email)
                    {
                        this.setState({productsID:element.Products.split(",")});
                    }
                    console.log();
                    i++;    
                });
            

            });
            
            firebase.database().ref("Products").once("value",(snap)=>{
                var values=Object.values(snap.toJSON());
                var products=[];   
                values.forEach(element=>{
                    var pIDs=this.state.productsID;
                    if(pIDs.includes((element.Id||element.ID)+""))
                    {
                        products.push(element);
                    }

                });
                console.log("render again pls!");
                this.setState({productArr:products});
            });
            
        }
    }
   async removeItemFromList()
    {
         //find user and loop over all products and update 
        await firebase.database().ref("Cart").once("value",(snap)=>{
            var vals=Object.values(snap.toJSON());
            var keys=Object.keys(snap.toJSON());
            j=0;
            vals.forEach((x)=>{
              currentKey=keys[j];
              if(firebase.auth().currentUser.email==x.EMail)
              {
                var newList="";
                var arr=x.Products.split(",");
                for(let i=0;i< arr.length;i++)
                {
                  if(arr[i]==(this.state.activeItem.ID||this.state.activeItem.Id)){
                   console.log("found done");
                    continue;
                  }
                  if(arr[i].trim()!=="")
                  newList+=arr[i]+",";
                }
                firebase.database().ref("Cart/"+currentKey).update({Products:newList});
              }
              j++;
            });
        });
        this.gatherCartListItems();
    }
  
    
    cartItem(products)
    {
        const ButtonsRight=[{
            text:"Delete",
            backgroundColor:"red", 
            underlayColor: '#ECE5DD',
            onPress:()=>{this.removeItemFromList()}
             }
         ];
         const ButtonsLeft=[
             {
             text:"View",
             backgroundColor:"#34B7F1", 
             underlayColor: '#ECE5DD',
             onPress:()=>{console.log("View pressed")}
             }
          ];
        
     return(
        products&&products.length>0? 
      
        
              products.map((p,i)=>
                <Swipeout key={i} autoClose={true} right={ButtonsRight} left={ButtonsLeft} onOpen={()=>this.setState({activeItem:p})} ><ListItem
              key={i}
              
              title={p.Name}
              
              leftAvatar={{source:{uri:p.Image}}}
              
              rightElement={<Text>{p.Price-(p.Price*p.Discount/100) + "$"}</Text>}
              rightSubtitle={<Text 
                  style={{
                    flex: 0.5,
                    fontSize: 12,
                    color: 'gray',
                    textAlign: 'center',
                    marginTop: 5,
                     textDecorationLine:'line-through'
                  }}
                >
                  {p.Discount>0?p.Price + "$":""}
                </Text>}
              bottomDivider
            /></Swipeout>)
            
        :<View style={{flex:1,justifyContent:"center",alignItems:"center"}}>
            <Text style={{color:"black"}}>Add something....</Text>
        </View>
          

     );
    }
/*<Card containerStyle={{padding:0}}>
          {
              
              products.map((p,i)=><ListItem
              key={i}
              
              title={p.Name}
              
              leftAvatar={{source:{uri:p.Image}}}
              
              rightElement={<Text>{p.Price-(p.Price*p.Discount/100) + "$"}</Text>}
              rightSubtitle={<Text 
                  style={{
                    flex: 0.5,
                    fontSize: 12,
                    color: 'gray',
                    textAlign: 'center',
                    marginTop: 5,
                     textDecorationLine:'line-through'
                  }}
                >
                  {p.Discount>0?p.Price + "$":""}
                </Text>}
              bottomDivider
            />)
  
  
          }        
          </Card> */
    render()
    {
        return(
            <ScrollView>{
            this.state.productArr?this.cartItem(this.state.productArr):null
            }
            </ScrollView>
        );
    }
}