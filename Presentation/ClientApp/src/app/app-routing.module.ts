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
    {
        path: 'organizations',
        loadChildren: () =>
            import('./organizations/feature/organization-shell/organization-shell.module').then(
                (m) => m.OrganizationShellModule
            ),
    },
    {
        path: 'volunteer',
        loadChildren: () =>
            import('./volunteer/feature/volunteer-shell/volunteer-shell.module').then(
                (m) => m.VolunteerShellModule
            ),
    },
    {
        path: 'home',
        loadChildren: () =>
            import('./home/feature/home-landing/home-landing.module').then(
                (m) => m.HomeLandingModule
            ),
    },
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: '**',
        redirectTo: 'home',
        pathMatch: 'full',
    },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
