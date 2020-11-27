import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-fetch-data",
  templateUrl: "./fetch-data.component.html",
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    http
      .post(baseUrl + "auth/register", {
        userName: "alma",
        password: "almaalma",
        repeatedPassword: "almaalma",
      })
      .subscribe(
        (result) => {
          console.log(result);
        },
        (error) => console.error(error)
      );
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
