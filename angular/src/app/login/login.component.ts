import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface LoginResult {
  result: number;
  description: string;
  // add token field later when you return JWT from backend
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  model = {
    userNameOrEmailAddress: '',
    password: '',
    rememberMe: true,
  };

  loading = false;
  error = '';

  constructor(private http: HttpClient, private router: Router) { }

  submit(form: HTMLFormElement) {
    if (!form.checkValidity()) {
      form.reportValidity();
      return;
    }

    this.error = '';
    this.loading = true;

    this.http
      .post<LoginResult>(
        'https://localhost:44366/api/account/login',
        this.model
      )
      .subscribe({
        next: (res) => {
          if (res.result === 1) {
            // TODO: store JWT when backend returns it
            this.router.navigate(['/home']);
          } else {
            this.error = res.description || 'Invalid username or password';
          }
          this.loading = false;
        },
        error: (err) => {
          console.error(err);
          this.error = 'Login failed. Please try again.';
          this.loading = false;
        },
      });
  }
}
