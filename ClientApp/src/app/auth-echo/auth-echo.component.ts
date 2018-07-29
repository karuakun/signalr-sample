import { Component, OnInit } from '@angular/core';
import { AuthService } from "../auth/auth.service"
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { environment } from './../../environments/environment';

@Component({
  selector: 'app-auth-echo',
  templateUrl: './auth-echo.component.html',
  styleUrls: ['./auth-echo.component.css']
})
export class AuthEchoComponent implements OnInit {
  public hubConnection: HubConnection;
  public globalMessages: string[] = [];
  public echoMessages: string[] = [];
  public message: string;
  public statusMessage: string;


  authService: AuthService;

  constructor(authService: AuthService) {
    this.authService = authService;
  }

  showToken() {
    alert(this.authService.accessToken);
  }

  ngOnInit() {
    let builder = new HubConnectionBuilder();

    const idToken = this.authService.idToken;
    console.info(this.authService.accessToken);
    this.hubConnection = builder.withUrl(`${environment.signalR.authHubUrl}?idToken=${idToken}`).build();

    this.hubConnection.on("Global", (message, sessionContext) => this.globalMessages.push(`${message} ${sessionContext.nickName}`));
    this.hubConnection.on("Echo", (sessionContext, message) => this.echoMessages.push(`${message.text} (${sessionContext.nickName})`));

    this.hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => {
        this.statusMessage = JSON.stringify(err);
        console.log('Error while establishing connection :(${err})');
      });
  }

  send() {
    this.hubConnection.invoke("Send", { text: this.message, clientTime: new Date() });
    this.message = "";
  }
}
