import React,{ Component } from "react";

import { View,Image,StyleSheet,TouchableHighlight } from "react-native";
import { Text,Avatar,Badge } from "react-native-elements";
import  Icon from "react-native-vector-icons/Ionicons";
export class Comment extends Component
{
    constructor(props)
    {
        super(props);
    }
    render(){
        return(

            <View style={styles.mainContainer}>
                <View>   
                <Avatar
                    small
                    rounded
                    ImageComponent={()=>null}
                    //https://s3.amazonaws.com/uifaces/faces/twitter/adhamdannaway/128.jpg
                    source={{uri: "https://api.adorable.io/avatars/120/abott@adorable.png"}}
                    activeOpacity={0.7}
                    />
                 
                <Badge containerStyle={{ backgroundColor: 'white',padding:5,flex:1,flexDirection:"row"}}>
                <Icon name="ios-heart" color="red"/>
                <Text style={{marginHorizontal:3}}>{this.props.likes}</Text>
                </Badge>
                </View>
                <View style={styles.subContainer}>
                <View style={styles.textContainer}>
                <Text style={styles.userName}>{this.props.userName}</Text>
                <Text style={{color:"#666"}}>{this.props.commentText}</Text>
                </View>
                <View style={styles.commentInteractionContainer}>
                    <TouchableHighlight style={styles.commentInteractionButtons} onPress={this.props.OnLikePress}>
                        <Text style={styles.buttonText}>Like</Text>
                    </TouchableHighlight>
                    <TouchableHighlight style={StyleSheet.flatten({marginRight:20},styles.commentInteractionButtons)}>
                        <Text style={styles.buttonText}>Report</Text>
                    </TouchableHighlight>
                </View>
                </View>
             
            </View>


        );


    }
}
const styles=StyleSheet.create(
{
mainContainer:{
    flex:1,
    flexDirection:"row",
    margin:10,


},
subContainer:{
    flex:1,
    backgroundColor:"#fff",
    borderRadius:8,
    padding:8,
    
},
textContainer:{
    flex:1,
    backgroundColor:"#eee",
    borderRadius:8,
    padding:8,
},
userName:{
    fontWeight:"bold",
    fontSize:16,
    margin:5
},
commentInteractionContainer:{
    backgroundColor:"white",
    flex:1,
    flexDirection:"row",
    justifyContent:"flex-end",
    alignItems:"flex-end",
    padding:3
},
commentInteractionButtons:{
    marginRight:50
},
buttonText:{
    fontSize:16,
    color:"#aaa"

}
}
);