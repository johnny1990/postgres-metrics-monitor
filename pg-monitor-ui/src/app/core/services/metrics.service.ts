import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Metric } from '../models/metric.model';
import { API } from '../constants/api.constants';

@Injectable({
  providedIn: 'root'
})
export class MetricsService {

  private http = inject(HttpClient);

  getLatest(): Observable<Metric> {
    return this.http.get<Metric>(`${API.BASE_URL}/latest`);
  }

  // getHistory(count: number = 20): Observable<Metric[]> {
  //   return this.http.get<Metric[]>(`${API.BASE_URL}/history/${count}`);
  // }

  getHistory(): Observable<Metric[]> {
  return this.http.get<Metric[]>(`${API.BASE_URL}/history`);
}
}