import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatListComponent } from './chat-list.component';

const routes: Routes = [
  {
    path: '',
    component: ChatListComponent,
    children: [
      {
        path: ':id',
        loadChildren: () =>
            import('../chat-window/chat-window.module').then(
                (m) => m.ChatWindowModule
            ),
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ChatListRoutingModule { }
