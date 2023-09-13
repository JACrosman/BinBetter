import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { WINDOW } from './core/tokens/window.token';
import { STORAGE_CONFIG } from './core/tokens/storage-config.token';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,

    AppRoutingModule
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    {
      provide: WINDOW,
      useFactory: () => window,
    },
    {
      provide: STORAGE_CONFIG,
      useFactory: () => ({
        prefix: 'bb'
      })
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
