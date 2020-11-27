import { Component, Inject } from "@angular/core";
import axios from "axios";

@Component({
  selector: "app-fetch-data",
  templateUrl: "./fetch-data.component.html",
})
export class FetchDataComponent {
  public testList: Test[];
  public baseURL: string;
  public header: {};

  constructor(@Inject("BASE_URL") baseUrl: string) {
    this.baseURL = baseUrl;
    this.generateAuthenticationHeadder();
    this.testList = [
      {
        name: "Register Test",
        action: () => {
          this.registerTest("Register Test");
        },
        status: "blue",
      },
      {
        name: "Login Test",
        action: () => {
          this.loginTest("Login Test");
        },
        status: "blue",
      },
      {
        name: "Get User Test",
        action: () => {
          this.getUserTest("Get User Test");
        },
        status: "blue",
      },
      {
        name: "Get All Users Test",
        action: () => {
          this.getAllUsersTest("Get All Users Test");
        },
        status: "blue",
      },
      {
        name: "Edit User Test",
        action: () => {
          this.editUserTest("Edit User Test");
        },
        status: "blue",
      },
      {
        name: "Change User Password Test",
        action: () => {
          this.changePasswordTest("Change User Password Test");
        },
        status: "blue",
      },
    ];
  }

  runAction = (name: string) => {
    console.log(name);
    this.testList.find((t) => t.name == name).action();
  };

  registerTest = (name: string) => {
    axios
      .post(this.baseURL + "auth/register", {
        userName: "alma",
        password: "almaalma",
        repeatedPassword: "almaalma",
      })
      .then(
        (success) => {
          console.log(success.data);
          this.testList.find((t) => t.name == name).status = "green";
        },
        (error) => {
          console.log(error.response.data.error);
          this.testList.find((t) => t.name == name).status = "red";
        }
      );
  };

  loginTest = (name: string) => {
    axios
      .post(this.baseURL + "auth/login", {
        userName: "admin",
        password: "SecretPassword",
      })
      .then(
        (success) => {
          console.log(success.data);
          this.testList.find((t) => t.name == name).status = "green";
        },
        (error) => {
          console.log(error.response.data.error);
          this.testList.find((t) => t.name == name).status = "red";
        }
      );
  };

  getUserTest = (name: string) => {
    axios({
      method: "GET",
      url: this.baseURL + "usermanagement",
      headers: this.header,
    }).then(
      (success) => {
        console.log(success.data);
        this.testList.find((t) => t.name == name).status = "green";
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  getAllUsersTest = (name: string) => {
    axios({
      method: "GET",
      url: this.baseURL + "usermanagement/all",
      headers: this.header,
    }).then(
      (success) => {
        console.log(success.data);
        this.testList.find((t) => t.name == name).status = "green";
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  editUserTest = (name: string) => {
    axios({
      method: "GET",
      url: this.baseURL + "usermanagement/all",
      headers: this.header,
    }).then(
      (successfulGet) => {
        axios({
          method: "PUT",
          url: this.baseURL + "usermanagement",
          headers: this.header,
          data: {
            id: successfulGet.data[successfulGet.data.length - 1].id,
            userName:
              successfulGet.data[successfulGet.data.length - 1].userName +
              "korte",
          },
        }).then(
          (success) => {
            console.log(success.data);
            this.testList.find((t) => t.name == name).status = "green";
          },
          (error) => {
            console.log(error.response.data.error);
            this.testList.find((t) => t.name == name).status = "red";
          }
        );
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  changePasswordTest = (name: string) => {
    axios
      .post(this.baseURL + "auth/register", {
        userName: "kacsa",
        password: "kacsakacsa",
        repeatedPassword: "kacsakacsa",
      })
      .then(
        (successfullReg) => {
          axios({
            method: "POST",
            url: this.baseURL + "usermanagement/change-password",
            headers: this.header,
            data: {
              id: successfullReg.data.id,
              userName: "kacsa",
              oldPassword: "kacsakacsa",
              newPassword: "kacsakacsa++",
              repeatedNewPassword: "kacsakacsa++",
            },
          }).then(
            (success) => {
              console.log(success.data);
              this.testList.find((t) => t.name == name).status = "green";
            },
            (error) => {
              console.log(error.response.data.error);
              this.testList.find((t) => t.name == name).status = "red";
            }
          );
        },
        (error) => {
          console.log(error.response.data.error);
          this.testList.find((t) => t.name == name).status = "red";
        }
      );
  };

  generateAuthenticationHeadder = () => {
    axios
      .post(this.baseURL + "auth/login", {
        userName: "admin",
        password: "SecretPassword",
      })
      .then((ret) => {
        this.header = {
          Authorization: "Bearer " + ret.data.token,
        };
      });
  };
}

interface Test {
  name: string;
  action: () => void;
  status: string;
}
