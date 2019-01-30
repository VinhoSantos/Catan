import * as signalR from '@aspnet/signalr';
import Render from './render';

export default class Game {
    
    private render: Render;
    private server = 'http://localhost:52257';
    private connection: signalR.HubConnection;

    constructor() {
        this.render = Render.getInstance();
    }

    public start() {
        this.drawLoadingScreen();
        this.setupConnection();
    }

    private drawLoadingScreen(): void {
        this.render.drawLoadingScreen();
    }

    private setupConnection() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.server}/hubs/client`)
            .build();

        this.connectToServer();
        this.setupClientMethods();
    }

    private connectToServer(): void {        
        this.connection.start()
            .then(() => {
                console.log('Connected to hub');
            })
            .catch((err: any) => {
                console.log('Connection failed');
                console.error(err.toString());
            });
    }

    private setupClientMethods(): void {
        //todo
    }
}