import React, { useState } from 'react';
import { Alert, ScrollView, Text, TouchableOpacity, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import FormField from '@/components/FormField';
import ActionButton from '@/components/ActionButton';
import { router } from 'expo-router';
import AsyncStorage from '@react-native-async-storage/async-storage';


export default function LoginScreen() {

    const [isSubmitting, setSubmitting] = useState(false);
    const [form, setForm] = useState({
        email: "",
        password: "",
    });

    const submit = async () => {
        if (form.email === "" || form.password === "") {
            Alert.alert("Error", "Please fill in all fields");
        } else {
            fetch('https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyDtcD1shRD160neQ13zTJCwtGWHzZ9PAtg', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: form.email,
                    password: form.password,
                    returnSecureToken: true
                }),
            })
                .then((response) => response.json())
                .then(async (data) => {
                    await AsyncStorage.setItem('token', data.idToken);
                    await AsyncStorage.setItem('email', data.email);
                    router.replace('(tabs)/resources');
                })
                .catch((error) => {
                    Alert.alert("Error", "Invalid email or password");
                });
        }
    };

    return (
        <SafeAreaView className="h-screen bg-background">
            <ScrollView>
                <View className="w-full flex justify-center h-full px-4 my-6">
                    <Text className="font-gbold text-3xl text-center mb-10">
                        Welcome back!
                    </Text>
                    <FormField
                        title="Email"
                        value={form.email}
                        placeholder="Enter your email"
                        handleChangeText={(e) => setForm({ ...form, email: e })}
                        otherStyles="mb-5"
                        keyboardType="email-address"
                    />
                    <FormField
                        title="Password"
                        value={form.password}
                        placeholder="Enter your password"
                        handleChangeText={(e) => setForm({ ...form, password: e })}
                        otherStyles=""
                    />
                    <ActionButton
                        title="Sign In"
                        handlePress={submit}
                        containerStyles="mt-7"
                        isLoading={isSubmitting}
                    />
                    <TouchableOpacity onPress={() => router.replace('/register')}>
                        <Text className="text-center mt-4">Don't have an account? <Text className="text-primary underline">Register</Text></Text>
                    </TouchableOpacity>
                </View>
            </ScrollView>
        </SafeAreaView>
    )
}