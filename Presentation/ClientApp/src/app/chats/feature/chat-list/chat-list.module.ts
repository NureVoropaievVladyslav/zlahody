import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatListComponent } from './chat-list.component';
import { ChatListRoutingModule } from './chat-list-routing.module';
import { FormsModule } from '@angular/forms';
import { ChatThumbnailModule } from '../../ui/chat-thumbnail/chat-thumbnail.module';



@NgModule({
  declarations: [ChatListComponent],
    imports: [
        CommonModule,
        ChatListRoutingModule,
        FormsModule,
        ChatThumbnailModule,
    ]
})
export class ChatListModule { }
