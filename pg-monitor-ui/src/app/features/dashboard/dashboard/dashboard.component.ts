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
  }

  loadData(): void {
    this.metricsService.getLatest().subscribe({
      next: result => {
        this.latestMetric = result;
      }
    });

    this.metricsService.getHistory(20).subscribe({
      next: result => {
        this.history = result;
      }
    });
  }
}