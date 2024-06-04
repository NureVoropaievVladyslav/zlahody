import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ChatsHubService {
  private hubConnection: HubConnection;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.chatHubUrl)
      .build();
  }

  connect(): Promise<void> {
    return this.hubConnection.start();
  }

  joinChatRoom(chatId: string): Promise<void> {
    return this.hubConnection.invoke('JoinChatRoom', chatId);
  }

  sendMessage(chatId: string, content: string): Promise<void> {
    return this.hubConnection.invoke('SendMessage', chatId, content);
  }

  onMessageReceived(callback: (message: string) => void): void {
    this.hubConnection.on('ReceiveMessage', callback);
  }
}
