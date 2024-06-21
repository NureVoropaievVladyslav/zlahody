import { SafeAreaView } from 'react-native-safe-area-context';
import React, { useRef } from 'react';
import { ScrollView, Text, View } from 'react-native';
import { useLocalSearchParams } from 'expo-router';
import { useFocusEffect } from '@react-navigation/native';
import { ApiUrl, HubUrl } from '@/constants/Api';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { MessageResponse } from '@/models/chat';
import FormField from '@/components/FormField';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export default function ChatScreen() {

    // Route fields
    const { id } = useLocalSearchParams();

    // State fields
    const [email, setEmail] = React.useState("");
    const [message, setMessage] = React.useState("");
    const [messages, setMessages] = React.useState<MessageResponse[]>([]);
    const [hubConnection, setHubConnection] = React.useState<HubConnection>();

    // DOM Reference fields
    const scrollView = useRef<ScrollView>(null);

    const getMessages = async () => {
        const token = await AsyncStorage.getItem('token');
        const response = await fetch(ApiUrl + 'chats/' + id + '/messages', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        const data: MessageResponse[] = await response.json();
        const messagesWithDateObjects = data.map(message => ({
            ...message,
            createdAt: new Date(message.createdAt)
        }));
        setMessages(messagesWithDateObjects);
    }

    const sendMessage = async (content: string) => {
        if (!content) return;
        const newMessage: MessageResponse = {
            content: content,
            senderEmail: email,
            senderName: messages.find(message => message.senderEmail === email)?.senderName || "",
            createdAt: new Date()
        }
        setMessages([...messages, newMessage]);

        await hubConnection?.invoke('SendMessage', id, content);
        const requestBody = {
            chatId: id,
            content: content
        };
        await fetch(ApiUrl + 'chats/messages', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${await AsyncStorage.getItem('token')}`
            },
            body: JSON.stringify(requestBody)
        }).then(() => {

        });
    }

    // Lifecycle hooks
    React.useEffect(() => {
        const createHubConnection = async () => {
            const hubConnection = new HubConnectionBuilder()
                .withUrl(HubUrl)
                .withAutomaticReconnect()
                .build();

            try {
                await hubConnection.start();
                console.log('Connection started');
            } catch (err) {
                console.error(err);
            }

            setHubConnection(hubConnection);
        }

        createHubConnection().then(() => {
            hubConnection?.on('ReceiveMessage', (message: string) => {
                console.log(message);

                const newMessage: MessageResponse = {
                    content: message,
                    senderEmail: email,
                    senderName: messages.find(message => message.senderEmail === email)?.senderName || "",
                    createdAt: new Date()
                }

                console.log(newMessage);
            });

            hubConnection?.invoke('JoinChatRoom', id).then();
        });

        const fetchEmail = async () => {
            const storedEmail = await AsyncStorage.getItem('email') as string;
            setEmail(storedEmail);
        };

        fetchEmail().then();

        return () => {
            hubConnection?.stop().then();
        }
    }, []);

    useFocusEffect(
        React.useCallback(() => {

            getMessages().then(() => {

            });

            return () => {};
        }, [])
    );

    return (
        <SafeAreaView className="h-screen bg-background p-4">
            <ScrollView className="mb-10"
                        automaticallyAdjustKeyboardInsets={true}
                        ref={scrollView}
                        onContentSizeChange={() => scrollView.current?.scrollToEnd({animated: true})}>
                {
                    messages.map((message, index) => {
                        return (
                            <View key={index} className={`rounded-lg flex mb-2 p-2 ${message.senderEmail === email ? 'bg-primary' : 'bg-secondary'}`}>
                                <Text className="text-sm font-pbold">{message.senderName}</Text>
                                <Text className="text-sm font-pregular">{message.content}</Text>
                                <Text className="text-xs self-end font-pregular">{message.createdAt.toTimeString().substring(0,5)}</Text>
                            </View>
                        )
                    })
                }

                <FormField
                    key={messages.length}
                    title=""
                    value={message}
                    placeholder="Type a message"
                    handleChangeText={(e: string) => setMessage(e)}
                    handleSubmit={() => {
                        sendMessage(message).then();
                        setMessage("");
                    }}
                    otherStyles="mb-10"
                    multiline={true}
                />
            </ScrollView>
        </SafeAreaView>
    )
}