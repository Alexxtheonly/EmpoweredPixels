import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@aspnet/signalr';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export abstract class BaseHubService {
  protected hubConnection: HubConnection;
  protected connectionEstablished$ = new BehaviorSubject<boolean>(false);

  constructor(path: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(path)
      .build();
  }

  public async connect(): Promise<void> {
    if (this.hubConnection.state === HubConnectionState.Disconnected) {
      await this.hubConnection
        .start()
        .then(() => console.log('hub connection established'))
        .catch(error => console.error(error));
    }
    this.connectionEstablished$.next(true);
    this.register();
  }

  public async disconnect(): Promise<void> {
    this.unregister();
    this.connectionEstablished$.next(false);
    if (this.hubConnection.state === HubConnectionState.Connected) {
      await this.hubConnection.stop()
        .then(() => console.log('hub connection closed'))
        .catch(error => console.error(error));
    }
  }

  protected IsConnected(): boolean {
    return this.hubConnection.state === HubConnectionState.Connected;
  }

  protected abstract register(): void;

  protected abstract unregister(): void;
}
