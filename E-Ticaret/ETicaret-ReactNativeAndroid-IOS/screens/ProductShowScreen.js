import React,{ Component } from "react";
import {View,ScrollView,Dimensions,Image,Text,Alert,ToastAndroid} from 'react-native'
import { BallIndicator } from "react-native-indicators";
import firebase from '@firebase/app';
import '@firebase/database'
import '@firebase/auth'
import { Comment } from "../components/Comment";
import "react-native-nested-scroll-view";
import TextBox from "../components/TextBox";
import { Button } from "react-native-elements";
import Icon from 'react-native-vector-icons/FontAwesome'


const SCREEN_WIDTH = Dimensions.get('window').width;
const SCREEN_HEIGHT = Dimensions.get('window').height;
const IMAGE_SIZE = 150;
var comments=[];
export class ProductShowScreen extends Component{
    constructor(props){
        super(props);
        this.state={
            productLoaded:false,
            productID:undefined,
            product:undefined,
            comment:"",
            commentMade:false,
            productIsInCart:false,
 
        }
        this.getProduct=this.getProduct.bind(this);
        this.getComments=this.getComments.bind(this);
        this.makeComment=this.makeComment.bind(this);
        this.addProductToCart=this.addProductToCart.bind(this);
        this.removeProductFromCart=this.removeProductFromCart.bind(this);
        if(firebase.auth().currentUser)
        {
          firebase.database().ref("Cart").once("value",(snap)=>{
            var vals=Object.values(snap.toJSON());
            vals.forEach((element)=>{
             if(element.EMail == firebase.auth().currentUser.email)
             {
               var arr=element.Products.split(",");
               if(arr.includes(this.state.productID+""))
               {
                 this.setState({productIsInCart:true});
                
               }
             }
           
              
            });


          });
        }
    }
    static navigationOptions = {
        title: "",
        tabBarVisible:false,
        androidStatusBarColor:'#fff',
        headerStyle: {
          backgroundColor: 'tomato',
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold',
        },
      };
      addProductToCart()
      {
        if(!firebase.auth().currentUser)
        {
            this.alertMessage('You need to sign in before doing it!');
        }
        else{
          firebase.database().ref("Cart").once('value',(snap)=>{
            obj = snap.toJSON();
            currentUser=firebase.auth().currentUser.email;
            if(obj){
            keys=Object.keys(obj);
            vals=Object.values(obj);
            //console.log(keys);
            //console.log(vals);
            i=0;
            
           let userFound=false;
            vals.forEach((v)=>{
              //console.log(i);
              if(v.EMail==currentUser)
              {
                userFound=true;
                console.log("user found:" + currentUser);
                products="";
                currentKey=keys[i];
                firebase.database().ref("Cart/"+currentKey).once("value",(s)=>{
                  value=Object.values(s.val());
                  //console.log(value[1]);
                
                  if(value){
                    products=value[1];
                    
                    console.log("value found for update");
                    products=products +"" + this.state.productID + ",";
                    //console.log(currentKey);
                    firebase.database().ref("Cart/"+currentKey).update({Products:products});
                
                    console.log(userFound);
                    //console.log("value found and its ok");
                    }
                    else{
                      console.log("value is undefined:"+value);
                    }
                
                
                });
               // console.log(products);
              
              }
              i++;
            });
            if(userFound==false)
            {
              //console.log("user found is false ?");
              products="";
              // console.log("nothing found so create another one");
              products+=this.state.productID + ",";
              firebase.database().ref("Cart").push({EMail:currentUser,Products:products});
            }
          }
          else{
            products="";
            products+=this.state.productID + ",";
            firebase.database().ref("Cart").push({EMail:currentUser,Products:products});
          }
            
          });
          this.setState({productIsInCart:true});
        }
        
      }
      removeProductFromCart()
      {
        //find user and loop over all products and update 
        firebase.database().ref("Cart").once("value",(snap)=>{
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
                if(arr[i]==this.state.productID){
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
        this.setState({productIsInCart:false});
      }
      getProduct(p)
      {
        productID=this.state.productID;
        if(p&&p.val()){
            var queryResult=Object.values(p.val());
            queryResult.forEach(element => {
                if(productID == (element.ID||element.Id))
                {
                    this.setState({product:element});
                   
                }
              });
             // this.setState({productLoaded:true});
      }
      firebase.database().ref("Comments").once('value',this.getComments);
    }
    getComments(c)
    {
      comments=[];
      if(c&&c.val()){
       // console.log(c);
        var queryResult=Object.values(c.val());
        var keys=Object.keys(c.val());
  
        
        i=0;
      
        queryResult.forEach(element => {
            if((this.state.product.ID||this.state.product.Id) == (element.ProductId))
            {
               comments.push({key:keys[i],val:element});
              
            }
            i++;
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
      makeComment()
      {
        if(firebase.auth().currentUser)
        {
          firebase.database().ref("Comments").push(
            {
              CommentId:"",
              CommentText:this.state.comment,
              CustomerUserName:firebase.auth().currentUser.email,
              LikeCount:0,
              DislikeCount:0,
              ProductId:this.state.productID
            }
          );
          firebase.database().ref("Comments").once('value',this.getComments);
          this.CommentInput.clear();
         this.setState({commentMade:!this.state.commentMade});
        }
        else
        {
          this.alertMessage('If you want to make comments you need to sign in');
        }
      }
      alertMessage(message)
      {
        Alert.alert(
          'Sign In',
          message,
          [
            
            {text: 'OK', style: 'cancel'},
           
          ],
          { cancelable: false }
        )
      }
      commentLiked(id)
      {
       
        firebase.database().ref("Comments/"+id).once('value',(snap)=>{commentValues=Object.values(snap.val()); console.log(commentValues[4]);likeCount=commentValues[4]; likeCount++;
          console.log(id);
          firebase.database().ref("Comments/"+id).update({LikeCount:likeCount});
          firebase.database().ref("Comments").once('value',this.getComments);
        });
      
        ToastAndroid.show("You liked comment!",ToastAndroid.SHORT);

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
                <Button
                  title={ this.state.productIsInCart==false?"Add to Cart":"Remove"}
                  titleStyle={{ fontWeight: 'bold', fontSize: 18 }}
                  buttonStyle={{
                    backgroundColor:this.state.productIsInCart?"#B83B62":"#5A66D1",
                    borderWidth: 0,
                    borderColor: 'transparent',
                    borderRadius: 0,
                  }}
                  containerStyle={{ marginVertical: 10, height: 60, width: 200, }}
                  icon={this.state.productIsInCart==false?<Icon name="arrow-right" size={15} color="white" />:<Icon name="minus" size={15} color="white" />}
                  iconRight
                  iconContainerStyle={{ marginLeft: 15 }}
                  onPress={this.state.productIsInCart==false?this.addProductToCart:this.removeProductFromCart}
                  ref={button=>this.AddtoCartButton=button}
            />
              </View>
              
              <View style={{flex:1,justifyContent:"center",alignItems:"center"}}>
                
              <Text style={{fontSize:18,fontWeight:"bold"}}>COMMENTS</Text>
              <View style={{flex:1,flexDirection:"row"}}>
              <TextBox onChangeText={comment=>this.setState({comment})} ref={input=>this.CommentInput=input} placeholder="Make Comment" textboxStyle={{flex:1,width:SCREEN_WIDTH - 100,borderWidth:2,borderRadius:8,borderColor:"#0084ff" ,marginHorizontal:5}}/>
              
              <Button clear buttonStyle={
                {backgroundColor:"#0084ff",flex:1,borderWidth:0,width:50,height:50,borderRadius:25,borderColor:"transparent" }}
               icon={{name:"send",color:"#0084ff"}} title="" onPress={()=>{this.makeComment();}}></Button>
              </View>
              </View>
              <ScrollView nestedScrollEnabled={true}  style={{height:400}}>
                  
                  {comments.map((item)=><Comment key={item.key} likes={item.val.LikeCount} OnLikePress={()=>{this.commentLiked(item.key);console.log(item.key)}} commentText={item.val.CommentText} userName={item.val.CustomerUserName}/>)}
                 
              </ScrollView>

            </ScrollView>



        );
    }

}