// src/app/home/home.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-home',
  template: `
    <div style="padding: 2rem; color: #e5e7eb; background:#020617; min-height:100vh;">
      <h1>Welcome</h1>
      <p>You are logged in via ABP backend.</p>
    </div>
  `,
  imports: [CommonModule],
})
export class HomeComponent { }
