import * as signalR from '@aspnet/signalr';
import RenderCanvas from './renders/render-canvas';
import RenderBackground from './renders/render-background';
import RenderDom from './renders/render-dom';
import * as contracts from './data/contracts';
type GameState = contracts.Scripts.Data.Models.GameState;
type Player = contracts.Scripts.Data.Models.Player;

export default class Game {
    
    private background: RenderBackground;
    private canvas: RenderCanvas;
    private loader: RenderDom;
    private server = 'http://localhost:52257';
    private connection: signalR.HubConnection;
    private player: Player;

    constructor() {
        this.background = RenderBackground.getInstance();
        this.canvas = RenderCanvas.getInstance();
        this.loader = new RenderDom('#loader');
    }

    public start() {
        this.drawLoadingScreen();
        this.setupConnection();
    }

    private drawLoadingScreen(): void {
        this.loader.write('Loading Catan...');
        this.loader.show();
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
                this.background.draw();
                this.canvas.drawBoard();
                this.loader.hide();
            })
            .catch((err: any) => {
                console.log('Connection failed');
                console.error(err.toString());
            });
    }

    private setupClientMethods(): void {
        this.connection.on('IsConnected', (player: Player) => {
            console.log(`IsConnected: ${player.name}`);
            this.player = player;

            window.addEventListener('beforeunload', () => this.disconnectFromServer());
            window.addEventListener('resize', () => this.windowResize());
        });

        this.connection.on('PlayerConnected', (player: Player) => {
            console.log(`PlayerConnected: ${player.name}`);
            //this.gameState.otherPlayers.push(player);
        });

        this.connection.on('OtherPlayerDisconnected', (player: Player) => {
            console.log(`OtherPlayerDisconnected: ${player.name}`);

            // const playerToBeRemoved = this.gameState.otherPlayers.indexOf(player);
            // if (playerToBeRemoved)
            //     this.gameState.otherPlayers.splice(playerToBeRemoved, 1);
        });
    }
    
    startGame(): any {
        this.connection.invoke('OnStartGame')
            .then(() => {
                console.log('OnStartGame successfully called on server');
            })
            .catch((err: any) => {
                console.error(err.toString())
            });
    }

    private disconnectFromServer(): void {
        this.connection.stop()
            .then(() => console.log('Disconnected from hub'))
            .catch((err: any) => console.error(err.toString()));
    }

    private windowResize(): any {
        this.background.recalculateMeasurements();
        this.background.draw();
        this.canvas.recalculateMeasurements();
        this.canvas.drawBoard();
    }
}