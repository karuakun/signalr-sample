import { Component, OnInit  } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit  {
  public hubConnection: HubConnection;
  public globalMessages: string[] = [];
  public echoMessages: string[] = [];
  public message: string;

  ngOnInit() {
    let builder = new HubConnectionBuilder();

    this.hubConnection = builder.withUrl(environment.signalR.echoHubUrl).build();

    this.hubConnection.on("Global", (message, connectionId) => { this.globalMessages.push(`${message} ${connectionId}`)});
    this.hubConnection.on("Echo", message => this.echoMessages.push(`${message.text} (${message.connectionId})`));

    this.hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :(${err})'));  }

  send() {
    this.hubConnection.invoke("Send", { text: this.message, clientTime: new Date()});
    this.message = "";
  }
}
