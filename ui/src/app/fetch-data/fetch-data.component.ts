import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  readonly rootURL : string = 'https://localhost:44394/api';

  constructor(http: HttpClient)  {
    http.get<WeatherForecast[]>(this.rootURL + '/Values/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    },);
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
