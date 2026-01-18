import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { map, Observable, throwError } from 'rxjs';

interface LoginRequest {
  userNameOrEmailAddress: string;
  password: string;
  rememberMe: boolean;
}

interface LoginResponse {
  result: number;
  description: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  login(req: LoginRequest): Observable<void> {
    return this.http
      .post<LoginResponse>(
        `${this.baseUrl}/api/account/login`,
        req,
        {
          // important so the browser sends/receives auth cookies
          withCredentials: true
        }
      )
      .pipe(
        map(res => {
          if (res.result !== 1) {
            throw new Error(res.description || 'Login failed');
          }
          // ABP sets auth cookie; no JWT to store on client.
          return;
        })
      );
  }

  logout(): Observable<void> {
    return this.http
      .post<void>(
        `${this.baseUrl}/api/account/logout`,
        {},
        { withCredentials: true }
      );
  }
}
