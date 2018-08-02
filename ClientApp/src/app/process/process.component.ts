import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['./process.component.css']
})
export class ProcessComponent implements OnInit {
  public hubConnection: HubConnection;
  public processes: string[] = [];
  public message: string;
  public processing: boolean = false;

  ngOnInit() {
    let builder = new HubConnectionBuilder();
    this.hubConnection = builder.withUrl('hubs/process').build();

    this.hubConnection.start().then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :(${err})'));
  }

  usage() {
    if (this.processing) {
      alert("実行中");
      return;
    }
    this.processing = true;

    var a= this.hubConnection.stream("Counter", 100, 500)
      .subscribe({
        next: item => {
          console.log(JSON.stringify(item));

          this.processes.splice(0, this.processes.length);
          this.processes.push(...item.map(i => `${i.processName} (${i.workingSet})`));

          this.message = "確認中...";
        },
        complete: () => {
          this.processing = false;
          this.message = "完了";
        },
        error: err => {

        }
      });
    console.log(JSON.stringify(a));
  }
}
