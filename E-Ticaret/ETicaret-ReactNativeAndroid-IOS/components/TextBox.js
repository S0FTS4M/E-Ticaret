import React,{Component} from 'react'
import {TextInput,View,StyleSheet,TouchableOpacity,Text} from 'react-native'

export default class TextBox extends Component{

    clear()
    {
        this.InputText.clear();
    }
    render(){
        return(
           <View style={styles.container}>
          <TextInput returnKeyType="next" ref={input=>this.InputText=input} value={this.props.text} keyboardType={this.props.keyboardType} secureTextEntry={this.props.secureTextEntry} testID={this.props.testID} placeholder={this.props.placeholder} style={StyleSheet.flatten([ styles.TextBoxStyle,this.props.textboxStyle])} onChangeText={this.props.onChangeText} ></TextInput>
          </View>

        );
    
}
}
const styles=StyleSheet.create({
    container:{
        backgroundColor: "#fff",
        
    },
    TextBoxStyle:{
        backgroundColor: "#fff",
        color:"black",
        borderBottomWidth: 3,
        
        margin:3,
        marginHorizontal:50,
        borderRadius:0,
        borderColor:"#C0C0C0",
        padding:8,
        
    },


});
