import * as signalR from '@aspnet/signalr';
import Drawer from './renders/drawer';

import * as contracts from './data/contracts';

import * as events from './data/events';
type PlayerConnectedEvent = events.Scripts.Data.Events.Player.PlayerConnectedEvent;
type PlayerDisconnectedEvent = events.Scripts.Data.Events.Player.PlayerDisconnectedEvent;
type OtherPlayerConnectedEvent = events.Scripts.Data.Events.Player.OtherPlayerConnectedEvent;
type OtherPlayerDisconnectedEvent = events.Scripts.Data.Events.Player.OtherPlayerDisconnectedEvent;

export default class Catan {
    
    private drawer: Drawer;
    private server = 'http://localhost:52257';
    private connection: signalR.HubConnection;
    private connectedPlayers: string[];

    constructor() {        
        this.drawer = new Drawer();
    }

    public start() {
        this.drawer.drawLoadingScreen();
        this.setupConnection();
    }

    private setupConnection() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.server}/hubs/client`)
            .build();

        this.connectToServer();
    }

    private connectToServer(): void {        
        this.connection.start()
            .then(() => {
                console.log('Connected to hub');
                this.drawer.drawHomeScreen();
                this.setupClientMethods();                                             
            })
            .catch((err: any) => {
                console.log('Connection failed');
                console.error(err.toString());
                setTimeout(() => this.start(), 5000);
            });
    }

    private setupClientMethods(): void {
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
        window.addEventListener('beforeunload', () => this.disconnectFromServer());
        window.addEventListener('resize', () => this.windowResize());
    }

    private onPlayerDisconnected = (event: PlayerDisconnectedEvent) => {

    }

    private onOtherPlayerConnected = (event: OtherPlayerConnectedEvent) => {

    }

    private onOtherPlayerDisconnected = (event: OtherPlayerDisconnectedEvent) => {

    }

    private disconnectFromServer(): void {
        this.connection
            .stop()
            .then(() => this.start())
            .catch((err: any) => console.log(err.toString()));
    }

    private windowResize(): any {
        this.drawer.resize();
    }
}