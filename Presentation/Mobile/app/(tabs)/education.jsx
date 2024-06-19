import React from 'react';
import { Text, ScrollView, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

export default function EducationScreen() {
    return (
        <SafeAreaView className="h-screen bg-background">
            <ScrollView>
                <View className="w-full flex justify-center h-full px-4 my-6">
                    <Text className="font-gbold text-3xl text-center mb-10">
                        Education
                    </Text>
                </View>
            </ScrollView>
        </SafeAreaView>
    );
}