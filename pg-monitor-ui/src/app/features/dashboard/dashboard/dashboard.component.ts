import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { interval } from 'rxjs';
import { MetricsService } from '../../../core/services/metrics.service';
import { Metric } from '../../../core/models/metric.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  private metricsService = inject(MetricsService);

  latestMetric?: Metric;
  history: Metric[] = [];

  ngOnInit(): void {
    this.loadData();

    interval(5000).subscribe(() => {
      this.loadData();
    });

    this.metricsService.getLatest().subscribe({
    next: (data) => {
      this.latestMetric = data;
    }
  });

  this.metricsService.getHistory().subscribe({
    next: (data) => {
      this.history = data;
      console.log(this.history);
    }
  });
  }

  loadData(): void {
    this.metricsService.getLatest().subscribe({
      next: result => {
        this.latestMetric = result;
      }
    });

    this.metricsService.getHistory().subscribe({
      next: result => {
        this.history = result;
      }
    });
  }
}