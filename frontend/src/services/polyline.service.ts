import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PolylineService {
  private apiUrl = 'http://localhost:8001'; 

  constructor(private http: HttpClient) {}

  getPolyline(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/polyline`);
  }

  getResults(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/process`);
  }
}
