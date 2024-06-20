import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatWindowComponent } from './chat-window.component';
import { ChatWindowRoutingModule } from './chat-window-routing.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [ChatWindowComponent],
    imports: [
        CommonModule,
        ChatWindowRoutingModule,
        FormsModule
    ],
  exports: [ChatWindowComponent]
})
export class ChatWindowModule { }
