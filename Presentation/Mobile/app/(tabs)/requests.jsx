import React from 'react';
import { ScrollView, Text, TouchableOpacity, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { FontAwesome6 } from '@expo/vector-icons';
import { router } from 'expo-router';
import { RequestType } from '../../constants/RequestType';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useFocusEffect } from '@react-navigation/native';

export default function RequestsScreen() {
    const [requests, setRequests] = React.useState([]);
    const getRequests = async () => {
        const token = await AsyncStorage.getItem('token');
        const response = await fetch('http://192.168.0.109:8080/api/requests/sent', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        const data = await response.json();
        setRequests(data);
    }

    useFocusEffect(
        React.useCallback(() => {
            getRequests().then();
            return () => {}; // optional cleanup function
        }, [])
    );

    return (
        <SafeAreaView className="h-screen bg-background p-4">
            <ScrollView className="mb-2">
                {
                    requests.map((request, index) => {
                        return (
                            <View key={index} className="w-full flex flex-row justify-between items-center border-b border-gray-300 p-2">
                                <View>
                                    <Text className="text-xs text-primary font-pbold">{RequestType[request.requestType]}</Text>
                                    <Text className="text-lg font-pbold">{request.content}</Text>
                                    <Text className="text-sm font-pregular">Status: {request.isApproved ? 'Approved' : 'Pending'}</Text>
                                </View>
                            </View>
                        )
                    })
                }
            </ScrollView>

            <View className="w-full flex justify-center mb-10">
                <TouchableOpacity className="w-full bg-primary rounded-lg flex flex-row justify-center items-center p-2" onPress={() => router.replace('/create')}>
                    <Text className="text-center text-xl mr-2 text-white font-pregular"> Create </Text>
                    <FontAwesome6 name="circle-plus" size={24} color="#fff" />
                </TouchableOpacity>
            </View>
        </SafeAreaView>
    );
}