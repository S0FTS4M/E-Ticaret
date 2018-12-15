import React,{ Component } from "react";
import {View,ScrollView,Dimensions,Image,Text} from 'react-native'
import { BallIndicator } from "react-native-indicators";
import firebase from '@firebase/app';
import '@firebase/database'




const SCREEN_WIDTH = Dimensions.get('window').width;

const IMAGE_SIZE = SCREEN_WIDTH - 80;
export class ProductShowScreen extends Component{
    constructor(props){
        super(props);
        this.state={
            productLoaded:false,
            productID:undefined,
            product:undefined
        }
        this.getProduct=this.getProduct.bind(this);
    }
    static navigationOptions = {
        title: "",
       
        headerStyle: {
          backgroundColor: '#f4511e',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold',
        },
      };
      getProduct(p)
      {
        if(p&&p.val()){
            var queryResult=Object.values(p.val());
            queryResult.forEach(element => {
                if(this.state.productID == (element.ID||element.Id))
                {
                    this.setState({product:element});
                }
              });
              this.setState({productLoaded:true});
      }
    }
      componentDidMount() {
        this.props.navigation.addListener('willFocus', (playload)=>{
          if(playload&&playload.action.params&&playload.action.params.productID){
            this.setState({productLoaded:false,productID:playload.action.params.productID});
        //call filler function here  
        
        firebase.database().ref("Products").once('value',this.getProduct);
        }
        });
      }
      
    render()
    {
        return(
            !this.state.productLoaded?
            <View style={{flex:1,justifyContent:"center",alignItems:"center"}}>
                <BallIndicator color="tomato"/>
            </View> :
            <ScrollView>
                  <View style={{ justifyContent: 'center', alignItems: 'center' }}>
                <Image
                  source={{
                    uri:
                      this.state.product.Image,
                  }}
                  style={{
                    width: IMAGE_SIZE,
                    height: IMAGE_SIZE,
                    borderRadius: 10,
                    resizeMode:"center"
                  }}
                />
              </View>
              <View
                style={{
                  flex: 1,
                  flexDirection: 'row',
                  marginTop: 20,
                  marginHorizontal: 40,
                  justifyContent: 'center',
                  alignItems: 'center',
                }}
              >
                <Text
                  style={{
                    flex: 1.2,
                    fontSize: 18,
                    color: 'black',
                    fontFamily: 'bold',
                  }}
                >
                  {this.state.product.Name}
                </Text>
                <Text 
                  style={{
                    flex: 0.5,
                    fontSize: 12,
                    color: 'gray',
                    textAlign: 'center',
                    marginTop: 5,
                     textDecorationLine:'line-through'
                  }}
                >
                  {this.state.product.Discount>0?this.state.product.Price + "$":""}
                </Text>
                
                <Text
                  style={{
                    flex:1,
                    fontSize: 26,
                    color: 'tomato',
                    textAlign: 'left',
                    marginTop: 5,
                    
                  }}
                  
                >
                  {this.state.product.Price-(this.state.product.Price*this.state.product.Discount/100) + "$"}
                </Text>
             
                <Text
                  style={{
                    flex: 1,
                    fontSize: 26,
                    color: 'green',
                    fontFamily: 'bold',
                    textAlign: 'right',
                  }}
                >
                  {-this.state.product.Discount + "%"}
                </Text>
              </View>
              <View
                style={{
                  flex: 1,
                  marginTop: 20,
                  width: SCREEN_WIDTH - 80,
                  marginLeft: 40,
                }}
              >
                <Text
                  style={{
                    flex: 1,
                    fontSize: 18,
                    color: 'black',
                    fontFamily: 'regular',
                    
                  }}
                >
                 Description : {this.state.product.Desc}
                </Text>
                <Text
                  style={{
                    flex: 1,
                    fontSize: 18,
                    color: 'black',
                    fontFamily: 'regular',
                    fontWeight:"bold",
                    marginTop:5
                  }}
                >
                 Amount:  {this.state.product.Amount}
                </Text>
                <Text
                  style={{
                    flex: 1,
                    fontSize: 18,
                    color: 'black',
                    fontFamily: 'regular',
                    fontWeight:"bold",
                    marginTop:5
                  }}
                >
                 Category:  {this.state.product.Category}
                </Text>
                <Text
                  style={{
                    flex: 1,
                    fontSize: 18,
                    color: 'black',
                    fontFamily: 'regular',
                    fontWeight:"bold",
                    marginTop:5
                  }}
                >
                 Type:  {this.state.product.SubCategory}
                </Text>
              </View>


            </ScrollView>



        );
    }

}