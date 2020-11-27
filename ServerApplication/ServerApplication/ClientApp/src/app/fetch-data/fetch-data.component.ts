import { Component, Inject } from "@angular/core";
import axios from "axios";

@Component({
  selector: "app-fetch-data",
  templateUrl: "./fetch-data.component.html",
})
export class FetchDataComponent {
  public testList: Test[];
  public baseURL: string;

  constructor(@Inject("BASE_URL") baseUrl: string) {
    this.baseURL = baseUrl;
    this.testList = [
      { name: "Register Test", action: this.registerTest, status: "blue" },
      { name: "Login Test", action: this.loginTest, status: "blue" },
    ];
  }

  runAction = (name: string) => {
    console.log(name);
    this.testList.find((t) => t.name == name).action();
  };

  registerTest = () => {
    axios
      .post(this.baseURL + "auth/register", {
        userName: "alma",
        password: "almaalma",
        repeatedPassword: "almaalma",
      })
      .then(
        (success) => {
          console.log(success.data);
          this.testList.find((t) => t.name == "Register Test").status = "green";
        },
        (error) => {
          console.log(error.response.data.error);
          this.testList.find((t) => t.name == "Register Test").status = "red";
        }
      );
  };

  loginTest = () => {
    axios
      .post(this.baseURL + "auth/login", {
        userName: "admin",
        password: "SecretPassword",
      })
      .then(
        (success) => {
          console.log(success.data);
          this.testList.find((t) => t.name == "Login Test").status = "green";
        },
        (error) => {
          console.log(error.response.data.error);
          this.testList.find((t) => t.name == "Login Test").status = "red";
        }
      );
  };
}

interface Test {
  name: string;
  action: () => void;
  status: string;
}
