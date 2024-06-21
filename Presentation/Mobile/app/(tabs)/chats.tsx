import React, { useEffect } from 'react';
import { Text, ScrollView, View, TouchableOpacity } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { HubConnection } from '@microsoft/signalr';
import ChatHubService from '@/services/chat-hub.service';
import { Link } from 'expo-router';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { ApiUrl } from '@/constants/Api';
import { ChatThumbnailResponse } from '@/models/chat';
import { ExpoRouter } from '@/.expo/types/router';
import { router } from 'expo-router';
import { useFocusEffect } from '@react-navigation/native';

export default function ChatsScreen() {
    const [chats, setChats] = React.useState<ChatThumbnailResponse[]>([]);

    const getChats = async () => {
        const token = await AsyncStorage.getItem('token');
        const response = await fetch(ApiUrl + 'chats', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        const data: ChatThumbnailResponse[] = await response.json();
        setChats(data);
    }

    useFocusEffect(
        React.useCallback(() => {
            getChats().then();
            return () => {}; // optional cleanup function
        }, [])
    );

    return (
        <SafeAreaView className="h-screen bg-background p-4">
            <ScrollView>
                {
                    chats.map((chat, index) => {
                        return (
                            <View key={index} className="w-full border-primary border-2 rounded-lg p-2 mb-2">
                                <TouchableOpacity onPress={() => router.replace(`/chats/${chat.chatId}`)}>
                                    <Text className="text-lg font-pbold">{chat.contactName}</Text>
                                </TouchableOpacity>
                            </View>
                        )
                    })
                }
            </ScrollView>
        </SafeAreaView>
    );
}