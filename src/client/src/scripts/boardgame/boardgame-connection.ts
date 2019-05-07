import * as signalR from "@aspnet/signalr";
import * as events from '../data/events';
type PlayerConnectedEvent = events.Scripts.Data.Events.Player.PlayerConnectedEvent;
type PlayerDisconnectedEvent = events.Scripts.Data.Events.Player.PlayerDisconnectedEvent;
type OtherPlayerConnectedEvent = events.Scripts.Data.Events.Player.OtherPlayerConnectedEvent;
type OtherPlayerDisconnectedEvent = events.Scripts.Data.Events.Player.OtherPlayerDisconnectedEvent;

export default class BoardGameConnection {

  constructor(private readonly connection: signalR.HubConnection) {
    this.connection.on("PlayerConnected", (event: PlayerConnectedEvent) =>
      this.onPlayerConnected(event)
    );
    this.connection.on("PlayerDisconnected", (event: PlayerDisconnectedEvent) =>
      this.onPlayerDisconnected(event)
    );
    this.connection.on("OtherPlayerConnected", (event: OtherPlayerConnectedEvent) =>
      this.onOtherPlayerConnected(event)
    );
    this.connection.on("OtherPlayerDisconnected", (event: OtherPlayerDisconnectedEvent) =>
      this.onOtherPlayerDisconnected(event)
    );
  }

  private onPlayerConnected = (event: PlayerConnectedEvent) => {

  }

  private onPlayerDisconnected = (event: PlayerDisconnectedEvent) => {

  }

  private onOtherPlayerConnected = (event: OtherPlayerConnectedEvent) => {

  }

  private onOtherPlayerDisconnected = (event: OtherPlayerDisconnectedEvent) => {

  }
}