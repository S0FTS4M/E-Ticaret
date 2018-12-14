import React,{Component} from 'react';
import { View,Text,TouchableOpacity,StyleSheet } from "react-native";

export default class MyButton extends Component{

    render(){
        return (
            <View style={StyleSheet.flatten([ styles.container,this.props.style])}>
                <TouchableOpacity onPress={this.props.onPress} style={StyleSheet.flatten([ styles.buttonStyle,this.props.style])}>
                    <Text style={StyleSheet.flatten([ styles.textStyle,this.props.textStyle])}>
                        {this.props.text}
                    </Text>
                </TouchableOpacity>
            </View>

        );
    }
}
const styles=StyleSheet.create({
    container:{
        width:150,
        height:50,
        backgroundColor:'tomato',
        justifyContent:'center',
        alignItems:'center',
        borderRadius:10,
    },
    buttonStyle:{
        width:150,
        height:50,
        backgroundColor:'tomato',
        justifyContent:'center',
        alignItems:'center',
        borderRadius:10,
    },
    textStyle:{
        color:'white',
        fontSize:18,
    },


});