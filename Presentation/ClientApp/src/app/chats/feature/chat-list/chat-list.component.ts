import { Component } from '@angular/core';
import { ChatsService } from '../../data-access/chats.service';
import { ChatThumbnail } from '../../models/chat';

@Component({
    selector: 'app-chat-list',
    templateUrl: './chat-list.component.html',
    styleUrls: ['./chat-list.component.sass']
})
export class ChatListComponent {

    chats: ChatThumbnail[] = [];

    constructor(private chatsService: ChatsService) {
        //chatsHubService.connect().then();
    }

    ngOnInit(): void {
        this.chatsService.getChats().subscribe(chats => {
            this.chats = chats;
        });
    }

    joinChatRoom(): void {

    }


}