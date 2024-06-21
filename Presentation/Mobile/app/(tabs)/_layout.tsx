import { Tabs } from 'expo-router';
import { StatusBar } from 'expo-status-bar';
import { TabBarIcon } from '@/components/navigation/TabBarIcon';

export default function TabsLayout() {
    return (
        <>
            <Tabs
                screenOptions={{
                    tabBarActiveTintColor: '#fff',
                    headerShown: false,
                }}>
                <Tabs.Screen
                    name="resources"
                    options={{
                        title: 'Resources',
                        tabBarIcon: ({ color, focused }) => (
                            <TabBarIcon name='tent' color={color}/>
                        ),
                    }}
                />
                <Tabs.Screen
                    name="requests"
                    options={{
                        title: 'Requests',
                        tabBarIcon: ({ color, focused }) => (
                            <TabBarIcon name='heart-circle-plus' color={color} />
                        ),
                    }}
                />
                <Tabs.Screen
                    name="chats"
                    options={{
                        title: 'Chats',
                        tabBarIcon: ({ color, focused }) => (
                            <TabBarIcon name='rocketchat' color={color} />
                        ),
                    }}
                />
                <Tabs.Screen
                    name="education"
                    options={{
                        href: null
                    }}
                />
                <Tabs.Screen
                    name="profile"
                    options={{
                        title: 'Profile',
                        tabBarIcon: ({ color, focused }) => (
                            <TabBarIcon name="user" color={color} />
                        ),
                    }}
                />
                <Tabs.Screen
                    name="create"
                    options={{
                        href: null
                    }}
                />
                <Tabs.Screen
                    name="chats/[id]"
                    options={{
                        href: null
                    }}
                />
            </Tabs>
            <StatusBar style="dark" />
        </>
    );
}