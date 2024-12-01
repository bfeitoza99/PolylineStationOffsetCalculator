import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing-module';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { GraphComponent } from './graph/graph.component';
import {  provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';


@NgModule({
    declarations: [
        AppComponent,
        GraphComponent,  
      ],
      imports: [
        BrowserModule,
        AppRoutingModule, 
        MatCardModule,
        MatButtonModule    
      ],
      providers: [provideHttpClient(withInterceptorsFromDi())],
      bootstrap: [AppComponent]
})
export class AppModule {}
