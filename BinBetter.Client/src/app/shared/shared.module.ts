import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

const modules = [
  CommonModule,
  ReactiveFormsModule,
  HttpClientModule
];

@NgModule({
  declarations: [],
  imports: modules,
  exports: modules
})
export class SharedModule { }
