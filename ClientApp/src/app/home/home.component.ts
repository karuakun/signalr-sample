import { Component, OnInit  } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [`
    .chat-room {
      margin: 5px;
    }
    .chat-room-name {
      font-size: 20px;
    }
    .chat-messages {
      margin-left: 20px;
    }
    .chat-messages p {
      margin: 2px;
    }
  `]
})
export class HomeComponent implements OnInit  {
  public hubConnection: HubConnection;
  public roomGlobalMessages: string[] = [];
  public roomTechMessages: string[] = [];
  public message: string;
  public roomGlobal: boolean;
  public roomTech: boolean;

  ngOnInit() {
    let builder = new HubConnectionBuilder();
    this.hubConnection = builder.withUrl('/hubs/echo').build();

    // Pushed Events
    this.hubConnection.on("Global", (message) => {
      this.roomGlobalMessages.push(message);
    });
    this.hubConnection.on("Tech", (message) => {
      this.roomTechMessages.push(message);
    });
    this.hubConnection.start();
  }

  send() {
    if (this.roomGlobal) {
      this.hubConnection.invoke("Send", "Global", this.message);
    }
    if (this.roomTech) {
      this.hubConnection.invoke("Send", "Tech", this.message);
    }
    this.message = "";
  }
}
