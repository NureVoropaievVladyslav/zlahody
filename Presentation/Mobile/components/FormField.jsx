import { TextInput, View, Text } from 'react-native';

export default function FormField ({
   title,
   value,
   placeholder,
   handleChangeText,
   otherStyles,
   ...props
}) {
    return (
        <View className={`space-y-2 ${otherStyles}`}>
            <Text className="text-xl font-gbold">{title}</Text>

            <View className="w-full h-16 px-4 bg-black-100 rounded-2xl border-2 border-black-200 focus:border-primary flex flex-row items-center">
                <TextInput
                    className="flex-1 text-text font-psemibold text-base"
                    value={value}
                    placeholder={placeholder}
                    placeholderTextColor="#7B7B8B"
                    onChangeText={handleChangeText}
                    secureTextEntry={title === "Password"}
                    {...props}
                />
            </View>
        </View>
    )
}