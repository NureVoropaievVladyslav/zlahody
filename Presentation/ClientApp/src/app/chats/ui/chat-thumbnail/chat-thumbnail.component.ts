import { Component, Input } from '@angular/core';
import { ChatThumbnail } from '../../models/chat';

@Component({
  selector: 'app-chat-thumbnail',
  templateUrl: './chat-thumbnail.component.html',
  styleUrl: './chat-thumbnail.component.sass'
})
export class ChatThumbnailComponent {

  @Input() chat!: ChatThumbnail;


}
