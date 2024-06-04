import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: 'chats',
        loadChildren: () =>
            import('./chats/feature/chat-shell/chat-shell.module').then(
                (m) => m.ChatShellModule
            ),
    },
    {
        path: 'auth',
        loadChildren: () =>
            import('./auth/feature/auth-shell/auth-shell.module').then(
                (m) => m.AuthShellModule
            ),
    },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
