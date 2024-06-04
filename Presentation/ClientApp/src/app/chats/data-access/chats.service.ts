import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { ChatThumbnail } from '../models/chat';
import { Message, MessageSendRequest } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class ChatsService {

  constructor(private http: HttpService) { }

  getChats() {
    return this.http.get<ChatThumbnail[]>('/chats');
  }

  sendMessage(messageToSend: MessageSendRequest) {
    return this.http.post(`/chats/messages`, messageToSend);
  }

    getMessages(chatId: string) {
        return this.http.get<Message[]>(`/chats/${chatId}/messages`);
    }
}
