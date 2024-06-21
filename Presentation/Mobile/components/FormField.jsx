import { Text, TextInput, View } from 'react-native';

export default function FormField ({
                                       title,
                                       value,
                                       placeholder,
                                       handleChangeText,
                                       handleSubmit,
                                       otherStyles,
                                       multiline,
                                       ...props
                                   }) {
    return (
        <View className={`space-y-2 ${otherStyles}`}>
            {
                title === "" ? null : (
                    <Text className="text-xl font-gbold">{title}</Text>
                )
            }

            <View className="w-full px-4 bg-black-100 rounded-2xl border-2 border-black-200 focus:border-primary flex flex-row items-center">
                <TextInput
                    className="flex-1 text-text font-psemibold text-base py-4"
                    value={value}
                    placeholder={placeholder}
                    placeholderTextColor="#7B7B8B"
                    onChangeText={handleChangeText}
                    onSubmitEditing={handleSubmit}
                    blurOnSubmit={false}
                    secureTextEntry={title === "Password"}
                    multiline={multiline}
                    onKeyPress={({ nativeEvent }) => {
                        if (nativeEvent.key === 'Enter') {
                            handleSubmit();
                        }
                    }}
                    {...props}
                />
            </View>
        </View>
    )
}