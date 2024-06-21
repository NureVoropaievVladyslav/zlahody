import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChatsHubService } from '../../data-access/chats-hub.service';
import { ChatsService } from '../../data-access/chats.service';
import { Message, MessageSendRequest } from '../../models/message';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrl: './chat-window.component.sass',
})
export class ChatWindowComponent {
  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  private chatId: string = '';

  messages: Message[] = [];

  message: string = '';

  constructor(
    private chatsHubService: ChatsHubService,
    private activatedRoute: ActivatedRoute,
    private chatService: ChatsService
  ) {}

  ngOnInit() {
    this.chatsHubService.connect().then(() => {
      this.chatsHubService.onMessageReceived((message: string) => {
        this.messages.push({
          content: message,
          senderEmail: '',
          createdAt: new Date(),
        });
      });

      this.activatedRoute.params.subscribe((params) => {
        const id = params['id'];
        if (id) {
          this.chatId = id;
          this.chatsHubService.joinChatRoom(this.chatId).then();
          this.chatService.getMessages(this.chatId).subscribe((messages) => {
            this.messages = messages;
          });
        }
      });
    });
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.messageContainer.nativeElement.scrollTop =
        this.messageContainer.nativeElement.scrollHeight;
    } catch (err) {}
  }

  sendMessage(): void {
    if (this.message) {
      const messageToSend: MessageSendRequest = {
        chatId: this.chatId,
        content: this.message,
      };

      this.chatService.sendMessage(messageToSend).subscribe(() => {
        this.chatsHubService
          .sendMessage(messageToSend.chatId, messageToSend.content)
          .then(() => {
            this.messages.push({
              content: messageToSend.content,
              senderEmail: localStorage.getItem('email')!,
              createdAt: new Date(),
            });
          });
        this.message = '';
      });
    }
  }

  protected readonly localStorage = localStorage;
}
