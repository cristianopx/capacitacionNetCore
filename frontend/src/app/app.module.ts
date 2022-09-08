import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

//modules
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { BienvenidaComponent } from './components/inicio/bienvenida/bienvenida.component';
import { LoginComponent } from './components/inicio/login/login.component';
import { RegisterComponent } from './components/inicio/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CambiarPasswordComponent } from './components/dashboard/cambiar-password/cambiar-password.component';
import { RoomsComponent } from './components/dashboard/rooms/rooms.component';
import { NavbarComponent } from './components/dashboard/navbar/navbar.component';
import { ToastrModule } from 'ngx-toastr';
import { LoadingComponent } from './shared/loading/loading.component';

//services
import { LoginService } from './services/login.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//interceptors
import { AddTokenInterceptor } from './helpers/add-token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    InicioComponent,
    BienvenidaComponent,
    LoginComponent,
    RegisterComponent,
    DashboardComponent,
    CambiarPasswordComponent,
    RoomsComponent,
    NavbarComponent,
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AddTokenInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
