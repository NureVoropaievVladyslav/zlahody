import React from 'react';
import { Text, ScrollView, TouchableOpacity } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { router } from 'expo-router';

export default function ProfileScreen() {
    const logout = async () => {
        await AsyncStorage.removeItem('token');
        router.replace('(auth)/login');
    }

    return (
        <SafeAreaView className="h-screen bg-background">
            <ScrollView>
                <TouchableOpacity className="w-full flex flex-row justify-between h-full px-4 my-6 font-gbold text-xl"
                                  onPress={logout}>
                    <Text className="font-pregular text-xl">
                        Log Out
                    </Text>
                </TouchableOpacity>
            </ScrollView>
        </SafeAreaView>
    );
}