import { ScrollView, View, Text, Alert } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Link } from 'expo-router';
import React, { useState } from 'react';
import ActionButton from '../../components/ActionButton';
import FormField from '../../components/FormField';

export default function RegisterScreen() {
    const [isSubmitting, setSubmitting] = useState(false);
    const [form, setForm] = useState({
        fullname: "",
        email: "",
        password: "",
    });

    const submit = () => {
        if (form.fullname === "" || form.email === "" || form.password === "") {
            Alert.alert("Error", "Please fill in all fields");
        } else {
            fetch('http://192.168.0.109:8080/api/users', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    fullname: form.fullname,
                    email: form.email,
                    password: form.password,
                }),
            })
                .then((response) => {
                    if (!response.ok) {
                        console.log(response.status);
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then((data) => {
                    console.log('Success:', data);
                })
                .catch((error) => {
                    Alert.alert("Error", "An error occurred. Please try again.");
                });
        }
    };

    return (
        <SafeAreaView className="h-screen bg-background">
            <ScrollView>
                <View className="w-full flex justify-center h-full px-4 my-6">
                    <Text className="font-gbold text-3xl text-center mb-10">
                        Welcome to Zlahody!
                    </Text>
                    <FormField
                        title="Fullname"
                        value={form.fullname}
                        placeholder="Enter your full name"
                        handleChangeText={(e) => setForm({ ...form, fullname: e })}
                        otherStyles="mb-5"
                        keyboardType="email-address"
                    />
                    <FormField
                        title="Email"
                        value={form.email}
                        placeholder="Enter your email"
                        handleChangeText={(e) => setForm({ ...form, email: e })}
                        otherStyles=""
                    />
                    <FormField
                        title="Password"
                        value={form.password}
                        placeholder="Enter your password"
                        handleChangeText={(e) => setForm({ ...form, password: e })}
                        otherStyles=""
                    />
                    <ActionButton
                        title="Sign Up"
                        handlePress={submit}
                        containerStyles="mt-7"
                        isLoading={isSubmitting}
                    />
                    <Link className="text-center"
                        href="/login">
                        Already have an account? <Text className="text-primary underline">Sign in.</Text>
                    </Link>
                </View>
            </ScrollView>
        </SafeAreaView>
    );
}