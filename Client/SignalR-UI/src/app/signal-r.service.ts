import {EventEmitter, Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  message = new EventEmitter<string>();
  hubConnection: any;

  constructor() {
    this.createConnection();
    this.register();
    this.startConnection();
    this.hubConnection =  HubConnection;
  }

  private createConnection() {
    debugger
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44392/inform')
      .configureLogging(LogLevel.Debug)
      .build();
  }

  private register(): void {
    debugger
    this.hubConnection.on('InformClient', (param: string) => {
      console.log(param);
      this.message.emit(param);
    });
  }

  private startConnection(): void {
    debugger
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started.');
      })
      .catch((err: string) => {
        console.log('Opps!' + err);
      });
  }
}
