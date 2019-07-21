import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
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
    await this.hubConnection.start().catch(error => console.error(error));
    this.connectionEstablished$.next(true);
    this.register();
  }

  public async disconnect(): Promise<void> {
    this.unregister();
    this.connectionEstablished$.next(false);
    await this.hubConnection.stop().catch(error => console.error(error));
  }

  protected abstract register(): void;

  protected abstract unregister(): void;
}
