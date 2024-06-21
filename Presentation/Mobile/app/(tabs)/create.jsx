import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView, TouchableOpacity, Alert } from 'react-native';
import FormField from '../../components/FormField';
import React, { useState } from 'react';
import { Picker } from '@react-native-picker/picker';
import { router } from 'expo-router';
import { useFocusEffect } from '@react-navigation/native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { ApiUrl } from '../../constants/Api';

export default function CreateScreen() {
    const [form, setForm] = useState({
        content: "",
        requestType: 0,
    });

    const submit = async () => {
        if (form.content.length < 10) {
            Alert.alert("Error", "Content must be at least 10 characters long");
            return;
        }

        const token = await AsyncStorage.getItem('token');
        if (!token) {
            Alert.alert("Error", "Your session has expired. Please log in again");
            router.replace('(auth)/login');
        }

        const response = await fetch(ApiUrl + 'requests', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(form)
        });

        if (response.ok) {
            Alert.alert("Success", "Request sent successfully");
            router.replace('(tabs)/requests');
        } else if (response.status === 401) {
            Alert.alert("Error", "Your session has expired. Please log in again");
            router.replace('(auth)/login');
        } else {
            Alert.alert("Error", "An error occurred. Please try again later");
        }
    }

    useFocusEffect(
        React.useCallback(() => {
            setForm({ content: "", requestType: 0 })
        }, [])
    );

    return (
        <SafeAreaView className="h-full bg-background p-4">
            <ScrollView>
                <Text className="text-3xl text-center font-gbold mb-10">Send a request</Text>
                <FormField
                    title="Content"
                    value={form.content}
                    placeholder="Describe your request"
                    handleChangeText={(e) => setForm({ ...form, content: e })}
                    multiline
                />

                <Text className="text-xl font-gbold mt-10">Request type</Text>
                <Picker
                    className="bg-black"
                    selectedValue={form.requestType}
                    onValueChange={(itemValue, itemIndex) =>
                        setForm({ ...form, requestType: parseInt(itemValue) })
                    }>
                    <Picker.Item label="Food" value={0} />
                    <Picker.Item label="Accomodation" value={1} />
                    <Picker.Item label="Psychological Support" value={2} />
                    <Picker.Item label="Information Request" value={3} />
                    <Picker.Item label="Complaint" value={4} />
                    <Picker.Item label="Feedback" value={5} />
                </Picker>

                <View className="w-full flex justify-center mt-2">
                    <TouchableOpacity className="w-full bg-primary rounded-lg flex flex-row justify-center items-center p-2" onPress={submit}>
                        <Text className="text-center text-xl mr-2 text-white font-pregular"> Send </Text>
                    </TouchableOpacity>
                </View>

                <View className="w-full flex justify-center mt-2">
                    <TouchableOpacity className="w-full border-secondary border-2 rounded-lg flex flex-row justify-center items-center p-2" onPress={() => router.replace('(tabs)/requests')}>
                        <Text className="text-center text-xl mr-2 text-text font-pregular"> Get back </Text>
                    </TouchableOpacity>
                </View>
            </ScrollView>
        </SafeAreaView>
    )
}