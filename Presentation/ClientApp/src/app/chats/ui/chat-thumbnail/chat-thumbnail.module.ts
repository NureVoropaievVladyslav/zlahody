import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatThumbnailComponent } from './chat-thumbnail.component';
import { RouterLink, RouterLinkActive } from '@angular/router';



@NgModule({
  declarations: [ChatThumbnailComponent],
  exports: [
    ChatThumbnailComponent
  ],
    imports: [
        CommonModule,
        RouterLink,
        RouterLinkActive
    ]
})
export class ChatThumbnailModule { }
