import { Component } from '@angular/core';
import { PolylineService } from '../../services/polyline.service';
import { Chart, registerables, TooltipItem } from 'chart.js';

@Component({
  selector: 'app-graph',
  standalone: false,

  templateUrl: './graph.component.html',
  styleUrl: './graph.component.scss',
})
export class GraphComponent {
  points: any[] = [];
  polyline: any[] = [];
  results: any[] = [];
  chart: any;

  constructor(private dataService: PolylineService) {
    Chart.register(...registerables);
  }

  ngOnInit() {
    this.loadGraphData();
  }

  loadGraphData() {
    this.dataService.getResults().subscribe((data) => {
      this.points = data;
      this.renderChart();
    });

    this.dataService.getPolyline().subscribe((data) => {
      this.polyline = data;
      this.renderChart();
    });
  }

  reloadGraph() {
    this.loadGraphData();
  }

  renderChart() {
    const ctx = document.getElementById('graphCanvas') as HTMLCanvasElement;

    if (this.chart) {
      this.chart.destroy();
    }

    this.chart = new Chart(ctx, {
      type: 'scatter',
      data: {
        datasets: [
          {
            label: 'Closest Point',
            data: this.points.map((p) => ({
              x: p.closestPoint.x,
              y: p.closestPoint.y,
            })),
            backgroundColor: 'blue',
          },
          {
            label: 'Points',
            data: this.points.map((p) => ({
              x: p.currentPoint.x,
              y: p.currentPoint.y,
              offset: p.offset,
              station: p.station,
            })),
            backgroundColor: 'blue',
          },
          {
            label: 'Polyline',
            data: this.polyline.map((p) => ({ x: p.x, y: p.y })),
            backgroundColor: 'Tan',
            showLine: true,
            borderColor: 'Tan',
          },

          ...this.points.map((p, index) => ({
            label: `Connection ${index + 1}`,
            data: [
              { x: p.currentPoint.x, y: p.currentPoint.y },
              { x: p.closestPoint.x, y: p.closestPoint.y },
            ],
            showLine: true,
            borderColor: 'blue',
            borderWidth: 2,
            pointRadius: 0,
          })),
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: true,
            labels: {
              filter: (legendItem) => {
                return !legendItem.text.startsWith('Connection');
              },
            },
          },
          tooltip: {
            callbacks: {
              label: (tooltipItem) => {
                const datasetLabel = tooltipItem.dataset.label;

                if (datasetLabel === 'Points') {
                  const raw = tooltipItem.raw as {
                    x: number;
                    y: number;
                    offset: number;
                    station: number;
                  };

                  return [
                    `Point: (${raw.x}, ${raw.y})`,
                    `Offset: ${raw.offset.toFixed(2)}`,
                    `Station: ${raw.station.toFixed(2)}`,
                  ];
                }

                if (datasetLabel === 'Polyline') {
                  const raw = tooltipItem.raw as { x: number; y: number };
                  return `Polyline: (${raw.x}, ${raw.y})`;
                }

                if (datasetLabel === 'Closest Point') {
                  const raw = tooltipItem.raw as { x: number; y: number };
                  return `Closest Point: (${raw.x}, ${raw.y})`;
                }

                return '';
              },
            },
            displayColors: false,
          },
        },
        scales: {
          x: {
            title: {
              display: true,
              text: 'X-axis',
            },
          },
          y: {
            title: {
              display: true,
              text: 'Y-axis',
            },
          },
        },
      },
    });
  }
}
