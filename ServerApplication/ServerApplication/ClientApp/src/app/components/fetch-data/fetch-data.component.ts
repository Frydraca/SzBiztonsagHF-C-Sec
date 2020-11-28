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
        name: "Logout Test",
        action: () => {
          this.logoutTest("Logout Test");
        },
        status: "blue",
      },
      {
        name: "Refresh JWT Test",
        action: () => {
          this.refreshJWTTest("Refresh JWT Test");
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
      {
        name: "Delete User Test",
        action: () => {
          this.deleteUserTest("Delete User Test");
        },
        status: "blue",
      },
      {
        name: "Create Caff File Test",
        action: () => {
          this.createCaffTest("Create Caff File Test");
        },
        status: "blue",
      },
      {
        name: "Get All Caff Files Test",
        action: () => {
          this.getAllCaffTest("Get All Caff Files Test");
        },
        status: "blue",
      },
      {
        name: "Get Caff File By Id Test",
        action: () => {
          this.getCaffByIdTest("Get Caff File By Id Test");
        },
        status: "blue",
      },
      {
        name: "Get Own Caff Files Test",
        action: () => {
          this.getOwnCaffFilesTest("Get Own Caff Files Test");
        },
        status: "blue",
      },
      {
        name: "Update Caff File Test",
        action: () => {
          this.updateCaffById("Update Caff File Test");
        },
        status: "blue",
      },
      {
        name: "Delete Caff File Test",
        action: () => {
          this.deleteCaffByIdTest("Delete Caff File Test");
        },
        status: "blue",
      },
      {
        name: "Add Comment Test",
        action: () => {
          this.addCommentTest("Add Comment Test");
        },
        status: "blue",
      },
      {
        name: "Get Comment By Id Test",
        action: () => {
          this.getCommentByIdTest("Get Comment By Id Test");
        },
        status: "blue",
      },
      {
        name: "Get All Comments Of Caff File Test",
        action: () => {
          this.getAllCommentsOfCaffFileTest(
            "Get All Comments Of Caff File Test"
          );
        },
        status: "blue",
      },
      {
        name: "Update Comment Test",
        action: () => {
          this.updateCommentTest("Update Comment Test");
        },
        status: "blue",
      },
      {
        name: "Delete Comment Test",
        action: () => {
          this.deleteCommentTest("Delete Comment Test");
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

  logoutTest = (name: string) => {
    axios({
      method: "POST",
      url: this.baseURL + "auth/logout",
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

  refreshJWTTest = (name: string) => {
    axios({
      method: "POST",
      url: this.baseURL + "auth/refresh-jwt",
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

  deleteUserTest = (name: string) => {
    axios({
      method: "GET",
      url: this.baseURL + "usermanagement/all",
      headers: this.header,
    }).then(
      (successfulGet) => {
        axios({
          method: "DELETE",
          url: this.baseURL + "usermanagement",
          headers: this.header,
          data: {
            id: successfulGet.data[successfulGet.data.length - 1].id,
            userName:
              successfulGet.data[successfulGet.data.length - 1].userName,
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

  createCaffTest = (name: string) => {
    axios({
      method: "POST",
      url: this.baseURL + "caff",
      headers: this.header,
      data: {
        name: "kacsa.caff",
        comments: [
          { text: "A kacsak nagyon fura allatok!" },
          { text: "A kacsa pucsi haha!" },
          { text: "A kacsak mesék!" },
        ],
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
  };

  getAllCaffTest = (name: string) => {
    axios({
      method: "GET",
      url: this.baseURL + "caff/all",
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

  getCaffByIdTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "medve.caff",
        comments: [
          { text: "Medveeeeee!" },
          { text: "A medve alszik!" },
          { text: "A medve mesék!" },
        ],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          url: this.baseURL + "caff/" + successfullpost.data.id,
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  getOwnCaffFilesTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "kutya.caff",
        comments: [{ text: "Kutyák!" }, { text: "Dogs" }],
      },
    }).then(
      () => {
        axios({
          method: "POST",
          headers: this.header,
          url: this.baseURL + "caff",
          data: {
            name: "cica.caff",
            comments: [{ text: "Yuuumiiiii!" }, { text: "cicccc" }],
          },
        }).then(
          () => {
            axios({
              method: "GET",
              url: this.baseURL + "caff",
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

  updateCaffById = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "madár.caff",
        comments: [{ text: "A madár az ász!" }, { text: "Sas!" }],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          headers: this.header,
          url: this.baseURL + "caff/" + successfullpost.data.id,
        }).then(
          (successFullget) => {
            successFullget.data.comments[0] = {
              ...successFullget.data.comments[0],
              text: "alma",
            };
            axios({
              method: "PUT",
              headers: this.header,
              url: this.baseURL + "caff",
              data: {
                id: successFullget.data.id,
                name: successFullget.data.name,
                creationDate: successFullget.data.creationDate,
                comments: successFullget.data.comments,
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  deleteCaffByIdTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "dino.caff",
        comments: [{ text: "T-rex!" }, { text: "Diiiiiiiiiino!" }],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "DELETE",
          url: this.baseURL + "caff/" + successfullpost.data.id,
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  addCommentTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "tigris.caff",
        comments: [{ text: "csikos" }, { text: "Fekete-sárga" }],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          headers: this.header,
          url: this.baseURL + "caff/" + successfullpost.data.id,
        }).then(
          (successFullget) => {
            axios({
              method: "POST",
              headers: this.header,
              url:
                this.baseURL + "caff/" + successfullpost.data.id + "/comments",
              data: {
                text: "nagymacska",
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  getCommentByIdTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "párduc.caff",
        comments: [{ text: "gyors" }, { text: "nagymacska" }],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          headers: this.header,
          url: this.baseURL + "caff/" + successfullpost.data.id,
        }).then(
          (successFullget) => {
            axios({
              method: "GET",
              headers: this.header,
              url:
                this.baseURL +
                "caff/" +
                successfullpost.data.id +
                "/comments/" +
                successFullget.data.comments[0].id,
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  getAllCommentsOfCaffFileTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "tücsök.caff",
        comments: [
          { text: "cirip" },
          { text: "ciripcirip" },
          { text: "tücsök úr" },
        ],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          headers: this.header,
          url: this.baseURL + "caff/" + successfullpost.data.id,
        }).then(
          (successFullget) => {
            axios({
              method: "GET",
              headers: this.header,
              url:
                this.baseURL + "caff/" + successfullpost.data.id + "/comments",
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  updateCommentTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "labda.caff",
        comments: [{ text: "kerek" }, { text: "kék" }],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          headers: this.header,
          url: this.baseURL + "caff/" + successfullpost.data.id,
        }).then(
          (successFullget) => {
            axios({
              method: "PUT",
              headers: this.header,
              url:
                this.baseURL + "caff/" + successfullpost.data.id + "/comments",
              data: {
                id: successFullget.data.comments[0].id,
                text: "gömbölyű",
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
      },
      (error) => {
        console.log(error.response.data.error);
        this.testList.find((t) => t.name == name).status = "red";
      }
    );
  };

  deleteCommentTest = (name: string) => {
    axios({
      method: "POST",
      headers: this.header,
      url: this.baseURL + "caff",
      data: {
        name: "blackhole.caff",
        comments: [{ text: "black" }, { text: "hole" }],
      },
    }).then(
      (successfullpost) => {
        axios({
          method: "GET",
          headers: this.header,
          url: this.baseURL + "caff/" + successfullpost.data.id,
        }).then(
          (successFullget) => {
            axios({
              method: "DELETE",
              headers: this.header,
              url:
                this.baseURL +
                "caff/" +
                successfullpost.data.id +
                "/comments/" +
                successFullget.data.comments[0].id,
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
