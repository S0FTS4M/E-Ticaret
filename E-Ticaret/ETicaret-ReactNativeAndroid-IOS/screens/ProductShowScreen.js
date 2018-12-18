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

const SCREEN_WIDTH = Dimensions.get('window').width;
const SCREEN_HEIGHT = Dimensions.get('window').height;
const IMAGE_SIZE = SCREEN_WIDTH - 80;
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
 
        }
        this.getProduct=this.getProduct.bind(this);
        this.getComments=this.getComments.bind(this);
        this.makeComment=this.makeComment.bind(this);
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
          
         this.setState({commentMade:!this.state.commentMade});
        }
        else
        {
          Alert.alert(
            'Sign In',
            'If you want to make comments you need to sign in',
            [
              
              {text: 'OK', style: 'cancel'},
             
            ],
            { cancelable: false }
          )
        }
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
              </View>
              
              <View style={{flex:1,justifyContent:"center",alignItems:"center"}}>
                
              <Text style={{fontSize:18,fontWeight:"bold"}}>COMMENTS</Text>
              <View style={{flex:1,flexDirection:"row"}}>
              <TextBox onChangeText={comment=>this.setState({comment})} ref={input=>this.CommentInput=input} placeholder="Make Comment" textboxStyle={{flex:1,width:SCREEN_WIDTH - 100,borderWidth:2,borderRadius:8,borderColor:"#0084ff" ,marginHorizontal:5}}/>
              
              <Button clear buttonStyle={{backgroundColor:"#0084ff",flex:1,borderWidth:0,width:50,height:50,borderRadius:25,borderColor:"transparent" }} icon={{name:"send",color:"#0084ff"}} title="" onPress={()=>{this.makeComment();this.CommentInput.clear()}}></Button>
              </View>
              </View>
              <ScrollView nestedScrollEnabled={true}  style={{height:400}}>
                  
                  {comments.map((item)=><Comment key={item.key} likes={item.val.LikeCount} OnLikePress={()=>{this.commentLiked(item.key);console.log(item.key)}} commentText={item.val.CommentText} userName={item.val.CustomerUserName}/>)}
                 
              </ScrollView>

            </ScrollView>



        );
    }

}