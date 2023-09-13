import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { LocalStorageService } from './local-storage.service';

export interface User {
  user: {
    username: string;
    token: string;
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private localStorage: LocalStorageService) {}

  login(email: string, password: string): Observable<User> {
    return this.http.post<User>('https://localhost:7241/api/users/authenticate', {
      email,
      password
    }).pipe(
      tap(res => {
        this.localStorage.setItem('user', res.user.token);
      })
    )
  }
}
