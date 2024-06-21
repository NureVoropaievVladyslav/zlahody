import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { HubUrl } from '@/constants/Api';

class ChatHubService {
    private hubConnection: HubConnection;
    constructor() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(HubUrl) // replace with your chat hub URL
            .build();
    }

    connect = () => {
        if (this.hubConnection.state === 'Connected') {
            return Promise.resolve();
        }

        return this.hubConnection.start();
    }

    joinChatRoom = (chatId: string) => {
        return this.hubConnection.invoke('JoinChatRoom', chatId);
    }

    sendMessage = (chatId: string, content: string) => {
        return this.hubConnection.invoke('SendMessage', chatId, content);
    }

    onMessageReceived = (callback: (message: string) => void) => {
        this.hubConnection.on('ReceiveMessage', callback);
    }
}

export default new ChatHubService();